using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using uss.utils;

public partial class publish_FterUpdate_con : System.Web.UI.UserControl
{
    private UssUrl sUrl = null;
    private footter footter = null;
    public string UrlImages
    {
        get
        {
            return Globals.UrlImages;
        }
    }
    public string UrlSetEditor
    {
        get
        {
            return Globals.UrlSetEditor;

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
        if (id == "" || id == null || id == string.Empty)
            Response.Redirect(Globals.URLCurrent + "footer.aspx");
        footter = new footter();
        string[] sUrlSetEditor = { UrlSetEditor };
        if (!this.IsPostBack)
        {
            // Set Editor
            Globals.CKEditor(txtContent12);

            imgIconHeader.ImageUrl = Globals.UrlImages + "icon_menu1.gif";
            ltlHeader.Text = Language.GetTextByID(343);
            tbxTitle.Attributes.Add("style", "width:300px");
            ltlContent.Text = Language.GetTextByID(35);
            ltlHeader.Text = Language.GetTextByID(29);
            ltlPublishDate.Text = Language.GetTextByID(156);
            ltlRequireTitle.Text = Language.GetTextByID(30);
            ltlRequireContent.Text = Language.GetTextByID(36);

            DataRow dr = footter.GetInfo(id);
            if (dr == null)
                Response.Redirect(Globals.URLCurrent + "footer.aspx");
            tbxTitle.Text = dr["title"].ToString();
            txtContent12.Text = dr["footertext"].ToString();
        }
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        footter = new footter();
        if (tbxTitle.Text == "")
        {
            ltlRequireTitle.Visible = true;
            return;
        }
        else
        {
            ltlRequireTitle.Visible = false;
        }
        if (txtContent12.Text == "")
        {
            ltlRequireContent.Visible = true;
            return;
        }
        else
        {
            ltlRequireContent.Visible = false;
        }
        footter.UpDate(id, tbxTitle.Text.Trim(), txtContent12.Text.Trim());
        Response.Redirect(Globals.URLCurrent + "footer.aspx");
    }
}
