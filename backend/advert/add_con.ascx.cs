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

public partial class advert_add_con : System.Web.UI.UserControl
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
            txtFileImages.Attributes.Add("style", "Width:250px");
            imgSelect.Attributes.Add("style", "cursor:hand; cursor:pointer");
            imgSelect.Src = Globals.UrlImages + "folder.gif";
            imgSelect.Attributes.Add("onclick", "SelectFile('" + txtFileImages.ClientID + "')");

            imgIconHeader.ImageUrl = Globals.UrlImages + "icon_menu1.gif"; ;
            ltlHeader.Text = Language.GetTextByID(130);
            ltlNote.Text = Language.GetTextByID(28);

            ltlTitle.Text = "* " + Language.GetTextByID(133);
            ltlRequiredTitle.Text = Language.GetTextByID(134);
            ltlLink.Text = "* " + Language.GetTextByID(125);
            ltlRequiredLink.Text = Language.GetTextByID(135);
            tbxLink.Text = "http://";
            ltlSummary.Text = Language.GetTextByID(31);
            tbxSummary.Attributes.Add("style", "width:200px; height:100px");
            ltlSize.Text = Language.GetTextByID(257);
            btnAddnew.Text = Language.GetTextByID(37);

        }

    }
    protected void btnAddnew_Click(object sender, EventArgs e)
    {
        Advert adv = new Advert();
        if (rdHtml.Checked == true)
        {
            string title = txttitlehtml.Text.Trim();
            string NoidungHtml = txtContenthtml.Text.Trim();
            if (title == string.Empty || title == null) { ltlReten.Visible = true; return; } else { ltlReten.Visible = false; }
            if (NoidungHtml == string.Empty || NoidungHtml == null) { ltlReNoidunghtm.Visible = true; return; } else { ltlReNoidunghtm.Visible = false; }

            adv.InsertHTML(title, NoidungHtml.ToString());
        }
        else
        {
            // Quảng cáo anh bt
            string sTitle = tbxTitle.Text.Trim();
            if (sTitle == "")
            {
                ltlRequiredTitle.Visible = true;
                return;
            }
            else
            {
                ltlRequiredTitle.Visible = false;
            }
            string sLink = tbxLink.Text.Trim();
            string sSummary = tbxSummary.Text.Trim();

            string sFilename = txtFileImages.Text;
            if (sFilename == string.Empty || sFilename == "" || sFilename == null)
            {
                ltlRequiredLogo.Visible = true;
                return;
            }
            adv.Insert(sTitle, sLink, sSummary, sFilename.Trim(), txtWidth.Text.Trim(), txtHeight.Text.Trim());
        }
        Response.Redirect(Globals.URLCurrent + "manager.aspx", true);
    }

    protected void rdHtml_CheckedChanged(object sender, EventArgs e)
    {
        RdAnh.Checked = false;
        tblAnh.Visible = false;
        tblFlash.Visible = true;
    }
    protected void RdAnh_CheckedChanged(object sender, EventArgs e)
    {
        rdHtml.Checked = false;
        tblFlash.Visible = false;
        tblAnh.Visible = true;
    }
}