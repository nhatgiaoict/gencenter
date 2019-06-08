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
using uss.utils;

public partial class news_Thuoctinhsp : System.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
    {
        cls_khoanggia clskhoanggia = new cls_khoanggia();
        cls_nhasanxuat clsNSX = new cls_nhasanxuat();
        if (!this.IsPostBack)
        {
            dropNSX.DataSource = clsNSX.GetAllDatabaseActive("tbl_nhasanxuat_vn");
            dropNSX.DataBind();

            DropKhoanggia.DataSource = clsNSX.GetAllDatabaseActive("tbl_khoanggia_vn");
            DropKhoanggia.DataBind();
        }
    }
}
