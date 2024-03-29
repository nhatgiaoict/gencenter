using System;
using System.IO;
using System.Collections;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Security.Principal;
using System.Configuration;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using uss.xml;


/// <summary>
/// Summary description for Globals
/// </summary>
public class Globals
{
    public static string CurrentLang
    {
        get
        {
            return (HttpContext.Current.Session["Lang"] == null) ? "vn" : HttpContext.Current.Session["Lang"].ToString();
        }
        set
        {
            HttpContext.Current.Session["Lang"] = value;
        }

    }
    public static string URLCurrent
    {
        get
        {
            string strCurrent = System.Web.HttpContext.Current.Request.CurrentExecutionFilePath;
            while (strCurrent.Length > 0 && !strCurrent.EndsWith("/"))
            {
                strCurrent = strCurrent.Substring(0, (strCurrent.Length) - 1);
            }
            strCurrent = (strCurrent.EndsWith("/") ? strCurrent : strCurrent + "/");
            return strCurrent;
        }
    }

    private static string GetConfigString(string configSection, string configKey, string defaultValue)
    {


        NameValueCollection configSettings = ConfigurationManager.GetSection(configSection) as NameValueCollection;
        if (configSettings != null)
        {
            string configValue = configSettings[configKey] as string;
            if (configValue != null)
            {
                return configValue;
            }
        }
        return defaultValue;
    }
    public static string ConectionString
    {
      
        get
        {
            return GetConfigString("appSettings", "conectionString", string.Empty);
        }
    }
    public static DataTable GetDatabaseXml()
    {
        xmltask.FileName = AbsData + "xmls/database.xml";
        return Globals.xmltask.Read();
    }
    public static string UrlRoot
    {
        get
        {

            string sRet = System.Web.HttpContext.Current.Request.ApplicationPath;

            if (!sRet.EndsWith("/"))
                sRet = sRet + "/";
            return sRet;
        }
    }
    public static string UrlUploads
    {
        get
        {
            string sRet = GetConfigString("appSettings", "urlUploads", string.Empty);
            return UrlRoot + sRet;
        }
    }
    public static string UrlLanguages
    {
        get
        {
            string sRet = GetConfigString("appSettings", "urlLanguages", string.Empty);
            return HttpContext.Current.Server.MapPath(UrlRoot + sRet);
        }
    }
    public static string CounterUrl
    {
        get
        {
            return UrlRoot + GetConfigString("appSettings", "CounterUrl", string.Empty);

        }
    }
    public static string UrlHot
    {
        get
        {
            return GetConfigString("appSettings", "UrlHot", string.Empty);
        }
    }
    public static string UrlCss
    {
        get
        {
            return UrlRoot + GetConfigString("appSettings", "urlCss", string.Empty);

        }
    }
    public static string UrlJavascript
    {
        get
        {
            string sRet = UrlRoot + GetConfigString("appSettings", "urlJavascript", "");
            if (!sRet.EndsWith("/"))
                sRet = sRet + "/";
            return sRet;
        }
    }
    public static string UrlImages
    {
        get
        {
            return UrlRoot + GetConfigString("appSettings", "urlImages", string.Empty);
        }
    }
    public static string UrlSetEditor
    {
        get
        {
            return UrlRoot + GetConfigString("appSettings", "UrlSetEditor", string.Empty);
        }
    }
    // thử chạy flash khi rewrite
    public static string UrlHost
    {
        get
        {
            return UrlRoot + GetConfigString("appSettings", "UrlHost", string.Empty);
        }
    }
    public static string nview
    {
        get
        {
            return GetConfigString("appSettings", "nview", string.Empty);
        }
    }
    public static string GroupDefault { get { return GetConfigString("appSettings", "GroupDefault", string.Empty); } }
    public static string TotalSP { get { return GetConfigString("appSettings", "TotalSP", string.Empty); } }
    public static string TotalNew { get { return GetConfigString("appSettings", "TotalNew", string.Empty); } }
    //end
    public static string AbsImages
    {
        get
        {
            return HttpContext.Current.Server.MapPath(UrlImages);
        }
    }
    public static string UrlRootImages
    {
        get
        {

            string sRet = System.Web.HttpContext.Current.Request.ApplicationPath;
            if (!sRet.EndsWith("/"))
                sRet = sRet + "/";
            return sRet;
        }
    }

