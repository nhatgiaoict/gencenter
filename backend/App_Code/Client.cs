using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Text;
using System.IO;

/// <summary>
/// Summary description for Client
/// </summary>
public class Client
{
    private CookieCollection cookies;
    private WebHeaderCollection header;

    //======================================================================
    public WebHeaderCollection Header
    {
        get { return header; }
    }
    //======================================================================
    public Client()
    {
    }
    //======================================================================
    private void SaveCookies(CookieCollection ResponseCookies)
    {
        if (cookies == null)
            cookies = ResponseCookies;
        else
        {
            foreach (Cookie RespCookie in ResponseCookies)
            {
                bool bMatch = false;
                foreach (Cookie ReqCookie in cookies)
                {
                    if (ReqCookie.Name == RespCookie.Name)
                    {
                        ReqCookie.Value = RespCookie.Name;
                        bMatch = true;
                        break; // 
                    }
                }
                if (!bMatch)
                    cookies.Add(RespCookie);
            }
        }
    }
    //======================================================================
    public string DownloadToMem(string Url)
    {
        string result = "";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url.Trim());

        request.Timeout = 10000;  // 10 secs
        request.UserAgent = "uss web client";

        //HttpContext.Current.Response.Write(Url);
        try
        {	// ---- send cookies if available -----
            //HttpContext.Current.Response.Write("1 ");
            if (cookies != null && cookies.Count > 0)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            // ---- Send and get Response data -----
            //HttpContext.Current.Response.Write("2 ");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // ----save cookies if any ------
            //HttpContext.Current.Response.Write("3 ");
            if (response.Cookies.Count > 0)
                SaveCookies(response.Cookies);
            //------ make StreamReader --------------	
            //HttpContext.Current.Response.Write("41 ");

            Encoding enc;
            if (response.ContentEncoding != null && response.ContentEncoding.Length > 0)
                enc = Encoding.GetEncoding(response.ContentEncoding);
            else if (response.ContentType.IndexOf("text/html") >= 0 || response.ContentType.IndexOf("text/xml") >= 0)
                enc = Encoding.UTF8;
            else
                enc = Encoding.GetEncoding(1252);
            /*
				
            */
            //HttpContext.Current.Response.Write("42 ");
            StreamReader ResponseStream = new StreamReader(response.GetResponseStream(), enc);
            //------ read ---------------
            //HttpContext.Current.Response.Write("5 ");
            result = ResponseStream.ReadToEnd();
            header = response.Headers;

            //HttpContext.Current.Response.Write("6 ");
            ResponseStream.Close();
            response.Close();
        }
        catch
        {            
        }
        //HttpContext.Current.Response.Write("<br>");
        return result;
    }
    //======================================================================
    public void DownloadToFile(string Url, string FileName)
    {
        string result = DownloadToMem(Url);

        FileStream file = new FileStream(FileName, FileMode.Create);
        file.Write(Encoding.GetEncoding(1252).GetBytes(result), 0, Encoding.GetEncoding(1252).GetByteCount(result));
        file.Close();
    }
}