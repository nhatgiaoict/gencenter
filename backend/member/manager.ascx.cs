using System;
using System.IO;
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

public partial class member_manager : System.Web.UI.UserControl
{
    private UssUrl sUrl = null;
    private static string _Lang
    {
        get { return Globals.CurrentLang; }
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
            imgIconHeader.ImageUrl = Globals.UrlImages + "icon_menu1.gif"; ;
            
            ltlHeader.Text = Language.GetTextByID(3);
            int TotalRecords, TotalPages;
            Member member = new Member();
            // status: tao moi:0; ok:1
            rptMember.DataSource = member.Searching(2, 1, 15, out TotalRecords, out TotalPages);
            rptMember.DataBind();

            BtDelete.Text = Language.GetTextByID(43);
        }
    }
    protected void rptMember_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            Literal ltlIndexHeader = (Literal)e.Item.FindControl("ltlIndexHeader");
            ltlIndexHeader.Text = Language.GetTextByID(40);
            Literal ltlTitleHeader = (Literal)e.Item.FindControl("ltlTitleHeader");
            ltlTitleHeader.Text = Language.GetTextByID(57);
            Literal ltlLastUpdateHeader = (Literal)e.Item.FindControl("ltlLastUpdateHeader");
            ltlLastUpdateHeader.Text = Language.GetTextByID(69);
            Literal ltlStatusHeader = (Literal)e.Item.FindControl("ltlStatusHeader");
            ltlStatusHeader.Text = Language.GetTextByID(39);
            Literal ltlEditHeader = (Literal)e.Item.FindControl("ltlEditHeader");
            ltlEditHeader.Text = Language.GetTextByID(42);
            Literal ltlDeleteHeader = (Literal)e.Item.FindControl("ltlDeleteHeader");
            ltlDeleteHeader.Text = Language.GetTextByID(43);
        }
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            HtmlTableRow trItems = (HtmlTableRow)e.Item.FindControl("trItems");
            Literal ltlID = (Literal)e.Item.FindControl("ltlID");
            Literal ltlID_Index = (Literal)e.Item.FindControl("ltlID_Index");
            Literal ltlTitle = (Literal)e.Item.FindControl("ltlTitle");
            Literal ltlCreated = (Literal)e.Item.FindControl("ltlCreated");
            LinkButton lbtOnline = (LinkButton)e.Item.FindControl("lbtOnline");
            HtmlAnchor hrEdit = (HtmlAnchor)e.Item.FindControl("hrEdit");
            
            Literal ltlIDMember = (Literal)e.Item.FindControl("ltlIDMember");
            CheckBox checkboxID = (CheckBox)e.Item.FindControl("checkboxID");
				

            DataRowView dr = (DataRowView)e.Item.DataItem;
            if (dr != null)
            {
                if (e.Item.ItemIndex % 2 == 1)
                    trItems.Attributes.Add("class", "alter");
                else
                    trItems.Attributes.Add("class", "item");
                ltlID.Text = dr["id"].ToString();
                ltlID_Index.Text = (e.Item.ItemIndex + 1).ToString();	//STT
                ltlIDMember.Text = dr["id"].ToString();

                ltlTitle.Text = dr["fullname"].ToString() + " (" + dr["username"].ToString() + ")";

                if (Membertask.IsAdministrator(dr["username"].ToString()))
                {
                    if (Convert.ToInt32(dr["IsAdmin"].ToString()) != 1)
                    {
                        ltlCreated.Text = Language.GetTextByID(68);
                    }
                    else
                    {
                        ltlCreated.Text = Language.GetTextByID(199);
                    }
                }
                else
                    ltlCreated.Text = Language.GetTextByID(2);

                lbtOnline.Text = (Convert.ToInt32(dr["status"]) == 1) ? "<font color='#0000FF'>" + Language.GetTextByID(73) + "</font>" : "<font color='#FF0000'>" + Language.GetTextByID(74) + "</font>";
                lbtOnline.CommandArgument = dr["id"].ToString();
                lbtOnline.Visible = false;

                sUrl = new UssUrl(Globals.URLCurrent + "update.aspx");
                sUrl.SetParam("id", dr["id"].ToString());                

                hrEdit.InnerText = Language.GetTextByID(42);
                hrEdit.HRef = sUrl.Url;
                hrEdit.Visible = false;

                checkboxID.Visible = false;
                if (Convert.ToInt32(dr["IsAdmin"].ToString()) != 1)
                {
                    hrEdit.Visible = true;
                    lbtOnline.Visible = true;
                    checkboxID.Visible = true;
                }
            }
        }

    }
    protected void rptMember_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            LinkButton lbtOnline = (LinkButton)e.Item.FindControl("lbtOnline");
            lbtOnline.CausesValidation = false;
            lbtOnline.Click += new System.EventHandler(lbtOnline_Click);
        }

    }
    protected void lbtOnline_Click(object sender, EventArgs e)
    {
        LinkButton lbtOnline = (LinkButton)sender;
        if (lbtOnline.CommandArgument != String.Empty)
        {
            string id = lbtOnline.CommandArgument.ToString();
            Member member = new Member();
            member.SetStatus(id);
            Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
        }
    }

    protected void BtDelete_Click(object sender, EventArgs e)
    {
        
        
        Member member = new Member();
        string id = string.Empty;
        foreach (RepeaterItem items in rptMember.Items)
        {
            CheckBox checkboxID = (CheckBox)items.FindControl("checkboxID");
            if (checkboxID.Checked)
            {
                Literal ltlIDMember = (Literal)items.FindControl("ltlIDMember");
                id = ltlIDMember.Text.Trim();
                member.Delete(id);
            }
        }
        try
        {
            Response.Redirect(Globals.UrlRoot + "temp.aspx?url="+Request.Url);
        }
        catch (Exception ae)
        {
            string ad = ae.ToString();
        }
    }
    
}