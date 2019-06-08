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
/// Summary description for Groups
/// </summary>
public class Groups
{
    public static int _nLength = 2;
    public static int vsohang = 0;
    
    public static string TableName
    {
        get { return "tbl_group_" + Globals.CurrentLang; }
    }
    public static string TableMenu
    {
        get { return "tbl_menu_" + Globals.CurrentLang; }
    }
    public static string TableNews
    {
        get { return "tbl_news_" + Globals.CurrentLang; }
    }
    public static string TablePublish
    {
        get { return "tbl_publish_" + Globals.CurrentLang; }
    }
    public static string TableTABnews
    {
        get { return "tbl_tapnews_" + Globals.CurrentLang; }
    }
    public static string TableTochuc
    {
        get { return "tbl_tochuctap_" + Globals.CurrentLang; }
    }    

    public Groups()
    {
    }
    public bool NextChild(string parentid)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT * FROM " + TableName + " ";
        Sql += "WHERE parentid = '" + parentid.Trim() + "' ";
        DataTable tb = db.GetData(Sql);
        if (tb == null || tb.Rows.Count == 0)
            return false;
        return true;
    }
    public DataRow GetImages(string id)
    {
        DbTask db = new DbTask();
        if (id == string.Empty || id == "")
            return null;
        string Sql = "SELECT * FROM " + TableName + " WHERE id = '" + id + "' ";
        DataTable dt = db.GetData(Sql);
        if (dt != null || dt.Rows.Count >= 0)
            return dt.Rows[0];
            return null;
   
    }
    public bool NextChildCap1(string parentid)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT * FROM " + TableName + " ";
        Sql += "WHERE id = '" + parentid.Trim() + "' ";
        DataTable tb = db.GetData(Sql);
        if (tb == null || tb.Rows.Count == 0)
            return false;
        return true;
    }
    public DataRow GetInfo(string id)
    {
        DbTask db = new DbTask();
        if (id == string.Empty || id == "")
            return null;
        string Sql = "SELECT * FROM " + TableName + " WHERE id = '" + id + "' ";
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }

    public DataRow GetInfoMenu()
    {
        DbTask db = new DbTask();

        string Sql = "SELECT distinct a.* , d.title as groupname FROM " + TableNews + "  as a," + TablePublish + " as b , " + TableMenu + " as c , " + TableName + " as d ";
        Sql += "WHERE a.status = 1 AND c.groupid ='3'  AND c.id = d.id AND a.groupid = d.id AND a.id = b.id ";
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
    //Tap
    public DataRow GetInfoMenu_Tab1(int ID)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT distinct a.*,c.tabname as tabname FROM " + TableNews + "  as a," + TableTochuc + " as b , " + TableTABnews + " as c  ";
        Sql += " WHERE a.status = 1 AND c.id =" + ID + "  AND c.id = b.idtab AND a.id = b.idnew  ";
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
    //No Content Tab1
    public DataRow GetName_Tab1(int ID)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT * from " + TableTABnews + " Where id =" + ID + " ";
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
    public DataRow GetInfoMenu_Tab2(int ID)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT distinct a.*,c.tabname as tabname FROM " + TableNews + "  as a," + TableTochuc + " as b , " + TableTABnews + " as c  ";
        Sql += " WHERE a.status = 1 AND c.id =" + ID + "  AND c.id = b.idtab AND a.id = b.idnew  ";
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
    //No Content Tab2
    public DataRow GetName_Tab2(int ID)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT * from " + TableTABnews + " Where id = " + ID + " ";
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
    public DataRow GetInfoMenu_Tab3(int ID)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT distinct a.*,c.tabname as tabname FROM " + TableNews + "  as a," + TableTochuc + " as b , " + TableTABnews + " as c  ";
        Sql += " WHERE a.status = 1 AND c.id =" + ID + "  AND c.id = b.idtab AND a.id = b.idnew  ";
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
    //No Content Tab3
    public DataRow GetName_Tab3(int ID)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT * from " + TableTABnews + " Where id = " + ID + " ";
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
    public DataRow GetInfoMenu_Tab4(int ID)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT distinct a.*,c.tabname as tabname FROM " + TableNews + "  as a," + TableTochuc + " as b , " + TableTABnews + " as c  ";
        Sql += " WHERE a.status = 1 AND c.id =" + ID + "  AND c.id = b.idtab AND a.id = b.idnew  ";
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
    //No Content Tab4
    public DataRow GetName_Tab4(int ID)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT * from " + TableTABnews + " Where id = " + ID + " ";
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
    //End Tap
    public DataTable Searching(string Parent)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        string Sql = string.Empty;
        Sql = "SELECT * FROM " + TableName + " ";
        Sql += "WHERE  status = 1 ";
        int n = _nLength;
        if (Parent != string.Empty && Parent != "" && Parent != null)
        {
            n += Parent.Length;
            Sql += " AND parentid = '" + Parent + "' ";
        }
        //Sql += "AND CHAR_LENGTH(id) = " + n + " ";
        Sql += " ORDER BY idx ASC ";
        dt = db.GetData(Sql);
        return dt;
    }
    public DataTable GetSiblings(string GroupID)
    {
        string Sql = "SELECT * FROM " + TableName + " ";
        Sql += "WHERE status =1 ";
        Sql += "AND parentid ='" + GroupID + "' ";
        Sql += "ORDER BY idx ASC";
        DbTask db = new DbTask();
        return db.GetData(Sql);

    }
    public DataTable GetChild(string ParentID)
    {
        string Sql = "SELECT * FROM " + TableName + " ";
        Sql += "WHERE status =1 ";
        if (ParentID != null && ParentID != string.Empty && ParentID != "")
            Sql += "AND parentid = '" + ParentID + "' ";
        else
            Sql += "AND parentid = '00' ";
        Sql += "ORDER BY idx ASC";

        DbTask db = new DbTask();
        return db.GetData(Sql);
    }
    //Voi Root la kieu du lieu (id:varchar 01,0101,010101)
    public DataTable GetChild_SiteMap(string ParentID)
    {
        DbTask dbtask = new DbTask();
        string parent = (ParentID == null || ParentID == string.Empty || ParentID == "") ? "00" : ParentID;
        string Sql = "Select id AS pid, title AS ptitle, parentid as pparentid, link as plink  ";
        //Sql += "(SELECT COUNT(id) FROM " + TableName + " WHERE parentid = pid) AS nextchild ";
        Sql += "FROM " + TableName + " WHERE parentid = '" + parent + "' ORDER BY idx ";
        return dbtask.GetData(Sql);
    }

    public DataRow GetInfoByShortLink(string shortlink)
    {
        DbTask db = new DbTask();
        string SQL = "Select title, summary, fimages, keywords, titlemeta, Description, kind, id From " + TableName + " where shortlink = '" + shortlink + "'";
        DataTable dt = db.GetData(SQL);
        if (dt.Rows.Count > 0 && dt != null)
            return dt.Rows[0];
        return null;
    }
    public string GetCountSPbyNSXandGroupID(string idnsx, string groupid)
    {
        DbTask db = new DbTask();
        string Sql = "select Count(id) as Tong from tbl_news_vn where idnhasanxuat = '" + idnsx + "' AND groupid = '" + groupid + "' AND status = 1";
        DataTable dt = db.GetData(Sql);
        if (dt.Rows.Count > 0 && dt != null)
            return dt.Rows[0]["tong"].ToString();
        return string.Empty;
    }
}
