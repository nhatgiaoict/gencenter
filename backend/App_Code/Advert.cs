using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Advert
/// </summary>
public class Advert
{
    public static string TableName { get { return "tbl_advert_" + Globals.CurrentLang; } }
    public static string AbsImages { get { return Globals.UrlImages + "advert/images/"; } }
    public static string UrlImages { get { return Globals.UrlImages + "advert/images/"; } }
    ~Advert() { }
    public Advert()
    {
    }
    private string GetAllID()
    {
        DbTask db = new DbTask();
        string Sql = "SELECT id FROM " + TableName + "";
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return string.Empty;
        string sRet = ",";
        foreach (DataRow dr in dt.Rows)
            sRet += dr["id"].ToString() + ",";
        return sRet;
    }

    private bool CheckExist(string AllID, string ID)
    {

        if (AllID == string.Empty || AllID == null || AllID == "")
            return false;
        AllID = AllID.StartsWith(",") ? AllID : "," + AllID;
        AllID = AllID.EndsWith(",") ? AllID : AllID + ",";
        if (AllID.IndexOf("," + ID + ",") > -1)
        {
            return true;
        }
        return false;
    }
    public string GetNewKey(string parentid)
    {
        string AllID = GetAllID();
        string sRet = string.Empty;
        string parent = (parentid == null || parentid == string.Empty) ? "" : parentid;
        for (int i = 1; i < 65535; i++)
        {
            sRet = string.Format("{0}{1:D2}", parent, i);
            if (!CheckExist(AllID, sRet))
                break;
        }
        return sRet;
    }

    public void Insert(string title, string linkurl, string summary, string filename, string swidth, string sHeight)
    {
        string Sql = "INSERT INTO " + TableName + "(id, title, linkurl, summary, filename, created, userid, status, nclick, fwidth, fheight, kind)";
        Sql += " VALUES(?id, ?title, ?linkurl, ?summary, ?filename, ?created, ?userid, ?status, ?nclick, ?fwidth, ?fheight, ?kind)";
        DbTask db = new DbTask();
        string id = db.GetNewKey(TableName, "id", string.Empty);
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "title", DbType.NVarChar, title);
        db.AddParameters(ref dt, "linkurl", DbType.NVarChar, linkurl);
        db.AddParameters(ref dt, "summary", DbType.NVarChar, summary);
        db.AddParameters(ref dt, "fwidth", DbType.NVarChar, swidth);
        db.AddParameters(ref dt, "fheight", DbType.NVarChar, sHeight);
        db.AddParameters(ref dt, "status", DbType.Int32, 0);
        db.AddParameters(ref dt, "created", DbType.Datetime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") );
        db.AddParameters(ref dt, "userid", DbType.NVarChar, Membertask.Name);
        db.AddParameters(ref dt, "filename", DbType.NVarChar, filename);
        db.AddParameters(ref dt, "kind", DbType.Int32, 0);
        db.AddParameters(ref dt, "nclick", DbType.Int32, 0);        
        db.ExecuteNonQuery(Sql, dt);
    }
    public void InsertHTML(string title, string ContentHtml)
    {
        string Sql = "INSERT INTO " + TableName + "(id, title, filename, kind, created, userid )";
        Sql += " VALUES(?id, ?title, ?filename, ?kind, ?created, ?userid)";
        DbTask db = new DbTask();
        string id = db.GetNewKey(TableName, "id", string.Empty);
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "title", DbType.NVarChar, title);
        db.AddParameters(ref dt, "kind", DbType.Int32, 1);
        db.AddParameters(ref dt, "filename", DbType.Text, ContentHtml);
        db.AddParameters(ref dt, "created", DbType.Datetime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        db.AddParameters(ref dt, "userid", DbType.NVarChar, Membertask.Name);
        db.ExecuteNonQuery(Sql, dt);
    }
    public void Update(string id, string title, string linkurl, string summary, string filename, string sWidth, string sHeight)
    {
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " title = ?title,  ";
        Sql += " linkurl = ?linkurl,  ";
        Sql += " summary = ?summary,  ";
        Sql += " created = ?created,  ";
        Sql += " filename = ?filename, ";
        Sql += " fwidth = ?fwidth, ";
        Sql += " fHeight = ?fheight, ";
        Sql += " userid = ?userid "; 
        Sql += " WHERE id=?id";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "title", DbType.NVarChar, title);
        db.AddParameters(ref dt, "summary", DbType.NVarChar, summary);
        db.AddParameters(ref dt, "fwidth", DbType.NVarChar, sWidth);
        db.AddParameters(ref dt, "fheight", DbType.NVarChar, sHeight);
        db.AddParameters(ref dt, "linkurl", DbType.NVarChar, linkurl);
        db.AddParameters(ref dt, "created", DbType.Datetime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        db.AddParameters(ref dt, "filename", DbType.NVarChar, filename);
        db.AddParameters(ref dt, "userid", DbType.NVarChar, Membertask.Name);
        db.ExecuteNonQuery(Sql, dt); 
    }
    public void UpdateHtml(string id, string title, string filename)
    {
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " title = ?title,  ";
        Sql += " filename = ?filename ";
        Sql += " WHERE id=?id";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "title", DbType.NVarChar, title);
        db.AddParameters(ref dt, "filename", DbType.NVarChar, filename);
        db.AddParameters(ref dt, "userid", DbType.NVarChar, Membertask.Name);
        db.ExecuteNonQuery(Sql, dt);
    }
    public void Delete(string id)
    {
        DbTask db = new DbTask();
        string Sql = "DELETE FROM " + TableName + " WHERE id = '" + id.Trim() + "'";
        db.ExecuteNonQuery(Sql);
    }

    public void SetStatus(string id)
    {
        DbTask db = new DbTask();
        DataRow dr = this.GetInfo(id);
        if (dr == null)
            return;
        int status;
        if (Convert.ToInt32(dr["status"]) == 0)
            status = 1;
        else
            status = 0;

        string Sql = "UPDATE " + TableName + " SET ";
        Sql += "status = " + status + " ";
        Sql += "WHERE id = '" + id + "'";
        db.ExecuteNonQuery(Sql);
    }

    public DataRow GetInfo(string id)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT * FROM " + TableName + " WHERE id = '" + id + "' ";
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }

    // tong so ban ghi, tong so trang;
    public DataTable Searching(int Status, int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages)
    {
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;
        DbTask db = new DbTask();

        string Sql = string.Empty;
        Sql = "SELECT COUNT(ID) AS Total FROM " + TableName + "  ";
        Sql += "WHERE 1=1 ";

        if (Status < 2)
            Sql += "AND status = " + Status + " ";
        
        dt = db.GetData(Sql.Trim());
        if (dt == null || dt.Rows.Count == 0)
            return null;
        TotalRecords = Convert.ToInt32(dt.Rows[0]["Total"]);
        
        // da ay duoc tong so ban ghi ;
        TotalPages = (int)TotalRecords / RecordPerPages;
        if ((int)TotalRecords % RecordPerPages > 0)
            TotalPages = TotalPages + 1;
        Sql = "select " + TableName + ".*,row_number() over (order by created ASC) as row_index INTO #Temp_Members  ";
        Sql += "From " + TableName + " ";
        Sql += "Where 1=1";
        if(Status >2)
        {
            Sql += "And status = " + Status + " ";
        }
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
