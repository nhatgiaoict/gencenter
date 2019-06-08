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
/// Summary description for Dbmanager
/// </summary>
public class Dbmanager
{
	const string tbl_name = "tbl_dbmanager";
    private DbTask dbtask = new DbTask();
    
    public void CreateTable()
    {
        string Sql = string.Empty;
        Sql += "CREATE TABLE IF NOT EXISTS "+tbl_name+"( ";
        Sql += "id tinyint(4) NOT NULL, ";
        Sql += "title nvarchar(500) default NULL, ";
        Sql += "dbname nvarchar(255) NOT NULL default '', ";
        Sql += "servername nvarchar(255) NOT NULL default '', ";
        Sql += "userid nvarchar(255) NOT NULL default '', ";
        Sql += "password nvarchar(255) default '', ";
        Sql += "PRIMARY KEY  (id)) ";
        Sql += "ENGINE=MyISAM DEFAULT CHARSET = utf8 COLLATE = utf8_bin;";
        dbtask.ExecuteNonQuery(Sql);
        return;
    }
    public void Insert(string title,string address, string dbname, string servername, string userid, string password)
    {
        int id = Convert.ToInt32(dbtask.GetNewKey(tbl_name, "id"));
        string Sql = string.Empty;
        Sql += "INSERT INTO " + tbl_name + "(id,title,address, dbname, servername, userid, password) ";
        Sql += "VALUES(?id, ?title,?address, ?dbname, ?servername, ?userid, ?password)";
        DataTable dt = null;
        dbtask.AddParameters(ref dt, "id", DbType.Int32, id);
        dbtask.AddParameters(ref dt, "title", DbType.NVarChar, title);
        dbtask.AddParameters(ref dt, "address", DbType.NVarChar, address);
        dbtask.AddParameters(ref dt, "dbname", DbType.NVarChar, dbname);
        dbtask.AddParameters(ref dt, "servername", DbType.NVarChar, servername);
        dbtask.AddParameters(ref dt, "userid", DbType.NVarChar, userid);
        dbtask.AddParameters(ref dt, "password", DbType.NVarChar, password);
        dbtask.ExecuteNonQuery(Sql, dt);
     }
    public void Update(int id,string title,string address, string dbname, string servername, string userid, string password)
    {
        string Sql = string.Empty;
        Sql += "UPDATE " + tbl_name + " SET ";
        Sql += "title = ?title, ";
        Sql += "address = ?address, ";
        Sql += "dbname = ?dbname, ";
        Sql += "servername = ?servername, ";
        Sql += "userid = ?userid, ";
        Sql += "password = ?password ";
        Sql += "WHERE id = ?id";
        DataTable dt = null;
        dbtask.AddParameters(ref dt, "title", DbType.NVarChar, title);
        dbtask.AddParameters(ref dt, "address", DbType.NVarChar, address);
        dbtask.AddParameters(ref dt, "dbname", DbType.NVarChar, dbname);
        dbtask.AddParameters(ref dt, "servername", DbType.NVarChar, servername);
        dbtask.AddParameters(ref dt, "userid", DbType.NVarChar, userid);
        dbtask.AddParameters(ref dt, "password", DbType.NVarChar, password);
        dbtask.AddParameters(ref dt, "id", DbType.Int32, id);
        dbtask.ExecuteNonQuery(Sql, dt);
    }
    public void Delete(int id)
    {
        string Sql = string.Empty;
        Sql += "DELETE FROM "+tbl_name+" WHERE id = "+id+"";
        dbtask.ExecuteNonQuery(Sql);
        return;
    }
    public DataTable GetData()
    {
        string Sql = string.Empty;
        Sql += "SELECT * FROM "+tbl_name+"";
        return dbtask.GetData(Sql);
    }
    public DataTable GetConcatData()
    {
        string Sql = string.Empty;
        Sql += "SELECT id, CONCAT(dbname, ' (' , servername, ')') AS title FROM " + tbl_name + "";
        return dbtask.GetData(Sql);
    }
    public DataRow GetInfo(int id)
    {
        string Sql = string.Empty;
        Sql += "SELECT * FROM " + tbl_name + " WHERE id = " + id + "";
        DataTable dt = dbtask.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        else
            return dt.Rows[0];

    }
    public DataTable Searching(int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;

        string Sql = string.Empty;
        Sql = "SELECT COUNT(ID) AS Total FROM " + tbl_name + "  ";
        Sql += "WHERE 1=1 ";

        dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;

        TotalRecords = Convert.ToInt32(dt.Rows[0]["Total"]);
        TotalPages = (int)TotalRecords / RecordPerPages;
        if ((int)TotalRecords % RecordPerPages > 0)
            TotalPages = TotalPages + 1;
        Sql = "select " + tbl_name + ".*, row_number() over (order by id DESC) as row_index INTO #Temp_Table ";
        Sql += "From " + tbl_name + " ";
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
