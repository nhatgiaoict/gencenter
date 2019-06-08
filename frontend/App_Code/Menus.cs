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
/// Summary description for Menus
/// </summary>
public class Menus
{
    private static string _TableName
    {
        get { return "tbl_menu_" + Globals.CurrentLang; }
    }
    public static string TableName
    {
        get { return "tbl_group_" + Globals.CurrentLang; }
    }

    private int _GroupID = 0;
    /// <summary>
    /// 1 = menu top, 2 = tin tức sự kiện, 3 = năng lực thiết bị, 4 = Phải 1, 5 = menu chân trang
    /// </summary>
    /// <param name="Group"></param>
    public Menus(int Group)
    {
        _GroupID = Group;
    }
    public DataTable GetMenu()
    {
        DbTask dbtask = new DbTask();
        DataTable dt = null;
        if (clsCache.Get("tbl_Menuc" + _GroupID.ToString() + Globals.CurrentLang) != null)
        {
            dt = (DataTable)clsCache.Get("tbl_Menuc" + _GroupID.ToString() + Globals.CurrentLang);
        }
        else
        {
            string Sql = "Select a.id AS pid, a.col, a.summary AS summary, a.shortlink as shortlink, a.icon as icon,  a.fimages as fimage, a.title AS ptitle, a.parentid as pparentid, a.link as plink, a.idx as idx, a.kind as pkind,b.idx as bidx, ";
            Sql += "(SELECT COUNT(c.id) FROM " + TableName + " as c WHERE c.parentid = a.id) AS nextchild ";
            Sql += " FROM " + TableName + " as a , " + _TableName + " as b ";
            Sql += "Where b.groupid = '" + _GroupID + "' AND a.id = b.id ";
            Sql += "ORDER BY b.idx ASC";
            dt = dbtask.GetData(Sql);
            clsCache.Max("tbl_Menuc" + _GroupID.ToString() + Globals.CurrentLang, (object)dt);
        }
        return dt;
    }
    public DataTable GetChild_Home(string ParentID)
    {
        DbTask dbtask = new DbTask();
        string parent = (ParentID == null || ParentID == string.Empty || ParentID == "") ? "00" : ParentID;
        string Sql = "Select a.id AS pid, a.shortlink as shortlink, a.sluongtin, a.title AS ptitle, a.parentid as pparentid,a.fimages as pfimages, a.link as plink,a.kind as kind, a.idx as idx ";
        //Sql += "(SELECT COUNT(c.id) FROM " + TableName + " as c WHERE c.parentid = pid) AS nextchild ";
        Sql += " FROM " + TableName + " as a ";
        if (parent == "00")
        {
            Sql += " ," + _TableName + " as b  ";
        }

        Sql += " WHERE a.parentid = '" + parent + "'  AND a.status = 1 AND a.home = 1 ";

        if (parent == "00")
        {
            Sql += " AND b.groupid = '" + _GroupID + "' AND a.id = b.id ";
            Sql += "ORDER BY b.idx ASC";
        }
        if (parent != "00")
        {
            Sql += "ORDER BY a.idx ASC";
        }
        return dbtask.GetData(Sql);
    }
    public DataTable GetmenuLeftNHS(string ParentID)
    {
        DbTask dbtask = new DbTask();
        string parent = (ParentID == null || ParentID == string.Empty || ParentID == "") ? "" : ParentID;
        string Sql = "Select a.id AS pid, a.title AS ptitle, a.parentid as pparentid,a.fimages as pfimages, a.link as plink,a.kind as kind, a.idx as idx ";
        Sql += " FROM " + TableName + " as a ";
        if (parent == "00")
        {
            Sql += " ," + _TableName + " as b  ";
        }

        Sql += " WHERE a.parentid = '" + parent + "'  AND a.status = 1 AND a.home = 1 ";

        if (parent == "00")
        {
            Sql += " AND b.groupid = '" + _GroupID + "' AND a.id = b.id ";
            Sql += "ORDER BY b.idx ASC";
        }
        if (parent != "00")
        {
            Sql += "ORDER BY a.idx ASC";
        }

        return dbtask.GetData(Sql);
    }
    public DataTable GetChild_MenuLeft(string ParentID)
    {
        DbTask dbtask = new DbTask();
        string parent = (ParentID == null || ParentID == string.Empty || ParentID == "") ? "00" : ParentID;
        string Sql = "Select a.id AS pid, a.shortlink, a.title AS ptitle, a.parentid as pparentid, a.link as plink,a.kind as pkind, a.idx as idx, ";
        Sql += "(SELECT COUNT(c.id) FROM " + TableName + " as c WHERE c.parentid = a.id) AS nextchild ";
        Sql += " FROM " + TableName + " as a ";
        if (parent == "00")
        {
            Sql += " ," + _TableName + " as b  ";
        }
        Sql += " WHERE a.parentid = '" + parent + "'  AND a.status = 1 AND a.home = 1 ";
        if (parent == "00")
        {
            Sql += " AND b.groupid = '" + _GroupID + "' AND a.id = b.id ";
            Sql += "ORDER BY b.idx ASC";
        }
        if (parent != "00")
        {
            Sql += "ORDER BY a.idx ASC";
        }
        return dbtask.GetData(Sql);
    }
    // check child homme; 
    public bool CheckChildHome(string ParentID)
    {
        DbTask db = new DbTask();
        string sql = "Select id from " + TableName + " ";
        sql += "Where parentid = '" + ParentID + "' ";
        DataTable dt = db.GetData(sql);
        if (dt.Rows.Count > 0)
            return true;
        else
            return false;
    }
    public DataTable GetChild(string ParentID)
    {
        DbTask dbtask = new DbTask();
        string parent = (ParentID == null || ParentID == string.Empty || ParentID == "") ? "00" : ParentID;
        string Sql = "Select a.id AS pid, a.col, a.shortlink, a.icon as icon, a.fimages, a.summary, a.title AS ptitle, a.parentid as pparentid, a.link as plink, a.idx as idx, ";
        Sql += "(SELECT COUNT(c.id) FROM " + TableName + " as c WHERE c.parentid = a.id) AS nextchild ";
        Sql += " FROM " + TableName + " as a ";
        if (parent == "00")
        {
            Sql += " ," + _TableName + " as b  ";
        }

        Sql += " WHERE a.parentid = '" + parent + "' AND a.status = 1  ";

        if (parent == "00")
        {
            Sql += " AND b.groupid = '1' AND a.id = b.id ";
            Sql += "ORDER BY b.idx ASC";
        }
        if (parent != "00")
        {
            Sql += "ORDER BY a.idx ASC";
        }

        return dbtask.GetData(Sql);
    }

