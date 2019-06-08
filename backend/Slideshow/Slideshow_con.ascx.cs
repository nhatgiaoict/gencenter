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

public partial class Slideshow_Slideshow_con : System.Web.UI.UserControl
{
    private UssUrl sUrl = null;
    private Slide Slide = null;
    private static bool KT = true;
    private static string IdUpdate = null;
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
            Slide = new Slide();
            txtFileImages.Attributes.Add("style", "Width:250px");
            imgSelect.Attributes.Add("style", "cursor:hand; cursor:pointer");
            imgSelect.Src = Globals.UrlImages + "folder.gif";
            imgSelect.Attributes.Add("onclick", "SelectFile('" + txtFileImages.ClientID + "')");
            imgIconHeader.ImageUrl = Globals.UrlImages + "icon_menu1.gif";

            UssUrl sUrl1 = new UssUrl(Request.CurrentExecutionFilePath);
            string PageQuery = "page";
            string strcPage = Request.QueryString[PageQuery];
            if (strcPage == null)
                strcPage = "1";
            int cPage = Convert.ToInt32(strcPage);
            int TotalRecords, TotalPages;

            rptGroup.DataSource = Slide.SearchingSlide(cPage, 15, out TotalRecords, out TotalPages);
            rptGroup.DataBind();

            PageList.m_pTotalPage = TotalPages;
            PageList.m_pPageQuery = PageQuery;
            PageList.m_pCurrentPage = cPage;
            PageList.m_pPageUrl = sUrl1.Url;
            PageList.m_pIconPath = Globals.UrlImages;

