using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Parent_uc_menuright : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Menus smenu = new Menus(5);
            rptDM.DataSource = smenu.GetMenu();
            rptDM.DataBind();
        }
    }
}
