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
using uss.utils;

public partial class advert_update_con : System.Web.UI.UserControl
{
    private UssUrl sUrl = null;
    public string UrlImages
    {
        get
        {
            return Globals.UrlImages;
        }
    }
    private string id
    {
        get
        {
            sUrl = new UssUrl();
            return sUrl.GetParam("id");
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
            if (id == "" || id == null || id == string.Empty)
                Response.Redirect(Globals.URLCurrent + "manager.aspx", true);
            Advert adv = new Advert();
            DataRow dr = adv.GetInfo(id);
            if (dr == null) Response.Redirect(Globals.URLCurrent + "manager.aspx", true);

            imgIconHeader.ImageUrl = Globals.UrlImages + "icon_menu1.gif";
            ltlHeader.Text = Language.GetTextByID(137);
            ltlNote.Text = Language.GetTextByID(28);
            ltlTitle.Text = "* " + Language.GetTextByID(133);
            ltlRequiredTitle.Text = Language.GetTextByID(134);
            tbxTitle.Attributes.Add("style", "width:200px");
            ltlLink.Text = "* " + Language.GetTextByID(125);
            ltlSixe.Text = Language.GetTextByID(257);
            ltlRequiredLink.Text = Language.GetTextByID(135);
            tbxLink.Attributes.Add("style", "width:200px");
            tbxLink.Text = dr["linkurl"].ToString();
            ltlSummary.Text = Language.GetTextByID(31);
            tbxSummary.Attributes.Add("style", "width:150px; height:100px");
            tbxTitle.Text = txttitlehtml.Text = dr["title"].ToString();
            txtContenthtml.Text = dr["filename"].ToString();
            tbxSummary.Text = dr["summary"].ToString();
            txtFileImages.Text = dr["filename"].ToString();
            txtWidth.Text = dr["fwidth"].ToString().Trim();
            txtHeight.Text = dr["fheight"].ToString();
            string sfimage = dr["filename"].ToString();
            string SFile = string.Empty;
            if (sfimage.Length > 0)
            {
                SFile = sfimage.Substring(sfimage.Length - 4);
                string sPath = Globals.UrlRoot + dr["filename"].ToString();
                if (SFile == ".swf")
                {
                    ltlLogo.Text = string.Format("<script language='javascript'>\n<!--\n embed_flash('{0}', '185', '','left');\n-->\n</script>", sPath);
                }
                else
                {
                    ltlLogo.Text = string.Format("<img src='{0}' border='0' width='185' >", sPath);
                }
            }
            else
            {
                ltlLogo.Text = string.Empty;
            }
            int kind = Convert.ToInt32(dr["kind"].ToString());
            if (kind == 1)
            {
                tblAnh.Visible = false;
                tblHtml.Visible = true;
                rdHtml.Checked = true;
                RdAnh.Checked = false;
            }
            else
            {
                tblHtml.Visible = false;
                tblAnh.Visible = true;
                rdHtml.Checked = false;
                RdAnh.Checked = true;
            }
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
            if (title == string.Empty || title == null)
            {
                ltlReten.Visible = true; return;
            }
            if (NoidungHtml == string.Empty || NoidungHtml == null)
            {
                ltlReNoidunghtm.Visible = true; return;
            }
            adv.UpdateHtml(id, title, NoidungHtml);
        }
        else
        {
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
            string sFilename = txtFileImages.Text.Trim();
            if (sFilename == string.Empty || sFilename == "" || sFilename == null)
            {
                sFilename = txtFileImages.Text.Trim();
            }
            adv.Update(id, sTitle, sLink, sSummary, sFilename, txtWidth.Text.Trim(), txtHeight.Text.Trim());
        }
        Response.Redirect(Globals.URLCurrent + "manager.aspx", true);
    }
    protected void RdAnh_CheckedChanged(object sender, EventArgs e)
    {
        rdHtml.Checked = false;
        tblHtml.Visible = false;
        tblAnh.Visible = true;
    }
    protected void rdHtml_CheckedChanged(object sender, EventArgs e)
    {
        RdAnh.Checked = false;
        tblHtml.Visible = true;
        tblAnh.Visible = false;
    }
}
