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

public partial class publish_FTer : System.Web.UI.UserControl
{
    private footter footter = null;
    private DataTable dt = null;
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
    protected void Page_Load(object sender, EventArgs e)
    {
        footter = new footter();
        string[] sUrlSetEditor = { UrlSetEditor }; // Set Editor
        if (!this.IsPostBack)
        {
            Globals.CKEditor(txtContent);
            dt = footter.Searchinh(2);
            rptNews.DataSource = dt;
            rptNews.DataBind();
            if (dt != null)
            {
                ltlTotal1.Text = string.Format(Language.GetTextByID(205), dt.Rows.Count);
            }

            imgIconHeader.ImageUrl = Globals.UrlImages + "icon_menu1.gif";
            ltlHeader.Text = Language.GetTextByID(343);
            tbxTitle.Attributes.Add("style", "width:300px");
            ltlContent.Text = Language.GetTextByID(35);
            ltlTitle.Text = Language.GetTextByID(29);
            ltlPublishDate.Text = Language.GetTextByID(156);
            ltlRequireTitle.Text = Language.GetTextByID(30);
            ltlRequireContent.Text = Language.GetTextByID(36);
            // dua du lieu len tren rptnew; 
            lbTaoMoi.Text = "<B>" + Language.GetTextByID(344) + "</B>";
            ltlDanhSach.Text = "<B>" + Language.GetTextByID(345) + "</B>";
        }
    }
    protected void rptNews_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            LinkButton lbtOnline = (LinkButton)e.Item.FindControl("lbtOnline");
            lbtOnline.CausesValidation = false;
            lbtOnline.Click += new System.EventHandler(lbtOnline_Click);
        }
    }
    protected void rptNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            Literal ltlIndexHeader = (Literal)e.Item.FindControl("ltlIndexHeader");
            ltlIndexHeader.Text = Language.GetTextByID(40);
            Literal ltlTitleHeader = (Literal)e.Item.FindControl("ltlTitleHeader");
            ltlTitleHeader.Text = Language.GetTextByID(29); //
            Literal ltlEditHeader = (Literal)e.Item.FindControl("ltlEditHeader");
            ltlEditHeader.Text = Language.GetTextByID(42);
            Literal ltlStatusHeader = (Literal)e.Item.FindControl("ltlStatusHeader");
            ltlStatusHeader.Text = Language.GetTextByID(39);
        }
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            HtmlTableRow trItems = (HtmlTableRow)e.Item.FindControl("trItems");
            Literal ltlID = (Literal)e.Item.FindControl("ltlID");
            HyperLink hlName = (HyperLink)e.Item.FindControl("hlName");
            LinkButton lbtOnline = (LinkButton)e.Item.FindControl("lbtOnline");
            HtmlAnchor hrEdit = (HtmlAnchor)e.Item.FindControl("hrEdit");
            Literal ltlIDNews = (Literal)e.Item.FindControl("ltlIDNews");
            CheckBox checkboxID = (CheckBox)e.Item.FindControl("checkboxID");

            DataRowView dr = (DataRowView)e.Item.DataItem;
            UssUrl sUrl = null;
            if (dr != null)
            {
                if (e.Item.ItemIndex % 2 == 1)
                    trItems.Attributes.Add("class", "alter");
                else
                    trItems.Attributes.Add("class", "item");

                ltlID.Text = (e.Item.ItemIndex + 1).ToString();
                ltlIDNews.Text = dr["id"].ToString();
                hlName.Text = dr["title"].ToString();
                sUrl = new UssUrl(Globals.URLCurrent + "Detailfooter.aspx");
                sUrl.SetParam("newsid", dr["id"].ToString());
                hlName.Attributes.Add("style", "cursor:hand; cursor:pointer;");
                hlName.Attributes.Add("onclick", "javascript:ShowPopup('" + sUrl.Url + "','700','400');");


                lbtOnline.Text = (Convert.ToInt32(dr["status"]) == 1) ? "<font color='#0000FF'>" + Language.GetTextByID(46) + "</font>" : "<font color='#FF0000'>" + Language.GetTextByID(47) + "</font>";
                lbtOnline.CommandArgument = dr["id"].ToString();

                sUrl = new UssUrl(Globals.URLCurrent + "footerUpdate.aspx");
                sUrl.SetParam("id", dr["id"].ToString());
                hrEdit.InnerText = Language.GetTextByID(42);
                hrEdit.HRef = sUrl.Url;
            }
        }
    }
    private void lbtOnline_Click(object sender, System.EventArgs e)
    {
        LinkButton lbtOnline = (LinkButton)sender;
        if (lbtOnline.CommandArgument != String.Empty)
        {
            string id = lbtOnline.CommandArgument.ToString();
            footter = new footter();
            footter.SetStatus(id);
            Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
        }
    }
    protected void BtDelete_Click(object sender, EventArgs e)
    {
        footter = new footter();
        string id = string.Empty;
        foreach (RepeaterItem items in rptNews.Items)
        {
            CheckBox checkboxID = (CheckBox)items.FindControl("checkboxID");
            if (checkboxID.Checked)
            {
                Literal ltlIDNews = (Literal)items.FindControl("ltlIDNews");
                id = ltlIDNews.Text.Trim();
                footter.Delete(id);
            }
        }
        Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        if (tbxTitle.Text == "")
        {
            ltlRequireTitle.Visible = true;
            return;
        }
        else
        {
            ltlRequireTitle.Visible = false;
        }
        if (txtContent.Text == "")
        {
            ltlRequireContent.Visible = true;
            return;
        }
        else
        {
            ltlRequireContent.Visible = false;
        }
        string Title = tbxTitle.Text.ToString();
        string created = rdpPublishDate.SelectedDate.ToString("yyyy-MM-dd HH:mm:ss");
        footter = new footter();
        footter.Inser(Title, txtContent.Text.Trim(), created);
        Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
    }
}
