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

public partial class profile_profile : System.Web.UI.UserControl
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
            ltlHeader.Text = Language.GetTextByID(25);
            ltlNote.Text = Language.GetTextByID(28);

            ltlUsername.Text = "(*) " + Language.GetTextByID(57);
            tbxUsername.Attributes.Add("style", "width:250px");
            ltlRequireUsername.Text = Language.GetTextByID(58);

            ltlFullname.Text = "(*) " + Language.GetTextByID(61);
            tbxFullname.Attributes.Add("style", "width:250px");
            ltlRequireFullname.Text = Language.GetTextByID(62);

            ltlEmail.Text = "(*) " + Language.GetTextByID(63);
            tbxEmail.Attributes.Add("style", "width:250px");
            ltlRequireEmail.Text = Language.GetTextByID(64);

            ltlTel.Text = Language.GetTextByID(65);
            tbxTel.Attributes.Add("style", "width:250px");

            ltlAddress.Text = Language.GetTextByID(66);
            tbxAddress.Attributes.Add("style", "width:250px");

            ltlJobtitle.Text = Language.GetTextByID(67);
            tbxJobtitle.Attributes.Add("style", "width:250px");
            Member member = new Member();
            DataRow dr = member.GetInfoName(Membertask.Name);
            if (dr != null)
            {
                tbxUsername.Text = dr["username"].ToString();
                tbxFullname.Text = dr["fullname"].ToString();
                tbxEmail.Text = dr["email"].ToString();
                tbxTel.Text = dr["tel"].ToString();
                tbxAddress.Text = dr["address"].ToString();
                tbxJobtitle.Text = dr["jobtitle"].ToString();
            }
        }

    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        int i = 0;
        string sUsername = tbxUsername.Text.Trim();
        if (sUsername == "")
        {
            i++;
            ltlRequireUsername.Visible = true;
        }
        else
        {
            ltlRequireUsername.Visible = false;
        }
        string sFullname = tbxFullname.Text.Trim();
        if (sFullname == "")
        {
            i++;
            ltlRequireFullname.Visible = true;
        }
        else
        {
            ltlRequireFullname.Visible = false;
        }
        string sEmail = tbxEmail.Text.Trim();
        if (sEmail == "")
        {
            i++;
            ltlRequireEmail.Visible = true;
        }
        else
        {
            ltlRequireEmail.Visible = false;
        }
        if (i > 0)
            return;
        Member member = new Member();
        member.UpdateProfile(tbxUsername.Text.Trim(), sFullname, tbxTel.Text.Trim(), sEmail, tbxAddress.Text.Trim(), tbxJobtitle.Text.Trim());
        ltlNote.Text = Language.GetTextByID(80);

    }
}
