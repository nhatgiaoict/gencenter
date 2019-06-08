using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home_uc_team : System.Web.UI.UserControl
{
    private News sNew = new News();
    protected void Page_Load(object sender, EventArgs e)
    {
        rptTeam.DataSource = sNew.GetTeam(4);
        rptTeam.DataBind();
    }
    public string sContent(string id)
    {
        return sNew.GetContent(id);
    }
}