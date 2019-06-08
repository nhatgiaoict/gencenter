using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using uss.utils;
using System.Web.UI.HtmlControls;
using System.Data;

public partial class Partner_uc_managerpartner : System.Web.UI.UserControl
{
    private static UssUrl sUrl { get { return new UssUrl(); } }
    private static bool KT = true;
    private clsPartner spartner = null;
    private static string IdUpdate = null;
    private string status
    {
        get
        {
            string status = sUrl.GetParam("Status");
            if (status == "" || status == string.Empty || status == null)
                status = "2";
            return status;
        }
    }
    private string keyword
    {
        get { return Server.UrlDecode(sUrl.GetParam("Keyword")); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        spartner = new clsPartner();
        if (!this.IsPostBack)
        {
            //

            //Button1.Text = Language.GetTextByID(350);
            //btlOk.Text = Language.GetTextByID(37);

            ddlStatus.Attributes.Add("style", "border:1 solid #ff0000; font-family:Verdana, Arial, Helvetica, sans-serif; font-size:11px; width:100px");
            ddlStatus.DataSource = Language.Status_duyetok();
            ddlStatus.DataValueField = "id";
            ddlStatus.DataTextField = Globals.CurrentLang;
            ddlStatus.DataBind();
            ddlStatus.SelectedValue = status;

            this.txtKeyword.Text = keyword;
            UssUrl sUrl1 = new UssUrl(Request.CurrentExecutionFilePath);
            sUrl1.SetParam("Keyword", Server.UrlEncode(txtKeyword.Text.ToString()));
            sUrl1.SetParam("Status", ddlStatus.SelectedValue.ToString());
            string PageQuery = "page";
            string strcPage = Request.QueryString[PageQuery];
            if (strcPage == null)
                strcPage = "1";
            int cPage = Convert.ToInt32(strcPage);
            int TotalRecords, TotalPages;

            rptGroup.DataSource = spartner.Searching(keyword, Convert.ToInt32(status), cPage, 20, out TotalRecords, out TotalPages);
            rptGroup.DataBind();
            SetControls(false);

            BtSTT.Text = Language.GetTextByID(48);
        }
    }
    protected void imgBtn_Click(object sender, ImageClickEventArgs e)
    {
        UssUrl sUrl = new UssUrl(Request.CurrentExecutionFilePath);
        sUrl.SetParam("Keyword", Server.UrlEncode(txtKeyword.Text.ToString()));
        sUrl.SetParam("Status", ddlStatus.SelectedValue.ToString());
        Response.Redirect(sUrl.Url);
    }
    protected void BtSTT_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem rptItem in rptGroup.Items)
        {
            Literal ltlIDChuyenmuc = (Literal)rptItem.FindControl("ltlIDChuyenmuc");
            TextBox ltlID_Index = (TextBox)rptItem.FindControl("ltlID_Index");
            string id = ltlIDChuyenmuc.Text.ToString();
            spartner.UpdateIndex(id, Convert.ToInt32(ltlID_Index.Text.ToString()));
        }
        Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);

    }
    protected void BtDelete_Click(object sender, EventArgs e)
    {
        // Delete
        string id = string.Empty;
        foreach (RepeaterItem items in rptGroup.Items)
        {
            CheckBox checkboxID = (CheckBox)items.FindControl("checkboxID");
            if (checkboxID.Checked)
            {
                Literal ltlIDChuyenmuc = (Literal)items.FindControl("ltlIDChuyenmuc");
                id = ltlIDChuyenmuc.Text.Trim();
                spartner.Delete(id);
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
        }
    }

   protected  void lbtStatus_Click(object sender, EventArgs e)
    {
        LinkButton lbtStatus = (LinkButton)sender;
        if (lbtStatus.CommandArgument != null)
        {
            string id = lbtStatus.CommandArgument.ToString();
            spartner.SetStatus(id);
            Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
        }
    }

    protected void lblEdit_Click(object sender, EventArgs e)
    {
        // Sửa
        KT = false;
        SetControls(true);
        LinkButton lblEdit = (LinkButton)sender;
        if (lblEdit.CommandArgument != null)
        {
            string id = lblEdit.CommandArgument.ToString();
            DataRow dr = spartner.GetInfo(id);
            if (dr != null)
            {
                txtTenNhom.Text = dr["title"].ToString().Trim();
                txtMoto.Text = dr["summary"].ToString().Trim();
                txtUrrl.Text = dr["url"].ToString().Trim();
                IdUpdate = dr["id"].ToString().Trim();
                txtFileImages.Value = dr["fimage"].ToString().Trim();
            }
        }
    }
    protected void rptGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            LinkButton lbtStatus = (LinkButton)e.Item.FindControl("lbtStatus");
            LinkButton lblEdit = (LinkButton)e.Item.FindControl("lblEdit");
            Literal ltlID = (Literal)e.Item.FindControl("ltlID");
            TextBox ltlID_Index = (TextBox)e.Item.FindControl("ltlID_Index");
            Literal ltlIDChuyenmuc = (Literal)e.Item.FindControl("ltlIDChuyenmuc");
            DataRowView dr = (DataRowView)e.Item.DataItem;
            if (dr != null)
            {
                string sid = dr["id"].ToString();
                ltlID.Text = sid;
                ltlIDChuyenmuc.Text = sid;

                lblEdit.Text = Language.GetTextByID(42);
                lblEdit.CommandArgument = dr["id"].ToString();

                ltlID_Index.Text = (e.Item.ItemIndex + 1).ToString();
                //status
                lbtStatus.Text = (Convert.ToInt32(dr["status"]) == 1) ? "<font color='#0000FF'>" + Language.GetTextByID(46) + "</font>" : "<font color='#FF0000'>" + Language.GetTextByID(47) + "</font>";
                lbtStatus.CommandArgument = dr["id"].ToString();
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        KT = true;
        txtTenNhom.Text = "";
        txtMoto.Text = "";
        txtUrrl.Text = "";
        SetControls(true);
    }
    protected void btlOk_Click(object sender, EventArgs e)
    {
        if (txtTenNhom.Text.Trim() == string.Empty || txtTenNhom.Text.Trim() == "")
        {
            ltlRequireTitle.Visible = true;
            return;
        }
        if (KT)
        {
            spartner.Insert(txtTenNhom.Text.Trim(), txtMoto.Text.Trim(), txtFileImages.Value.Trim(), txtUrrl.Text.Trim());
        }
        else
        {
            spartner.Update(IdUpdate, txtTenNhom.Text.Trim(), txtFileImages.Value.Trim(), txtMoto.Text.Trim(), txtUrrl.Text.Trim());
        }
        Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
    }
    private void SetControls(bool status)
    {
        txtMoto.Enabled = status;
        txtTenNhom.Enabled = status;
        txtUrrl.Enabled = status;
        txtFileImages.Visible = status;
    }
}
