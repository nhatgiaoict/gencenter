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
/// Summary description for Partners
/// </summary>
public class Partners
{
	public Partners()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static string TableStore
    {
        get { return "tbl_advert_" + Globals.CurrentLang; }
    }

    private static string TablePartner
    {
        get { return "tbl_publish_partner_" + Globals.CurrentLang; }
    }

    public DataTable GetPartners(string groupid)
    {
        DbTask db = new DbTask();
        string thisTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string Sql = "SELECT " + TableStore + ".*," + TablePartner + ".groupid as groupid FROM " + TableStore + " INNER JOIN " + TablePartner + " ON " + TableStore + ".id = " + TablePartner + ".id ";
        Sql += "WHERE " + TableStore + ".status =1 ";
        Sql += "AND " + TablePartner + ".groupid = '" + groupid + "' ";
        Sql += "AND " + TablePartner + ".startdate <= '" + thisTime + "' ";
        Sql += "AND " + TablePartner + ".enddate >='" + thisTime + "' ";
        Sql += "ORDER BY " + TablePartner + ".idx ASC ," + TableStore + ".created DESC ";
        return db.GetData(Sql);
    }

    public DataTable Getdoitac()
    {
        DbTask db = new DbTask();
        string SQL = " SELECT title, fimage,url, summary from " + TablePartner + " where status = 1 ";
        return db.GetData(SQL);
    }
}
