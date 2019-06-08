using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home_uc_hotproduct : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            News snew = new News();
            rptNB.DataSource = snew.GetDuannoibat(10);
            rptNB.DataBind();
        }
    }
}
