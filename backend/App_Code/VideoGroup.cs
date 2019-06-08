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
/// Summary description for VideoGroup
/// </summary>
public class VideoGroup
{
	public VideoGroup()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static string TableName
    {
        get { return "tbl_Video_" + Globals.CurrentLang; }
    }
    public void InsertVideo(string title, string filename,string summary,string filenameimg)
    {
        DbTask db = new DbTask();
        string Sql = "Insert Into " + TableName + " (id, title, filename, summary, datetime,filenameimg) ";
        Sql += " Values (?id, ?title,?filename,?summary, ?datetime,?filenameimg)";
        string id = db.GetNewKey(TableName, "id", string.Empty);
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "title", DbType.NVarChar, title);
        db.AddParameters(ref dt, "filename", DbType.NVarChar, filename);
        db.AddParameters(ref dt, "summary", DbType.NVarChar, summary);
        db.AddParameters(ref dt, "datetime", DbType.NVarChar, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        db.AddParameters(ref dt, "filenameimg", DbType.NVarChar, filenameimg);
        db.ExecuteNonQuery(Sql, dt);
    }
    public DataTable GetVideo()
    {
        DbTask db = new DbTask();
        string Sql = "Select * from " + TableName + "";
        return db.GetData(Sql);
    }
    public DataRow GetInfoVideo(string id)
    {
        DbTask db = new DbTask();
        string Sql = "Select * from " + TableName + " ";
        Sql += "Where id = '" + id + "'";
        DataTable dt = null;
        dt = db.GetData(Sql);
        if (dt != null)
        {
            return dt.Rows[0];
        }
        else
        {
            return null;
        }
    }
    public void UpdateVideo(string id, string title, string filename,string summary,string filenameimg)
    {
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " title = ?title,  ";
        Sql += " filename = ?filename,  ";
        Sql += " summary = ?summary, ";
        Sql += " filenameimg = ?filenameimg ";
        Sql += " WHERE id = ?id";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "title", DbType.NVarChar, title);
        db.AddParameters(ref dt, "filename", DbType.NVarChar,filename);
        db.AddParameters(ref dt, "summary", DbType.NVarChar, summary);
        db.AddParameters(ref dt, "filenameimg", DbType.NVarChar, filenameimg);
        db.ExecuteNonQuery(Sql, dt);
    }

    public void DeleteVideo(string id)
    {
        DbTask db = new DbTask();
        string Sql = string.Empty;
        Sql = "DELETE FROM " + TableName + " WHERE id = '" + id.Trim() + "'";
        db.ExecuteNonQuery(Sql);
    }
    public void UpdateIndexVideo(string id, int Index)
    {
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += "idx = " + Index + " ";
        Sql += "WHERE id = '" + id + "'";
        DbTask db = new DbTask();
        db.ExecuteNonQuery(Sql);
    }
    public void SetStatus(string id)
    {
        DbTask db = new DbTask();
        DataRow dr = this.GetInfo(id);
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
    public DataRow GetInfo(string id)
    {
        string Sql = "SELECT * FROM " + TableName + " WHERE id = '" + id.Trim() + "' ";
        DbTask db = new DbTask();
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
}

