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
/// Summary description for AdvertOnline
/// </summary>
public class AdvertOnline
{
    public static DataTable GetAllPage()
    {
        if ((DataTable)HttpContext.Current.Application["AllPage"] == null)
        {
            Globals.xmltask.FileName = Globals.AbsData + "xmls/advpage.xml";
            HttpContext.Current.Application["AllPage"] = Globals.xmltask.Read();
        }
        return (DataTable)HttpContext.Current.Application["AllPage"];
    }

    private static string _TableName
    {
        get
        {
            return "tbl_advertonline_" + Globals.CurrentLang;
        }
    }
    private string _GroupID = "00";
    private string _Col = "L";
    public AdvertOnline(string GroupID, string Col)
    {
        _GroupID = GroupID;
        _Col = Col;
        try
        {
            //Create Table
            string Sql = "CREATE TABLE IF NOT EXISTS " + _TableName + "(";
            Sql += "groupid nvarchar(255) NOT NULL default '', ";
            Sql += "id nvarchar(10) NOT NULL default '', ";
            Sql += "col nvarchar(2) NOT NULL default '', ";
            Sql += "startdate datetime default NULL, ";
            Sql += "enddate datetime default NULL, ";
            Sql += "idx tinyint(4) default 0, ";
            Sql += "PRIMARY KEY  (groupid,id,col)) ";
            Sql += "ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_bin;";
            DbTask db = new DbTask();
            db.ExecuteNonQuery(Sql);
            //

        }
        catch { }
    }
    public void AddToAdvert(string id)
    {
        try
        {
            DbTask db = new DbTask();
            string Sql = string.Empty;
            Sql = "UPDATE " + _TableName + " SET idx = idx + 1";
            db.ExecuteNonQuery(Sql);

            DateTime sStartDate = DateTime.Now;
            DateTime dt = DateTime.Now;
            int nYear = dt.Year + 1;
            dt = new DateTime(nYear, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);

            Sql = "INSERT INTO " + _TableName + "(groupid, id, col, startdate, enddate, idx) ";
            Sql += " VALUES(?groupid, ?id, ?col, ?startdate, ?enddate, ?idx)"; 
            DataTable datatable = null;
            db.AddParameters(ref datatable, "groupid", DbType.NVarChar, _GroupID);
            db.AddParameters(ref datatable, "id", DbType.NVarChar, id);
            db.AddParameters(ref datatable, "col", DbType.NVarChar, _Col);
            db.AddParameters(ref datatable, "startdate", DbType.Datetime, sStartDate);
            db.AddParameters(ref datatable, "enddate", DbType.Datetime, dt);
            db.AddParameters(ref datatable, "idx", DbType.Int32, 1);

            db.ExecuteNonQuery(Sql, datatable);
           
        }
        catch(Exception ae) {

            string error = ae.ToString();
        }
    }
    private int GetIndex(string id)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT idx FROM " + _TableName + " ";
        Sql += "WHERE groupid = '" + _GroupID + "' ";
        Sql += "AND id = '" + id + "' ";
        Sql += "AND col = '" + _Col + "' ";
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return 0;
        return Convert.ToInt32(dt.Rows[0]["idx"]);
    }
    public void RemoveFromAdvert(string id)
    {
        try
        {
            DbTask db = new DbTask();
            int idx = GetIndex(id);

            string Sql = "DELETE FROM " + _TableName + " ";
            Sql += "WHERE groupid = '" + _GroupID + "' ";
            Sql += "AND id = '" + id + "' ";
            Sql += "AND col = '" + _Col + "' ";
            db.ExecuteNonQuery(Sql);
            //
            Sql = "UPDATE " + _TableName + " SET idx = idx -1 ";
            Sql += "WHERE groupid = '" + _GroupID + "' ";
            Sql += "AND col = '" + _Col + "' ";
            Sql += "AND idx > '" + idx + "' ";
            db.ExecuteNonQuery(Sql);
        }
        catch { }
    }
    public void Update(string id, DateTime startdate, DateTime enddate, int idx)
    {
        try
        {
            string Sql = "UPDATE " + _TableName + " SET ";
            Sql += " idx = ?idx,  ";
            Sql += " startdate = ?startdate,  ";
            Sql += " enddate = ?enddate  ";
            Sql += "WHERE groupid = '" + _GroupID + "' ";
            Sql += "AND id = '" + id + "' ";
            Sql += "AND col = '" + _Col + "' ";
            DbTask db = new DbTask();
            DataTable dt = null;           
            db.AddParameters(ref dt, "groupid", DbType.NVarChar, _GroupID);
            db.AddParameters(ref dt, "id", DbType.NVarChar, id);
            db.AddParameters(ref dt, "col", DbType.NVarChar, _Col);
            db.AddParameters(ref dt, "startdate", DbType.Datetime, startdate );
            db.AddParameters(ref dt, "enddate", DbType.Datetime,  enddate);
            db.AddParameters(ref dt, "idx", DbType.Int32, idx);           
            db.ExecuteNonQuery(Sql, dt); 

        }
        catch (Exception ae)
        {

            string error = ae.ToString();
        }
    }

    public DataTable GetAdvert()
    {
        string tbl_Advert = Advert.TableName;
        string Sql = "SELECT " + tbl_Advert + ".id AS id," + tbl_Advert + ".title AS title," + tbl_Advert + ".filename AS filename, " + tbl_Advert + ".summary AS summary, " + _TableName + ".startdate AS startdate, " + _TableName + ".enddate AS enddate, " + _TableName + ".idx AS idx FROM " + tbl_Advert + " INNER JOIN " + _TableName + " ON " + tbl_Advert + ".id = " + _TableName + ".id ";
        Sql += "WHERE " + tbl_Advert + ".status = 1 ";
        Sql += "AND " + _TableName + ".groupid = '" + _GroupID + "' ";
        Sql += "AND " + _TableName + ".col = '" + _Col + "' ";
        Sql += "ORDER BY " + _TableName + ".idx ASC," + tbl_Advert + ".created DESC ";
        DbTask db = new DbTask();
        return db.GetData(Sql);
    }
    public DataTable GetNotInAdvert(int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;

        string tbl_Advert = Advert.TableName;
        string Sql = string.Empty;
        Sql = "SELECT COUNT(id) AS Total FROM " + tbl_Advert + " ";
        Sql += "WHERE id NOT IN(SELECT id FROM " + _TableName + " WHERE groupid = '" + _GroupID + "' AND col = '" + _Col + "') ";
        Sql += "AND status =1 ";

        dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;

        TotalRecords = Convert.ToInt32(dt.Rows[0]["Total"]);
        TotalPages = (int)TotalRecords / RecordPerPages;
        if ((int)TotalRecords % RecordPerPages > 0)
            TotalPages = TotalPages + 1;
        // thuydhv;
        Sql = "Select " + tbl_Advert + ".*,row_number() over (order by created DESC) as row_index INTO #Temp_Members ";
        Sql += "From " + tbl_Advert + " ";
        Sql += " WHERE id NOT IN(SELECT id FROM " + _TableName + " WHERE groupid = '" + _GroupID + "' AND col= '" + _Col + "') ";
        Sql += "AND status = 1 ";
        int nS = RecordPerPages * (CurrentPage - 1);
        int record_next = nS + RecordPerPages;
        Sql += "Select * from #Temp_Members ";
        if (nS == 0)
            Sql += " WHERE (row_index >=" + nS + ") AND (row_index <=" + record_next + ")";
        else
            Sql += " WHERE (row_index >" + nS + ") AND (row_index <=" + record_next + ")";
        Sql += " ORDER BY row_index ASC ";
        return db.GetData(Sql);
    }
}
