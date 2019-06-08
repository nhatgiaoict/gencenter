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
/// Summary description for WebLink
/// </summary>
public class WebLink
{
    
    public static string TableName
    {
        get { return "tbl_weblink_" + Globals.CurrentLang; }
    }
    public void UpdateIndex(int id, int idx)
    {
        try
        {
            string Sql = "UPDATE " + TableName + " ";
            Sql += "SET sott = " + idx + " ";
            Sql += "WHERE id = " + id + " ";
            DbTask db = new DbTask();
            db.ExecuteNonQuery(Sql);
        }
        catch { }
    }
    public void Insert(string name, string link, int sott)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        string Sql = "INSERT INTO " + TableName + "(name,link,sott) ";
        Sql += " VALUES(?name,?link,?sott)";
        db.AddParameters(ref dt, "name", DbType.NVarChar, name);
        db.AddParameters(ref dt, "link", DbType.NVarChar, link);
        db.AddParameters(ref dt, "sott", DbType.Int32, sott);
        db.ExecuteNonQuery(Sql, dt);
    }
    public void Update(string vid, string name, string link)
    {
        int id = Convert.ToInt32(vid);
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " link = ?link,  ";
        Sql += " name = ?name  ";
        Sql += "WHERE ?id = id ";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.Int32, id);
        db.AddParameters(ref dt, "name", DbType.NVarChar, name);
        db.AddParameters(ref dt, "link", DbType.NVarChar, link);
        db.ExecuteNonQuery(Sql, dt); 

    }
    public DataRow Getdata(string id)
    {
        int vID = Convert.ToInt32(id);
        string Sql = "SELECT * FROM " + TableName + " WHERE id = " + vID + " ";
        DbTask db = new DbTask();
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
    public DataRow GetInfo()
    {
        string Sql = "SELECT * FROM " + TableName + " ";
        DbTask db = new DbTask();
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
    public void Delete(string id)
    {
        string Sql = "DELETE FROM " + TableName + " WHERE id = "+ Convert.ToInt32(id)+" ";
        DbTask db = new DbTask();
        db.ExecuteNonQuery(Sql);
    }
    public DataRow GetCount()
    {
        string Sql = "SELECT COUNT(ID) AS Total FROM " + TableName + " ";
        DbTask db = new DbTask();
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];

    }
    public DataTable Searching(int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;

        string Sql = string.Empty;
        Sql = "SELECT COUNT(ID) AS Total FROM " + TableName + "  ";
        Sql += "WHERE 1=1 ";

        dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;

        TotalRecords = Convert.ToInt32(dt.Rows[0]["Total"]);
        TotalPages = (int)TotalRecords / RecordPerPages;
        if ((int)TotalRecords % RecordPerPages > 0)
            TotalPages = TotalPages + 1;
        // thuycode
        Sql = "select " + TableName + ".*, row_number() over (order by sott ASC) as row_index INTO #Temp_Table ";
        Sql += "From " + TableName + " ";
        Sql += "where 1 = 1";
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
}