            if (TotalRecords == 0)
            {
                ltlTotal.Visible = false;
            }
            else
            {
                ltlTotal.Text = "";//string.Format(Language.GetTextByID(240), TotalRecords);

            }
            SetControls(false);
            /*
            ltltitleblog.Text = Language.GetTextByID(411);
            ltlUrl.Text = Language.GetTextByID(410);
            ltlMoTa.Text = Language.GetTextByID(412);
            btlAdd.Text = Language.GetTextByID(407);
            btlOk.Text = Language.GetTextByID(408);
            ltlTenVideo.Text = Language.GetTextByID(409);
            ltlHeader.Text = Language.GetTextByID(413);
             */
        }
    }
    private void SetControls(bool status)
    {
        txtMoto.Enabled = status;
        txtTenNhom.Enabled = status;
        txtFileImages.Enabled = status;
        txtUrl.Enabled = status;
    }
    protected void BtSTT_Click(object sender, EventArgs e)
    {
        Slide = new Slide();
        foreach (RepeaterItem rptItem in rptGroup.Items)
        {
            Literal ltlID = (Literal)rptItem.FindControl("ltlID");
            TextBox ltlID_Index = (TextBox)rptItem.FindControl("ltlID_Index");
            string id = ltlID.Text.ToString().Trim();
            Slide.UpdateIndexSlide(id, Convert.ToInt32(ltlID_Index.Text.ToString()));
        }
        Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
    }

    protected void BtDelete_Click(object sender, EventArgs e)
    {
      
        Slide = new Slide();
        string id = string.Empty;
        foreach (RepeaterItem items in rptGroup.Items)
        {
            CheckBox checkboxID = (CheckBox)items.FindControl("checkboxID");
            if (checkboxID.Checked)
            {
                Literal ltlIDChuyenmuc = (Literal)items.FindControl("ltlIDChuyenmuc");
                id = ltlIDChuyenmuc.Text.Trim();
                Slide.DeleteSlide(id);
            }
        }
        try
        {
            Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
        }
        catch (Exception ae)
        {
            string ad = ae.ToString();
        }
    }

    protected void rptGroup_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            LinkButton lblEdit = (LinkButton)e.Item.FindControl("lblEdit");
            lblEdit.CausesValidation = false;
            lblEdit.Click += new EventHandler(lblEdit_Click);

            LinkButton lbtStatus = (LinkButton)e.Item.FindControl("lbtStatus");
            lbtStatus.CausesValidation = false;
            lbtStatus.Click += new EventHandler(lbtStatus_Click);

            //LinkButton lbtAnh = (LinkButton)e.Item.FindControl("lbtAnh");
            //lbtAnh.CausesValidation = false;
            //lbtAnh.Click += new EventHandler(lbtAnh_Click);
        }
    }

    protected void lbtStatus_Click(object sender, EventArgs e)
    {
        LinkButton lbtStatus = (LinkButton)sender;
        if (lbtStatus.CommandArgument != null)
        {
            string id = lbtStatus.CommandArgument.ToString();
            Slide = new Slide();
            Slide.SetStatus(id);
            Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
        }
    }

    //protected void lbtAnh_Click(object sender, EventArgs e)
    //{
    //    LinkButton lbtAnh = (LinkButton)sender;
    //    if (lbtAnh.CommandArgument != null)
    //    {
    //        string id = lbtAnh.CommandArgument.ToString();
    //        Slide = new Slide();
    //        Slide.SetAnh(id);
    //        Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
    //    }
    //}
    protected void lblEdit_Click(object sender, EventArgs e)
    {
        KT = false;
        SetControls(true);
        LinkButton lblEdit = (LinkButton)sender;
        if (lblEdit.CommandArgument != null)
        {
            // getdata;
            string id = lblEdit.CommandArgument.ToString();
            Slide = new Slide();
            DataRow dr = Slide.GetInfo(id);
            if (dr != null)
            {
                txtTenNhom.Text = dr["title"].ToString().Trim();
                txtMoto.Text = dr["summary"].ToString().Trim();
                txtFileImages.Text = dr["filename"].ToString().Trim();
                txtUrl.Text = dr["Url"].ToString().Trim();
                IdUpdate = dr["id"].ToString().Trim();

            }
        }
        // cap nhat hay lam gio day chu
    }

    protected void rptGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            Literal ltlIndexHeader = (Literal)e.Item.FindControl("ltlIndexHeader");
            ltlIndexHeader.Text = Language.GetTextByID(40);

            Literal ltlTitleHeader = (Literal)e.Item.FindControl("ltlTitleHeader");
            ltlTitleHeader.Text = Language.GetTextByID(420);

            Literal ltlNote = (Literal)e.Item.FindControl("ltlNote");
            ltlNote.Text = Language.GetTextByID(421);

            Literal ltlStatus = (Literal)e.Item.FindControl("ltlStatus");
            ltlStatus.Text = Language.GetTextByID(39);

            //Literal ltlAnh = (Literal)e.Item.FindControl("ltlAnh");
            //ltlAnh.Text = Language.GetTextByID(150);

            Literal ltlEditHeader = (Literal)e.Item.FindControl("ltlEditHeader");
            ltlEditHeader.Text = Language.GetTextByID(42);

            Literal ltlDeleteHeader = (Literal)e.Item.FindControl("ltlDeleteHeader");
            ltlDeleteHeader.Text = Language.GetTextByID(43);

        }
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            Literal ltlID = (Literal)e.Item.FindControl("ltlID");
            TextBox ltlID_Index = (TextBox)e.Item.FindControl("ltlID_Index");
            LinkButton lblEdit = (LinkButton)e.Item.FindControl("lblEdit");
            LinkButton lbtStatus = (LinkButton)e.Item.FindControl("lbtStatus");

            //LinkButton lbtAnh = (LinkButton)e.Item.FindControl("lbtAnh");

            Literal ltlIDChuyenmuc = (Literal)e.Item.FindControl("ltlIDChuyenmuc");
            CheckBox checkboxID = (CheckBox)e.Item.FindControl("checkboxID");
            HtmlAnchor hrEdit = (HtmlAnchor)e.Item.FindControl("hrEdit");
            DataRowView dr = (DataRowView)e.Item.DataItem;
            if (dr != null)
            {
                string sid = dr["id"].ToString();
                ltlID.Text = sid;
                ltlIDChuyenmuc.Text = sid;
                ltlID_Index.Attributes.Add("style", "width:30px");
                ltlID_Index.Text = (e.Item.ItemIndex + 1).ToString();	//STT
                lblEdit.Text = Language.GetTextByID(42);
                lblEdit.CommandArgument = dr["id"].ToString();

                lbtStatus.Text = (Convert.ToInt32(dr["status"]) == 1) ? "<font color='#0000FF'>" + Language.GetTextByID(46) + "</font>" : "<font color='#FF0000'>" + Language.GetTextByID(47) + "</font>";
                lbtStatus.CommandArgument = dr["id"].ToString();

                //lbtAnh.Text = (Convert.ToInt32(dr["anh"]) == 1) ? "<font color='#0000FF'>" + Language.GetTextByID(46) + "</font>" : "<font color='#FF0000'>" + Language.GetTextByID(47) + "</font>";
                //lbtAnh.CommandArgument = dr["id"].ToString();
            }
        }
    }

    protected void btlAdd_Click(object sender, EventArgs e)
    {
        // add lai trang thai la them mới
        KT = true;
        txtTenNhom.Text = "";
        txtMoto.Text = "";
        txtFileImages.Text = "";
        txtUrl.Text = "";
        SetControls(true);
    }
    protected void btlOk_Click(object sender, EventArgs e)
    {
        Slide = new Slide();
        int i = 0;
        string sTitle = txtTenNhom.Text.Trim();
        if (sTitle == "")
        {
            i++;
            ltlRequireTitle.Visible = true;
        }
        else
        {
            ltlRequireTitle.Visible = false;
        }
        if (i > 0)
            return;
        string sFilename = txtFileImages.Text.Trim();
           string Note = txtMoto.Text.Trim();

        if (KT)
        {
            Slide.InsertSlide(sTitle, sFilename, Note, txtUrl.Text.Trim());
        }
        else
        {
            // update;
            Slide.UpdateSlide(IdUpdate, sTitle, sFilename, Note, txtUrl.Text.Trim());
        }
        // Cap nhat lai thong tin sua hoac them mơi
        // 
        Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
    }
}
