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

public partial class controls_login : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            imgKey.Src = Globals.UrlImages + "key.gif";
            ltlTitle.Text = Language.GetTextByID(75);
            ltlID.Text = Language.GetTextByID(57);
            ltlPwd.Text = Language.GetTextByID(59);
            btnSubmit.Text = Language.GetTextByID(76);
            btnPassLost.Text = Language.GetTextByID(200);

        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string sUsername = txtUserID.Value.Trim();
        string sPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPwd.Value.Trim(), "MD5");
        Member member = new Member();
        DataRow dr = member.Login(sUsername, sPassword);
        if (dr == null)
        {
            ltlError.Visible = true;
            ltlError.Text = Language.GetTextByID(78);
            return;
        }
        if (Convert.ToInt32(dr["status"].ToString()) == 0)
        {
            ltlError.Visible = true;
            ltlError.Text = Language.GetTextByID(79);
            return;
        }
        Session["UserName"] = dr["username"].ToString();
        Response.Redirect(Globals.UrlRoot + "default.aspx");

    }
    protected void btnPassLost_Click(object sender, EventArgs e)
    {
        Response.Redirect("sendpassword.aspx");
    }
}
