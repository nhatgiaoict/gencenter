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
/// Summary description for cls_yahoo_help
/// </summary>
public class cls_yahoo_help
{
    public static string TableName
    {
        get { return "tbl_yahoo_" + Globals.CurrentLang; }
    }
	public cls_yahoo_help()
	{
	}
    public DataTable GetYahoo()
    {
        DataTable dt = new DataTable();
        if (clsCache.Get("Yahoo_tbl") != null)
        {
            dt = (DataTable)clsCache.Get("Yahoo_tbl");
        }
        else
        {
            DbTask db = new DbTask();
            string Sql = "Select * from " + TableName + " where status = 1 Order by idx";
            dt = db.GetData(Sql);
            clsCache.Max("Yahoo_tbl", (object)dt);
        }
        if (dt == null && dt.Rows.Count == 0)
            return null;
        return dt;
    }
}
