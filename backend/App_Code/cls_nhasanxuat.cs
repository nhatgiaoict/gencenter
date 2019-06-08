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
/// Summary description for cls_nhasanxuat
/// </summary>
public class cls_nhasanxuat
{
    public static string TableName { get { return "tbl_nhasanxuat_vn"; } }
    public static string Tablekhoangia
    {
        get
        {
            return "tbl_khoanggia_vn";
        }
    }
	public cls_nhasanxuat()
	{
	}
    public void Insert(string stitle, string TenBang)
    {
        string Sql = "INSERT INTO " + TenBang + "(id, title)";
        Sql += " VALUES (?id, ?title)";
        DbTask db = new DbTask();
        string id = db.GetNewKey(TenBang, "id", string.Empty);
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "title", DbType.NVarChar, stitle);
        db.ExecuteNonQuery(Sql, dt);
    }

    public void InsertPrice(string stitle, string TenBang, string Min, string Max)
    {
        string Sql = "INSERT INTO " + TenBang + "(id, title, Min, Max)";
        Sql += " VALUES (?id, ?title, ?Min, ?Max)";
        DbTask db = new DbTask();
        string id = db.GetNewKey(TenBang, "id", string.Empty);
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "title", DbType.NVarChar, stitle);
        db.AddParameters(ref dt, "Min", DbType.NVarChar, Min);
        db.AddParameters(ref dt, "Max", DbType.NVarChar, Max);
        db.ExecuteNonQuery(Sql, dt);
    }

    public bool CheckExist(string titls, string TenBang)
    {
        string Sql = "SELECT * FROM  " + TenBang + " ";
        Sql += "WHERE title = N'" + titls.Trim() + "' ";
        DbTask db = new DbTask();
        DataTable dt = db.GetData(Sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            return true;
        }
        return false;
    }
    public void Update(string id, string title, string idx, string TenBang)
    {
        DbTask db = new DbTask();
        string SQL = "Update " + TenBang + " SET ";
        SQL += "title = ?title,  ";
        SQL += "idx = ?idx ";
        SQL += "Where id = ?id ";
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "title", DbType.NVarChar, title);
        db.AddParameters(ref dt, "idx", DbType.NVarChar, idx);
        db.ExecuteNonQuery(SQL, dt);
    }
    public void UpdatePrice(string id, string title, string idx, string TenBang, string Min, string Max)
    {
        DbTask db = new DbTask();
        string SQL = "Update " + TenBang + " SET ";
        SQL += "title = ?title,  ";
        SQL += "Min = ?Min,  ";
        SQL += "Max = ?Max,  ";
        SQL += "idx = ?idx ";
        SQL += "Where id = ?id ";
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "title", DbType.NVarChar, title);
        db.AddParameters(ref dt, "Min", DbType.NVarChar, Min);
        db.AddParameters(ref dt, "Max", DbType.NVarChar, Max);
        db.AddParameters(ref dt, "idx", DbType.NVarChar, idx);
        db.ExecuteNonQuery(SQL, dt);
    }
    public DataTable GetAllDatabase(string TenBang)
    {
        DbTask db = new DbTask();
        string sql = "Select * from " + TenBang + " Order by idx ";
        return db.GetData(sql);
    }
    public DataTable GetAllDatabaseActive(string TenBang)
    {
        DbTask db = new DbTask();
        string sql = "Select * from " + TenBang + " Where status = 1 Order by idx ";
        return db.GetData(sql);
    }
    public void Delete(string id, string TenBang)
    {
        DbTask db = new DbTask();
        string SQL = "Delete from " + TenBang + " where id = '" + id + "'";
        db.ExecuteNonQuery(SQL);
    }
}
