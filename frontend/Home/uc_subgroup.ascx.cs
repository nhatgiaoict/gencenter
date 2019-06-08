using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home_uc_subgroup : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Menus smenu = new Menus(2);
            rptG.DataSource = smenu.GetMenu();
            rptG.DataBind();
        }
    }
}
