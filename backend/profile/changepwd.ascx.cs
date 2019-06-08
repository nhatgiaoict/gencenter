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

public partial class profile_changepwd : System.Web.UI.UserControl
{
    public string UrlImages
    {
        get
        {
            return Globals.UrlImages;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            imgIconHeader.ImageUrl = Globals.UrlImages + "icon_menu1.gif"; ;
            ltlHeader.Text = Language.GetTextByID(26);
            ltlNote.Text = Language.GetTextByID(28);

            ltlPassword.Text = "(*) " + Language.GetTextByID(82);
            tbxPassword.Attributes.Add("style", "width:250px");
            ltlRequirePassword.Text = Language.GetTextByID(81);

            ltlNewPassword.Text = "(*) " + Language.GetTextByID(83);
            tbxNewPassword.Attributes.Add("style", "width:250px");
            ltlRequireNewPassword.Text = Language.GetTextByID(84);

            ltlRetypePassword.Text = "(*) " + Language.GetTextByID(87);
            tbxRetypePasword.Attributes.Add("style", "width:250px");
            ltlRequireNewPassword.Text = Language.GetTextByID(88);
        }

    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        int i = 0;
        if (tbxPassword.Text.Trim() == "")
        {
            i++;
            ltlRequirePassword.Visible = true;
        }
        else
            ltlRequirePassword.Visible = false;
        if (tbxNewPassword.Text.Trim() == "")
        {
            i++;
            ltlRequireNewPassword.Visible = true;
        }
        else
            ltlRequireNewPassword.Visible = false;
        if (tbxRetypePasword.Text.Trim() == "")
        {
            i++;
            ltlRequireRetypePassword.Visible = true;
        }
        else
            ltlRequireRetypePassword.Visible = false;

        string vPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(tbxPassword.Text.Trim(), "MD5");
        string NewPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(tbxNewPassword.Text.Trim(), "MD5");
        //string Re_Password = FormsAuthentication.HashPasswordForStoringInConfigFile(tbxRetypePasword.Text.Trim(),"MD5");

        if (tbxNewPassword.Text.Trim() != tbxRetypePasword.Text.Trim())
        {
            i++;
            ltlNote.Text = Language.GetTextByID(86);
        }
        if (i > 0)
            return;
        Member member = new Member();
        //Check at DataBase
        DataRow dr = member.Login(Membertask.Name, vPassword);
        if (dr == null)
        {
            ltlRequirePassword.Text = Language.GetTextByID(85);
            return;
        }
        //Save
        member.ChangePwd(Membertask.Name, NewPassword);
        ltlNote.Text = Language.GetTextByID(89);

    }
}
