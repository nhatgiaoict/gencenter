using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
/// <summary>
/// Summary description for News
/// </summary>
public class News
{
    public string TableName
    {
        get { return "tbl_news_" + Globals.CurrentLang; }
    }
    public string TableMenu
    {
        get { return "tbl_menu_" + Globals.CurrentLang; }
    }
    public string TableGroup
    {
        get { return "tbl_group_" + Globals.CurrentLang; }
    }
    public string TableName_AboutUs
    {
        get { return "tbl_publish_moinhat_" + Globals.CurrentLang; }
    }
    public string TableName_noibat
    {
        get { return "tbl_noibat_home_" + Globals.CurrentLang; }
    }

    public string TableName_publish_technical
    {
        get { return "tbl_publish_technical_" + Globals.CurrentLang; }
    }
    public static string AbsImages
    {
        get
        {
            return Globals.AbsData + "news/images/";
        }
    }
    public static string UrlImages
    {
        get
        {
            return Globals.UrlData + "news/images/";
        }
    }
    public string AbsHtmls
    {
        get
        {
            return Globals.AbsData + "news/htmls/";
        }
    }

    public string GetContent(string id)
    {
        string sFilename = this.AbsHtmls + id + "_" + Globals.CurrentLang + ".htm";
        if (File.Exists(sFilename))
        {
            StringBuilder sb = new StringBuilder();
            StreamReader strdr = File.OpenText(sFilename);
            string input = null;
            while ((input = strdr.ReadLine()) != null)
            {
                sb.Append(input);
            }
            strdr.Close();
            return sb.ToString();
        }
        return string.Empty;
    }
    public DataTable SearchingNews(string GroupID, string Keyword, int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;
        string dtNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string Sql = string.Empty;
        Sql = "SELECT COUNT(a.ID) AS Total FROM " + TableName + " as a ";
        Sql += "WHERE a.status = 1 ";
        Sql += "AND a.created <= '" + dtNow + "' ";
        if (GroupID != null && GroupID != string.Empty && GroupID != "")
        {
            Sql += " AND a.groupid LIKE N'" + GroupID + "%'";
        }
        if (Keyword != string.Empty && Keyword != null && Keyword != "")
        {
            string dau = Keyword.Trim().Substring(0, 1);
            string sau = Keyword.Trim().Substring(1);
            string abc = dau.ToUpper() + sau.ToLower();
            Sql += "AND( a.title LIKE N'%" + Keyword.Trim() + "%' OR a.title LIKE N'%" + Keyword.Trim().ToLower() + "%' OR a.title LIKE N'%" + Keyword.Trim().ToUpper() + "%' OR  a.title LIKE N'%" + abc + "%' ";
            Sql += "OR a.summary LIKE N'%" + Keyword.Trim() + "%' OR a.summary LIKE N'%" + Keyword.Trim().ToUpper() + "%' OR a.summary LIKE N'%" + Keyword.Trim().ToLower() + "%' OR  a.summary LIKE N'%" + abc + "%') ";
        }
        dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        TotalRecords = Convert.ToInt32(dt.Rows[0]["Total"]);
        TotalPages = (int)TotalRecords / RecordPerPages;
        if ((int)TotalRecords % RecordPerPages > 0)
            TotalPages = TotalPages + 1;

        Sql = "SELECT  distinct a.*, row_number() over (order by created DESC) as row_index INTO #Temp_Table FROM " + TableName + " as a ";
        Sql += " WHERE a.status = 1 ";
        Sql += " AND a.created <= '" + dtNow + "' ";
        if (GroupID != null && GroupID != string.Empty && GroupID != "")
        {
            Sql += " AND a.groupid LIKE N'" + GroupID + "%'";
        }

        if (Keyword != string.Empty && Keyword != null && Keyword != "")
        {
            string dau = Keyword.Trim().Substring(0, 1);
            string sau = Keyword.Trim().Substring(1);
            string abc = dau.ToUpper() + sau.ToLower();
            Sql += "AND( a.title LIKE N'%" + Keyword.Trim() + "%' OR a.title LIKE N'%" + Keyword.Trim().ToLower() + "%' OR a.title LIKE N'%" + Keyword.Trim().ToUpper() + "%' OR  a.title LIKE N'%" + abc + "%' ";
            Sql += "OR a.summary LIKE N'%" + Keyword.Trim() + "%' OR a.summary LIKE N'%" + Keyword.Trim().ToUpper() + "%' OR a.summary LIKE N'%" + Keyword.Trim().ToLower() + "%' OR  a.summary LIKE N'%" + abc + "%') ";
        }
        int nS = RecordPerPages * (CurrentPage - 1);
        int record_next = nS + RecordPerPages;
        Sql += "Select * from #Temp_Table ";
        if (nS == 0)
            Sql += " WHERE (row_index >=" + nS + ") AND (row_index <=" + record_next + ")";
        else
            Sql += " WHERE (row_index >" + nS + ") AND (row_index <=" + record_next + ")";
        Sql += " ORDER BY row_index ASC ";
        dt = db.GetData(Sql);
        return dt;
    }
    public void Detail(string groupid, string id, out DataTable dt_new, out DataTable dt_old, out DataRow dr_info)
    {
        DbTask db = new DbTask();
        dt_new = null;
        dt_old = null;
        dr_info = null;
        string Sql = string.Empty;
        string SqlCreated = "( SELECT created FROM " + TableName + " WHERE id= '" + id.Trim() + "' ) ";
        string SqlCommon1 = " SELECT TOP 5 a.id, a.groupid, a.title, a.shortlink, a.created,a.fimage, RANK() OVER (ORDER BY  a.created DESC , a.id DESC) AS SortRank FROM " + TableName + " AS a ";
        SqlCommon1 += " WHERE  a.status = 1 ";
        if (groupid != string.Empty && groupid != null && groupid != "")
            SqlCommon1 += " AND a.groupid like '" + groupid + "%' ";
        string SqlCommon2 = " SELECT TOP 5 a.id, a.groupid, a.title, a.shortlink, a.created,a.fimage, RANK() OVER (ORDER BY  a.created ASC , a.id ASC) AS SortRank FROM " + TableName + " AS a ";
        SqlCommon2 += " WHERE  a.status = 1 ";
        if (groupid != string.Empty && groupid != null && groupid != "")
            SqlCommon2 += " AND a.groupid like '" + groupid + "%' ";
        Sql += " " + SqlCommon1 + " AND (a.created <= " + SqlCreated + " ) ";
        Sql += " UNION ";
        Sql += " " + SqlCommon2 + " AND (a.created >= " + SqlCreated + " )";
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return;
        DataRow[] arr_dr = dt.Select("", "created DESC");
        dt_new = dt.Clone();
        dt_old = dt.Clone();
        string sid = string.Empty;
        //int n = dt.Rows.Count;
        int n = arr_dr.Length;
        int i = 0;
        bool bRet = false;
        while (!bRet && i < n)
        {
            sid = arr_dr[i]["id"].ToString();
            if (sid == id)
            {
                dr_info = arr_dr[i];
                bRet = true;
            }
            else
                dt_new.ImportRow(arr_dr[i]);
            i++;
        }
        dt_new.AcceptChanges();
        while (i < n)
        {
            dt_old.ImportRow(arr_dr[i]);
            i++;
        }
        dt_old.AcceptChanges();
    }

