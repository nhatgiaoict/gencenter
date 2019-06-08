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

public class Keyword
{
    public Keyword()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static string TableName
    {
        get { return "tbl_keyword_" + Globals.CurrentLang; }
    }
    public void InsertKeyWord(string title, string link)
    {
        DbTask db = new DbTask();
        string Sql = "Insert Into " + TableName + " (id,title,link) ";
        Sql += "Values (?id, ?title,?link)";
        string id = db.GetNewKey(TableName, "id", string.Empty);
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "title", DbType.NVarChar, title);
        db.AddParameters(ref dt, "link", DbType.NVarChar, link);
        db.ExecuteNonQuery(Sql, dt);
    }

    public void UpDateKeyWord(string id, string title, string link)
    {
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " title = ?title,  ";
        Sql += " link = ?link ";
        Sql += " WHERE id = ?id";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "title", DbType.NVarChar, title);
        db.AddParameters(ref dt, "link", DbType.NVarChar, link);
        db.ExecuteNonQuery(Sql, dt);
    }
    public DataTable GetAllKeyWord()
    {
        DbTask db = new DbTask();
        string Sql = "Select * from " + TableName + " ";
        return db.GetData(Sql);
    }
    public void SetStatus(string id)
    {
        DbTask db = new DbTask();
        DataRow dr = GetInfoKeyWord(id);
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
    public void UpdateIndexKeyWord(string id, int Index)
    {
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += "idx = " + Index + " ";
        Sql += "WHERE id = '" + id + "'";
        DbTask db = new DbTask();
        db.ExecuteNonQuery(Sql);
    }
    public DataRow GetInfoKeyWord(string id)
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
    public void DeleteKeyWord(string id)
    {
        DbTask db = new DbTask();
        string Sql = string.Empty;
        Sql = "DELETE FROM " + TableName + " WHERE id = '" + id.Trim() + "'";
        db.ExecuteNonQuery(Sql);
        //Xoa cac keyword trong bang keyword
    }
}

