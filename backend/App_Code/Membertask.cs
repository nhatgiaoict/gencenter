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
/// Summary description for Membertask
/// </summary>
public class Membertask
{
    private static string _Lang
    {
        get { return Globals.CurrentLang; }
    }
    public static string Name
    {
        get
        {
            return (HttpContext.Current.Session["UserName"] == null) ? string.Empty : HttpContext.Current.Session["UserName"].ToString();
            //return "hieudt";
        }
    }
    private static Memberinfo memberinfo = new Memberinfo();
    //======== Quan tri he thong =========
    public static bool IsAdministrator()
    {
        memberinfo.Name = Name;
        string sAdministrator = memberinfo.GetConfigItem("Administrator", "IsAdministrator_" + _Lang);
        if (sAdministrator == "true")
            return true;
        else
            return false;
    }
    public static bool IsAdministrator(string _mName)
    {
        memberinfo.Name = _mName;
        string sAdministrator = memberinfo.GetConfigItem("Administrator", "IsAdministrator_" + _Lang);
        if (sAdministrator == "true")
            return true;
        else
            return false;
    }
    //======== Thanh Vien  =========   
    public static string IsThanhvien()
    {
        memberinfo.Name = Name;
        string sTopicID = memberinfo.GetConfigItem("Thanhvien", "IsThanhvien_" + _Lang);
        if (sTopicID != "")
            return sTopicID;
        else
            return string.Empty;
    }
    public static string IsThanhvien(string _mName)
    {
        memberinfo.Name = _mName;
        string sTopicID = memberinfo.GetConfigItem("Thanhvien", "IsThanhvien_" + _Lang);
        if (sTopicID != "")
            return sTopicID;
        else
            return string.Empty;
    }
    //======== Chuyen muc  =========    
    public static string IsChuyenmuc()
    {
        memberinfo.Name = Name;
        string sTopicID = memberinfo.GetConfigItem("Chuyenmuc", "IsChuyenmuc_" + _Lang);
        if (sTopicID != "")
            return sTopicID;
        else
            return string.Empty;
    }
    public static string IsChuyenmuc(string _mName)
    {
        memberinfo.Name = _mName;
        string sTopicID = memberinfo.GetConfigItem("Chuyenmuc", "IsChuyenmuc_" + _Lang);
        if (sTopicID != "")
            return sTopicID;
        else
            return string.Empty;
    }
    //======== Tao noi dung =========
    public static string IsTaonoidung()
    {
        memberinfo.Name = Name;
        string sTopicID = memberinfo.GetConfigItem("Taonoidung", "IsTaonoidung_" + _Lang);
        if (sTopicID != "")
            return sTopicID;
        else
            return string.Empty;
    }
    public static string IsTaonoidung(string _mName)
    {
        memberinfo.Name = _mName;
        string sTopicID = memberinfo.GetConfigItem("Taonoidung", "IsTaonoidung_" + _Lang);
        if (sTopicID != "")
            return sTopicID;
        else
            return string.Empty;

    }
    //======== Bien tap Check Content ===========
    public static string IsCheckcontent()
    {
        memberinfo.Name = Name;
        string sTopicID = memberinfo.GetConfigItem("Checkcontent", "IsCheckcontent_" + _Lang);
        if (sTopicID != "")
            return sTopicID;
        else
            return string.Empty;
    }

    public static string IsCheckcontent(string _mName)
    {
        memberinfo.Name = _mName;
        string sTopicID = memberinfo.GetConfigItem("Checkcontent", "IsCheckcontent_" + _Lang);
        if (sTopicID != "")
            return sTopicID;
        else
            return string.Empty;
    }
    //======== Quan tri  Noi dung ==========
    public static string IsNewsTask()
    {
        memberinfo.Name = Name;
        string sTopicID = memberinfo.GetConfigItem("News", "IsNews_" + _Lang);
        if (sTopicID != "")
            return sTopicID;
        else
            return string.Empty;
    }
    public static string IsNewsTask(string _mName)
    {
        memberinfo.Name = _mName;
        string sTopicID = memberinfo.GetConfigItem("News", "IsNews_" + _Lang);
        if (sTopicID != "")
            return sTopicID;
        else
            return string.Empty;
    }
    //======== To Chuc  Tin Tuc ==========
    public static string IsTochuc()
    {
        memberinfo.Name = Name;
        string sTopicID = memberinfo.GetConfigItem("Tochuc", "IsTochuc_" + _Lang);
        if (sTopicID != "")
            return sTopicID;
        else
            return string.Empty;
    }
    public static string IsTochuc(string _mName)
    {
        memberinfo.Name = _mName;
        string sTopicID = memberinfo.GetConfigItem("Tochuc", "IsTochuc_" + _Lang);
        if (sTopicID != "")
            return sTopicID;
        else
            return string.Empty;
    }
    //======== Quan tri Don Vi Thanh vien ==========
    public static string IsDonViThanhVien()
    {
        memberinfo.Name = Name;
        string sTopicID = memberinfo.GetConfigItem("DonViThanhVien", "IsDonViThanhVien_" + _Lang);
        if (sTopicID != "")
            return sTopicID;
        else
            return string.Empty;
    }
    public static string IsDonViThanhVien(string _mName)
    {
        memberinfo.Name = _mName;
        string sTopicID = memberinfo.GetConfigItem("DonViThanhVien", "IsDonViThanhVien_" + _Lang);
        if (sTopicID != "")
            return sTopicID;
        else
            return string.Empty;
    }
    //======== Quan tri Gui bai Dang ngay ==========
    public static string IsPostOn()
    {
        memberinfo.Name = Name;
        string sTopicID = memberinfo.GetConfigItem("PostOn", "IsPostOn_" + _Lang);
        if (sTopicID != "")
            return sTopicID;
        else
            return string.Empty;
    }
    public static string IsPostOn(string _mName)
    {
        memberinfo.Name = _mName;
        string sTopicID = memberinfo.GetConfigItem("PostOn", "IsPostOn_" + _Lang);
        if (sTopicID != "")
            return sTopicID;
        else
            return string.Empty;
    }
    //======== Quan tri TAB ==========
    public static string IsTAB()
    {
        memberinfo.Name = Name;
        string sTopicID = memberinfo.GetConfigItem("TAB", "IsTAB_" + _Lang);
        if (sTopicID != "")
            return sTopicID;
        else
            return string.Empty;
    }
    public static string IsTAB(string _mName)
    {
        memberinfo.Name = _mName;
        string sTopicID = memberinfo.GetConfigItem("TAB", "IsTAB_" + _Lang);
        if (sTopicID != "")
            return sTopicID;
        else
            return string.Empty;
    }
    //======== Quan tri Quang cao ==========
    public static string IsAdvert()
    {
        memberinfo.Name = Name;
        string sTopicID = memberinfo.GetConfigItem("Advert", "IsAdvert_" + _Lang);
        if (sTopicID != "")
            return sTopicID;
        else
            return string.Empty;
    }
    public static string IsAdvert(string _mName)
    {
        memberinfo.Name = _mName;
        string sTopicID = memberinfo.GetConfigItem("Advert", "IsAdvert_" + _Lang);
        if (sTopicID != "")
            return sTopicID;
        else
            return string.Empty;
    }
    
}
