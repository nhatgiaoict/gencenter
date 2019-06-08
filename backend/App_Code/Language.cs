using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


/// <summary>
/// Summary description for Language
/// </summary>
public class Language
{
	public Language()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataTable TableLang
    {
        get
        {
            if ((DataTable)HttpContext.Current.Session["Language"] == null)
            {
                Globals.xmltask.FileName = Globals.AbsData + "xmls/language.xml";
                HttpContext.Current.Session["Language"] = Globals.xmltask.Read();
            }
            return (DataTable)HttpContext.Current.Session["Language"];
        }
    }
    public static string GetTextByIndex(int index)
    {
        try
        {
            return TableLang.Rows[index][Globals.CurrentLang].ToString();
        }
        catch
        {
            return string.Empty;
        }
    }
    public static string GetTextByID(int id)
    {
        try
        {
            DataRow[] dr = TableLang.Select("id='" + id + "'");
            if (dr != null && dr.Length > 0)
                return dr[0][Globals.CurrentLang].ToString();
            return string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }
    public static DataTable GetAllLanguage()
    {
        if ((DataTable)HttpContext.Current.Application["AllLanguage"] == null)
        {
            Globals.xmltask.FileName = Globals.AbsData + "xmls/multilanguage.xml";
            HttpContext.Current.Application["AllLanguage"] = Globals.xmltask.Read();
        }
        return (DataTable)HttpContext.Current.Application["AllLanguage"];
    }
    public static DataTable Status_Bientap()
    {
        if ((DataTable)HttpContext.Current.Application["Status_btap"] == null)
        {
            Globals.xmltask.FileName = Globals.AbsData + "xmls/status_btap.xml";
            HttpContext.Current.Application["Status_btap"] = Globals.xmltask.Read();
        }
        return (DataTable)HttpContext.Current.Application["Status_btap"];

    }
    public static DataTable Status_duyetok()
    {
        if ((DataTable)HttpContext.Current.Application["Status_duyet"] == null)
        {
            Globals.xmltask.FileName = Globals.AbsData + "xmls/status_ok.xml";
            HttpContext.Current.Application["Status_duyet"] = Globals.xmltask.Read();
        }
        return (DataTable)HttpContext.Current.Application["Status_duyet"];

    }
    public static DataTable DataYesNo()
    {
        if ((DataTable)HttpContext.Current.Application["YesNo"] == null)
        {
            Globals.xmltask.FileName = Globals.AbsData + "xmls/yes_no.xml";
            HttpContext.Current.Application["YesNo"] = Globals.xmltask.Read();
        }
        return (DataTable)HttpContext.Current.Application["YesNo"];

    }
    public static DataTable Status_images()
    {
        if ((DataTable)HttpContext.Current.Application["Status_image"] == null)
        {
            Globals.xmltask.FileName = Globals.AbsData + "xmls/Status_images.xml";
            HttpContext.Current.Application["Status_image"] = Globals.xmltask.Read();
        }
        return (DataTable)HttpContext.Current.Application["Status_image"];

    }
    public static DataTable AllStatus()
    {
        if ((DataTable)HttpContext.Current.Application["Status"] == null)
        {
            Globals.xmltask.FileName = Globals.AbsData + "xmls/status_news.xml";
            HttpContext.Current.Application["Status"] = Globals.xmltask.Read();
        }
        return (DataTable)HttpContext.Current.Application["Status"];

    }
}
