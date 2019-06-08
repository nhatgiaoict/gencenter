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

public partial class groups_uc_thuoctinhgroup : System.Web.UI.UserControl
{
    private Groups group = new Groups(1);
    private static bool KT = true;
    private static string IdUpdate = null;
    private cls_thuoctinh sThuoctinh = new cls_thuoctinh();
    private UssUrl sUrl = null;
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
        if (id == string.Empty || id == null) return;
        if (!this.IsPostBack)
        {
            DataRow dr = group.GetInfo(id);
            ltltitlegroup.Text = dr["title"].ToString().Trim();
            SetControl(false);
            //GetData
            GetData();

        }
    }
    private void SetControl(bool kt)
    {
        txtTitle.Enabled = kt;
        btnThem.Visible = !kt;
        btnOk.Visible = kt;
    }
    protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            HtmlTableRow trItems = (HtmlTableRow)e.Item.FindControl("trItems");
            DataRowView dr = (DataRowView)e.Item.DataItem;
            Literal ltlID = (Literal)e.Item.FindControl("ltlID");
            LinkButton ltllAddThuoctin = (LinkButton)e.Item.FindControl("ltllAddThuoctin");
            LinkButton ltbstatus = (LinkButton)e.Item.FindControl("ltbstatus");
            LinkButton lblEdit = (LinkButton)e.Item.FindControl("lblEdit");
            LinkButton lbtDelete = (LinkButton)e.Item.FindControl("lbtDelete");
            if (dr != null)
            {
                if (e.Item.ItemIndex % 2 == 1)
                    trItems.Attributes.Add("class", "alter");
                else
                    trItems.Attributes.Add("class", "item");
                ltlID.Text = (e.Item.ItemIndex + 1).ToString();

                ltbstatus.Text = (Convert.ToInt32(dr["status"]) == 1) ? "<font color='#0000FF'>" + Language.GetTextByID(46) + "</font>" : "<font color='#FF0000'>" + Language.GetTextByID(47) + "</font>";
                ltbstatus.CommandArgument = dr["id"].ToString();

                lblEdit.CommandArgument = dr["id"].ToString();
                lbtDelete.CommandArgument = dr["id"].ToString();

                sUrl = new UssUrl(Globals.URLCurrent + "AddValueOfthuoctinh.aspx");
                sUrl.SetParam("id", dr["id"].ToString()); // làm parentID;
                sUrl.SetParam("groupid", dr["groupid"].ToString()); // sử dụng cho việt insert đúng chuyên mục

                //ltllAddThuoctin.Attributes.Add("style", "cursor:hand;cursor:pointer");
                ltllAddThuoctin.Attributes.Add("onclick", "javascript:ShowPopup('" + sUrl.Url + "','400','300');");
            }
        }
    }
    protected void btnThem_Click(object sender, EventArgs e)
    {
        KT = true;
        SetControl(true);
    }
    protected void rptList_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            LinkButton lblEdit = (LinkButton)e.Item.FindControl("lblEdit");
            lblEdit.CausesValidation = false;
            lblEdit.Click += new EventHandler(lblEdit_Click);

            LinkButton ltbstatus = (LinkButton)e.Item.FindControl("ltbstatus");
            ltbstatus.CausesValidation = false;
            ltbstatus.Click += new EventHandler(ltbstatus_Click);
            //
            LinkButton lbtDelete = (LinkButton)e.Item.FindControl("lbtDelete");
            lbtDelete.CausesValidation = false;
            lbtDelete.Click += new EventHandler(lbtDelete_Click);
        }
    }

    protected void lbtDelete_Click(object sender, EventArgs e)
    {
        // Delete
        LinkButton lbtDelete = (LinkButton)sender;
        if (lbtDelete.CommandArgument != String.Empty)
        {
            string id = lbtDelete.CommandArgument.ToString();
            sThuoctinh.Delete(id);
            Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
        }
    }
    protected void ltbstatus_Click(object sender, EventArgs e)
    {
        LinkButton ltbstatus = (LinkButton)sender;
        if (ltbstatus.CommandArgument != String.Empty)
        {
            string id = ltbstatus.CommandArgument.ToString();
            sThuoctinh.SetStatus(id);
            Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
        }
    }

    protected void lblEdit_Click(object sender, EventArgs e)
    {
        KT = false;
        SetControl(true);
        LinkButton lblEdit = (LinkButton)sender;
        if (lblEdit.CommandArgument != null)
        {
            // getdata;
            string id = lblEdit.CommandArgument.ToString();
            DataRow dr = sThuoctinh.GetInfo(id);
            if (dr != null)
            {
                //txtTenKeyWord.Text = dr["title"].ToString().Trim();
                //txtlink.Text = dr["link"].ToString().Trim();
                txtTitle.Text = dr["title"].ToString();
                IdUpdate = dr["id"].ToString().Trim();
            }
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        // Cập nhật vào DB
        string stitle = txtTitle.Text.Trim();
        if (stitle == string.Empty || stitle == "")
        {
            spanTB.Visible = true;
            return;
        }
        if (KT)
        {
            sThuoctinh.Insert(stitle, id, string.Empty); // thêm mới
        }
        else
        {
            // update;
            sThuoctinh.Update(IdUpdate, stitle); // Update
        }
        // Cap nhat lai thong tin sua hoac them mơi
        Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
        //GetData();
    }
    private void GetData()
    {
        string PageQuery = "page";
        string strcPage = Request.QueryString[PageQuery];
        if (strcPage == null)
            strcPage = "1";
        int cPage = Convert.ToInt32(strcPage);
        int TotalRecords, TotalPages;
        rptList.DataSource = sThuoctinh.Searching(cPage, 20, out TotalRecords, out TotalPages, id, "00"); //lay theo nhóm chính parentid = '00';
        rptList.DataBind();
    }
}
