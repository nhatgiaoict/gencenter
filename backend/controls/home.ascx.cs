using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class controls_home : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ltlWelcome.Text = "<P align=center><font color=#ff6600 size=5 face=Arial>" +Language.GetTextByID(23)+"</font></p>";
    }
}
