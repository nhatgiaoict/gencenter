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
/// Summary description for AdvertOnline
/// </summary>
public class AdvertOnline
{
    private static string _TableName
    {
        get
        {
            return "tbl_advertonline_" + Globals.CurrentLang;
        }
    }
    private string _GroupID = "00";
    private string _Col = "L";
    public AdvertOnline(string GroupID, string Col)
    {
        _GroupID = GroupID;
        _Col = Col;
    }
    private int GetIndex(string id)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT idx FROM " + _TableName + " ";
        Sql += "WHERE groupid = '" + _GroupID + "' ";
        Sql += "AND id = '" + id + "' ";
        Sql += "AND col = '" + _Col + "' ";
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return 0;
        return Convert.ToInt32(dt.Rows[0]["idx"]);
    }

    public DataTable GetAdvert()
    {
        string tbl_Advert = Advert.TableStore;
        string Sql = "SELECT " + tbl_Advert + ".id AS id, " + tbl_Advert + ".linkurl, " + tbl_Advert + ".title AS title," + tbl_Advert + ".filename AS filename, " + tbl_Advert + ".summary AS summary, " + _TableName + ".startdate AS startdate, " + _TableName + ".enddate AS enddate, " + _TableName + ".idx AS idx FROM " + tbl_Advert + " INNER JOIN " + _TableName + " ON " + tbl_Advert + ".id = " + _TableName + ".id ";
        Sql += "WHERE " + tbl_Advert + ".status = 1 ";
        Sql += "AND " + _TableName + ".groupid = '" + _GroupID + "' ";
        Sql += "AND " + _TableName + ".col = '" + _Col + "' ";
        Sql += "ORDER BY " + _TableName + ".idx ASC," + tbl_Advert + ".created DESC ";
        DbTask db = new DbTask();
        return db.GetData(Sql);
    }

    public DataTable GetAdvertIMG()
    {
        string tbl_Advert = Advert.TableStore;
        string Sql = "SELECT " + tbl_Advert + ".id AS id, " + tbl_Advert + ".linkurl, " + tbl_Advert + ".title AS title," + tbl_Advert + ".filename AS filename, " + tbl_Advert + ".summary AS summary, " + _TableName + ".startdate AS startdate, " + _TableName + ".enddate AS enddate, " + _TableName + ".idx AS idx FROM " + tbl_Advert + " INNER JOIN " + _TableName + " ON " + tbl_Advert + ".id = " + _TableName + ".id ";
        Sql += "WHERE " + tbl_Advert + ".status = 1 ";
        Sql += "AND " + _TableName + ".groupid = '" + _GroupID + "' ";
        Sql += "AND " + _TableName + ".col = '" + _Col + "' ";
        Sql += "ORDER BY " + _TableName + ".idx ASC," + tbl_Advert + ".created DESC ";
        DbTask db = new DbTask();
        return db.GetData(Sql);
    }
    //public DataTable GetAdvertbyIdx(int idx)
    //{
    //    string tbl_Advert = Advert.TableStore;
    //    string Sql = "SELECT " + tbl_Advert + ".id AS id, " + tbl_Advert + ".linkurl AS linkurl, " + tbl_Advert + ".title AS title," + tbl_Advert + ".filename AS filename, " + tbl_Advert + ".summary AS summary, " + _TableName + ".startdate AS startdate, " + _TableName + ".enddate AS enddate, " + _TableName + ".idx AS idx FROM " + tbl_Advert + " INNER JOIN " + _TableName + " ON " + tbl_Advert + ".id = " + _TableName + ".id ";
    //    Sql += "WHERE " + tbl_Advert + ".status = 1 ";
    //    Sql += "AND " + _TableName + ".groupid = '" + _GroupID + "' ";
    //    Sql += "AND " + _TableName + ".col = '" + _Col + "' ";
    //    Sql += "ORDER BY " + _TableName + ".idx ASC," + tbl_Advert + ".created DESC ";
    //    DbTask db = new DbTask();
    //    return db.GetData(Sql);
    //}
}
