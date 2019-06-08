using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home_uc_duanhoanthanh : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            News snew = new News();
            rptSPF.DataSource = snew.GetDuanhoanthanh(10);
            rptSPF.DataBind();
        }
    }
}
