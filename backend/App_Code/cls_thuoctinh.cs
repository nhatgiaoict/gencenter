using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for cls_thuoctinh
/// </summary>
public class cls_thuoctinh
{
    public static string TableName
    {
        get { return "tbl_thuoctinh_vn"; }
    }
	public cls_thuoctinh()
	{
	}
    public void Insert(string title, string groupid, string parentid)
    {
        string parent = (parentid == null || parentid == string.Empty || parentid == "") ? "00" : parentid;
        string Sql = "INSERT INTO " + TableName + "(id, title, status, groupid, parentid) ";
        Sql += " VALUES(?id, ?title, ?status, ?groupid, ?parentid)";
        DbTask db = new DbTask();
        string id = db.GetNewKey(TableName, "id", string.Empty);
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "title", DbType.NVarChar, title);
        db.AddParameters(ref dt, "status", DbType.Int32, 1);
        db.AddParameters(ref dt, "parentid", DbType.NVarChar, parent);
        db.AddParameters(ref dt, "groupid", DbType.NVarChar, groupid);
        db.ExecuteNonQuery(Sql, dt);
    }
    public void Update(string id, string title)
    {
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " title = ?title  ";
        Sql += " WHERE id=?id";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "title", DbType.NVarChar, title);
        db.ExecuteNonQuery(Sql, dt); 
    }
    public void Delete(string id)
    {
        DbTask db = new DbTask();
        string sql = "Delete  from " + TableName + " where id = " + id + "";
        db.ExecuteNonQuery(sql);
    }
    public DataRow GetInfo(string id)
    {
        DbTask db = new DbTask();
        string Sql = "Select * from " + TableName + " Where id = '" + id + "' ";
        DataTable dt = db.GetData(Sql);
        if (dt != null)
        {
            return dt.Rows[0];
        }
        else
        {
            return null;
        }
    }
    public DataTable GetChildByparent(string parentid)
    {
        DbTask db = new DbTask();
        string sql = "Select * from " + TableName + " where parentid = '" + parentid + "'";
        return db.GetData(sql);
    }
    public void SetStatus(string id)
    {
        DbTask db = new DbTask();
        DataRow dr = GetInfo(id);
        if (dr == null)
            return;
        int status;
        if (Convert.ToInt32(dr["status"].ToString()) == 0)
            status = 1;
        else
            status = 0;
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " status = " + status + " ";
        Sql += " WHERE id = '" + id.Trim() + "' ";
        db.ExecuteNonQuery(Sql);
    }
    public DataTable Searching(int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages, string groupid, string parentid)
    {
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;
        DbTask db = new DbTask();

        string Sql = string.Empty;
        Sql = "SELECT COUNT(ID) AS Total FROM " + TableName + "  ";
        Sql += "WHERE 1=1 and groupid = '" + groupid + "' And parentid = '" + parentid + "' ";
        //if (Status < 2)
        //    Sql += "AND status = " + Status + " ";

        dt = db.GetData(Sql.Trim());
        if (dt == null || dt.Rows.Count == 0)
            return null;
        TotalRecords = Convert.ToInt32(dt.Rows[0]["Total"]);

        // da ay duoc tong so ban ghi ;
        TotalPages = (int)TotalRecords / RecordPerPages;
        if ((int)TotalRecords % RecordPerPages > 0)
            TotalPages = TotalPages + 1;
        Sql = "select " + TableName + ".*,row_number() over (order by idx) as row_index INTO #Temp_Members  ";
        Sql += "From " + TableName + " ";
        Sql += "Where 1=1 and groupid = '" + groupid + "' And parentid = '" + parentid + "'";
        //if (Status > 2)
        //{
        //    Sql += "And status = " + Status + " ";
        //}
        int nS = RecordPerPages * (CurrentPage - 1);
        int record_next = nS + RecordPerPages;
        Sql += "Select * from #Temp_Members ";
        if (nS == 0)
            Sql += " WHERE (row_index >=" + nS + ") AND (row_index <=" + record_next + ")";
        else
            Sql += " WHERE (row_index >" + nS + ") AND (row_index <=" + record_next + ")";
        Sql += " ORDER BY row_index ASC ";
        dt = db.GetData(Sql.Trim());
        return dt;
    }
}