    public DataTable GetChildMenu(string ParentID)
    {
        DbTask dbtask = new DbTask();
        string Sql = "Select a.id AS pid, a.col, a.shortlink, a.icon as icon, a.fimages, a.summary, a.title AS ptitle, a.parentid as pparentid, a.link as plink, a.idx as idx, ";
        Sql += "(SELECT COUNT(c.id) FROM " + TableName + " as c WHERE c.parentid = a.id) AS nextchild ";
        Sql += " FROM " + TableName + " as a ";
        Sql += " WHERE a.parentid = '" + ParentID + "' AND a.status = 1  ";
        Sql += "ORDER BY a.idx ASC";
        return dbtask.GetData(Sql);
    }

    public DataRow GetParent(string childID)
    {
        DbTask dbtask = new DbTask();
        string Sql = "select parentID from " + TableName + " where id='" + childID + "'";

        DataTable dt = dbtask.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
    /// <summary>
    /// Menu context
    /// </summary>
    /// <param name="childID"></param>
    /// <returns></returns>
    public DataTable GetMutil_Parent(string childID)
    {
        DbTask dbtask = new DbTask();
        string Sql = "select * from " + TableName + " where id='" + childID + "'";
        DataTable dt = dbtask.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        string _childID = dt.Rows[0]["parentid"].ToString();
        DataTable dtTmp = null;
        while (_childID != "00")
        {
            Sql = "select * from " + TableName + " where id='" + _childID + "'";
            dtTmp = dbtask.GetData(Sql);
            if (dtTmp != null && dtTmp.Rows.Count > 0)
            {
                dt.ImportRow(dtTmp.Rows[0]);
                _childID = dtTmp.Rows[0]["parentid"].ToString();
            }
            else
                break;
        }
        // return dt; // ve xuoi
        // Ve nguoc
        DataTable dtTmp1 = null;
        dt.AcceptChanges();
        dtTmp1 = dt.Clone(); //Copy structure
        int n = dt.Rows.Count;
        for (int i = n - 1; i >= 0; i--)
        {
            dtTmp1.ImportRow(dt.Rows[i]);
        }
        dtTmp1.AcceptChanges();
        return dtTmp1;
    }
    public DataTable GetTopMenuRight_Home()
    {
        string tbl_Group = Groups.TableName;
        string Sql = "SELECT " + tbl_Group + ".id AS id," + tbl_Group + ".title AS title," + tbl_Group + ".link AS link, " + tbl_Group + ".summary AS summary, " + _TableName + ".idx AS idx, " + tbl_Group + ".kind AS kind ";
        Sql += "FROM " + tbl_Group + " INNER JOIN " + _TableName + " ON " + tbl_Group + ".id = " + _TableName + ".id ";
        Sql += "WHERE " + tbl_Group + ".status = 1 AND  " + tbl_Group + ".home = 1 ";
        Sql += "AND " + _TableName + ".groupid = " + _GroupID + " ";
        Sql += "ORDER BY " + _TableName + ".idx ASC";

        DbTask db = new DbTask();
        return db.GetData(Sql);
    }
    public DataTable GetTopMenuRight()
    {
        string tbl_Group = Groups.TableName;
        string Sql = "SELECT " + tbl_Group + ".id AS id," + tbl_Group + ".title AS title," + tbl_Group + ".link AS link, " + tbl_Group + ".summary AS summary, " + _TableName + ".idx AS idx, " + tbl_Group + ".kind AS kind ";
        Sql += "FROM " + tbl_Group + " INNER JOIN " + _TableName + " ON " + tbl_Group + ".id = " + _TableName + ".id ";
        Sql += "WHERE " + tbl_Group + ".status = 1 ";
        Sql += "AND " + _TableName + ".groupid = " + _GroupID + " ";
        Sql += "ORDER BY " + _TableName + ".idx ASC";

        DbTask db = new DbTask();
        return db.GetData(Sql);
    }
    public DataTable GetMenuTop()
    {
        string tbl_Group = Groups.TableName;
        string Sql = "SELECT " + tbl_Group + ".id AS id," + tbl_Group + ".title AS title," + tbl_Group + ".link AS link, " + tbl_Group + ".kind AS kind," + tbl_Group + ".summary AS summary, " + _TableName + ".idx AS idx, " + tbl_Group + ".kind AS kind ";
        Sql += "FROM " + tbl_Group + " INNER JOIN " + _TableName + " ON " + tbl_Group + ".id = " + _TableName + ".id ";
        Sql += "WHERE " + tbl_Group + ".status = 1 ";
        Sql += "AND " + _TableName + ".groupid = " + _GroupID + " ";
        Sql += "ORDER BY " + _TableName + ".idx ASC";
        DbTask db = new DbTask();
        return db.GetData(Sql);
    }
    public DataTable GetMenu_Left()
    {
        string tbl_Group = Groups.TableName;
        string Sql = "SELECT " + tbl_Group + ".id AS id," + tbl_Group + ".title AS title," + tbl_Group + ".link AS link, " + tbl_Group + ".summary AS summary, " + _TableName + ".idx AS idx, " + tbl_Group + ".kind AS kind ";
        Sql += "FROM " + tbl_Group + " INNER JOIN " + _TableName + " ON " + tbl_Group + ".id = " + _TableName + ".id ";
        Sql += "WHERE " + tbl_Group + ".status = 1 ";
        Sql += "AND " + _TableName + ".groupid = 1 ";
        Sql += "ORDER BY " + tbl_Group + ".idx ASC";
        DbTask db = new DbTask();
        return db.GetData(Sql);
    }
    public DataTable GetBottomMenu()
    {
        string tbl_Group = Groups.TableName;
        string Sql = "SELECT " + tbl_Group + ".id AS id," + tbl_Group + ".title AS title," + tbl_Group + ".link AS link, " + tbl_Group + ".summary AS summary, " + _TableName + ".idx AS idx, " + tbl_Group + ".kind AS kind ";
        Sql += "FROM " + tbl_Group + " INNER JOIN " + _TableName + " ON " + tbl_Group + ".id = " + _TableName + ".id ";
        Sql += "WHERE " + tbl_Group + ".status = 1 AND  " + tbl_Group + ".home = 1 ";
        Sql += "AND " + _TableName + ".groupid = " + _GroupID + " ";
        Sql += "ORDER BY " + _TableName + ".idx ASC";
        DbTask db = new DbTask();
        return db.GetData(Sql);
    }
    public DataRow GetTitle_Menu(string groupid)
    {
        DbTask dbtask = new DbTask();
        string Sql = "select title from " + TableName + " where id='" + groupid + "'";

        DataTable dt = dbtask.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }

    public DataTable GetGroupForSearch()
    {
        DbTask db = new DbTask();
        string Sql = "Select title, id from " + TableName + " where parentid = 00 And kind = 1";
        return db.GetData(Sql);
    }

    public DataTable GetNotNull(string ParentID)
    {
        DbTask dbtask = new DbTask();
        string parent = (ParentID == null || ParentID == string.Empty || ParentID == "") ? "00" : ParentID;
        string Sql = "Select a.id AS pid, a.col, a.shortlink, a.icon as icon, a.fimages, a.summary, a.title AS ptitle, a.parentid as pparentid, a.link as plink, a.idx as idx, ";
        Sql += "(SELECT COUNT(c.id) FROM " + TableName + " as c WHERE c.parentid = a.id) AS nextchild ";
        Sql += " FROM " + TableName + " as a ";
        if (parent == "00")
        {
            Sql += " ," + _TableName + " as b  ";
        }

        Sql += " WHERE a.parentid = '" + parent + "' AND a.status = 1  ";

        if (parent == "00")
        {
            Sql += " AND b.groupid = '1' AND a.id = b.id ";
            Sql += "ORDER BY b.idx ASC";
        }
        if (parent != "00")
        {
            Sql += "ORDER BY a.idx ASC";
        }

        return dbtask.GetData(Sql);
    }
}
