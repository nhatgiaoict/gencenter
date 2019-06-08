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

public partial class YahooHelp_Yhaoo_con : System.Web.UI.UserControl
{
    private YhaooHelp syahoohelp = null;
    private static bool KTTT = true;
    private static string IdUpdate = null;
    public string UrlImages
    {
        get
        {
            return Globals.UrlImages;
        }
    }

    private static UssUrl sUrl
    {
        get
        {
            return new UssUrl();
        }
    }
    private string IndexSelect
    {
        get
        {
            string indexsl = sUrl.GetParam("Status");
            if (indexsl == "" || indexsl == string.Empty || indexsl == null)
                indexsl = "0";
            return indexsl;
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        syahoohelp = new YhaooHelp(); 
        if (!this.IsPostBack)
        {
            imgIconHeader.ImageUrl = Globals.UrlImages + "icon_menu1.gif";
            DataTable dt = syahoohelp.GetAllYahoo();
            if (dt != null)
            {
                rptYahoo.DataSource = dt;
                rptYahoo.DataBind();
            }
            Setcontrols(false);
        }
    }

    private void BinDrop(DropDownList MyDrop)
    {
        MyDrop.Items.Clear();
        DataTable dt = syahoohelp.GetgroupYahoo();
        if (dt != null && dt.Rows.Count > 0)
        {            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListItem Item = new ListItem(dt.Rows[i]["name"].ToString().Trim(), dt.Rows[i]["id"].ToString().Trim());
                MyDrop.Items.Add(Item);
            }
        }
    }
    private void Setcontrols(bool status)
    {
        txtFullname.Enabled = status;
        txtEmail.Enabled = status;
        txtNickYahoo.Enabled = status;
        txtPhone.Enabled = status;
        txtFullname.Focus();
    }

    // cap nhat lai so thu tu cac Yhoo
    protected void BtSTT_Click(object sender, EventArgs e)
    {
        syahoohelp = new YhaooHelp();
        foreach (RepeaterItem rptItem in rptYahoo.Items)
        {
            Literal ltlID = (Literal)rptItem.FindControl("ltlID");
            TextBox ltlID_Index = (TextBox)rptItem.FindControl("ltlID_Index");
            string id = ltlID.Text.ToString().Trim();
            syahoohelp.UpdateIndexYahoo(id, Convert.ToInt32(ltlID_Index.Text.ToString()));
        }
        Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
    }
    // delete
    protected void BtDelete_Click(object sender, EventArgs e)
    {
        syahoohelp = new YhaooHelp();
        string id = string.Empty;
        foreach (RepeaterItem items in rptYahoo.Items)
        {
            CheckBox checkboxID = (CheckBox)items.FindControl("checkboxID");
            if (checkboxID.Checked)
            {
                Literal ltlIDChuyenmuc = (Literal)items.FindControl("ltlIDChuyenmuc");
                id = ltlIDChuyenmuc.Text.Trim();
                syahoohelp.DeleteYahooforID(id);
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

    // thiet lap lai trang thai them moi
    protected void btlAdd_Click(object sender, EventArgs e)
    {
        txtFullname.Text = "";
        txtEmail.Text = "";
        txtNickYahoo.Text = "";
        txtPhone.Text = "";
        KTTT = true;
        Setcontrols(true);
    }
    
    // ok
    protected void btlOk_Click(object sender, EventArgs e)
    {
        int i = 0;
        string sfullname = txtFullname.Text.Trim();
        string sYahooName = txtNickYahoo.Text.Trim();
        string sEmail = txtEmail.Text.Trim();
        //string sLinhvuc = txtlinhvuc.Text.Trim();
        //string sfimage = txtfimage.Text.Trim();
        string sgroupid = "00";//DropGroupYahoo.SelectedItem.Value.ToString();
        string Phone = txtPhone.Text.Trim();
        if (sfullname == "" || sYahooName == "")
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
        if (KTTT)
        {
            // Insert
            syahoohelp.InsertYahoo(sfullname, sYahooName, sEmail, sgroupid, Phone);
            Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
        }
        else
        {
            syahoohelp.UpDateYahoo(IdUpdate, sfullname,sYahooName, sEmail, "00", Phone);
            Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
        }
        
    }
    protected void rptYahoo_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            LinkButton lblEdit = (LinkButton)e.Item.FindControl("lblEdit");
            lblEdit.CausesValidation = false;
            lblEdit.Click += new EventHandler(lblEdit_Click);
            
            LinkButton lblStatus = (LinkButton)e.Item.FindControl("lblStatus");
            lblStatus.CausesValidation = false;
            lblStatus.Click += new EventHandler(lblStatus_Click);
        }
    }

    protected void lblStatus_Click(object sender, EventArgs e)
    {
        // cap nhat lai trang thai tat bat
        LinkButton lblStatus = (LinkButton)sender;
        if (lblStatus.CommandArgument != string.Empty)
        {
            string id = lblStatus.CommandArgument;
            syahoohelp = new YhaooHelp();
            syahoohelp.SetStatus(id);
            Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
        }
    }

    protected void lblEdit_Click(object sender, EventArgs e)
    {
        KTTT = false;
        Setcontrols(true);
        LinkButton lblEdit = (LinkButton)sender;
        if (lblEdit.CommandArgument != null)
        {
            string id = lblEdit.CommandArgument.ToString().Trim();
            DataRow dryahoo = syahoohelp.GetInfoYhaoo(id);
            if (dryahoo != null)
            {
                txtFullname.Text = dryahoo["fullName"].ToString().Trim();
                txtNickYahoo.Text = dryahoo["YahooName"].ToString().Trim();
                txtEmail.Text = dryahoo["Email"].ToString().Trim();
                txtPhone.Text = dryahoo["phone"].ToString().Trim();
                IdUpdate = dryahoo["id"].ToString().Trim();
            }
        }
    }

    protected void rptYahoo_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            DataRowView dr = (DataRowView)e.Item.DataItem;
            Literal ltlID = (Literal)e.Item.FindControl("ltlID");
            TextBox ltlID_Index = (TextBox)e.Item.FindControl("ltlID_Index");
            LinkButton lblEdit = (LinkButton)e.Item.FindControl("lblEdit");
            Literal ltlIDChuyenmuc = (Literal)e.Item.FindControl("ltlIDChuyenmuc");
            CheckBox checkboxID = (CheckBox)e.Item.FindControl("checkboxID");
            HtmlAnchor hrEdit = (HtmlAnchor)e.Item.FindControl("hrEdit");
            LinkButton lblStatus = (LinkButton)e.Item.FindControl("lblStatus");
            if (dr != null)
            {
                string sid = dr["id"].ToString();
                ltlID.Text = sid;
                ltlIDChuyenmuc.Text = sid;
                ltlID_Index.Attributes.Add("style", "width:30px");
                ltlID_Index.Text = (e.Item.ItemIndex + 1).ToString();	//STT

                lblEdit.Text = Language.GetTextByID(42);
                lblEdit.CommandArgument = dr["id"].ToString();
                syahoohelp = new YhaooHelp();
                lblStatus.Text = (Convert.ToInt32(dr["status"]) == 1) ? "<font color='#0000FF'>" + Language.GetTextByID(46) + "</font>" : "<font color='#FF0000'>" + Language.GetTextByID(47) + "</font>";
                lblStatus.CommandArgument = dr["id"].ToString().Trim();
            }
        }
    }
}
