using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class product_uc_checkkind : System.Web.UI.UserControl
{
    private Groups sGroup = new Groups();
    private string kind
    {
        get
        {
            string _groupid = string.Empty;
            if (Request.QueryString["shortlink"] != null)
            {
                string shortlink = Request.QueryString["shortlink"].ToString();
                DataRow dr = sGroup.GetInfoByShortLink(shortlink.Trim());
                if (dr != null)
                {
                    _groupid = dr["kind"].ToString().Trim();
                }
            }
            return _groupid;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (kind == "0")
        {
            panelcontel.Controls.Add(Page.LoadControl("~/Parent/uc_parentnews.ascx"));
        }
        else
        {
            panelcontel.Controls.Add(Page.LoadControl("~/product/uc_product.ascx"));
        }
    }
}
