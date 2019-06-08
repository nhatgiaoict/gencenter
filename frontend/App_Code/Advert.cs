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
    public static string TableStore
    {
        get { return "tbl_advert_" + Globals.CurrentLang; }
    }
    private static string TableOnline
    {
        get { return "tbl_advertonline_" + Globals.CurrentLang; }
    }
    public static DataTable GetAdvert(string groupid, string col)
    {
        DataTable dt = new DataTable();
        if (clsCache.Get("GetAdvert") != null)
        {
            dt = (DataTable)clsCache.Get("GetAdvert");
        }
        else
        {
            DbTask db = new DbTask();
            string thisTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string Sql = "SELECT  " + TableStore + ".*," + TableOnline + ".groupid as groupid FROM " + TableStore + " INNER JOIN " + TableOnline + " ON " + TableStore + ".id = " + TableOnline + ".id ";
            Sql += "WHERE " + TableStore + ".status =1 ";
            Sql += "AND " + TableOnline + ".groupid = '" + groupid + "' ";
            Sql += "AND " + TableOnline + ".col = '" + col + "' ";
            Sql += "AND " + TableOnline + ".startdate <= '" + thisTime + "' ";
            Sql += "AND " + TableOnline + ".enddate >='" + thisTime + "' ";
            Sql += "ORDER BY " + TableOnline + ".idx ASC ," + TableStore + ".created DESC ";
            dt = db.GetData(Sql);
        }
        if (dt == null && dt.Rows.Count == 0)
            return null;
        return dt;
    }
    public static string IncClick(string id)
    {
        DbTask db = new DbTask();
        string Sql = "UPDATE " + TableStore + " SET nclick = nClick +1 ";
        Sql += "WHERE id = '" + id + "'";
        db.ExecuteNonQuery(Sql);
        Sql = "SELECT linkurl FROM " + TableStore + " WHERE id = '" + id + "'";
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return string.Empty;
        return dt.Rows[0]["linkurl"].ToString();
    }
}