    public DataTable Search_keyword(string keyword)
    {
        DbTask db = new DbTask();
        string Sql = "select a.* FROM " + TableName + " as a where a.title like N'%" + keyword + "%' OR a.summary like N'%" + keyword + "%'";
        return db.GetData(Sql);
    }
    public DataTable SearchingDeTail(string GroupID, int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages, out int Total)
    {
        string tbl_News = this.TableName;
        DbTask db = new DbTask();
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;
        string Sql = string.Empty;
        Sql = " SELECT COUNT(ID) AS Total FROM " + tbl_News + "  ";
        Sql += " WHERE groupid Like '" + GroupID + "%' AND status= 1 ";
        dt = db.GetData(Sql);
        Total = Convert.ToInt32(dt.Rows[0]["Total"].ToString());
        if (dt == null || dt.Rows.Count == 0)
            return null;

        TotalRecords = Convert.ToInt32(dt.Rows[0]["Total"]);
        TotalPages = (int)TotalRecords / RecordPerPages;
        if ((int)TotalRecords % RecordPerPages > 0)
            TotalPages = TotalPages + 1;
        Sql = "Select " + tbl_News + ".*, " + TableGroup + ".shortlink as pshortlink , row_number() over (order by " + tbl_News + ".created DESC) as row_index INTO #Temp_Table ";
        Sql += "from " + tbl_News + "  INNER JOIN " + TableGroup + " ON " + tbl_News + ".groupid = " + TableGroup + ".Id ";
        Sql += "where " + tbl_News + ".groupid Like  '" + GroupID + "%' AND " + tbl_News + ".status = 1 ";
        int nS = RecordPerPages * (CurrentPage - 1);
        int record_next = nS + RecordPerPages;
        Sql += "Select * from #Temp_Table ";
        if (nS == 0)
            Sql += " WHERE (row_index >=" + nS + ") AND (row_index <=" + record_next + ")";
        else
            Sql += " WHERE (row_index >" + nS + ") AND (row_index <=" + record_next + ")";
        Sql += " ORDER BY row_index ASC ";
        dt = db.GetData(Sql);
        return dt;
    }

