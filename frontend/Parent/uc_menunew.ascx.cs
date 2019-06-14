using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Parent_uc_menunew : System.Web.UI.UserControl
{
    private News objNew = new News();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt;
            if (clsCache.Get("CacheRecentNews" + Globals.CurrentLang) != null)
            {
                dt = (DataTable)clsCache.Get("CacheRecentNews" + Globals.CurrentLang);
            }
            else
            {
                dt = objNew.GetNewLates(10);
                clsCache.Max("CacheRecentNews" + Globals.CurrentLang, (object)dt);
            }
            rptRecentNews.DataSource = dt;
            rptRecentNews.DataBind();
        }
    }
}
