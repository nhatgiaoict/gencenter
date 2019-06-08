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
using uss.utils;
using System.IO;
using System.Text;

public partial class hoivien_detail : System.Web.UI.Page
{
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
    private string newsid
    {
        get
        {
            string newsid = sUrl.GetParam("newsid");
            if (newsid == null || newsid == "")
                newsid = string.Empty;
            return newsid;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ltlLabTitle.Text = Language.GetTextByID(217);
            LabHeaderCmuc.Text = Language.GetTextByID(91);
            LabTieude.Text = Language.GetTextByID(29);
            LabNoidung.Text = Language.GetTextByID(35);
            LabTomtat.Text = Language.GetTextByID(31);
            LabImg.Text = Language.GetTextByID(33);


            clsHoivien hoivien = new clsHoivien();
            DataRow drow = hoivien.GetInfo(newsid);

            LabTenTieude.Text = drow["title"].ToString();
            LabND_tomtat.Text = drow["summary"].ToString();
            LabNoidungbai.Text = hoivien.GetContent(newsid);

            string groupid = drow["groupid"].ToString();
            Groups objgroup = new Groups();
            DataRow vdr = objgroup.GetInfo(groupid);
            LabChuyenmuc.Text = vdr["title"].ToString().Trim();
            //images
            string sFile = drow["logo"].ToString().Trim();
            if (sFile.Length > 0)
            {
                string cImages = sFile.Substring(0, 4);
                if (cImages != "data")
                {
                    fImage.Attributes.Add("style", "width:100px;height:100px");
                    fImage.Src = sFile;
                }
                else
                    fImage.Src = Thumbnails.viewAsThumbnails(sFile, Convert.ToInt32(drow["fwidth"].ToString()), Convert.ToInt32(drow["fheight"].ToString()), "news_m", drow["id"].ToString());
            }
            else
            {
                fImage.Src = Globals.UrlRootImages + "data/images/no-image.gif";
            }           

        }

    }
}