    public static string AbsConfs
    {
        get
        {
            string sRet = GetConfigString("appSettings", "absConfs", "");
            if (!sRet.EndsWith("/"))
                sRet = sRet + "/";
            return HttpContext.Current.Server.MapPath(UrlRoot + sRet);
        }
    }
    public static string UrlJs
    {
        get
        {
            return UrlRoot + GetConfigString("appSettings", "urlJs", string.Empty);
        }
    }
    public static string AbsXmls
    {
        get
        {
            string sRet = GetConfigString("appSettings", "absXmls", string.Empty);
            return HttpContext.Current.Server.MapPath(UrlRoot + sRet);

        }
    }
    public static bool CheckExist
    {
        get
        {
            string bRet = GetConfigString("appSettings", "checkExist", "");
            if (bRet == "true")
                return true;
            return false;
        }
    }
    public static string UrlData
    {
        get
        {
            return UrlRoot + GetConfigString("appSettings", "urlData", "");

        }
    }
    public static string AbsData
    {
        get
        {
            return HttpContext.Current.Server.MapPath(UrlData);
        }
    }
    public static string UrlCounter
    {
        get
        {
            return HttpContext.Current.Server.MapPath(CounterUrl);
        }
    }
    public static string MailMaster
    {
        get
        {
            WebInfor objweb = new WebInfor();
            string id = "1";
            DataRow dr = objweb.Getdata(id);
            return dr["mailmaster"].ToString().Trim();
        }
    }
    public static string SEOH1
    {
        get
        {
            WebInfor objweb = new WebInfor();
            string id = "1";
            DataRow dr = objweb.Getdata(id);
            return dr["SEOH1"].ToString().Trim();
        }
    }
    public static string Hotline
    {
        get
        {
            WebInfor objweb = new WebInfor();
            string id = "1";
            DataRow dr = objweb.Getdata(id);
            return dr["slogan"].ToString().Trim();
        }
    }
    public static string Motoduanmoi
    {
        get
        {
            WebInfor objweb = new WebInfor();
            string id = "1";
            DataRow dr = objweb.Getdata(id);
            return dr["Diachi"].ToString().Trim();
        }
    }
    public static string MailServer
    {
        get
        {
            WebInfor objweb = new WebInfor();
            string id = "1";
            DataRow dr = objweb.Getdata(id);
            return dr["mailserver"].ToString().Trim();
        }
    }
    public static string MailPort
    {
        get
        {
            WebInfor objweb = new WebInfor();
            string id = "1";
            DataRow dr = objweb.Getdata(id);
            return dr["mailport"].ToString().Trim();
        }
    }
    public static string MailContact
    {
        get
        {
            WebInfor objweb = new WebInfor();
            string id = "1";
            DataRow dr = objweb.Getdata(id);
            return dr["mailcontact"].ToString().Trim();
        }
    }
    //
    public static FileInfo[] fi
    {
        get
        {
            DirectoryInfo di = new DirectoryInfo(AbsImages + "icons/");
            return di.GetFiles("*.gif");

        }
    }
    public static string GetUniqueFileName(string strDir, string strFileName)
    {
        if (strDir != "" && !(strDir.EndsWith("/") || strDir.EndsWith("\\"))) strDir += "/";

        string strExt = System.IO.Path.GetExtension(strFileName);
        strFileName = System.IO.Path.GetFileNameWithoutExtension(strFileName);
        strFileName = strFileName.Replace(" ", "_");
        //strFileName = Encrypt(strFileName);
        strFileName = strFileName + strExt;
        int file_append = 0;
        while (System.IO.File.Exists(strDir + strFileName))
        {
            file_append++;
            strFileName = System.IO.Path.GetFileNameWithoutExtension(strFileName) + file_append.ToString() + strExt;
        }

        return strFileName;
    }
    public static string Encrypt(string mess)
    {
        System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] bs = System.Text.Encoding.UTF8.GetBytes(mess);
        bs = x.ComputeHash(bs);
        System.Text.StringBuilder s = new System.Text.StringBuilder();
        foreach (byte b in bs)
        {
            s.Append(b.ToString("x2").ToLower());
        }
        return s.ToString().ToUpper();
    }
    public static string VietnameseDate(DateTime dt)
    {
        return dt.Day.ToString() + "/" + dt.Month.ToString() + "/" + dt.Year.ToString();
    }

    public static string EnglishDate(DateTime dt)
    {
        return dt.Month.ToString() + "/" + dt.Day.ToString() + "/" + dt.Year.ToString();
    }
    public static string VietnameseDateFull(DateTime dt)
    {
        return dt.Day.ToString() + "/" + dt.Month.ToString() + "/" + dt.Year.ToString() + " " + dt.Hour.ToString() + ":" + dt.Minute.ToString() + ":" + dt.Second.ToString();
    }

    public static string EnglishDateFull(DateTime dt)
    {
        return dt.Month.ToString() + "/" + dt.Day.ToString() + "/" + dt.Year.ToString() + " " + dt.Hour.ToString() + ":" + dt.Minute.ToString() + ":" + dt.Second.ToString();
    }
    public static string VietnameseDate(string strEDate)
    {
        if (strEDate == String.Empty) return String.Empty;
        string[] arrDate = strEDate.Split(new char[] { '-', '/' });
        if (arrDate.Length != 3) return String.Empty;
        return arrDate[1] + "/" + arrDate[0] + "/" + arrDate[2];
    }

    public static string EnglishDate(string strVDate)
    {
        return VietnameseDate(strVDate);
    }
    public static string FullDateTime(DateTime dt)
    {
        return dt.ToString("dd/MM/yyyy") + ", " + dt.ToShortTimeString();

    }
    public static string yyyy_MM_dd(string strVDate)
    {
        if (strVDate == String.Empty) return "Null";
        string[] arrDate = strVDate.Split(new char[] { '-', '/' });
        if (arrDate.Length != 3) return "Null";
        return arrDate[2] + "-" + arrDate[1] + "-" + arrDate[0];
    }
    public static string yyyyMMdd_hhmmss(string ddMMyyyy_hhmmss)
    {
        string[] arrDate = ddMMyyyy_hhmmss.Split(' ');
        if (arrDate.Length != 2)
            return "Null";
        string[] arrDay = arrDate[0].ToString().Split(new char[] { '-', '/' });
        if (arrDay.Length != 3)
            return "Null";
        return arrDay[2].ToString() + "-" + arrDay[1].ToString() + "-" + arrDay[0].ToString() + " " + arrDate[1].ToString();
    }
    public static string GetArrParentID(string ArrID, string IDParent, int nLevel)
    {
        //string ArrID = "01,0202,020401,02030405";
        //string IDParent = "02";
        if (ArrID == null || ArrID == "" || ArrID == string.Empty)
            return string.Empty;

        string sRet = ",";
        int len = nLevel;
        if (IDParent != "" && IDParent != null && IDParent != string.Empty)
            len += IDParent.Length;
        string[] arrID = ArrID.Split(',');
        int c = arrID.Length;
        string sTemp = string.Empty;
        for (int i = 0; i < c; i++)
        {
            sTemp = arrID[i].ToString();
            if (sTemp.Length >= len)
            {
                sTemp = sTemp.Substring(0, len);
                if (sRet.IndexOf("," + sTemp + ",") < 0)
                {
                    sRet += sTemp + ",";
                }
            }
        }
        while (sRet.StartsWith(","))
            sRet = sRet.Substring(1, sRet.Length - 1);
        while (sRet.EndsWith(","))
            sRet = sRet.Substring(0, sRet.Length - 1);
        return sRet;
    }
    static xmltask m_xmltask = new xmltask();
    public static xmltask xmltask
    {
        get
        {
            return m_xmltask;
        }

    }
    /// <summary>
    /// Resource file CSS và JS
    /// </summary>
    public static bool UseFileSet
    {
        get
        {
            return Convert.ToBoolean(ConfigurationManager.AppSettings["UseFileSet"]);
        }
    }
    /// <summary>
    /// phiên bản của Css hay Js là bao nhiêu v=1.0 hay v=1.1
    /// </summary>
    public static string FileSetVession
    {
        get
        {
            return ConfigurationManager.AppSettings["Vession"].ToString();
        }
    }
}
