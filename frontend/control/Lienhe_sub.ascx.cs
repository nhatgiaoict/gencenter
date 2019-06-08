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
using System.Xml.Linq;

public partial class controls_Lienhe_sub : System.Web.UI.UserControl
{
    private WebInfor sWebinfo = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        sWebinfo = new WebInfor();
        if (!this.IsPostBack)
        {
            DataRow dr = sWebinfo.GetInfo();
            if (dr != null)
            {
                ltlAddCity.Text = dr["contact"].ToString();
                //spanTenCong.Text = dr["TenCongty"].ToString().Trim();
                //ltlFax.Text = dr["Fax"].ToString();
                //ltlWebsite.Text = dr["Website"].ToString();
            }
        }
    }
}
