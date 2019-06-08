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

public partial class keyword_keyword_con : System.Web.UI.UserControl
{
    private Keyword sKeyword = null;
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
        sKeyword = new Keyword();
        if (!this.IsPostBack)
        {
            imgIconHeader.ImageUrl = Globals.UrlImages + "icon_menu1.gif";
            DataTable dt = sKeyword.GetAllKeyWord();
            if (dt != null)
            {
                rptGroup.DataSource = dt;
                rptGroup.DataBind();
            }
            SetControls(false);
        }
    }
    private void SetControls(bool status)
    {
        txtTenKeyWord.Enabled = status;
        txtlink.Enabled = status;
    }
    protected void BtSTT_Click(object sender, EventArgs e)
    {
        sKeyword = new Keyword();
        foreach (RepeaterItem rptItem in rptGroup.Items)
        {
            Literal ltlID = (Literal)rptItem.FindControl("ltlID");
            TextBox ltlID_Index = (TextBox)rptItem.FindControl("ltlID_Index");
            string id = ltlID.Text.ToString().Trim();
            sKeyword.UpdateIndexKeyWord(id, Convert.ToInt32(ltlID_Index.Text.ToString()));
        }
        Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
    }

    protected void BtDelete_Click(object sender, EventArgs e)
    {
        sKeyword = new Keyword();
        string id = string.Empty;
        foreach (RepeaterItem items in rptGroup.Items)
        {
            CheckBox checkboxID = (CheckBox)items.FindControl("checkboxID");
            if (checkboxID.Checked)
            {
                Literal ltlIDChuyenmuc = (Literal)items.FindControl("ltlIDChuyenmuc");
                id = ltlIDChuyenmuc.Text.Trim();
                sKeyword.DeleteKeyWord(id);
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

            LinkButton lbtOnline = (LinkButton)e.Item.FindControl("lbtOnline");
            lbtOnline.CausesValidation = false;
            lbtOnline.Click += new EventHandler(lbtOnline_Click);
        }
    }

    void lbtOnline_Click(object sender, EventArgs e)
    {
        LinkButton lbtOnline = (LinkButton)sender;
        if (lbtOnline.CommandArgument != String.Empty)
        {
            string id = lbtOnline.CommandArgument.ToString();
            sKeyword = new Keyword();
            sKeyword.SetStatus(id);
            Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
        }
    }
    protected void lblEdit_Click(object sender, EventArgs e)
    {
        KT = false;
        SetControls(true);
        LinkButton lblEdit = (LinkButton)sender;
        if (lblEdit.CommandArgument != null)
        {
            // getdata;
            string id = lblEdit.CommandArgument.ToString();
            sKeyword = new Keyword();
            DataRow dr = sKeyword.GetInfoKeyWord(id);
            if (dr != null)
            {
                txtTenKeyWord.Text = dr["title"].ToString().Trim();
                txtlink.Text = dr["link"].ToString().Trim();
                IdUpdate = dr["id"].ToString().Trim();
            }
        }
        // cap nhat hay lam gio day chu
    }

    protected void rptGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            Literal ltlID = (Literal)e.Item.FindControl("ltlID");
            TextBox ltlID_Index = (TextBox)e.Item.FindControl("ltlID_Index");
            LinkButton lblEdit = (LinkButton)e.Item.FindControl("lblEdit");
            LinkButton lbtOnline = (LinkButton)e.Item.FindControl("lbtOnline");
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

                lbtOnline.Text = (Convert.ToInt32(dr["status"]) == 1) ? "<font color='#0000FF'>" + Language.GetTextByID(46) + "</font>" : "<font color='#FF0000'>" + Language.GetTextByID(47) + "</font>";
                lbtOnline.CommandArgument = dr["id"].ToString();
            }
        }
    }

    protected void btlAdd_Click(object sender, EventArgs e)
    {
        // add lai trang thai la them mới
        KT = true;
        txtTenKeyWord.Text = "";
        txtlink.Text = "";
        SetControls(true);
    }
    protected void btlOk_Click(object sender, EventArgs e)
    {
        sKeyword = new Keyword();
        int i = 0;
        string sTitle = txtTenKeyWord.Text.Trim();
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
        string sLink = txtlink.Text.Trim();
        if (KT)
        {
            sKeyword.InsertKeyWord(sTitle, sLink);
        }
        else
        {
            // update;
            sKeyword.UpDateKeyWord(IdUpdate,sTitle, sLink);
        }
        // Cap nhat lai thong tin sua hoac them mơi
        // 
        Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
    }
}
