using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home_uc_SPTabHome : System.Web.UI.UserControl
{
    private Menus menu = new Menus(2);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            rptGNew.DataSource = rptMenu.DataSource = menu.GetMenu();
            rptMenu.DataBind();
            rptGNew.DataBind();

        }

    }

    protected void rptGNew_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            DataRowView dr = (DataRowView)e.Item.DataItem;
            Repeater rptNew = (Repeater)e.Item.FindControl("rptNew");
            News snew = new News();
            if (dr != null)
            {
                rptNew.DataSource = snew.GetNewByLikeGroup(dr["pid"].ToString(), 8);
                rptNew.DataBind();
            }
        }
    }
}