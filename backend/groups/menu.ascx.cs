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

public partial class groups_menu : System.Web.UI.UserControl
{
    private static Groups group = new Groups();
    private Menus menus = null;
    private UssUrl sUrl = null;

    private int index
    {
        get
        {
            sUrl = new UssUrl();
            string sRet = sUrl.GetParam("index");
            if (sRet == "" || sRet == null || sRet == string.Empty)
                sRet = "1";
            return Convert.ToInt32(sRet);
        }
    }
    private string ParentID
    {
        get
        {
            sUrl = new UssUrl();
            return sUrl.GetParam("ParentID");
        }
    }
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
            ddlMenu.DataSource = Menus.GetAllMenu();
            ddlMenu.DataValueField = "id";
            ddlMenu.DataTextField = "title";
            ddlMenu.DataBind();
            ddlMenu.Attributes.Add("style", "border:1 solid #ff0000; font-family:Verdana, Arial, Helvetica, sans-serif; font-size:11px; font-weight:bold; color: #ff0000; width:250px");
            try
            {
                ddlMenu.SelectedValue = index.ToString();
            }
            catch
            {
            }
            imgIconHeader.ImageUrl = Globals.UrlImages + "icon_menu1.gif";
            ltlHeader1.Text = Language.GetTextByID(18);

            menus = new Menus(index);
            rptMenu.DataSource = menus.GetMenu();
            rptMenu.DataBind();

