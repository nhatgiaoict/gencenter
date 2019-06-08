using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;

/// <summary>
/// Summary description for CommanUrl
/// </summary>
namespace CMDU
{
    public class CommanUrl
    {
        public CommanUrl(){}
        public static string UrlGroup(string slink, string shortlink)
        {
            string sresult = "";
            if (slink.Length > 7) { sresult = slink; }
            else
            {
                sresult = Globals.UrlHost + shortlink + "/";
            }
            return sresult;
        }
        public static string UrlDetail(string shortlink)
        {
            return Globals.UrlHost + shortlink + ".html";
        }
        public static string SubString(string Vao, int N)
        {
            string Tem = string.Empty;
            if (Vao.Length > N)
            {
                Tem = Vao.Substring(0, N);
                Tem = Vao.Substring(0, Tem.LastIndexOf(" ")) + "..";
            }
            else
            { Tem = Vao; }
            return Tem;
        }
        public static string GetUrlCart(string id)
        {
            return Globals.UrlHost + id;
        }
        public static string GetUrlCartQuanty(string id, string soluong)
        {
            return Globals.UrlHost + "Card.aspx?card=" + id + "&soluong=" + soluong + "";
        }
        //public static string GetUrlGroup(string slink, string groupid, string title, string kind)
        //{
        //    string sresult = "";
        //    if (slink.Length > 7) { sresult = slink; }
        //    else
        //    {
        //        if (kind == "1")
        //        {
        //            sresult = Globals.UrlRoot + "vanban/" + groupid + "/" + AutoTag.UCS2_Convert(title).Trim().Replace(" ", "-");
        //        }
        //        else
        //        {
        //            sresult = Globals.UrlRoot + groupid + "/" + AutoTag.UCS2_Convert(title).Trim().Replace(" ", "-") + "/";
        //        }
        //    }
        //    return sresult;
        //}
        //public static string GetUrlDetailNew(string id, string groupid, string title)
        //{
        //    return Globals.UrlRoot + groupid + "-" + id + "-" + AutoTag.UCS2_Convert(title).Trim().Replace(" ", "-");
        //}
        //public static string GetUrlDetailVideo(string id, string groupid, string title)
        //{
        //    return Globals.UrlRoot + groupid + "-" + id + "-" + AutoTag.UCS2_Convert(title).Trim().Replace(" ", "-");
        //}
        //public static string GetUrlRSS(string Groupid)
        //{
        //    return Globals.UrlHot + Groupid + "/RSS.htm";
        //}
        //public static string GetUrlProductDetailRSS(string id, string groupid, string title)
        //{
        //    return Globals.UrlHot + AutoTag.UCS2_Convert(title).Trim().Replace(" ", "-") + "-" + groupid + "-" + id;
        //}
        //public static string GetUrlDetailSearch(string id, string groupid, string title, string kind)
        //{
        //    if (kind == "3")
        //    {
        //        return Globals.UrlRoot + groupid + "/" + id + "/" + AutoTag.UCS2_Convert(title).Trim().Replace(" ", "-");
        //    }
        //    else
        //    {
        //        return Globals.UrlRoot + groupid + "-" + id + "/" + AutoTag.UCS2_Convert(title).Trim().Replace(" ", "-");
        //    }
        //}
        //public static string GetUrlKeywords(string link, string title)
        //{
        //    string sresult = "";
        //    if (link.Length > 0)
        //    {
        //        sresult = link;
        //    }
        //    else
        //    {
        //        sresult = Globals.UrlHot + title.Replace(" ", "-").Replace("  ", "-") + "/tim.htm";
        //    }
        //    return sresult;
        //}
        //public static string GetUrlSearch(string keywords)
        //{
        //    return Globals.UrlHot + "/" + keywords.Replace(" ", "-").Replace("  ", "-") + "/tim.htm";
        //}


    }
}