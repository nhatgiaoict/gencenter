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
    public static DataTable GetAllMenu()
    {
        if ((DataTable)HttpContext.Current.Session["MenuNews"] == null)
        {
            Globals.xmltask.FileName = Globals.AbsData + "xmls/menu_news.xml";
            HttpContext.Current.Session["MenuNews"] = Globals.xmltask.Read();
        }
        return (DataTable)HttpContext.Current.Session["MenuNews"];
    }

    private static string _TableName
    {
        get { return "tbl_menu_" + Globals.CurrentLang; }
    }
    private int _GroupID = 0;
    public Menus(int Group)
    {
        _GroupID = Group;
        if (Globals.CheckExist)
        {
            try
            {
                //Create Table
                string Sql = "CREATE TABLE IF NOT EXISTS " + _TableName + "(";
                Sql += "groupid tinyint(4) NOT NULL default 0, ";
                Sql += "id varchar(255) NOT NULL default '', ";
                Sql += "idx tinyint(4) default 0, ";
                Sql += "PRIMARY KEY  (groupid, id)) ";
                Sql += "ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_bin;";
                DbTask db = new DbTask();
                db.ExecuteNonQuery(Sql);
            }
            catch { }
        }
    }
    public void RemoveFromMenu(string id)
    {
        DbTask db = new DbTask();
        int idx = GetIndex(id);
        string Sql = " DELETE FROM " + _TableName + " ";
        Sql += " WHERE groupid = " + _GroupID + " ";
        Sql += " AND id = '" + id.Trim() + "'";
        db.ExecuteNonQuery(Sql);
        Sql = " UPDATE " + _TableName + " SET idx = idx -1 ";
        Sql += " WHERE groupid = '" + _GroupID + "' ";
        Sql += " AND idx > '" + idx + "'";
        db.ExecuteNonQuery(Sql);
    }
    private int GetIndex(string id)
    {
        string Sql = " SELECT idx FROM " + _TableName + " ";
        Sql += " WHERE groupid = '" + _GroupID + "' ";
        Sql += " AND id = '" + id.Trim() + "' ";

        DbTask db = new DbTask();
        //return (int)db.ExecuteScalar(Sql);
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return 0;
        int re = Convert.ToInt32(dt.Rows[0]["idx"]);
        return re;

    }
    public DataTable GetNotInMenu(string ParentID)
    {
        string Sql = "SELECT a.* FROM " + Groups.TableName + " as a ";
        Sql += " WHERE a.id NOT IN(SELECT b.id FROM " + _TableName + " as b  WHERE b.groupid = " + _GroupID + ") ";

        if (ParentID.Length > 0)
            Sql += " AND a.parentid = '" + ParentID + "' ";
        else
            Sql += " AND a.parentid = '00' ";

        Sql += " ORDER BY idx ASC";
        DbTask db = new DbTask();
        return db.GetData(Sql);
    }
    public void AddToMenu(string id)
    {
        try
        {
            DbTask db = new DbTask();
            string Sql = string.Empty;
            Sql = " UPDATE " + _TableName + " SET idx = idx +1";
            db.ExecuteNonQuery(Sql);

            Sql = " INSERT INTO " + _TableName + "(groupid,id,idx) ";
            Sql += " VALUES (" + _GroupID + ",'" + id + "',1)";
            db.ExecuteNonQuery(Sql);
        }
        catch { }
    }
    public void UpdateIndexMenu(string id, int idx)
    {
        try
        {
            string Sql = "UPDATE " + _TableName + " ";
            Sql += " SET idx = " + idx + " ";
            Sql += " WHERE groupid = " + _GroupID + " ";
            Sql += " AND id = '" + id + "'";
            DbTask db = new DbTask();
            db.ExecuteNonQuery(Sql);
        }
        catch { }
    }

    public DataTable GetMenu()
    {
        string tbl_Group = Groups.TableName;
        string Sql = "SELECT " + tbl_Group + ".id AS id," + tbl_Group + ".title AS title, " + tbl_Group + ".summary AS summary, " + _TableName + ".idx AS idx ";
        Sql += " FROM " + tbl_Group + " INNER JOIN " + _TableName + " ON " + tbl_Group + ".id = " + _TableName + ".id ";
        Sql += " WHERE " + _TableName + ".groupid = " + _GroupID + " ";
        Sql += " ORDER BY " + _TableName + ".idx ASC";
        DbTask db = new DbTask();
        return db.GetData(Sql);

    }
}