            Button1.Text = Language.GetTextByID(48);
            rptGroup.DataSource = menus.GetNotInMenu(ParentID);
            rptGroup.DataBind();
        }

    }
    protected void rptMenu_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            LinkButton lbtDelete = (LinkButton)e.Item.FindControl("lbtDelete");
            lbtDelete.Attributes.Add("onclick", "javascript:{ return ConfirmDelete('" + Language.GetTextByID(44) + "'); }");
            lbtDelete.CausesValidation = false;
            lbtDelete.Click += new System.EventHandler(lbtDelete_Click);
        }

    }
    protected void lbtDelete_Click(object sender, EventArgs e)
    {
        LinkButton lbtDelete = (LinkButton)sender;
        if (lbtDelete.CommandArgument != String.Empty)
        {
            string id = lbtDelete.CommandArgument.ToString();
            menus = new Menus(index);
            menus.RemoveFromMenu(id);
            Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
        }
    }
    protected void rptMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            Literal ltlIndexHeader = (Literal)e.Item.FindControl("ltlIndexHeader");
            ltlIndexHeader.Text = Language.GetTextByID(40);
            Literal ltlTitleHeader = (Literal)e.Item.FindControl("ltlTitleHeader");
            ltlTitleHeader.Text = Language.GetTextByID(29);
            Literal ltlSummaryHeader = (Literal)e.Item.FindControl("ltlSummaryHeader");
            ltlSummaryHeader.Text = Language.GetTextByID(31);
            Literal ltlDeleteHeader = (Literal)e.Item.FindControl("ltlDeleteHeader");
            ltlDeleteHeader.Text = Language.GetTextByID(43);
        }
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            HtmlTableRow trItems1 = (HtmlTableRow)e.Item.FindControl("trItems1");
            Literal ltlID = (Literal)e.Item.FindControl("ltlID");
            TextBox txtIdx = (TextBox)e.Item.FindControl("txtIdx");
            Literal ltlTitle = (Literal)e.Item.FindControl("ltlTitle");
            Literal ltlSummary = (Literal)e.Item.FindControl("ltlSummary");
            LinkButton lbtDelete = (LinkButton)e.Item.FindControl("lbtDelete");
            HtmlImage imgDelete = (HtmlImage)e.Item.FindControl("imgDelete");
            DataRowView dr = (DataRowView)e.Item.DataItem;
            if (dr != null)
            {
                if (e.Item.ItemIndex % 2 == 1)
                    trItems1.Attributes.Add("class", "alter");
                else
                    trItems1.Attributes.Add("class", "item");

                ltlID.Text = dr["id"].ToString();

                txtIdx.Attributes.Add("style", "width:30px");
                txtIdx.Text = (e.Item.ItemIndex + 1).ToString();

                ltlTitle.Text = dr["title"].ToString();
                ltlSummary.Text = dr["summary"].ToString();
                imgDelete.Src = Globals.UrlImages + "delete.gif";
                lbtDelete.CommandArgument = dr["id"].ToString();

            }
        }

    }
    protected void rptGroup_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            LinkButton lbtSelect1 = (LinkButton)e.Item.FindControl("lbtSelect1");
            lbtSelect1.Click += new System.EventHandler(lbtSelect_Click);
        }

    }
    protected void lbtSelect_Click(object sender, EventArgs e)
    {
        LinkButton lbtSelect1 = (LinkButton)sender;
        if (lbtSelect1.CommandArgument != String.Empty)
        {
            string id = lbtSelect1.CommandArgument.ToString();
            menus = new Menus(index);
            menus.AddToMenu(id);
            Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
        }
    }
    protected void rptGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            Literal ltlIdxHeader1 = (Literal)e.Item.FindControl("ltlIdxHeader1");
            ltlIdxHeader1.Text = Language.GetTextByID(40);
            Literal ltlTitleHeader1 = (Literal)e.Item.FindControl("ltlTitleHeader1");
            ltlTitleHeader1.Text = Language.GetTextByID(29);
            Literal ltlSummaryHeader1 = (Literal)e.Item.FindControl("ltlSummaryHeader1");
            ltlSummaryHeader1.Text = Language.GetTextByID(31);
            Literal ltlSelectHeader1 = (Literal)e.Item.FindControl("ltlSelectHeader1");
            ltlSelectHeader1.Text = Language.GetTextByID(122);
        }
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            HtmlTableRow trItems2 = (HtmlTableRow)e.Item.FindControl("trItems2");
            Literal ltlIndex1 = (Literal)e.Item.FindControl("ltlIndex1");
            HyperLink hlTitle1 = (HyperLink)e.Item.FindControl("hlTitle1");
            Literal ltlSummary1 = (Literal)e.Item.FindControl("ltlSummary1");
            LinkButton lbtSelect1 = (LinkButton)e.Item.FindControl("lbtSelect1");
            DataRowView dr = (DataRowView)e.Item.DataItem;
            if (dr != null)
            {
                if (e.Item.ItemIndex % 2 == 1)
                    trItems2.Attributes.Add("class", "alter");
                else
                    trItems2.Attributes.Add("class", "item");

                ltlIndex1.Text = (e.Item.ItemIndex + 1).ToString();
                hlTitle1.Text = dr["title"].ToString();
                string sid = dr["id"].ToString();
                if (group.NextChild(sid))
                {
                    sUrl = new UssUrl();
                    sUrl.SetParam("ParentID", sid);
                    hlTitle1.NavigateUrl = sUrl.Url;
                }
                ltlSummary1.Text = dr["summary"].ToString();
                lbtSelect1.Text = Language.GetTextByID(122);
                lbtSelect1.CommandArgument = dr["id"].ToString();
            }
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        menus = new Menus(index);
        foreach (RepeaterItem rptItem in rptMenu.Items)
        {
            Literal ltlID = (Literal)rptItem.FindControl("ltlID");
            TextBox txtIdx = (TextBox)rptItem.FindControl("txtIdx");
            string id = ltlID.Text.ToString();
            menus.UpdateIndexMenu(id, Convert.ToInt32(txtIdx.Text.ToString()));
        }
        Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);

    }
    protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        sUrl = new UssUrl();
        sUrl.SetParam("index", ddlMenu.SelectedValue.ToString());
        Response.Redirect(sUrl.Url, true);
    }
}
