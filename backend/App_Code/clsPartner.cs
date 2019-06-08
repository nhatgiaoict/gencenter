using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for clsPartner
/// </summary>
public class clsPartner
{
    public static string TableName { get { return "tbl_publish_partner_" + Globals.CurrentLang; } }
	public clsPartner()
	{

	}
    public void Insert(string title, string summary, string fimage, string url)
    {
        DbTask db = new DbTask();
        string Sql = "Insert Into " + TableName + " (title,summary, fimage, Created, url) ";
        Sql += " Values (?title,?summary, ?fimage, ?Created, ?url)";
        DataTable dt = null;
        db.AddParameters(ref dt, "title", DbType.NVarChar, title);
        db.AddParameters(ref dt, "fimage", DbType.NVarChar, fimage);
        db.AddParameters(ref dt, "summary", DbType.NVarChar, summary);
        db.AddParameters(ref dt, "url", DbType.NVarChar, url);
        db.AddParameters(ref dt, "Created", DbType.Datetime, DateTime.Now.ToString());
        db.ExecuteNonQuery(Sql, dt);
    }
    public DataTable Searching(string Keyword, int status, int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages)
    {

        DbTask db = new DbTask();
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;
        string Sql = string.Empty;
        Sql = " SELECT COUNT(ID) AS Total FROM " + TableName + " where 1 = 1 ";
        if (Keyword != string.Empty && Keyword != null && Keyword != "")
        {
            string dau = Keyword.Trim().Substring(0, 1);
            string sau = Keyword.Trim().Substring(1);
            string abc = dau.ToUpper() + sau.ToLower();
            Sql += "AND( title LIKE N'%" + Keyword.Trim() + "%' OR title LIKE N'%" + Keyword.Trim().ToLower() + "%' OR title LIKE N'%" + Keyword.Trim().ToUpper() + "%' OR  title LIKE N'%" + abc + "%') ";
        }
        if (status < 2)
        {
            Sql += " AND status = '" + status + "'";
        }
        dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        TotalRecords = Convert.ToInt32(dt.Rows[0]["Total"]);
        TotalPages = (int)TotalRecords / RecordPerPages;
        if ((int)TotalRecords % RecordPerPages > 0)
            TotalPages = TotalPages + 1;
        Sql = "Select " + TableName + ".*, row_number() over (order by idx ASC) as row_index INTO #Temp_Table ";
        Sql += "from " + TableName + " where 1 = 1 ";

        if (Keyword != string.Empty && Keyword != null && Keyword != "")
        {
            string dau = Keyword.Trim().Substring(0, 1);
            string sau = Keyword.Trim().Substring(1);
            string abc = dau.ToUpper() + sau.ToLower();
            Sql += "AND( title LIKE N'%" + Keyword.Trim() + "%' OR title LIKE N'%" + Keyword.Trim().ToLower() + "%' OR title LIKE N'%" + Keyword.Trim().ToUpper() + "%' OR  title LIKE N'%" + abc + "%') ";
        }
        if (status < 2)
        {
            Sql += " AND status = '" + status + "'";
        }

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
    public void Update(string id, string title, string filename, string summary, string url)
    {
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " title = ?title,  ";
        Sql += " fimage = ?fimage,  ";
        Sql += " summary = ?summary, ";
        Sql += " url = ?url ";
        Sql += " WHERE id = ?id";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "title", DbType.NVarChar, title);
        db.AddParameters(ref dt, "fimage", DbType.NVarChar, filename);
        db.AddParameters(ref dt, "summary", DbType.NVarChar, summary);
        db.AddParameters(ref dt, "url", DbType.NVarChar, url);
        db.ExecuteNonQuery(Sql, dt);
    }
    public void Delete(string id)
    {
        DbTask db = new DbTask();
        string Sql = string.Empty;
        Sql = "DELETE FROM " + TableName + " WHERE id = '" + id.Trim() + "'";
        db.ExecuteNonQuery(Sql);
    }
    public void UpdateIndex(string id, int Index)
    {
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += "idx = " + Index + " ";
        Sql += "WHERE id = '" + id + "'";
        DbTask db = new DbTask();
        db.ExecuteNonQuery(Sql);
    }
    public DataRow GetInfo(string id)
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
}
