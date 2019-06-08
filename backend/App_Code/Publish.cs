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
/// Summary description for Publish
/// </summary>
public class Publish
{
    private static string _TableName
    {
        get { return "tbl_publish_" + Globals.CurrentLang; }
    }
    private static string TableName_MoiNhat
    {
        get { return "tbl_noibat_home_" + Globals.CurrentLang; }
    }
    public static string TableName_MoiNhatNews
    {
        get { return "tbl_publish_moinhat_" + Globals.CurrentLang; }
    }

    public static string TableName_Ykienkhachhang
    {
        get { return "tbl_publish_technical_" + Globals.CurrentLang; }
    }

    private string _GroupID = string.Empty;

    public Publish()
    {

    }
    public Publish(string GroupID)
    {
        _GroupID = GroupID;
        try
        {
            //Create Table
            string Sql = "CREATE TABLE IF NOT EXISTS " + _TableName + "(";
            Sql += "groupid nvarchar(255) NOT NULL default '', ";
            Sql += "id nvarchar(10) NOT NULL default '', ";
            Sql += "idx tinyint(4) default 0, ";
            Sql += "PRIMARY KEY  (groupid, id)) ";
            Sql += "ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_bin;";
            DbTask db = new DbTask();
            db.ExecuteNonQuery(Sql);
        }
        catch { }
    }
    public void AddToPublish(string id)
    {
        try
        {
            DbTask db = new DbTask();
          
            string Sql = string.Empty;
            /*
          Sql = "UPDATE " + _TableName + " SET idx = idx +1 ";
          db.ExecuteNonQuery(Sql);
          */
            Sql = "INSERT INTO " + _TableName + "(groupid, id,idx) ";
            Sql += "VALUES ('" + _GroupID + "','" + id + "',1)";
            db.ExecuteNonQuery(Sql);
        }
        catch { }
    }
    private int GetIndex(string id)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT idx FROM " + _TableName + " ";
        Sql += "WHERE groupid = '" + _GroupID + "' ";
        Sql += "AND id = '" + id + "' ";
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return 0;
        return Convert.ToInt32(dt.Rows[0]["idx"]);
    }
    public void RemoveFromPublish(string id)
    {
        try
        {
            DbTask db = new DbTask();
            int idx = GetIndex(id);
            string Sql = "DELETE FROM " + _TableName + " ";
            Sql += "WHERE groupid = '" + _GroupID + "' ";
            Sql += "AND id = '" + id + "' ";
            db.ExecuteNonQuery(Sql);

            Sql = "UPDATE " + _TableName + " SET idx = idx -1 ";
            Sql += "WHERE groupid = '" + _GroupID + "' ";
            Sql += "AND idx > '" + idx + "'";
            db.ExecuteNonQuery(Sql);
        }
        catch { }
    }
    public void UpdateIndex(string id, int idx)
    {
        try
        {
            DbTask db = new DbTask();
            string Sql = "UPDATE " + _TableName + " ";
            Sql += "SET idx = " + idx + " ";
            Sql += "WHERE groupid = '" + _GroupID + "' ";
            Sql += "AND id = '" + id + "' ";
            db.ExecuteNonQuery(Sql);
        }
        catch { }
    }

    public DataTable GetPublish()
    {
        DbTask db = new DbTask();
        string tbl_News = News.TableName;
        string Sql = "SELECT " + tbl_News + ".id AS id," + tbl_News + ".title AS title, " + _TableName + ".idx AS idx FROM " + tbl_News + " INNER JOIN " + _TableName + " ON " + tbl_News + ".id = " + _TableName + ".id ";
        Sql += "WHERE " + tbl_News + ".status = 1 ";
        Sql += "AND " + _TableName + ".groupid = '" + _GroupID + "' ";
        if (_GroupID != "00")
        {
            Sql += "AND " + tbl_News + ".groupid LIKE '" + _GroupID + "%' ";
        }
        Sql += "ORDER BY " + _TableName + ".idx ASC, " + tbl_News + ".created DESC ";
        return db.GetData(Sql);
    }
    public DataTable GetNotInPublish(int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages)
    {
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;
        DbTask db = new DbTask();
        string tbl_news = News.TableName;
        string Sql = string.Empty;
        Sql = "SELECT COUNT(a.id) AS Total FROM " + tbl_news + " as a ";
        Sql += "WHERE a.id NOT IN(SELECT id FROM " + _TableName + " WHERE groupid = '" + _GroupID + "') ";
        Sql += "AND status =1 ";
        if (_GroupID != "00")
        {
            Sql += "AND a.groupid IN ( '" + _GroupID + "') ";
        }
        dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;

        TotalRecords = Convert.ToInt32(dt.Rows[0]["Total"]);
        TotalPages = (int)TotalRecords / RecordPerPages;
        if ((int)TotalRecords % RecordPerPages > 0)
            TotalPages = TotalPages + 1;

        Sql = "SELECT " + tbl_news + ".*, row_number() over (order by created DESC) as row_index INTO #Temp_Table ";
        Sql += "FROM " + tbl_news + "  ";
        Sql += "WHERE id NOT IN(SELECT id FROM " + _TableName + " WHERE groupid = '" + _GroupID + "') ";
        Sql += "AND status =1 ";
        if (_GroupID != "00")
        {
            Sql += "AND groupid IN( '" + _GroupID + "') ";
        }
        int nS = RecordPerPages * (CurrentPage - 1);
        int record_next = nS + RecordPerPages;
        Sql += "Select * from #Temp_Table ";
        if (nS == 0)
            Sql += " WHERE (row_index >=" + nS + ") AND (row_index <=" + record_next + ")";
        else
            Sql += " WHERE (row_index >" + nS + ") AND (row_index <=" + record_next + ")";
        Sql += " ORDER BY row_index ASC ";
        return db.GetData(Sql);
    }
    //Moi Nhat
    public DataTable GetPublish_MoiNhat()
    {
        DbTask db = new DbTask();
        string tbl_News = News.TableName;
        string Sql = "SELECT " + tbl_News + ".id AS id," + tbl_News + ".title AS title, " + TableName_MoiNhat + ".idx AS idx FROM " + tbl_News + " INNER JOIN " + TableName_MoiNhat + " ON " + tbl_News + ".id = " + TableName_MoiNhat + ".id ";
        Sql += "WHERE " + tbl_News + ".status = 1 AND " + tbl_News + ".kind = 0 ";
        Sql += "ORDER BY " + TableName_MoiNhat + ".idx ASC, " + tbl_News + ".created DESC ";
        return db.GetData(Sql);
    }
    public DataTable GetNotInPublish_MoiNhat(string Keyword, string GroupID, int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages)
    {
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;
        DbTask db = new DbTask();
        string tbl_news = News.TableName;
        string Sql = string.Empty;
        Sql = "SELECT COUNT(a.id) AS Total FROM " + tbl_news + " as a ";
        Sql += "WHERE a.id NOT IN(SELECT id FROM " + TableName_MoiNhat + ")";
        Sql += " AND a.status = 1 AND a.kind = 0 ";
        if (GroupID != null && GroupID != string.Empty && GroupID != "")
        {
            Sql += " AND groupid IN (" + GroupID + ") ";
        }
        if (Keyword != string.Empty && Keyword != null && Keyword != "")
        {
            string dau = Keyword.Trim().Substring(0, 1);
            string sau = Keyword.Trim().Substring(1);
            string abc = dau.ToUpper() + sau.ToLower();
            Sql += "AND( title LIKE '%" + Keyword.Trim() + "%' OR title LIKE '%" + Keyword.Trim().ToLower() + "%' OR title LIKE '%" + Keyword.Trim().ToUpper() + "%' OR  title LIKE '%" + abc + "%' ";
            Sql += "OR summary LIKE '%" + Keyword.Trim() + "%' OR summary LIKE '%" + Keyword.Trim().ToUpper() + "%' OR summary LIKE '%" + Keyword.Trim().ToLower() + "%' OR  summary LIKE '%" + abc + "%') ";
        }

        dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;

        TotalRecords = Convert.ToInt32(dt.Rows[0]["Total"]);
        TotalPages = (int)TotalRecords / RecordPerPages;
        if ((int)TotalRecords % RecordPerPages > 0)
            TotalPages = TotalPages + 1;

        Sql = "SELECT " + tbl_news + ".*, row_number() over (order by created DESC) as row_index INTO #Temp_Table FROM " + tbl_news + " ";
        Sql += "WHERE id NOT IN(SELECT id FROM " + TableName_MoiNhat + ")";
        Sql += " AND status =1 AND kind = 0 ";
        if (GroupID != null && GroupID != string.Empty && GroupID != "")
        {
            Sql += " AND groupid IN (" + GroupID + ") ";
        }
        if (Keyword != string.Empty && Keyword != null && Keyword != "")
        {
            string dau = Keyword.Trim().Substring(0, 1);
            string sau = Keyword.Trim().Substring(1);
            string abc = dau.ToUpper() + sau.ToLower();
            Sql += "AND( title LIKE '%" + Keyword.Trim() + "%' OR title LIKE '%" + Keyword.Trim().ToLower() + "%' OR title LIKE '%" + Keyword.Trim().ToUpper() + "%' OR  title LIKE '%" + abc + "%' ";
            Sql += "OR summary LIKE '%" + Keyword.Trim() + "%' OR summary LIKE '%" + Keyword.Trim().ToUpper() + "%' OR summary LIKE '%" + Keyword.Trim().ToLower() + "%' OR  summary LIKE '%" + abc + "%') ";
        }
        int nS = RecordPerPages * (CurrentPage - 1);
        int record_next = nS + RecordPerPages;
        Sql += "Select * from #Temp_Table ";
        if (nS == 0)
            Sql += " WHERE (row_index >=" + nS + ") AND (row_index <=" + record_next + ")";
        else
            Sql += " WHERE (row_index >" + nS + ") AND (row_index <=" + record_next + ")";
        Sql += " ORDER BY row_index ASC ";
        return db.GetData(Sql);
    }
    public void UpdateIndex_MoiNhat(string id, int idx)
    {
        try
        {
            DbTask db = new DbTask();
            string Sql = "UPDATE " + TableName_MoiNhat + " ";
            Sql += "SET idx = " + idx + " ";
            Sql += "WHERE id = '" + id + "' ";
            db.ExecuteNonQuery(Sql);
        }
        catch { }
    }
    public void RemoveFromPublish_MoiNhat(string id)
    {
        try
        {
            DbTask db = new DbTask();
            int idx = GetIndex(id);
            string Sql = "DELETE FROM " + TableName_MoiNhat + " ";
            Sql += "WHERE id = '" + id + "' ";
            db.ExecuteNonQuery(Sql);

            Sql = "UPDATE " + TableName_MoiNhat + " SET idx = idx -1 ";
            Sql += "WHERE id = '" + id + "' ";
            Sql += "AND idx > '" + idx + "'";
            db.ExecuteNonQuery(Sql);
        }
        catch { }
    }
    public void AddToPublish_MoiNhat(string id)
    {
        try
        {
            DbTask db = new DbTask();
            string Sql = string.Empty;
            Sql = "UPDATE " + TableName_MoiNhat + " SET idx = idx +1 ";
            db.ExecuteNonQuery(Sql);

            Sql = "INSERT INTO " + TableName_MoiNhat + "(groupid, id,idx) ";
            Sql += "VALUES ('0','" + id + "',1)";
            db.ExecuteNonQuery(Sql);
        }
        catch { }
    }
    //-------------------------------------
    //lay tin moi nhat cho trang chu tin tuc
    public DataTable GetPublish_MoiNhatNews()
    {
        DbTask db = new DbTask();
        string tbl_News = News.TableName;
        string Sql = "SELECT " + tbl_News + ".id AS id," + tbl_News + ".title AS title, " + TableName_MoiNhatNews + ".idx AS idx FROM " + tbl_News + " INNER JOIN " + TableName_MoiNhatNews + " ON " + tbl_News + ".id = " + TableName_MoiNhatNews + ".id ";
        Sql += "WHERE " + tbl_News + ".status = 1 ";
        Sql += "ORDER BY " + TableName_MoiNhatNews + ".idx ASC, " + tbl_News + ".created DESC ";
        return db.GetData(Sql);
    }
    //-----------------------------------------------------

    public DataTable GetPublish_Ykienkhachhang()
    {
        DbTask db = new DbTask();
        string tbl_News = News.TableName;
        string Sql = "SELECT " + tbl_News + ".id AS id," + tbl_News + ".title AS title, " + TableName_Ykienkhachhang + ".idx AS idx FROM " + tbl_News + " INNER JOIN " + TableName_Ykienkhachhang + " ON " + tbl_News + ".id = " + TableName_Ykienkhachhang + ".id ";
        Sql += "WHERE " + tbl_News + ".status = 1 ";
        Sql += "ORDER BY " + TableName_Ykienkhachhang + ".idx ASC, " + tbl_News + ".created DESC ";
        return db.GetData(Sql);
    }

    //lay nhung tin ko fai la tin moi nhat cua trang chu tin tuc

    public DataTable GetNotInPublish_MoiNhatNews(string Keyword, string GroupID, int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages)
    {
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;
        DbTask db = new DbTask();
        string tbl_news = News.TableName;
        string Sql = string.Empty;
        Sql = "SELECT COUNT(a.id) AS Total FROM " + tbl_news + " as a ";
        Sql += "WHERE a.id NOT IN(SELECT id FROM " + TableName_MoiNhatNews + ")";
        Sql += " AND status = 1";
        if (GroupID != null && GroupID != string.Empty && GroupID != "")
        {
            Sql += " AND groupid IN (" + GroupID + ") ";
        }
        if (Keyword != string.Empty && Keyword != null && Keyword != "")
        {
            string dau = Keyword.Trim().Substring(0, 1);
            string sau = Keyword.Trim().Substring(1);
            string abc = dau.ToUpper() + sau.ToLower();
            Sql += "AND( title LIKE N'%" + Keyword.Trim() + "%' OR title LIKE N'%" + Keyword.Trim().ToLower() + "%' OR title LIKE N'%" + Keyword.Trim().ToUpper() + "%' OR  title LIKE N'%" + abc + "%' ";
            Sql += "OR summary LIKE N'%" + Keyword.Trim() + "%' OR summary LIKE N'%" + Keyword.Trim().ToUpper() + "%' OR summary LIKE N'%" + Keyword.Trim().ToLower() + "%' OR  summary LIKE N'%" + abc + "%') ";
        }

        dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;

        TotalRecords = Convert.ToInt32(dt.Rows[0]["Total"]);
        TotalPages = (int)TotalRecords / RecordPerPages;
        if ((int)TotalRecords % RecordPerPages > 0)
            TotalPages = TotalPages + 1;

        Sql = "SELECT " + tbl_news + ".*, row_number() over (order by created DESC) as row_index INTO #Temp_Table FROM " + tbl_news + "  ";
        Sql += "WHERE id NOT IN(SELECT id FROM " + TableName_MoiNhatNews + ")";
        Sql += " AND status =1  ";
        if (GroupID != null && GroupID != string.Empty && GroupID != "")
        {
            Sql += " AND groupid IN (" + GroupID + ") ";
        }
        if (Keyword != string.Empty && Keyword != null && Keyword != "")
        {
            string dau = Keyword.Trim().Substring(0, 1);
            string sau = Keyword.Trim().Substring(1);
            string abc = dau.ToUpper() + sau.ToLower();
            Sql += "AND( title LIKE N'%" + Keyword.Trim() + "%' OR title LIKE N'%" + Keyword.Trim().ToLower() + "%' OR title LIKE N'%" + Keyword.Trim().ToUpper() + "%' OR  title LIKE N'%" + abc + "%' ";
            Sql += "OR summary LIKE N'%" + Keyword.Trim() + "%' OR summary LIKE N'%" + Keyword.Trim().ToUpper() + "%' OR summary LIKE N'%" + Keyword.Trim().ToLower() + "%' OR  summary LIKE N'%" + abc + "%') ";
        }
        int nS = RecordPerPages * (CurrentPage - 1);
        int record_next = nS + RecordPerPages;
        Sql += "Select * from #Temp_Table ";
        if (nS == 0)
            Sql += " WHERE (row_index >=" + nS + ") AND (row_index <=" + record_next + ")";
        else
            Sql += " WHERE (row_index >" + nS + ") AND (row_index <=" + record_next + ")";
        Sql += " ORDER BY row_index ASC ";
        return db.GetData(Sql);
    }

    // Lay tin y kien khach hang khong phai moi nhat
    public DataTable GetNotInPublish_Ykienkhachhang(string Keyword, string GroupID, int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages)
    {
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;
        DbTask db = new DbTask();
        string tbl_news = News.TableName;
        string Sql = string.Empty;
        Sql = "SELECT COUNT(a.id) AS Total FROM " + tbl_news + " as a ";
        Sql += "WHERE a.id NOT IN(SELECT id FROM " + TableName_Ykienkhachhang + ")";
        Sql += " AND status = 1";
        if (GroupID != null && GroupID != string.Empty && GroupID != "")
        {
            Sql += " AND groupid IN (" + GroupID + ") ";
        }
        if (Keyword != string.Empty && Keyword != null && Keyword != "")
        {
            string dau = Keyword.Trim().Substring(0, 1);
            string sau = Keyword.Trim().Substring(1);
            string abc = dau.ToUpper() + sau.ToLower();
            Sql += "AND( title LIKE N'%" + Keyword.Trim() + "%' OR title LIKE N'%" + Keyword.Trim().ToLower() + "%' OR title LIKE N'%" + Keyword.Trim().ToUpper() + "%' OR  title LIKE N'%" + abc + "%' ";
            Sql += "OR summary LIKE N'%" + Keyword.Trim() + "%' OR summary LIKE N'%" + Keyword.Trim().ToUpper() + "%' OR summary LIKE N'%" + Keyword.Trim().ToLower() + "%' OR  summary LIKE N'%" + abc + "%') ";
        }

        dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;

        TotalRecords = Convert.ToInt32(dt.Rows[0]["Total"]);
        TotalPages = (int)TotalRecords / RecordPerPages;
        if ((int)TotalRecords % RecordPerPages > 0)
            TotalPages = TotalPages + 1;

        Sql = "SELECT " + tbl_news + ".*, row_number() over (order by created DESC) as row_index INTO #Temp_Table FROM " + tbl_news + "  ";
        Sql += "WHERE id NOT IN(SELECT id FROM " + TableName_Ykienkhachhang + ")";
        Sql += " AND status =1  ";
        if (GroupID != null && GroupID != string.Empty && GroupID != "")
        {
            Sql += " AND groupid IN (" + GroupID + ") ";
        }
        if (Keyword != string.Empty && Keyword != null && Keyword != "")
        {
            string dau = Keyword.Trim().Substring(0, 1);
            string sau = Keyword.Trim().Substring(1);
            string abc = dau.ToUpper() + sau.ToLower();
            Sql += "AND( title LIKE N'%" + Keyword.Trim() + "%' OR title LIKE N'%" + Keyword.Trim().ToLower() + "%' OR title LIKE N'%" + Keyword.Trim().ToUpper() + "%' OR  title LIKE N'%" + abc + "%' ";
            Sql += "OR summary LIKE N'%" + Keyword.Trim() + "%' OR summary LIKE N'%" + Keyword.Trim().ToUpper() + "%' OR summary LIKE N'%" + Keyword.Trim().ToLower() + "%' OR  summary LIKE N'%" + abc + "%') ";
        }
        int nS = RecordPerPages * (CurrentPage - 1);
        int record_next = nS + RecordPerPages;
        Sql += "Select * from #Temp_Table ";
        if (nS == 0)
            Sql += " WHERE (row_index >=" + nS + ") AND (row_index <=" + record_next + ")";
        else
            Sql += " WHERE (row_index >" + nS + ") AND (row_index <=" + record_next + ")";
        Sql += " ORDER BY row_index ASC ";
        return db.GetData(Sql);
    }


    public void UpdateIndex_Ykienkhachhang(string id, int idx)
    {
        try
        {
            DbTask db = new DbTask();
            string Sql = "UPDATE " + TableName_Ykienkhachhang + " ";
            Sql += "SET idx = " + idx + " ";
            Sql += "WHERE id = '" + id + "' ";
            db.ExecuteNonQuery(Sql);
        }
        catch { }
    }
   
    //-------------------------------------------------------------------

    //update index tin moi nhat trang chu tin tuc

    public void UpdateIndex_MoiNhatNews(string id, int idx)
    {
        try
        {
            DbTask db = new DbTask();
            string Sql = "UPDATE " + TableName_MoiNhatNews + " ";
            Sql += "SET idx = " + idx + " ";
            Sql += "WHERE id = '" + id + "' ";
            db.ExecuteNonQuery(Sql);
        }
        catch { }
    }

    // Update tin y kien khach hang moi nhat
   
    //----------------------------------------------------------

    //remove khoi tin moi nhat trang chu tin tuc
    public void RemoveFromPublish_MoiNhatNews(string id)
    {
        try
        {
            DbTask db = new DbTask();
            int idx = GetIndex(id);
            string Sql = "DELETE FROM " + TableName_MoiNhatNews + " ";
            Sql += "WHERE id = '" + id + "' ";
            db.ExecuteNonQuery(Sql);

            Sql = "UPDATE " + TableName_MoiNhatNews + " SET idx = idx -1 ";
            Sql += "WHERE id = '" + id + "' ";
            Sql += "AND idx > '" + idx + "'";
            db.ExecuteNonQuery(Sql);
        }
        catch { }
    }

    // remove khoi tin y kien khach hang
    public void RemoveFromPublish_Ykienkhachhang(string id)
    {
        try
        {
            DbTask db = new DbTask();
            int idx = GetIndex(id);
            string Sql = "DELETE FROM " + TableName_Ykienkhachhang + " ";
            Sql += "WHERE id = '" + id + "' ";
            db.ExecuteNonQuery(Sql);

            Sql = "UPDATE " + TableName_Ykienkhachhang + " SET idx = idx -1 ";
            Sql += "WHERE id = '" + id + "' ";
            Sql += "AND idx > '" + idx + "'";
            db.ExecuteNonQuery(Sql);
        }
        catch { }
    }
    //-------------------------------------
    //add them tin moi nhat cho trang chu tin tuc
    public void AddToPublish_MoiNhatNews(string id)
    {
        try
        {
            DbTask db = new DbTask();
            string Sql = string.Empty;
            Sql = "UPDATE " + TableName_MoiNhatNews + " SET idx = idx +1 ";
            db.ExecuteNonQuery(Sql);

            Sql = "INSERT INTO " + TableName_MoiNhatNews + "(groupid, id,idx) ";
            Sql += "VALUES ('0','" + id + "',1)";
            db.ExecuteNonQuery(Sql);
        }
        catch { }
    }

    // Them tin y kien khach hang
    public void AddToPublish_Ykienkhachhang(string id)
    {
        try
        {
            DbTask db = new DbTask();
            string Sql = string.Empty;
            Sql = "UPDATE " + TableName_Ykienkhachhang + " SET idx = idx +1 ";
            db.ExecuteNonQuery(Sql);

            Sql = "INSERT INTO " + TableName_Ykienkhachhang + "(groupid, id,idx) ";
            Sql += "VALUES ('0','" + id + "',1)";
            db.ExecuteNonQuery(Sql);
        }
        catch { }
    }
    //het cua tin moi nhat -------------------------------end---------------------------
    /// <summary>
    /// xóa publish
    /// </summary>
    /// <param name="groupid"></param>
    public void DeletePublish(string groupid)
    {
        DbTask db = new DbTask();
        string Sql = "delete from " + _TableName + " WHERE groupid='" + groupid + "'";
        db.ExecuteNonQuery(Sql);
    }

    //Moi Nhat
    public DataTable GetPublish_Noibat()
    {
        DbTask db = new DbTask();
        string tbl_News = News.TableName;
        string Sql = "SELECT " + tbl_News + ".id AS id," + tbl_News + ".title AS title, " + TableName_MoiNhat + ".idx AS idx FROM " + tbl_News + " INNER JOIN " + TableName_MoiNhat + " ON " + tbl_News + ".id = " + TableName_MoiNhat + ".id ";
        Sql += "WHERE " + tbl_News + ".status = 1 AND " + tbl_News + ".kind = 0 ";
        Sql += "ORDER BY " + TableName_MoiNhat + ".idx ASC, " + tbl_News + ".created DESC ";
        return db.GetData(Sql);
    }
    public DataTable GetNotInPublish_Noibat(string Keyword, string GroupID, int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages)
    {
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;
        DbTask db = new DbTask();
        string tbl_news = News.TableName;
        string Sql = string.Empty;
        Sql = "SELECT COUNT(a.id) AS Total FROM " + tbl_news + " as a ";
        Sql += "WHERE a.id NOT IN(SELECT id FROM " + TableName_MoiNhat + ")";
        Sql += " AND a.status = 1 AND a.kind = 0 ";
        if (GroupID != null && GroupID != string.Empty && GroupID != "")
        {
            Sql += " AND groupid IN (" + GroupID + ") ";
        }
        if (Keyword != string.Empty && Keyword != null && Keyword != "")
        {
            string dau = Keyword.Trim().Substring(0, 1);
            string sau = Keyword.Trim().Substring(1);
            string abc = dau.ToUpper() + sau.ToLower();
            Sql += "AND( title LIKE '%" + Keyword.Trim() + "%' OR title LIKE '%" + Keyword.Trim().ToLower() + "%' OR title LIKE '%" + Keyword.Trim().ToUpper() + "%' OR  title LIKE '%" + abc + "%' ";
            Sql += "OR summary LIKE '%" + Keyword.Trim() + "%' OR summary LIKE '%" + Keyword.Trim().ToUpper() + "%' OR summary LIKE '%" + Keyword.Trim().ToLower() + "%' OR  summary LIKE '%" + abc + "%') ";
        }

        dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;

        TotalRecords = Convert.ToInt32(dt.Rows[0]["Total"]);
        TotalPages = (int)TotalRecords / RecordPerPages;
        if ((int)TotalRecords % RecordPerPages > 0)
            TotalPages = TotalPages + 1;

        Sql = "SELECT " + tbl_news + ".*, row_number() over (order by created DESC) as row_index INTO #Temp_Table FROM " + tbl_news + " ";
        Sql += "WHERE id NOT IN(SELECT id FROM " + TableName_MoiNhat + ")";
        Sql += " AND status =1 AND kind = 0 ";
        if (GroupID != null && GroupID != string.Empty && GroupID != "")
        {
            Sql += " AND groupid IN (" + GroupID + ") ";
        }
        if (Keyword != string.Empty && Keyword != null && Keyword != "")
        {
            string dau = Keyword.Trim().Substring(0, 1);
            string sau = Keyword.Trim().Substring(1);
            string abc = dau.ToUpper() + sau.ToLower();
            Sql += "AND( title LIKE '%" + Keyword.Trim() + "%' OR title LIKE '%" + Keyword.Trim().ToLower() + "%' OR title LIKE '%" + Keyword.Trim().ToUpper() + "%' OR  title LIKE '%" + abc + "%' ";
            Sql += "OR summary LIKE '%" + Keyword.Trim() + "%' OR summary LIKE '%" + Keyword.Trim().ToUpper() + "%' OR summary LIKE '%" + Keyword.Trim().ToLower() + "%' OR  summary LIKE '%" + abc + "%') ";
        }
        int nS = RecordPerPages * (CurrentPage - 1);
        int record_next = nS + RecordPerPages;
        Sql += "Select * from #Temp_Table ";
        if (nS == 0)
            Sql += " WHERE (row_index >=" + nS + ") AND (row_index <=" + record_next + ")";
        else
            Sql += " WHERE (row_index >" + nS + ") AND (row_index <=" + record_next + ")";
        Sql += " ORDER BY row_index ASC ";
        return db.GetData(Sql);
    }
    public void UpdateIndex_NoiBat(string id, int idx)
    {
        try
        {
            DbTask db = new DbTask();
            string Sql = "UPDATE " + TableName_MoiNhat + " ";
            Sql += "SET idx = " + idx + " ";
            Sql += "WHERE id = '" + id + "' ";
            db.ExecuteNonQuery(Sql);
        }
        catch { }
    }
    public void RemoveFromPublish_NoiBat(string id)
    {
        try
        {
            DbTask db = new DbTask();
            int idx = GetIndex(id);
            string Sql = "DELETE FROM " + TableName_MoiNhat + " ";
            Sql += "WHERE id = '" + id + "' ";
            db.ExecuteNonQuery(Sql);

            Sql = "UPDATE " + TableName_MoiNhat + " SET idx = idx -1 ";
            Sql += "WHERE id = '" + id + "' ";
            Sql += "AND idx > '" + idx + "'";
            db.ExecuteNonQuery(Sql);
        }
        catch { }
    }
    public void AddToPublish_NoiBat(string id)
    {
        try
        {
            DbTask db = new DbTask();
            string Sql = string.Empty;
            Sql = "UPDATE " + TableName_MoiNhat + " SET idx = idx +1 ";
            db.ExecuteNonQuery(Sql);

            Sql = "INSERT INTO " + TableName_MoiNhat + "(groupid, id,idx) ";
            Sql += "VALUES ('0','" + id + "',1)";
            db.ExecuteNonQuery(Sql);
        }
        catch { }
    }
}
