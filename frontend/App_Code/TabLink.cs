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
/// Summary description for TabLink
/// </summary>
public class TabLink
{
    public static string TableTochucLink_Home
    {
        get { return "tbl_tochuc_linktab_" + Globals.CurrentLang; }
    }
    public static string TableTabLink_Home
    {
        get { return "tbl_link_tabsp_" + Globals.CurrentLang; }
    }
    //TAB01
    public DataTable GetInforLink_Tab_Home(int IDTAB, int recordPages)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        string Sql = "SELECT Top " + recordPages + " a.*,b.idx as idx FROM " + TableTabLink_Home + "  as a," + TableTochucLink_Home + " as b ";
        Sql += " WHERE b.idtab = " + IDTAB + "  AND a.id = b.linktab_id ORDER BY idx ASC ";
        dt = db.GetData(Sql);
        return dt;
    }
}
