using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Parent_uc_menucontext : System.Web.UI.UserControl
{
    private Groups sGroup = new Groups();
    private string groupid
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
                    _groupid = dr["id"].ToString().Trim();
                    ltltitleG.Text = dr["title"].ToString().Trim();
                }
            }
            return _groupid;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Menus smenu = new Menus(1);
            rptCT.DataSource = smenu.GetMutil_Parent(groupid);
            rptCT.DataBind();
        }
    }
}
