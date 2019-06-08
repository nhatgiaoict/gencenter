using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Home_uc_hotnew : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Menus smenu = new Menus(5);
            rptG.DataSource = smenu.GetMenu();
            rptG.DataBind();
        }
    }
    protected void rptG_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            Repeater rptHotnew = (Repeater)e.Item.FindControl("rptHotnew");
            DataRowView dr = (DataRowView)e.Item.DataItem;
            News snew = new News();
            if (dr != null)
            {
                rptHotnew.DataSource = snew.GetNewLikeGroup(dr["pid"].ToString().Trim(), 16);
                rptHotnew.DataBind();
            }
        }
    }
}


           

    