    public DataRow GetCount(string groupid)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT COUNT(id) as pid FROM " + TableName + " WHERE ";
        Sql += "groupid='" + groupid + "'";
        DataTable dt = null;
        dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
    public DataRow GetIDnews(string groupid)
    {
        DbTask db = new DbTask();

        string Sql = "SELECT id as pid FROM " + TableName + " WHERE ";
        Sql += "groupid='" + groupid + "'";
        DataTable dt = null;
        dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
    public DataTable GetHotLike(int N)
    {
        DbTask db = new DbTask();
        string SQL = "Select TOP " + N + " title, fimage, id, summary,created, groupid, shortlink from " + TableName + " where status = 1 AND hotlike = 1 AND kind = 0 Order by created DESC ";
        return db.GetData(SQL);
    }
    public DataTable GetDuannoibat(int N)
    {
        DbTask db = new DbTask();
        string SQL = "Select TOP " + N + " title, fimage, created, ncount, id, groupid, shortlink from " + TableName + " where status = 1 AND hotnew = 1 AND kind = 1 Order by created DESC ";
        return db.GetData(SQL);
    }
    public DataTable GetDuanhoanthanh(int N)
    {
        DbTask db = new DbTask();
        string SQL = "Select TOP " + N + "title, fimage, summary, created, ncount, id, groupid, shortlink from " + TableName + " where status = 1 AND hotlike = 1 AND kind = 1 Order by created DESC ";
        return db.GetData(SQL);
    }

    public DataTable GetNewByCount(int N)
    {
        DbTask db = new DbTask();
        string SQL = "Select Top " + N + " title, fimage, created, ncount, id, shortlink from " + TableName + " where status = 1 AND kind = 0 Order by ncount DESC ";
        return db.GetData(SQL);
    }

    /// <summary>
    /// Sản phẩm mới của trang chủ
    /// </summary>
    /// <param name="N"></param>
    /// <returns></returns>
    public DataTable GetNewAboutUs(int N)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT a.id,a.shortlink,a.title,a.groupid, a.fimage, a.summary AS summary,  b.idx AS idx  FROM " + TableName + " as a INNER JOIN " + TableName_noibat + " as b ON a.id = b.id ";
        Sql += "WHERE a.status = 1 ";
        Sql += "ORDER BY b.idx ASC, a.created DESC ";
        return db.GetData(Sql);
    }
    /// <summary>
    /// lấy tin liên quan của News
    /// </summary>
    /// <param name="id"></param>
    /// <param name="groupid"></param>
    /// <returns></returns>
    public DataTable GetNewsLienQuan(string id, string groupid)
    {
        DataTable dt = new DataTable();
        DbTask db = new DbTask();
        DataRow dr = GetCreatedNews(id);
        if (dr != null)
        {
            DateTime a = Convert.ToDateTime(dr["created"].ToString());
            string created = a.ToString();
            string Sql = "SELECT TOP 5 * FROM " + TableName + " WHERE status=1 AND kind = 0 ";
            Sql += "AND groupid='" + groupid + "' AND created > '" + created + "'";
            Sql += " ORDER BY created DESC";
            dt = db.GetData(Sql);
            if (dt != null && dt.Rows.Count > 0)
            {

            }
            else
            {
                Sql = "SELECT TOP 5 * FROM " + TableName + " WHERE status = 1 AND kind = 0 ";
                Sql += "AND groupid='" + groupid + "' AND created < '" + created + "'";
                Sql += " ORDER BY created DESC";
                dt = db.GetData(Sql);
            }
        }
        return dt;
    }
    private DataRow GetCreatedNews(string id)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT created FROM " + TableName + " WHERE id='" + id + "' AND kind = 0 ";
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
    // Thuytv
    public DataTable GetTinLQChuan(int kind, string id, string GroupID, int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;
        string dtNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string Sql = string.Empty;
        Sql = "SELECT COUNT(a.ID) AS Total FROM " + TableName + " as a ";
        Sql += "WHERE a.status = 1 AND kind = " + kind + " AND id != '" + id + "'";
        Sql += "AND a.created <= '" + dtNow + "' ";
        if (GroupID != null && GroupID != string.Empty && GroupID != "")
        {
            Sql += " AND a.groupid LIKE N'" + GroupID + "%'";
        }
        dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        TotalRecords = Convert.ToInt32(dt.Rows[0]["Total"]);
        TotalPages = (int)TotalRecords / RecordPerPages;
        if ((int)TotalRecords % RecordPerPages > 0)
            TotalPages = TotalPages + 1;

        Sql = "SELECT  distinct a.*, row_number() over (order by created DESC) as row_index INTO #Temp_Table FROM " + TableName + " as a ";
        Sql += " WHERE a.status = 1 AND kind= " + kind + " AND id != '" + id + "' ";
        Sql += " AND a.created <= '" + dtNow + "' ";
        if (GroupID != null && GroupID != string.Empty && GroupID != "")
        {
            Sql += " AND a.groupid LIKE N'" + GroupID + "%'";
        }
        int nS = RecordPerPages * (CurrentPage - 1);
        int record_next = nS + RecordPerPages;
        Sql += "Select * from #Temp_Table ";
        if (nS == 0)
            Sql += " WHERE (row_index >=" + nS + ") AND (row_index <=" + record_next + ")";
        else
            Sql += " WHERE (row_index >" + nS + ") AND (row_index <=" + record_next + ")";
        Sql += " ORDER BY row_index ASC ";
        dt = db.GetData(Sql);
        return dt;
    }
    public DataTable GetNewBygroup(string groupid, int N, string idNew)
    {
        DataTable dt = new DataTable();
        if (clsCache.Get("NewGroup_" + groupid + N) != null)
        {
            dt = (DataTable)clsCache.Get("NewGroup_" + groupid + N);
        }
        else
        {
            DbTask db = new DbTask();
            string Sql = "SELECT top " + N + " title, fimage, id, groupid, shortlink, summary, created from " + TableName + " ";
            Sql += " WHERE status = 1 AND groupid = '" + groupid + "' ";
            if (idNew != string.Empty || idNew != "")
                Sql += " AND id != '" + idNew + "' ";
            Sql += " ORDER BY idx ";
            dt = db.GetData(Sql);
            clsCache.Max("NewGroup_" + groupid + N, (object)dt);
        }
        if (dt == null && dt.Rows.Count == 0)
            return null;
        return dt;
    }
    public DataTable GetNewByLikeGroup(string groupid, int N)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT top " + N + " a.title, a.fimage, a.id, a.groupid, a.shortlink, a.title, a.summary, a.created from " + TableName + " AS a ";
        Sql += " WHERE a.status = 1 AND a.groupid like '" + groupid + "%' ORDER BY created DESC ";
        return db.GetData(Sql);
    }

    public DataTable Getsanphannoibat(string groupid)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT a.title, a.fimage,  a.id, a.groupid, a.shortlink, a.title, a.summary, a.created from " + TableName + " AS a ";
        Sql += " WHERE a.status = 1 AND a.groupid like '" + groupid + "%' AND hotnew = 1 ORDER BY created DESC ";
        return db.GetData(Sql);
    }

    /// <summary>
    /// Lấy N sản phẩm của chuyên mục sắp xếp theo số thứ tự trong CMS
    /// </summary>
    /// <param name="groupid"></param>
    /// <param name="N"></param>
    /// <returns></returns>
    /// 

    public DataTable GetProductByLikeGroup(int N)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT top " + N + " a.title, a.fimage, a.id, a.groupid, a.shortlink, a.summary, a.created,b.idx as bidx, b.shortlink as pshortlink  from " + TableName + " AS a ";
        Sql += " INNER JOIN " + TableGroup + " as b ON a.groupid = b.id ";
        Sql += " WHERE a.status = 1 AND a.hotlike = 1 ORDER BY created DESC ";
        return db.GetData(Sql);
    }
    public DataRow GetInfoGroup(string id)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        string Sql = "SELECT title, id, shortlink, link, fimages from " + TableGroup + " ";
        Sql += " WHERE id = ?id ";
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        dt = db.GetData(Sql, dt);
        if (dt.Rows.Count > 0) return dt.Rows[0];
        else return null;
    }

    public DataTable GetNewLikeGroup(string groupid, int N)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT top " + N + " a.title, a.fimage, a.id, a.groupid, a.shortlink, a.summary, a.created from " + TableName + " AS a";
        Sql += " WHERE a.status = 1 AND a.groupid like '" + groupid + "%' ORDER BY a.created DESC ";
        return db.GetData(Sql);
    }
    public DataTable GetNewLates(int N)
    {
        DbTask db = new DbTask();
        string SQl = "SELECT TOP " + N + " a.id, a.groupid, a.title, a.shortlink, a.created, b.shortlink as pshortlink from " + TableName + " AS a ";
        SQl += " INNER JOIN " + TableGroup + " As b ON a.groupid = b.id ";
        SQl += " Where a.status = 1 Order by a.created DESC ";
        return db.GetData(SQl);
    }
    public DataRow GetInfo(string id)
    {
        DbTask db = new DbTask();
        string Sql = "Select title, DescriptionMeta, KeywordMeta from " + TableName + " where id = " + id + "";
        return db.GetData(Sql).Rows[0];
    }


    public DataRow GetInfoByShortLink(string shortlink)
    {
        DbTask db = new DbTask();
        string Sql = "Select kind, groupid, id from " + TableName + " where shortlink = '" + shortlink + "'";
        return db.GetData(Sql).Rows[0];
    }
    public DataRow GetInfoAllByShortLink(string shortlink)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        string Sql = "Select * from " + TableName + " Where shortlink = ?shortlink AND status = 1 ";
        db.AddParameters(ref dt, "shortlink", DbType.NVarChar, shortlink);

        dt = db.GetData(Sql, dt);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0];
        }
        else { return null; }
    }
    public DataTable GetSanphamlienquan(string id, string idhoivien)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        string Sql = "Select title, shortlink, fimage, id from " + TableName + " Where idhoivien = ?idhoivien AND status = 1  AND id <> '" + id + "'";
        db.AddParameters(ref dt, "idhoivien", DbType.NVarChar, idhoivien);
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        dt = db.GetData(Sql, dt);
        return dt;
    }
    public DataTable SearchProduc(string Keyword, int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;
        string dtNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string Sql = string.Empty;
        Sql = "SELECT COUNT(a.ID) AS Total FROM " + TableName + " as a ";
        Sql += "WHERE a.status = 1 ";
        Sql += "AND a.created <= '" + dtNow + "' ";
        if (Keyword != string.Empty && Keyword != null && Keyword != "")
        {
            string dau = Keyword.Trim().Substring(0, 1);
            string sau = Keyword.Trim().Substring(1);
            string abc = dau.ToUpper() + sau.ToLower();
            Sql += "AND( a.title LIKE N'%" + Keyword.Trim() + "%' OR a.title LIKE N'%" + Keyword.Trim().ToLower() + "%' OR a.title LIKE N'%" + Keyword.Trim().ToUpper() + "%' OR  a.title LIKE N'%" + abc + "%' ";
            Sql += "OR a.summary LIKE N'%" + Keyword.Trim() + "%' OR a.summary LIKE N'%" + Keyword.Trim().ToUpper() + "%' OR a.summary LIKE N'%" + Keyword.Trim().ToLower() + "%' OR  a.summary LIKE N'%" + abc + "%') ";
        }
        dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        TotalRecords = Convert.ToInt32(dt.Rows[0]["Total"]);
        TotalPages = (int)TotalRecords / RecordPerPages;
        if ((int)TotalRecords % RecordPerPages > 0)
            TotalPages = TotalPages + 1;

        Sql = "SELECT a.title, a.id, a.shortlink, a.groupid,  a.fimage , row_number() over (order by created DESC) as row_index INTO #Temp_Table FROM " + TableName + " as a ";
        Sql += " WHERE a.status = 1 ";
        Sql += " AND a.created <= '" + dtNow + "' ";

        if (Keyword != string.Empty && Keyword != null && Keyword != "")
        {
            string dau = Keyword.Trim().Substring(0, 1);
            string sau = Keyword.Trim().Substring(1);
            string abc = dau.ToUpper() + sau.ToLower();
            Sql += "AND( a.title LIKE N'%" + Keyword.Trim() + "%' OR a.title LIKE N'%" + Keyword.Trim().ToLower() + "%' OR a.title LIKE N'%" + Keyword.Trim().ToUpper() + "%' OR  a.title LIKE N'%" + abc + "%' ";
            Sql += "OR a.summary LIKE N'%" + Keyword.Trim() + "%' OR a.summary LIKE N'%" + Keyword.Trim().ToUpper() + "%' OR a.summary LIKE N'%" + Keyword.Trim().ToLower() + "%' OR  a.summary LIKE N'%" + abc + "%') ";
        }
        int nS = RecordPerPages * (CurrentPage - 1);
        int record_next = nS + RecordPerPages;
        Sql += "Select * from #Temp_Table ";
        if (nS == 0)
            Sql += " WHERE (row_index >=" + nS + ") AND (row_index <=" + record_next + ")";
        else
            Sql += " WHERE (row_index >" + nS + ") AND (row_index <=" + record_next + ")";
        Sql += " ORDER BY row_index ASC ";
        dt = db.GetData(Sql);
        return dt;
    }

    public DataTable GetAllDataSelect(string idLienKet)
    {
        if (idLienKet.Length > 0)
        {
            DbTask db = new DbTask();
            string Sql = "Select a.title,a.shortlink, a.fimage, a.id, a.groupid, b.shortlink as pshortlink from " + TableName + " as a  CROSS JOIN " + TableGroup + " as b  where a.id IN (" + idLienKet + ") ";
            Sql += "AND a.groupid = b.id ";
            return db.GetData(Sql);
        }
        else { return null; }
    }
    public DataTable GetAllDataSelect(string idLienKet, string GroupID)
    {
        if (idLienKet.Length > 0)
        {
            DbTask db = new DbTask();
            string Sql = "Select a.title,a.shortlink, a.fimage, a.id, a.groupid, b.shortlink as pshortlink from " + TableName + " as a  CROSS JOIN " + TableGroup + " as b  where a.id IN (" + idLienKet + ") ";
            Sql += "AND b.id = '" + GroupID + "' ";
            return db.GetData(Sql);
        }
        else { return null; }
    }


    /// <summary>
    /// Creat table dtCart ;
    /// </summary>
    /// <returns></returns>
    public DataTable dtCart()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("id", typeof(string));
        dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };
        return dt;
    }

    /// <summary>
    /// Add thêm sản phẩm vào giỏ hàng
    /// </summary>
    /// <param name="id"></param>
    public void AddToCart(string id)
    {
        try
        {
            DataTable dt = null;
            if (HttpContext.Current.Session["Cart"] == null)
                dt = dtCart();
            else
            {
                dt = (DataTable)HttpContext.Current.Session["Cart"];
            }
            DataRow dr = dt.NewRow();
            dr["id"] = id;
            dt.Rows.Add(dr);
            dt.AcceptChanges();
            HttpContext.Current.Session["Cart"] = dt;
        }
        catch
        {
        }
    }

    /// <summary>
    /// Remove sản phẩm khỏi giỏ hàng
    /// </summary>
    /// <param name="id">ID Sản phẩm</param>
    public void RemoveCart(string id)
    {
        if (HttpContext.Current.Session["Cart"] == null)
            return;
        else
        {
            DataTable dt = (DataTable)HttpContext.Current.Session["Cart"];
            dt.Rows.Remove(dt.Rows.Find(id));
            dt.AcceptChanges();
            HttpContext.Current.Session["Cart"] = dt;
        }
    }

    /// <summary>
    /// Lấy thông tin các sản phẩm có trong giỏ hàng
    /// </summary>
    /// <returns></returns>
    /// <summary>
    /// Sản phẩm đã xem
    /// </summary>
    /// <returns></returns>
    public DataTable dtRead()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("id", typeof(string));
        dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };
        return dt;
    }

    /// <summary>
    /// Add da xem
    /// </summary>
    /// <param name="id"></param>
    public void AddToRead(string id)
    {
        try
        {
            DataTable dt = null;
            if (HttpContext.Current.Session["Read"] == null)
                dt = dtCart();
            else
            {
                dt = (DataTable)HttpContext.Current.Session["Read"];
            }
            DataRow dr = dt.NewRow();
            dr["id"] = id;
            dt.Rows.Add(dr);
            dt.AcceptChanges();
            HttpContext.Current.Session["Read"] = dt;
        }
        catch
        {
        }
    }
    public void RemoveRead(string id)
    {
        if (HttpContext.Current.Session["Read"] == null)
            return;
        else
        {
            DataTable dt = (DataTable)HttpContext.Current.Session["Read"];
            dt.Rows.Remove(dt.Rows.Find(id));
            dt.AcceptChanges();
            HttpContext.Current.Session["Read"] = dt;
        }
    }

    public void DetailProduct(string groupid, string id, out DataTable dt_new, out DataTable dt_old, out DataRow dr_info)
    {
        DbTask db = new DbTask();
        dt_new = null;
        dt_old = null;
        dr_info = null;
        string Sql = string.Empty;
        string SqlCreated = "( SELECT created FROM " + TableName + " WHERE id= '" + id.Trim() + "' ) ";
        string SqlCommon1 = "SELECT TOP 5 a.id, a.groupid, a.ncount, a.title, a.shortlink, a.created,a.fimage, b.shortlink as pshortlink, RANK() OVER (ORDER BY  a.created DESC , a.id DESC) AS SortRank FROM " + TableName + " AS a ";
        SqlCommon1 += " INNER JOIN " + TableGroup + " As b ON a.groupid = b.id ";
        SqlCommon1 += " WHERE  a.status = 1 ";
        if (groupid != string.Empty && groupid != null && groupid != "")
            SqlCommon1 += " AND a.groupid like '" + groupid + "%' ";
        string SqlCommon2 = "SELECT TOP 5 a.id, a.groupid, a.ncount, a.title, a.shortlink, a.created, a.fimage, b.shortlink as pshortlink ,RANK() OVER (ORDER BY  a.created ASC , a.id ASC) AS SortRank FROM " + TableName + " AS a ";
        SqlCommon2 += " INNER JOIN " + TableGroup + " As b ON a.groupid = b.id ";
        SqlCommon2 += " WHERE  a.status = 1 ";
        if (groupid != string.Empty && groupid != null && groupid != "")
            SqlCommon2 += " AND a.groupid like '" + groupid + "%' ";
        Sql += " " + SqlCommon1 + " AND (a.created <= " + SqlCreated + " ) ";
        Sql += " UNION ";
        Sql += " " + SqlCommon2 + " AND (a.created >= " + SqlCreated + " )";
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return;
        DataRow[] arr_dr = dt.Select("", "created DESC");
        dt_new = dt.Clone();
        dt_old = dt.Clone();
        string sid = string.Empty;
        //int n = dt.Rows.Count;
        int n = arr_dr.Length;
        int i = 0;
        bool bRet = false;
        while (!bRet && i < n)
        {
            sid = arr_dr[i]["id"].ToString();
            if (sid == id)
            {
                dr_info = arr_dr[i];
                bRet = true;
            }
            else
                dt_new.ImportRow(arr_dr[i]);
            i++;
        }
        dt_new.AcceptChanges();
        while (i < n)
        {
            dt_old.ImportRow(arr_dr[i]);
            i++;
        }
        dt_old.AcceptChanges();
    }


    public void Inceatncount(string id)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        string SQL = "Update " + TableName + " SET ncount = ncount + 1 Where id = ?id ";
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.ExecuteNonQuery(SQL, dt);
        HttpContext.Current.Session["view_" + id] = id;
    }

    public DataTable Getykienkhachhang()
    {
        DbTask db = new DbTask();
        string Sql = "SELECT a.id,a.shortlink,a.title,a.groupid, a.fimage, a.summary AS summary,  b.idx AS idx  FROM " + TableName + " as a INNER JOIN " + TableName_publish_technical + " as b ON a.id = b.id ";
        Sql += "WHERE a.status = 1 ";
        Sql += "ORDER BY b.idx ASC, a.created DESC ";
        return db.GetData(Sql);
    }

    public DataTable GetTeam(int N)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT a.id,a.shortlink,a.title,a.groupid, a.fimage, a.summary AS summary,  b.idx AS idx  FROM " + TableName + " as a INNER JOIN " + TableName_AboutUs + " as b ON a.id = b.id ";
        //Sql += " INNER JOIN " + TableGroup + " as c ON a.groupid = c.id ";
        Sql += "WHERE a.status = 1 ";
        Sql += "ORDER BY b.idx ASC, a.created DESC ";
        return db.GetData(Sql);
    }
}
