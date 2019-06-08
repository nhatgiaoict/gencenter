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
/// Summary description for Slide
/// </summary>
public class Slide
{
    public static string TableName
    {
        get
        {
            return "tbl_slide_" + Globals.CurrentLang;
        }
    }
	public Slide()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void InsertSlide(string title, string filename, string summary, string Url)
    {
        DbTask db = new DbTask();
        string Sql = "Insert Into " + TableName + " (id, title, filename, summary, datetime, Url) ";
        Sql += " Values (?id, ?title,?filename,?summary, ?datetime, ?Url)";
        string id = db.GetNewKey(TableName, "id", string.Empty);
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "title", DbType.NVarChar, title);
        db.AddParameters(ref dt, "filename", DbType.NVarChar, filename);
        db.AddParameters(ref dt, "summary", DbType.NVarChar, summary);
        db.AddParameters(ref dt, "Url", DbType.NVarChar, Url);
        db.AddParameters(ref dt, "datetime", DbType.NVarChar, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        db.ExecuteNonQuery(Sql, dt);
    }
    public DataTable GetSlide()
    {
        DbTask db = new DbTask();
        string Sql = "Select * from " + TableName + "";
        return db.GetData(Sql);
    }
    public DataTable SearchingSlide(int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages)
    {

        DbTask db = new DbTask();
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;
        string Sql = string.Empty;
        Sql = " SELECT COUNT(ID) AS Total FROM " + TableName + "";
        dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        TotalRecords = Convert.ToInt32(dt.Rows[0]["Total"]);
        TotalPages = (int)TotalRecords / RecordPerPages;
        if ((int)TotalRecords % RecordPerPages > 0)
            TotalPages = TotalPages + 1;
        Sql = "Select " + TableName + ".*, row_number() over (order by id ASC) as row_index INTO #Temp_Table ";
        Sql += "from " + TableName + " ";
        int nS = RecordPerPages * (CurrentPage - 1);
        int record_next = nS + RecordPerPages;
        Sql += "Select * from #Temp_Table ";
        if (nS == 0)
            Sql += " WHERE (row_index >=" + nS + ") AND (row_index <=" + record_next + ")";
        else
            Sql += " WHERE (row_index >" + nS + ") AND (row_index <=" + record_next + ")";
        Sql += " ORDER BY row_index ASC ";
        dt = db.GetData(Sql);
        return dt;
    }
    public DataRow GetInfoSlide(string id)
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
    public void UpdateSlide(string id, string title, string filename, string summary, string Url)
    {
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " title = ?title,  ";
        Sql += " filename = ?filename,  ";
        Sql += " Url = ?Url,  ";
        Sql += " summary = ?summary ";
        Sql += " WHERE id = ?id";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "title", DbType.NVarChar, title);
        db.AddParameters(ref dt, "filename", DbType.NVarChar, filename);
        db.AddParameters(ref dt, "summary", DbType.NVarChar, summary);
        db.AddParameters(ref dt, "Url", DbType.NVarChar, Url);
        db.ExecuteNonQuery(Sql, dt);
    }

    public void DeleteSlide(string id)
    {
        DbTask db = new DbTask();
        string Sql = string.Empty;
        Sql = "DELETE FROM " + TableName + " WHERE id = '" + id.Trim() + "'";
        db.ExecuteNonQuery(Sql);
    }
    public void UpdateIndexSlide(string id, int Index)
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

    public void SetAnh(string id)
    {
        DataRow dr = this.GetInfo(id);
        if (dr == null)
            return;
        int status = 0;
        if (Convert.ToInt32(dr["anh"]) == 0)
            status = 1;
        else
            status = 0;
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " anh = ?anh ";
        Sql += " WHERE id = ?id ";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "anh", DbType.Int32, status);
        db.ExecuteNonQuery(Sql, dt);
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

