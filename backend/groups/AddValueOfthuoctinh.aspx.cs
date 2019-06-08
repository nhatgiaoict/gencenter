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

public partial class groups_AddValueOfthuoctinh : System.Web.UI.Page
{
    private UssUrl sUrl = null;
    private string id
    {
        get
        {
            sUrl = new UssUrl();
            return sUrl.GetParam("id");
        }
    }
    private DataTable dt = null;
    private cls_thuoctinh clthuoctinh = new cls_thuoctinh();
    private string groupid
    {
        get
        {
            sUrl = new UssUrl();
            return sUrl.GetParam("groupid");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (id == string.Empty || id == null || groupid == null || groupid == string.Empty) return;
        if (!this.IsPostBack)
        {
            GetData();
            rptGaitri.DataSource = dt;
            rptGaitri.DataBind();
        }
    }
    public void GetData()
    {
        dt = clthuoctinh.GetChildByparent(id);
    }
    protected void rptGaitri_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "AddRow")
        {
            GetData();
            dt.Rows.Add(0, ""); // Thêm một dòng vào dt và vẽ nó ở rpt
            rptGaitri.DataSource = dt;
            rptGaitri.DataBind();
            return;
        }
        if (e.CommandName == "edit")
        {
            // sửa, cập nhật lại giá trị của thuộc tính
            ((TextBox)e.Item.FindControl("txtValue")).Enabled = true;
            ((LinkButton)e.Item.FindControl("lnkEdit")).Visible = false;
            ((LinkButton)e.Item.FindControl("lnkDelete")).Visible = false;
            ((LinkButton)e.Item.FindControl("lnkUpdate")).Visible = true;
            ((LinkButton)e.Item.FindControl("lnkCancel")).Visible = true;
        }
        if (e.CommandName == "cancel")
        {
            // sửa, cập nhật lại giá trị của thuộc tính
            ((TextBox)e.Item.FindControl("txtValue")).Enabled = false;
            ((LinkButton)e.Item.FindControl("lnkEdit")).Visible = true;
            ((LinkButton)e.Item.FindControl("lnkDelete")).Visible = true;
            ((LinkButton)e.Item.FindControl("lnkUpdate")).Visible = false;
            ((LinkButton)e.Item.FindControl("lnkCancel")).Visible = false;
        }
        if (e.CommandName == "update")
        {
            string stitle = ((TextBox)e.Item.FindControl("txtValue")).Text;
            string idT = e.CommandArgument.ToString();
            if (stitle == string.Empty || stitle == null)
            {
                spanTB.Visible = true;
                return;
            }
            else
            {
                // Khác 0 chứng tỏ là sửa
                if (e.CommandArgument.ToString() != "0") 
                {
                    // Update
                    clthuoctinh.Update(idT, stitle);
                    ((TextBox)e.Item.FindControl("txtValue")).Enabled = false;
                    ((LinkButton)e.Item.FindControl("lnkEdit")).Visible = true;
                    ((LinkButton)e.Item.FindControl("lnkDelete")).Visible = true;
                    ((LinkButton)e.Item.FindControl("lnkUpdate")).Visible = false;
                    ((LinkButton)e.Item.FindControl("lnkCancel")).Visible = false;
                }
                    //Thêm mới
                else
                {
                    // Inset
                    clthuoctinh.Insert(stitle, groupid, id);
                    ((TextBox)e.Item.FindControl("txtValue")).Enabled = false;
                    ((LinkButton)e.Item.FindControl("lnkEdit")).Visible = true;
                    ((LinkButton)e.Item.FindControl("lnkDelete")).Visible = true;
                    ((LinkButton)e.Item.FindControl("lnkUpdate")).Visible = false;
                    ((LinkButton)e.Item.FindControl("lnkCancel")).Visible = false;
                }
            }
            // update
        }
        if (e.CommandName == "delete" && e.CommandArgument.ToString() != "")
        {
            string id = e.CommandArgument.ToString();
            clthuoctinh.Delete(id);
            Response.Redirect(Request.Url.AbsoluteUri, true);
        }
    }
}
