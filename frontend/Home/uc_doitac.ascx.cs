using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home_uc_doitac : System.Web.UI.UserControl
{
    private Partners sPartners = new Partners();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
                 rptDT.DataSource = sPartners.Getdoitac();
                 rptDT.DataBind();
        }
    }
  
}
