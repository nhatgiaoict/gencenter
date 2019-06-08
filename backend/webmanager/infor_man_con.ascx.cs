using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class webmanager_infor_man_con : System.Web.UI.UserControl
{
    public string UrlImages
    {
        get
        {
            return Globals.UrlImages;
        }
    }
    //private static int test_lang = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            imgIconHeader.ImageUrl = Globals.UrlImages + "icon_menu.gif"; ;
            ltlHeader.Text = Language.GetTextByID(314);
            ltlError.Text = string.Empty;
            ltlSuccess.Text = string.Empty;
            btOK.Text = Language.GetTextByID(37);

            WebInfor objweb = new WebInfor();
            string id = "1";

            DataRow dr = objweb.Getdata(id);
            tbxBackend.Text = dr["backend"].ToString().Trim();
            tbxFrontend.Text = dr["frontend"].ToString().Trim();
            tbxMailContact.Text = dr["mailcontact"].ToString().Trim();
            //tbxMailPort.Text = dr["mailport"].ToString().Trim();
            //tbxMailServer.Text = dr["mailserver"].ToString().Trim();
            txtkeywords.Text = dr["keywords"].ToString().Trim();
            txtDescription.Text = dr["Description"].ToString().Trim();
            txtContent.Text = dr["diachi"].ToString().Trim();
            txtContact.Text = dr["contact"].ToString().Trim();
            txtH1.Text = dr["SEOH1"].ToString();
            txtNameCO.Text = dr["tencongty"].ToString();
            txtSlogan.Text = dr["slogan"].ToString().Trim();
        }
    }
    protected void btOK_Click(object sender, EventArgs e)
    {
        ltlSuccess.Text = string.Empty;
        if (tbxBackend.Text.Trim() == "" || tbxBackend.Text.Trim() == string.Empty)
        {
            ltlError.Text = Language.GetTextByID(197);
            return;
        }
        if (tbxFrontend.Text.Trim() == "" || tbxFrontend.Text.Trim() == string.Empty)
        {
            ltlError.Text = Language.GetTextByID(197);
            return;
        }
        if (tbxMailContact.Text.Trim() == "" || tbxMailContact.Text.Trim() == string.Empty)
        {
            ltlError.Text = Language.GetTextByID(197);
            return;
        }
        string sslogan = txtSlogan.Text.Trim();
        string sDiachi = txtContent.Text.Trim();
        WebInfor objweb = new WebInfor();
        string id = "1";

        try
        {
            //Save DataBase
            objweb.Update(id, tbxMailContact.Text.Trim(), tbxBackend.Text.Trim(), tbxFrontend.Text.Trim(), txtkeywords.Text.Trim(), txtDescription.Text.Trim(), sDiachi, txtContact.Text.Trim(), txtH1.Text.Trim(), txtNameCO.Text.Trim(), sslogan);
            //Save XML file
            //Globals.xmltask.FileName = Globals.AbsData + "xmls/database.xml";
            //DataTable dt = Globals.xmltask.Read();
            /*
            DataRow dr_xml = dt.Rows[0];
            dr_xml["database"] = tbxDatabasename.Text.Trim();
            dr_xml["server"] = tbxServer.Text.Trim();
            dr_xml["uid"] = tbxuid.Text.Trim();
            dr_xml["pwd"] = tbxPass.Text.Trim();
            //dr_xml["charset"] = tbxcharset.Text.Trim();
            dt.AcceptChanges();
            Globals.xmltask.Write();
            */
            //========== 
            ltlError.Text = string.Empty;
            ltlSuccess.Text = Language.GetTextByID(80);
        }
        catch { ltlSuccess.Text = string.Empty; }

    }

}
