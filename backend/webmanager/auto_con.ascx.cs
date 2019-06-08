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
using System.Text;
using System.IO;
public partial class webmanager_auto_con : System.Web.UI.UserControl
{
    public string UrlImages
    {
        get
        {
            return Globals.UrlImages;
        }
    }
    private MultiDbTask mutil = new MultiDbTask();

    private static int test_lang = 0;
    private static string sourcePath = string.Empty;
    private static string destPath = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        string sLang = Globals.CurrentLang;
        if (sLang == "vn")
        {
            sLang = "vi";
            test_lang = 1;
        }
        else
        {
            test_lang = 2;
        }
        if (!this.IsPostBack)
        {
            imgIconHeader.ImageUrl = Globals.UrlImages + "icon_menu.gif"; ;
            ltlHeader.Text = Language.GetTextByID(313);
            ltlError.Text = string.Empty;
            ltlSuccess.Text = string.Empty;
            btOK.Text = Language.GetTextByID(313);

            tbxDomain_Backend.Attributes.Add("style", "Width:250px");
            tbxDomain_Frontend.Attributes.Add("style", "Width:250px");
            tbxDirect.Attributes.Add("style", "Width:250px");

            tbxDatabasename.Attributes.Add("style", "Width:250px");
            tbxServer.Attributes.Add("style", "Width:250px");
            tbxuid.Attributes.Add("style", "Width:250px");
            tbxPass.Attributes.Add("style", "Width:250px");
            //tbxcharset.Attributes.Add("style", "Width:250px");
        }

    }
    protected void btOK_Click(object sender, EventArgs e)
    {
             ltlSuccess.Text = string.Empty;
        if (tbxDomain_Backend.Text.Trim() == "" || tbxDomain_Backend.Text.Trim() == string.Empty)
        {
            ltlError.Text = Language.GetTextByID(197);
            return;
        }
        if (tbxDomain_Frontend.Text.Trim() == "" || tbxDomain_Frontend.Text.Trim() == string.Empty)
        {
            ltlError.Text = Language.GetTextByID(197);
            return;
        }
        if (tbxDirect.Text.Trim() == "" || tbxDirect.Text.Trim() == string.Empty)
        {
            ltlError.Text = Language.GetTextByID(197);
            return;
        }
        if (tbxDatabasename.Text.Trim() == "" || tbxDatabasename.Text.Trim() == string.Empty)
        {
            ltlError.Text = Language.GetTextByID(197);
            return;
        }
        if (tbxServer.Text.Trim() == "" || tbxServer.Text.Trim() == string.Empty)
        {
            ltlError.Text = Language.GetTextByID(197);
            return;
        }
        if (tbxuid.Text.Trim() == "" || tbxuid.Text.Trim() == string.Empty)
        {
            ltlError.Text = Language.GetTextByID(197);
            return;
        }

        //if (tbxcharset.Text.Trim() == "" || tbxcharset.Text.Trim() == string.Empty)
        //{
        //    ltlError.Text = Language.GetTextByID(197);
        //    return;        //}

        //===========
        try
        {
            //Copy data
            sourcePath = Globals.UrlSourcePath;
            //Goc copy den
            string DestPath = Globals.UrlDestPath;
            destPath = DestPath + tbxDirect.Text.Trim();
            //Create Direct
            if (!System.IO.Directory.Exists(destPath))
                System.IO.Directory.CreateDirectory(destPath);
            //Copy
            CopyDirectory(sourcePath, destPath, true);

            //Config
            //Save XML file
            Globals.xmltask.FileName = destPath + "/data/data/xmls/database.xml";
            DataTable dt = Globals.xmltask.Read();
            DataRow dr_xml = dt.Rows[0];
            if (dr_xml != null)
            {
                dr_xml["database"] = tbxDatabasename.Text.Trim();
                dr_xml["server"] = tbxServer.Text.Trim();
                dr_xml["uid"] = tbxuid.Text.Trim();
                dr_xml["pwd"] = tbxPass.Text.Trim();
                //dr_xml["charset"] = tbxcharset.Text.Trim();
                dr_xml.AcceptChanges();
                Globals.xmltask.Write();
            }
            else
            { return; }

            //MySQL
            //Create Database Name
            //Run SQL
            string fileSQL = destPath + "/Database/viglacera.sql";            
            StringBuilder sb = new StringBuilder();
            StreamReader strdr = File.OpenText(fileSQL);
            string input = null;
            while ((input = strdr.ReadLine()) != null)
            {
                sb.Append(input);
            }
            strdr.Close();
            string Sql = sb.ToString();
            Sql += "INSERT INTO `tblinforweb` VALUES ('1', 'mail.3c.com.vn', '25', 'quanmh@3c.com.vn', 'quanmh@3c.com.vn', '"+tbxDomain_Backend.Text.Trim()+"','"+tbxDomain_Frontend.Text.Trim()+"', '"+tbxDomain_Backend.Text.Trim()+"', '"+tbxDomain_Frontend.Text.Trim()+"');";
            //
            int i = AddNewDB(tbxDatabasename.Text.Trim(), tbxServer.Text.Trim(), tbxuid.Text.Trim(), tbxPass.Text.Trim(), Sql);
            if (i == 0)
            {
                ltlError.Text = Language.GetTextByID(305) + "&nbsp;Server,Uid,Password !";
                return;
            }
            //=======
            ltlSuccess.Text = Language.GetTextByID(315);
            ltlError.Text = string.Empty;
            btOK.Enabled = false;
        }
        catch { ltlSuccess.Text = string.Empty; }        

    }
    private void CopyDirectory(string sourcePath, string destPath, bool overwrite)
    {
        System.IO.DirectoryInfo sourceDir = new System.IO.DirectoryInfo(sourcePath);
        System.IO.DirectoryInfo destDir = new System.IO.DirectoryInfo(destPath);
        
        if(sourceDir.Exists)
        {
            if(!destDir.Exists) destDir.Create();
        
            foreach(System.IO.FileInfo file in sourceDir.GetFiles())
            {
                if(overwrite)
                    file.CopyTo(System.IO.Path.Combine(destDir.FullName, file.Name),true);
                else
                {
                   if((System.IO.File.Exists(System.IO.Path.Combine(destDir.FullName, file.Name))) == false)
                   {
                       file.CopyTo(System.IO.Path.Combine(destDir.FullName, file.Name), false);
                   }
                }
            }

            foreach(System.IO.DirectoryInfo dir in sourceDir.GetDirectories())
            {
                if(dir.FullName != Server.MapPath(tbxDirect.Text))
                    CopyDirectory(dir.FullName, System.IO.Path.Combine(destDir.FullName, dir.Name), overwrite);
                        // lblStatusMessage.Text = "Folder copied successfully"
                        // lblStatusMessage.Visible = True
                else
                    ltlError.Text = "Can't Create";
            }
        }
    }
    public int AddNewDB(string dbname, string servername, string userid, string password, string Sql_common)
    {
        mutil = new MultiDbTask(servername, userid, password, dbname);
        try
        {
            mutil.GetConnection();
            mutil.ExecuteNonQuery(Sql_common);
            return 1;
        }
        catch
        {
            mutil = new MultiDbTask(servername, userid, password);
            try
            {
                mutil.GetConnection();
                try
                {
                    string Sql = "CREATE DATABASE IF NOT EXISTS " + dbname + "";
                    mutil.ExecuteNonQuery(Sql);
                    mutil = new MultiDbTask(servername, userid, password, dbname);
                    mutil.ExecuteNonQuery(Sql_common);
                    return 1;
                }
                catch
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }
    }

}
