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

public partial class controls_left : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            
            Member mem = new Member();
            DataRow dr = mem.GetInfoName(Membertask.Name);
            ltlWelcome.Text = Language.GetTextByID(109) + ": " + dr["fullname"].ToString();

            //ddlLang.DataSource = Language.GetAllLanguage();
            //ddlLang.DataValueField = "id";
            //ddlLang.DataTextField = "title";
            //ddlLang.DataBind();
            //ddlLang.Attributes.Add("style", "border:1 solid #ff0000; font-family:Verdana, Arial, Helvetica, sans-serif; font-size:11px; font-weight:bold; color: #ff0000; width:130px");
            //try
            //{
            //    ddlLang.SelectedValue = Globals.CurrentLang;
            //}
            //catch
            //{
            //}
            // ltlLanguage.Text = Language.GetTextByID(1);
            ltlProfile.Text = Language.GetTextByID(24);

            hlProfile.Text = Language.GetTextByID(25);
            hlProfile.NavigateUrl = Globals.UrlRoot + "profile/profile.aspx";

            hlChangePwd.Text = Language.GetTextByID(26);
            hlChangePwd.NavigateUrl = Globals.UrlRoot + "profile/changepwd.aspx";

            hlSignout.Text = Language.GetTextByID(27);
            hlSignout.NavigateUrl = Globals.UrlRoot + "profile/signout.aspx";

            //=========== Thanh vien ===========
            if (!Membertask.IsAdministrator() && Membertask.IsThanhvien() == string.Empty)
            {
                tblMember.Visible = false;
            }
            else
            {
                tblMember.Visible = true;
                ltlMember.Text = Language.GetTextByID(2);
                hlRegMember.Text = Language.GetTextByID(4);
                hlRegMember.NavigateUrl = Globals.UrlRoot + "member/register.aspx";
                hlMemberManager.Text = Language.GetTextByID(3);
                hlMemberManager.NavigateUrl = Globals.UrlRoot + "member/manager.aspx";

            }
            //=========== Chuyen muc ===========
            if (!Membertask.IsAdministrator() && Membertask.IsChuyenmuc() == string.Empty)
            {
                tblGroups.Visible = false;
            }
            else
            {
                tblGroups.Visible = true;
                ltlGroup.Text = Language.GetTextByID(91);
                //tin
                hlRegGroup.Text = Language.GetTextByID(92);
                hlRegGroup.NavigateUrl = Globals.UrlRoot + "groups/register.aspx";
                hlGroupManager.Text = Language.GetTextByID(93);
                hlGroupManager.NavigateUrl = Globals.UrlRoot + "groups/manager.aspx";
                hlMenus.Text = Language.GetTextByID(14);
                hlMenus.NavigateUrl = Globals.UrlRoot + "groups/menu.aspx";
            }
            //=========== Tao bai viet ===========
            if (!Membertask.IsAdministrator() && Membertask.IsNewsTask() == string.Empty)
            {
                tblCreate_new.Visible = false;
            }
            else
            {
                tblCreate_new.Visible = true;
                ltlCreate_content.Text = "Quản trị nội dung"; //Language.GetTextByID(158);
                hlCreate.Text = Language.GetTextByID(159);
                hlCreate.NavigateUrl = Globals.UrlRoot + "news/register.aspx";

                hlList.Text = Language.GetTextByID(160);
                hlList.NavigateUrl = Globals.UrlRoot + "news/listbaiviet.aspx";
            }
            //=========== To Chuc ============
            if (!Membertask.IsAdministrator() && Membertask.IsTochuc() == string.Empty)
            {
                tblOrgNews.Visible = false;
            }
            else
            {
                tblOrgNews.Visible = true;
                ltlOrgNews.Text = Language.GetTextByID(11);
                hrBanner.Text = Language.GetTextByID(342);
                hrBanner.NavigateUrl = Globals.UrlRoot + "Slideshow/Slideshow.aspx";
                hrfooter.Text = Language.GetTextByID(343);
                hrfooter.NavigateUrl = Globals.UrlRoot + "publish/footer.aspx";
            }
            //========Quan tri Web ===========
            if (!Membertask.IsAdministrator())
            {
                tblAdministrator.Visible = false;
            }
            else
            {
                tblAdministrator.Visible = true;
                ltlAdministrator.Text = Language.GetTextByID(312);

                hlInforWeb.Text = Language.GetTextByID(314);
                hlInforWeb.NavigateUrl = Globals.UrlRoot + "webmanager/infor_manager.aspx";
            }
        }
    }
    //protected void ddlLang_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    Globals.CurrentLang = ddlLang.SelectedValue.ToString();
    //    Response.Redirect(Request.RawUrl);
    //    Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
    //}
}