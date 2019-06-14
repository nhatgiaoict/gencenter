using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for Paging
/// </summary>
public class Paging
{
	public Paging()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string PhanTrang(int curpage, int recordinpage, int recordcount)
    {
        string str = "";
        int start = 1;
        int end = 0;
        int k = recordcount / recordinpage;
        //kiem tra co sang trang ko
        int div = recordcount % recordinpage;
        if (div != 0)
        {
            k = k + 1;
        }
        else
        {
            k = recordcount / recordinpage;
        }

        if (curpage <= 10)
        {
            start = 1;
        }
        else if ((curpage % 10) == 0)
        {
            start = curpage - 9;
        }
        else
        {
            start = curpage - (curpage % 10) + 1;
        }
        end = start + 9;
        if (end > k)
        {
            end = k + 1;
        }
        // trang hien tai
        string pageaspx = "";
        string strQueryString = "";
        string pQueryPara = "";
        for (int i = 0; i < HttpContext.Current.Request.QueryString.Count; i++)
        {
            if (HttpContext.Current.Request.QueryString.Keys[i] != "page")
            {
                strQueryString = strQueryString + "&";
                pQueryPara = HttpContext.Current.Request.QueryString.Keys[i];
                strQueryString = strQueryString + HttpContext.Current.Server.UrlEncode(pQueryPara) + "=";
                strQueryString = strQueryString + HttpContext.Current.Server.UrlEncode(HttpContext.Current.Request.QueryString[pQueryPara].ToString());
            }
        }
        if (strQueryString.Length != 0)
        {
            strQueryString = "?" + strQueryString.Substring(1) + "&";//            
        }
        else
        {
            strQueryString = "?";
        }
        //pageaspx = HttpContext.Current.Request.CurrentExecutionFilePath + strQueryString;
        //nếu phân trang thì dùng cái này còn ko thì dùng cái trên
        pageaspx = HttpContext.Current.Request.CurrentExecutionFilePath + "?";
        //phan trang
        if (recordcount > recordinpage)
        {
            //dau tien
            //if (curpage != 1)
            //{
            //    int l = 1;
            //    str += "<li><a class='' href=\"" + pageaspx + "";
            //    str += "page=" + l.ToString();
            //    str += "\">First</a></li>";
            //}

            ////dau
            //if (curpage != 1)
            //{
            //    int l = 0;
            //    l = curpage - 1;
            //    str += "<li><a class='' href=\"" + pageaspx + "";
            //    str += "page=" + l.ToString();
            //    //str += "\">" + Language.GetTextByID(900) + "</a>";
            //    str += "\">Previous</a></li>";
            //}

            //giua
            int middle = end;
            if ((end % 10) == 0)
            {
                middle = end + 1;
            }
            for (int i = start; i < middle; i++)
            {
                if (i == curpage)
                {
                    str += " <li><a class=\"current\">[" + i.ToString() + "]</a></li>";

                }
                else
                {
                    str += " <li><a class='' href=" + pageaspx + "page=" + i.ToString();
                    str += " > " + i.ToString() + "</a></li>";
                }
            }

            //cuoi
        //    if (curpage < k)
        //    {
        //        int l = 0;
        //        l = curpage + 1;
        //        str += "<li><a class='' href=\"" + pageaspx + "";
        //        str += "page=" + l.ToString();
        //        //str += "\">" + Language.GetTextByID(901) + "</a>";
        //        str += "\">Next</a></li>";
        //    }
        //    //cuoi cung
        //    if (curpage < k)
        //    {
        //        int l = 0;
        //        l = k;
        //        str += "<li> <a class='' href=\"" + pageaspx + "";
        //        str += "page=" + l.ToString();
        //        str += "\">Last</a></li>";
        //    }
        }
        else
        {
            str += "";
        }
        return str;
    }
    public string PhanTrangSearch(int curpage, int recordinpage, int recordcount)
    {
        string str = "";
        int start = 1;
        int end = 0;
        int k = recordcount / recordinpage;
        //kiem tra co sang trang ko
        int div = recordcount % recordinpage;
        if (div != 0)
        {
            k = k + 1;
        }
        else
        {
            k = recordcount / recordinpage;
        }

        if (curpage <= 10)
        {
            start = 1;
        }
        else if ((curpage % 10) == 0)
        {
            start = curpage - 9;
        }
        else
        {
            start = curpage - (curpage % 10) + 1;
        }
        end = start + 9;
        if (end > k)
        {
            end = k + 1;
        }
        // trang hien tai
        string pageaspx = "";
        string strQueryString = "";
        string pQueryPara = "";
        for (int i = 0; i < HttpContext.Current.Request.QueryString.Count; i++)
        {
            if (HttpContext.Current.Request.QueryString.Keys[i] != "page")
            {
                strQueryString = strQueryString + "&";
                pQueryPara = HttpContext.Current.Request.QueryString.Keys[i];
                strQueryString = strQueryString + HttpContext.Current.Server.UrlEncode(pQueryPara) + "=";
                strQueryString = strQueryString + HttpContext.Current.Server.UrlEncode(HttpContext.Current.Request.QueryString[pQueryPara].ToString());
            }
        }
        if (strQueryString.Length != 0)
        {
            strQueryString = "?" + strQueryString.Substring(1) + "&";//            
        }
        else
        {
            strQueryString = "?";
        }
        pageaspx = HttpContext.Current.Request.CurrentExecutionFilePath + strQueryString;
        //nếu phân trang thì dùng cái này còn ko thì dùng cái trên
        //pageaspx = HttpContext.Current.Request.CurrentExecutionFilePath + "?";
        //phan trang
        if (recordcount > recordinpage)
        {
            //dau tien
            if (curpage != 1)
            {
                int l = 1;
                str += "<a class='' href=\"" + pageaspx + "";
                str += "page=" + l.ToString();
                str += "\">First</a>";
            }

            //dau
            if (curpage != 1)
            {
                int l = 0;
                l = curpage - 1;
                str += "<a class='' href=\"" + pageaspx + "";
                str += "page=" + l.ToString();
                //str += "\">" + Language.GetTextByID(900) + "</a>";
                str += "\">Previous</a>";
            }

            //giua
            int middle = end;
            if ((end % 10) == 0)
            {
                middle = end + 1;
            }
            for (int i = start; i < middle; i++)
            {
                if (i == curpage)
                {
                    str += " <b>[" + i.ToString() + "]</b>";

                }
                else
                {
                    str += " <a class='' href=" + pageaspx + "page=" + i.ToString();
                    str += " > " + i.ToString() + "</a>";
                }
            }

            //cuoi
            if (curpage < k)
            {
                int l = 0;
                l = curpage + 1;
                str += " <a class='' href=\"" + pageaspx + "";
                str += "page=" + l.ToString();
                //str += "\">" + Language.GetTextByID(901) + "</a>";
                str += "\">Next</a>";
            }
            //cuoi cung
            if (curpage < k)
            {
                int l = 0;
                l = k;
                str += " <a class='' href=\"" + pageaspx + "";
                str += "page=" + l.ToString();
                str += "\">Last</a>";
            }
        }
        else
        {
            str += "";
        }
        return str;
    }
}
