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

public partial class Boloc_uc_managernsx : System.Web.UI.UserControl
{
    private DataTable dt = null;
    private cls_nhasanxuat clsnsx = new cls_nhasanxuat();
    public static string TableName { get { return "tbl_nhasanxuat_vn"; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            GetData();
            rptHang.DataSource = dt;
            rptHang.DataBind();
        }
    }

    protected void rptHang_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "edit")
        {
            ((TextBox)e.Item.FindControl("txtSTT")).Enabled = true;
            ((TextBox)e.Item.FindControl("txttitle")).Enabled = true;
            ((LinkButton)e.Item.FindControl("lnkEdit")).Visible = false;
            ((LinkButton)e.Item.FindControl("lnkDelete")).Visible = false;
            ((LinkButton)e.Item.FindControl("lnkUpdate")).Visible = true;
            ((LinkButton)e.Item.FindControl("lnkCancel")).Visible = true;
        }
        if (e.CommandName == "update")
        {
            // INSET
            string sSTT = ((TextBox)e.Item.FindControl("txtSTT")).Text;
            string sTitle = ((TextBox)e.Item.FindControl("txttitle")).Text;
            string id = e.CommandArgument.ToString();
            clsnsx.Update(id, sTitle, sSTT, TableName);
            GetData();
            ((TextBox)e.Item.FindControl("txtSTT")).Enabled = false;
            ((TextBox)e.Item.FindControl("txttitle")).Enabled = false;
            ((LinkButton)e.Item.FindControl("lnkEdit")).Visible = true;
            ((LinkButton)e.Item.FindControl("lnkDelete")).Visible = true;
            ((LinkButton)e.Item.FindControl("lnkUpdate")).Visible = false;
            ((LinkButton)e.Item.FindControl("lnkCancel")).Visible = false;
        }
        if (e.CommandName == "delete")
        {
            string id = e.CommandArgument.ToString();
            clsnsx.Delete(id, TableName);
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        if (e.CommandName == "cancel")
        {
            ((TextBox)e.Item.FindControl("txtSTT")).Enabled = false;
            ((TextBox)e.Item.FindControl("txttitle")).Enabled = false;
            ((LinkButton)e.Item.FindControl("lnkEdit")).Visible = true;
            ((LinkButton)e.Item.FindControl("lnkDelete")).Visible = true;
            ((LinkButton)e.Item.FindControl("lnkUpdate")).Visible = false;
            ((LinkButton)e.Item.FindControl("lnkCancel")).Visible = false ;
        }
    }
    private void GetData()
    {
        dt = clsnsx.GetAllDatabase(TableName);
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        // Thêm mới
        string stitle = txtTitle.Text.Trim();
        if (stitle.Length > 0)
        {
            // Check Exit
            if (clsnsx.CheckExist(stitle, TableName))
            {
                spanmess.InnerText = "Nhà sản xuất này đã có trong hệ thống";
                spanmess.Visible = true;
                return;
            }
            else
            {
                clsnsx.Insert(stitle, TableName);
                spanmess.InnerText = "Thêm mới thàng công";
                spanmess.Visible = true;
            }
        }
        else
        {
            spanmess.Visible = true;
            return;
        }
        Response.Redirect(Request.Url.AbsoluteUri);
    }
}
