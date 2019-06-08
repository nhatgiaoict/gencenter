using System;
using System.Data;
using System.IO;
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

    public DataTable GetInfo()
    {
        DataTable dt = new DataTable();
        if (clsCache.Get("GetInfoLienket") != null)
        {
            dt = (DataTable)clsCache.Get("GetInfoLienket");
        }
        else
        {
            string Sql = "SELECT * FROM " + TableName + "  Order by sott ASC ";
            DbTask db = new DbTask();
            dt = db.GetData(Sql);
            clsCache.Max("GetInfoLienket", (object)dt);
        }
        return dt;
    }
}

