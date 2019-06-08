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
/// Summary description for Partner
/// </summary>
public class Partner
{
    public static DataTable GetAllPage()
    {
        if ((DataTable)HttpContext.Current.Application["AllPage_partner"] == null)
        {
            Globals.xmltask.FileName = Globals.AbsData + "xmls/doitac_pages.xml";
            HttpContext.Current.Application["AllPage_partner"] = Globals.xmltask.Read();
        }
        return (DataTable)HttpContext.Current.Application["AllPage_partner"];
    }

    private static string TableName
    {
        get
        {
            return "tbl_publish_partner_" + Globals.CurrentLang;
        }
    }
    public DataTable GetPartner(string groupid)
    {
        string tbl_Advert = Advert.TableName;
        string Sql = "SELECT " + tbl_Advert + ".id AS id," + tbl_Advert + ".title AS title," + tbl_Advert + ".filename AS filename, " + tbl_Advert + ".summary AS summary, " + TableName + ".startdate AS startdate, " + TableName + ".enddate AS enddate, " + TableName + ".idx AS idx FROM " + tbl_Advert + " INNER JOIN " + TableName + " ON " + tbl_Advert + ".id = " + TableName + ".id ";
        Sql += "WHERE " + tbl_Advert + ".status = 1 ";
        Sql += "AND " + TableName + ".groupid = '" + groupid + "' ";
        Sql += "ORDER BY " + TableName + ".idx ASC," + tbl_Advert + ".created DESC ";
        DbTask db = new DbTask();
        return db.GetData(Sql);
    }
    public DataTable GetNotInPartner(string groupid,int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;

        string tbl_Advert = Advert.TableName;
        string Sql = string.Empty;
        Sql = "SELECT COUNT(id) AS Total FROM " + tbl_Advert + " ";
        Sql += "WHERE id NOT IN(SELECT id FROM " + TableName + " WHERE groupid = '" + groupid + "' AND  status =1) ";

        dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;

        TotalRecords = Convert.ToInt32(dt.Rows[0]["Total"]);
        TotalPages = (int)TotalRecords / RecordPerPages;
        if ((int)TotalRecords % RecordPerPages > 0)
            TotalPages = TotalPages + 1;
        Sql = "select " + tbl_Advert + ".*, row_number() over (order by created DESC) as row_index INTO #Temp_Table ";
        Sql += "from " + tbl_Advert + " ";
        Sql += "WHERE id NOT IN(SELECT id FROM " + TableName + " WHERE  groupid = '" + groupid + "' AND status =1) ";
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
    private int GetIndex(string id,string groupid)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT idx FROM " + TableName + " ";
        Sql += "WHERE id = '" + id + "' AND  groupid = '" + groupid + "' ";
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return 0;
        return Convert.ToInt32(dt.Rows[0]["idx"]);
    }
    public void RemoveFromPartner(string id,string groupid)
    {
        try
        {
            DbTask db = new DbTask();
            int idx = GetIndex(id,groupid);

            string Sql = "DELETE FROM " + TableName + " ";
            Sql += "WHERE groupid = '" + groupid + "' ";
            Sql += "AND id = '" + id + "' ";
            db.ExecuteNonQuery(Sql);
            //
            Sql = "UPDATE " + TableName + " SET idx = idx -1 ";
            Sql += "WHERE groupid = '" + groupid + "' ";
            Sql += "AND idx > '" + idx + "' ";
            db.ExecuteNonQuery(Sql);
        }
        catch { }
    }
    public void Update(string id,string groupid, DateTime startdate, DateTime enddate, int idx)
    {
        try
        {
            string Sql = "UPDATE " + TableName + " SET ";
            Sql += " idx = ?idx,  ";
            Sql += " startdate = ?startdate,  ";
            Sql += " enddate = ?enddate  ";
            Sql += "WHERE groupid = '" + groupid + "' ";
            Sql += "AND id = '" + id + "' ";
            DbTask db = new DbTask();
            DataTable dt = null;
            db.AddParameters(ref dt, "groupid", DbType.NVarChar, groupid);
            db.AddParameters(ref dt, "id", DbType.NVarChar, id);
            db.AddParameters(ref dt, "startdate", DbType.Datetime, startdate);
            db.AddParameters(ref dt, "enddate", DbType.Datetime, enddate);
            db.AddParameters(ref dt, "idx", DbType.Int32, idx);
            db.ExecuteNonQuery(Sql, dt);

        }
        catch (Exception ae)
        {

            string error = ae.ToString();
        }
    }
    public void AddTopartner(string id,string groupid)
    {
        try
        {
            DbTask db = new DbTask();
            string Sql = string.Empty;
            Sql = "UPDATE " + TableName + " SET idx = idx + 1";
            db.ExecuteNonQuery(Sql);

            DateTime sStartDate = DateTime.Now;
            DateTime dt = DateTime.Now;
            int nYear = dt.Year + 1;
            dt = new DateTime(nYear, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);

            Sql = "INSERT INTO " + TableName + "(groupid, id, startdate, enddate, idx) ";
            Sql += " VALUES(?groupid, ?id, ?startdate, ?enddate, ?idx)";
            DataTable datatable = null;
            db.AddParameters(ref datatable, "groupid", DbType.NVarChar, groupid);
            db.AddParameters(ref datatable, "id", DbType.NVarChar, id);
            db.AddParameters(ref datatable, "startdate", DbType.Datetime, sStartDate);
            db.AddParameters(ref datatable, "enddate", DbType.Datetime, dt);
            db.AddParameters(ref datatable, "idx", DbType.Int32, 1);

            db.ExecuteNonQuery(Sql, datatable);

        }
        catch (Exception ae)
        {

            string error = ae.ToString();
        }
    }
}
