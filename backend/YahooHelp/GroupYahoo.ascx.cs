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

public partial class YahooHelp_GroupYahoo : System.Web.UI.UserControl
{
    private YhaooHelp syahoohelp = null;
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
        syahoohelp = new YhaooHelp();
        if (!this.IsPostBack)
        {
            imgIconHeader.ImageUrl = Globals.UrlImages + "icon_menu1.gif";
            DataTable dt = syahoohelp.GetgroupYahoo();
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
        txtMoto.Enabled = status;
        txtTenNhom.Enabled = status;
        txtdiachi.Enabled = status;
        txtDienThoai.Enabled = status;
        txtFax.Enabled = status;
    }
    protected void BtSTT_Click(object sender, EventArgs e)
    {
        syahoohelp = new YhaooHelp();
        foreach (RepeaterItem rptItem in rptGroup.Items)
        {
            Literal ltlID = (Literal)rptItem.FindControl("ltlID");
            TextBox ltlID_Index = (TextBox)rptItem.FindControl("ltlID_Index");
            string id = ltlID.Text.ToString().Trim();
            syahoohelp.UpdateIndexGroupYahoo(id, Convert.ToInt32(ltlID_Index.Text.ToString()));
        }
        Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
    }

    protected void BtDelete_Click(object sender, EventArgs e)
    {
        // xoa cac nhom dong thoi can xoa luon cac yahoo trong nhom;
        syahoohelp = new YhaooHelp();
        string id = string.Empty;
        foreach (RepeaterItem items in rptGroup.Items)
        {
            CheckBox checkboxID = (CheckBox)items.FindControl("checkboxID");
            if (checkboxID.Checked)
            {
                Literal ltlIDChuyenmuc = (Literal)items.FindControl("ltlIDChuyenmuc");
                id = ltlIDChuyenmuc.Text.Trim();
                syahoohelp.DeletegroupYahoo(id);
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
            syahoohelp = new YhaooHelp();
            DataRow dr = syahoohelp.GetInfoGroup(id);
            if (dr != null)
            {
                txtTenNhom.Text = dr["name"].ToString().Trim();
                txtMoto.Text = dr["Note"].ToString().Trim();
                txtdiachi.Text = dr["Address"].ToString().Trim();
                txtDienThoai.Text = dr["Phone"].ToString().Trim();
                txtFax.Text = dr["Fax"].ToString().Trim();
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
            }
        }
    }

    protected void btlAdd_Click(object sender, EventArgs e)
    {
        // add lai trang thai la them mới
        KT = true;
        txtTenNhom.Text = "";
        txtMoto.Text = "";
        txtdiachi.Text = "";
        txtDienThoai.Text = "";
        txtFax.Text = "";
        SetControls(true);
    }
    protected void btlOk_Click(object sender, EventArgs e)
    {
        syahoohelp = new YhaooHelp();
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
        string sAddress = txtdiachi.Text.Trim();
        string sPhone = txtDienThoai.Text.Trim();
        string SFax = txtFax.Text.Trim();
        string Note = txtMoto.Text.Trim();
        if (KT)
        {
            syahoohelp.InsertGroupYahoo(sTitle, Note, sAddress, sPhone, SFax);
        }
        else
        {
            // update;
            syahoohelp.UpdategroupYahoo(IdUpdate, sTitle, Note, sAddress, sPhone, SFax);
        }
        // Cap nhat lai thong tin sua hoac them mơi
        // 
        Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
    }
}
