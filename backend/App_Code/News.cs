using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;
/// <summary>
/// Summary description for News
/// </summary>
public class News
{
    public static string TableName
    {
        get { return "tbl_news_" + Globals.CurrentLang; }
    }
    private static string TableName_TinTucSuKien
    {
        get { return "tbl_publish_technical_" + Globals.CurrentLang; }
    }
    public static string TableName_NewsTrangChu
    {
        get { return "tbl_publish_moinhat_" + Globals.CurrentLang; }
    }
    private static string TablePublish
    {
        get { return "tbl_publish_" + Globals.CurrentLang; }
    }
    private static string TableNoiBat
    {
        get { return "tbl_publish_NoiBat_" + Globals.CurrentLang; }
    }

    private static string _mGroupIdAllow = string.Empty;
    public News()
    {
        if (Globals.CheckExist)
        {
            try
            {
                //Create Table
                string StrSql = "CREATE TABLE IF NOT EXISTS " + TableName + "(";
                StrSql += "id nvarchar(255) NOT NULL default '', ";
                StrSql += "title nvarchar(500) default NULL, ";
                StrSql += "groupid nvarchar(255) default NULL, ";
                StrSql += "fimage nvarchar(255) default NULL, ";
                StrSql += "summary mediumtext, ";
                StrSql += "created datetime default NULL, ";
                StrSql += "userid nvarchar(255) default NULL, ";
                StrSql += "status tinyint(4) default 2, ";
                StrSql += "PRIMARY KEY  (id)) ";
                StrSql += "ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_bin;";
                DbTask db = new DbTask();
                db.ExecuteNonQuery(StrSql);

                //Create Directory
                if (!System.IO.Directory.Exists(AbsImages))
                    System.IO.Directory.CreateDirectory(AbsHtmls);

                if (!System.IO.Directory.Exists(AbsHtmls))
                    System.IO.Directory.CreateDirectory(AbsHtmls);
            }
            catch
            {

            }
        }
        if (!Membertask.IsAdministrator())
            _mGroupIdAllow = Membertask.IsNewsTask();
        else
            _mGroupIdAllow = string.Empty;
    }

    private string GetAllID()
    {
        DbTask db = new DbTask();
        string Sql = "SELECT id FROM " + TableName + "";
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return string.Empty;
        string sRet = ",";
        foreach (DataRow dr in dt.Rows)
            sRet += dr["id"].ToString() + ",";
        return sRet;
    }
    private bool CheckExist(string AllID, string ID)
    {

        if (AllID == string.Empty || AllID == null || AllID == "")
            return false;
        AllID = AllID.StartsWith(",") ? AllID : "," + AllID;
        AllID = AllID.EndsWith(",") ? AllID : AllID + ",";
        if (AllID.IndexOf("," + ID + ",") > -1)
        {
            return true;
        }
        return false;
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
    public string UrlHtmls
    {
        get
        {
            return Globals.UrlData + "news/htmls/";
        }
    }
    public string Insert(string title, string groupid, string fimage, string summary, string content, string created, int status,  string Titlemeta,  string Keywordmeta, string DescriptionMeta, string shortlink,
        int kind)
    {
        string Sql = "INSERT INTO " + TableName + "(title,groupid,summary,created,userid,status,fimage,titlemeta, keywords, Description, shortlink, kind)";
        Sql += " VALUES(?title, ?groupid, ?summary, ?created, ?userid, ?status, ?fimage,  ?titlemeta, ?keywords , ?Description, ?shortlink, ?kind)";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "title", DbType.NVarChar, title);
        db.AddParameters(ref dt, "groupid", DbType.NVarChar, groupid);
        db.AddParameters(ref dt, "summary", DbType.NVarChar, summary);
        db.AddParameters(ref dt, "status", DbType.Int32, status);
        db.AddParameters(ref dt, "created", DbType.Datetime, created);
        db.AddParameters(ref dt, "userid", DbType.NVarChar, Membertask.Name);
        db.AddParameters(ref dt, "fimage", DbType.NVarChar, fimage);

        db.AddParameters(ref dt, "titlemeta", DbType.NVarChar, Titlemeta);
        db.AddParameters(ref dt, "keywords", DbType.NVarChar, Keywordmeta);
        db.AddParameters(ref dt, "shortlink", DbType.NVarChar, shortlink);
        db.AddParameters(ref dt, "Description", DbType.NVarChar, DescriptionMeta);
        db.AddParameters(ref dt, "kind", DbType.Int32, kind);
        db.ExecuteNonQuery(Sql, dt);

        // Lấy ID lớn nhất đẻ vẽ html
        Sql = "select * from " + TableName + " order by id DESC";
        DataTable dtGetID = db.GetData(Sql);
        if (dtGetID == null || dtGetID.Rows.Count == 0) return string.Empty;
        string ID = dtGetID.Rows[0]["id"].ToString();

        this.WriteContentToHtml(ID, content);
        return ID;
    }
    //insert Video or Image
    public string InsertProduct(string groupid, string title, string summary, string content, string created, int status, int kind, string fimage,
        string fimagekhac, string shortlink, string titlemeta, string keyword, string description )
    {
        string Sql = "INSERT INTO " + TableName + "(groupid, title, summary, created, userid, status ,kind, fimage, fimagekhac, shortlink, titlemeta, keywords, Description, idlienket)";
        Sql += " VALUES(?groupid, ?title, ?summary,  ?created, ?userid, ?status, ?kind, ?fimage, ?fimagekhac, ?shortlink, ?titlemeta, ?keywords, ?Description, ?idlienket)";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "groupid", DbType.NVarChar, groupid);
        db.AddParameters(ref dt, "title", DbType.NVarChar, title);
        db.AddParameters(ref dt, "summary", DbType.NVarChar, summary);
        db.AddParameters(ref dt, "created", DbType.Datetime, created);
        db.AddParameters(ref dt, "userid", DbType.NVarChar, Membertask.Name);
        db.AddParameters(ref dt, "status", DbType.Int32, status);
        db.AddParameters(ref dt, "kind", DbType.Int32, kind);
        db.AddParameters(ref dt, "fimage", DbType.NVarChar, fimage);
        db.AddParameters(ref dt, "fimagekhac", DbType.NVarChar, fimagekhac);
        db.AddParameters(ref dt, "shortlink", DbType.NVarChar, shortlink);
        db.AddParameters(ref dt, "titlemeta", DbType.NVarChar, titlemeta);
        db.AddParameters(ref dt, "keywords", DbType.NVarChar, keyword);
        db.AddParameters(ref dt, "Description", DbType.NVarChar, description);
        db.AddParameters(ref dt, "idlienket", DbType.NVarChar, string.Empty);
        db.ExecuteNonQuery(Sql, dt);

        Sql = "select * from " + TableName + " order by id DESC";
        DataTable dtGetID = db.GetData(Sql);
        if (dtGetID == null || dtGetID.Rows.Count == 0) return string.Empty;
        string ID = dtGetID.Rows[0]["id"].ToString();

        //
        this.WriteContentToHtml(ID, content);
        return ID;
    }
    private void WriteContentToHtml(string id, string content)
    {
        string sFilename = this.AbsHtmls + id + "_" + Globals.CurrentLang + ".htm";
        StreamWriter sw = new StreamWriter(sFilename, false, Encoding.UTF8);
        sw.Write(content);
        sw.Close();
    }
    private void DeleteHtmlFile(string id)
    {
        string sFilename = this.AbsHtmls + id + "_" + Globals.CurrentLang + ".htm";
        if (File.Exists(sFilename))
            File.Delete(sFilename);
    }
    //Users
    public DataTable Searching_ofUsers(string Keyword, string GroupID, int kind, int Status, int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;

        string Sql = string.Empty;
        Sql = "SELECT COUNT(ID) AS Total FROM " + TableName + "  ";
        Sql += "WHERE kind = " + kind + " ";
        if (Status == 0) { Sql += "AND status = 0 "; }
        if (Status == 1) { Sql += "AND status = 1 "; }
        if (Status == 2) { Sql += "AND 1 = 1 "; }
        if (GroupID != null && GroupID != string.Empty && GroupID != "")
        {
            Sql += "AND groupid IN (" + GroupID + ") ";
        }
        if (Keyword != string.Empty && Keyword != null && Keyword != "")
        {
            string dau = Keyword.Trim().Substring(0, 1);
            string sau = Keyword.Trim().Substring(1);
            string abc = dau.ToUpper() + sau.ToLower();
            Sql += "AND( title LIKE N'%" + Keyword.Trim() + "%' OR title LIKE N'%" + Keyword.Trim().ToLower() + "%' OR title LIKE N'%" + Keyword.Trim().ToUpper() + "%' OR  title LIKE N'%" + abc + "%' ";
            //Sql += "OR masanpham LIKE N'%" + Keyword.Trim() + "%' OR masanpham LIKE N'%" + Keyword.Trim().ToUpper() + "%' OR masanpham LIKE N'%" + Keyword.Trim().ToLower() + "%' OR  masanpham LIKE N'%" + abc + "%' ";
            Sql += "OR summary LIKE N'%" + Keyword.Trim() + "%' OR summary LIKE N'%" + Keyword.Trim().ToUpper() + "%' OR summary LIKE N'%" + Keyword.Trim().ToLower() + "%' OR  summary LIKE N'%" + abc + "%') ";
        }
        dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        TotalRecords = Convert.ToInt32(dt.Rows[0]["Total"]);
        TotalPages = (int)TotalRecords / RecordPerPages;
        if ((int)TotalRecords % RecordPerPages > 0)
            TotalPages = TotalPages + 1;
        Sql = "Select " + TableName + ".*, row_number() over (order by created DESC) as row_index INTO #Temp_Table ";
        Sql += "From " + TableName + " ";
        Sql += "where kind = " + kind + "";
        if (Status == 0) { Sql += "AND status = 0 "; }
        if (Status == 1) { Sql += "AND status = 1 "; }
        if (Status == 2) { Sql += "AND 1 = 1 "; }
        if (GroupID != null && GroupID != string.Empty && GroupID != "")
        {
            Sql += "AND groupid IN (" + GroupID + ") ";
        }
        if (Keyword != string.Empty && Keyword != null && Keyword != "")
        {
            string dau = Keyword.Trim().Substring(0, 1);
            string sau = Keyword.Trim().Substring(1);
            string abc = dau.ToUpper() + sau.ToLower();
            Sql += "AND( title LIKE N'%" + Keyword.Trim() + "%' OR title LIKE N'%" + Keyword.Trim().ToLower() + "%' OR title LIKE N'%" + Keyword.Trim().ToUpper() + "%' OR  title LIKE N'%" + abc + "%' ";
            //Sql += "OR masanpham LIKE N'%" + Keyword.Trim() + "%' OR masanpham LIKE N'%" + Keyword.Trim().ToUpper() + "%' OR masanpham LIKE N'%" + Keyword.Trim().ToLower() + "%' OR  masanpham LIKE N'%" + abc + "%' ";
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
        dt = db.GetData(Sql);
        return dt;
    }
    //
    public DataTable Searching_ImageOrVideo(string Keyword, string GroupID, int Status, int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages, int kind)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;

        string Sql = string.Empty;
        Sql = "SELECT COUNT(ID) AS Total FROM " + TableName + "  ";
        Sql += "WHERE 1=1 and kind = " + kind + "";
        if (GroupID != null && GroupID != string.Empty && GroupID != "")
        {
            Sql += "AND groupid IN (" + GroupID + ") ";
        }
        if (Status == 0)
        {
            Sql += "AND status = 0 ";
        }
        if (Status == 1)
        {
            Sql += "AND status = 1 ";
        }
        if (Status == 2)
        {
            Sql += "AND 1 = 1 ";
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
        Sql = "Select " + TableName + ".*, row_number() over (order by created DESC) as row_index INTO #Temp_Table ";
        Sql += "From " + TableName + " ";
        Sql += "where 1 = 1 and kind =" + kind + " ";
        if (GroupID != null && GroupID != string.Empty && GroupID != "")
        {
            Sql += "AND groupid IN (" + GroupID + ") ";
        }
        if (Status == 0)
        {
            Sql += "AND status = 0 ";
        }
        if (Status == 1)
        {
            Sql += "AND status = 1 ";
        }
        if (Status == 2)
        {
            Sql += "AND 1 = 1 ";
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
        dt = db.GetData(Sql);
        return dt;
    }
    public void Delete(string id)
    {
        DbTask db = new DbTask();
        string Sql = "DELETE FROM " + TableName + " WHERE id = '" + id.Trim() + "'";
        db.ExecuteNonQuery(Sql);
        this.DeleteHtmlFile(id);

        ////xóa tin trong publish trang chủ TableName_NewsTrangChu
        //Sql = "";
        //Sql = "DELETE FROM " + TableName_NewsTrangChu + " WHERE id = '" + id.Trim() + "'";
        //db.ExecuteNonQuery(Sql);

        ////xóa tin trong publish tổ chức tin tức- sự kiện
        //Sql = "";
        //Sql = "DELETE FROM " + TableName_TinTucSuKien + " WHERE id = '" + id.Trim() + "'";
        //db.ExecuteNonQuery(Sql);

        ////xóa publish
        //Sql = "";
        //Sql = "DELETE FROM " + TablePublish + " WHERE id = '" + id.Trim() + "'";
        //db.ExecuteNonQuery(Sql);

        ////xóa trong publish tổ chức news bảng tbl_publish_noibat
        //Sql = "";
        //Sql = "DELETE FROM " + TableNoiBat + " WHERE id = '" + id.Trim() + "'";
        //db.ExecuteNonQuery(Sql);

    }
    public DataRow GetInfo(string id)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT * FROM " + TableName + " WHERE id = '" + id.Trim() + "' ";
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
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
    public static void GetImageSize(string strAbsolutePath, out int w, out int h)
    {
        try
        {
            System.Drawing.Image img2Scale = System.Drawing.Image.FromFile(strAbsolutePath);
            w = Convert.ToInt32(img2Scale.Width);
            h = Convert.ToInt32(img2Scale.Height);
            img2Scale.Dispose();
        }
        catch (ArgumentException)
        {
            w = 0; h = 0;
        }
    }
    public void Update(string id, string title, string groupid, string summary, string content, string fimage, string sTitleMeta, string KeywordMeta, string DescriptionMeta, string sShortLink)
    {
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " title = ?title,  ";
        Sql += " groupid = ?groupid,  ";
        Sql += " summary = ?summary,  ";
        Sql += " fimage = ?fimage,  ";
        Sql += " titlemeta = ?titlemeta, ";
        Sql += " Description = ?Description, ";
        Sql += " keywords = ?keywords, ";
        Sql += " shortlink = ?shortlink "; // sử dung linkurl thay cho trường sokyhieu để đỡ đổi db
        Sql += " WHERE id=?id";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "title", DbType.NVarChar, title);
        db.AddParameters(ref dt, "summary", DbType.NVarChar, summary);
        db.AddParameters(ref dt, "groupid", DbType.NVarChar, groupid);
        db.AddParameters(ref dt, "fimage", DbType.NVarChar, fimage);
        db.AddParameters(ref dt, "titlemeta", DbType.NVarChar, sTitleMeta);
        db.AddParameters(ref dt, "Description", DbType.NVarChar, DescriptionMeta);
        db.AddParameters(ref dt, "shortlink", DbType.NVarChar, sShortLink);
        db.AddParameters(ref dt, "keywords", DbType.NVarChar, KeywordMeta);
        db.ExecuteNonQuery(Sql, dt);
        this.WriteContentToHtml(id, content);
    }

    public void UpdateSanpham(string id, string groupid, string title, string summary, string content, string created, string fimage, string fimagekhac, string shortlink,
        string titlemeta, string keyword, string description)
    {
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " title = ?title,  ";
        Sql += " groupid = ?groupid, ";
        Sql += " summary = ?summary,  ";
        Sql += " created = ?created,";
        Sql += " fimage = ?fimage,  ";
        Sql += " fimagekhac = ?fimagekhac,  ";
        Sql += " shortlink = ?shortlink,  ";
        Sql += " titlemeta = ?titlemeta,  ";
        Sql += " keywords = ?keywords,  ";
        Sql += " Description = ?Description  ";
        Sql += " WHERE id=?id";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "groupid", DbType.NVarChar, groupid);
        db.AddParameters(ref dt, "title", DbType.NVarChar, title);
        db.AddParameters(ref dt, "summary", DbType.NVarChar, summary);
        db.AddParameters(ref dt, "created", DbType.Datetime, created);
        db.AddParameters(ref dt, "fimage", DbType.NVarChar, fimage);
        db.AddParameters(ref dt, "fimagekhac", DbType.NVarChar, fimagekhac);
        db.AddParameters(ref dt, "shortlink", DbType.NVarChar, shortlink);
        db.AddParameters(ref dt, "titlemeta", DbType.NVarChar, titlemeta);
        db.AddParameters(ref dt, "keywords", DbType.NVarChar, keyword);
        db.AddParameters(ref dt, "Description", DbType.NVarChar, description);

        db.ExecuteNonQuery(Sql, dt);
        this.WriteContentToHtml(id, content);
    }
    //update abum
    public void UpdateVideoOrImages(string id, string title, string groupid, string summary, string fimage, string fvideoimg)
    {
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " title = ?title,  ";
        Sql += " groupid = ?groupid,  ";
        Sql += " summary = ?summary,  ";
        Sql += " fimage = ?fimage,  ";
        Sql += " fvideoimg = ?fvideoimg,  ";
        Sql += " created = ?created  ";
        Sql += " WHERE id=?id";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "title", DbType.NVarChar, title);
        db.AddParameters(ref dt, "summary", DbType.NVarChar, summary);
        db.AddParameters(ref dt, "groupid", DbType.NVarChar, groupid);
        db.AddParameters(ref dt, "fimage", DbType.NVarChar, fimage);
        db.AddParameters(ref dt, "fvideoimg", DbType.NVarChar, fvideoimg);
        db.AddParameters(ref dt, "Created", DbType.NVarChar, DateTime.Now.ToString());
        db.ExecuteNonQuery(Sql, dt);
        // this.WriteContentToHtml(id, content);
    }

    //Bien tap
    public DataTable Searching_ofBientap(string Keyword, string GroupID, int Status, int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;

        string Sql = string.Empty;
        Sql = "SELECT COUNT(ID) AS Total FROM " + TableName + "  ";
        Sql += "WHERE 1=1 and kind = 0 ";
        if (GroupID != null && GroupID != string.Empty && GroupID != "")
        {
            Sql += "AND groupid IN (" + GroupID + ") ";
        }
        if (_mGroupIdAllow != string.Empty)
        {
            Sql += "AND groupid IN(" + _mGroupIdAllow + ") ";
        }

        if (Status == 0)
        {
            Sql += "AND status = 3 ";
        }
        if (Status == 1)
        {
            Sql += "AND (status >= 4 OR status = 1 OR status = 0) ";
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
        Sql = "Select " + TableName + ".*, row_number() over (order by created DESC) as row_index INTO #Temp_Table ";
        Sql += "From " + TableName + " ";
        Sql += "Where 1=1 and kind = 0 ";
        if (GroupID != null && GroupID != string.Empty && GroupID != "")
        {
            Sql += "AND groupid  IN (" + GroupID + ") ";
        }
        if (_mGroupIdAllow != string.Empty)
        {
            Sql += "AND groupid IN(" + _mGroupIdAllow + ") ";
        }
        if (Status == 0)
        {
            Sql += "AND status = 3 ";
        }
        if (Status == 1)
        {
            Sql += "AND (status >= 4 OR status = 1 OR status = 0) ";
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
        dt = db.GetData(Sql);
        return dt;
    }
    //Cho Duyet
    public DataTable Searching_ofChoDuyet(string Keyword, string GroupID, int Status, int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;

        string Sql = string.Empty;
        Sql = "SELECT COUNT(ID) AS Total FROM " + TableName + "  ";
        Sql += "WHERE 1=1 and kind = 0 ";
        if (GroupID != null && GroupID != string.Empty && GroupID != "")
        {
            Sql += "AND groupid IN (" + GroupID + ") ";
        }
        if (_mGroupIdAllow != string.Empty)
        {
            Sql += "AND groupid IN(" + _mGroupIdAllow + ") ";
        }

        Sql += "AND status = " + Status + " "; //Cho duyet

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
        
        Sql = "Select " + TableName + ".*, row_number() over (order by created DESC) as row_index INTO #Temp_Table ";
        Sql += "From " + TableName + " ";
        Sql += "where 1=1 and kind = 0";
        if (GroupID != null && GroupID != string.Empty && GroupID != "")
        {
            Sql += "AND groupid  IN (" + GroupID + ") ";
        }
        if (_mGroupIdAllow != string.Empty)
        {
            Sql += "AND groupid IN(" + _mGroupIdAllow + ") ";
        }
        Sql += "AND status = " + Status + " "; //Cho duyet

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
        dt = db.GetData(Sql);
        return dt;
    }
    public void SetStatus_Bientap(string id)
    {
        DataRow dr = this.GetInfo(id);
        if (dr == null)
            return;
        int status = 0;
        if (Convert.ToInt32(dr["status"]) == 2)
        {
            status = 3; //Bientap
        }
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " status = ?status ";
        Sql += " WHERE id = ?id ";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "status", DbType.Int32, status);
        db.ExecuteNonQuery(Sql, dt);
    }
    public void SetStatus_Choduyet(string id)
    {
        DataRow dr = this.GetInfo(id);
        if (dr == null)
            return;
        int status = 0;
        if (Convert.ToInt32(dr["status"]) == 3)
        {
            status = 4; // Cho duyet
        }
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " status = ?status ";
        Sql += " WHERE id = ?id ";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "status", DbType.Int32, status);
        db.ExecuteNonQuery(Sql, dt);
    }
    public void SetStatus_Taomoi(string id)
    {
        DataRow dr = this.GetInfo(id);
        if (dr == null)
            return;
        int status = 0;
        if (Convert.ToInt32(dr["status"]) == 3)
        {
            status = 2;// Tao moi
        }
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " status = ?status ";
        Sql += " WHERE id = ?id ";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "status", DbType.Int32, status);
        db.ExecuteNonQuery(Sql, dt);
    }
    public void SetStatus_ok(string id)
    {
        DataRow dr = this.GetInfo(id);
        if (dr == null)
            return;
        int status = 0;
        if (Convert.ToInt32(dr["status"]) == 4)
        {
            status = 0;//Ok
        }
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " status = ?status ";
        Sql += " WHERE id = ?id ";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "status", DbType.Int32, status);
        db.ExecuteNonQuery(Sql, dt);
    }
    public void SetStatus_notok(string id)
    {
        DataRow dr = this.GetInfo(id);
        if (dr == null)
            return;
        int status = 0;
        if (Convert.ToInt32(dr["status"]) == 4)
        {
            status = 3;//Bientap
        }
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " status = ?status ";
        Sql += " WHERE id = ?id ";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "status", DbType.Int32, status);
        db.ExecuteNonQuery(Sql, dt);

    }
    public void SetStatus(string id)
    {
        DataRow dr = this.GetInfo(id);
        if (dr == null)
            return;
        int status = 0;
        if (Convert.ToInt32(dr["status"]) == 0)
            status = 1;
        else
            status = 0;
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " status = ?status ";
        Sql += " WHERE id = ?id ";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "status", DbType.Int32, status);
        db.ExecuteNonQuery(Sql, dt);

    }
    public void SetHotlike(string id)
    {
        DataRow dr = this.GetInfo(id);
        if (dr == null)
            return;
        int status = 0;
        if (Convert.ToInt32(dr["hotlike"]) == 0)
            status = 1;
        else
            status = 0;
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " hotlike = ?hotlike ";
        Sql += " WHERE id = ?id ";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "hotlike", DbType.Int32, status);
        db.ExecuteNonQuery(Sql, dt);
    }

    public void Setquicksale(string id)
    {
        DataRow dr = this.GetInfo(id);
        if (dr == null)
            return;
        int status = 0;
        if (Convert.ToInt32(dr["sale"]) == 0)
            status = 1;
        else
            status = 0;
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " sale = ?sale ";
        Sql += " WHERE id = ?id ";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "sale", DbType.Int32, status);
        db.ExecuteNonQuery(Sql, dt);
    }
    public void SetHotNew(string id)
    {
        DataRow dr = this.GetInfo(id);
        if (dr == null)
            return;
        int status = 0;
        if (Convert.ToInt32(dr["hotnew"]) == 0)
            status = 1;
        else
            status = 0;
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " hotnew = ?hotnew ";
        Sql += " WHERE id = ?id ";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "hotnew", DbType.Int32, status);
        db.ExecuteNonQuery(Sql, dt);
    }
    public void Setamthuc(string id)
    {
        DataRow dr = this.GetInfo(id);
        if (dr == null)
            return;
        int amthuc = 0;
        if (Convert.ToInt32(dr["amthuc"]) == 0)
            amthuc = 1;
        else
            amthuc = 0;
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " amthuc = ?amthuc ";
        Sql += " WHERE id = ?id ";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "amthuc", DbType.Int32, amthuc);
        db.ExecuteNonQuery(Sql, dt);
    }

    public void Setthucdon(string id)
    {
        DataRow dr = this.GetInfo(id);
        if (dr == null)
            return;
        int thucdon = 0;
        if (Convert.ToInt32(dr["thucdon"]) == 0)
            thucdon = 1;
        else
            thucdon = 0;
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " thucdon = ?thucdon ";
        Sql += " WHERE id = ?id ";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "thucdon", DbType.Int32, thucdon);
        db.ExecuteNonQuery(Sql, dt);
    }
    public void SetStatus_Share(string id)
    {
        DataRow dr = this.GetInfo(id);
        if (dr == null)
            return;
        int share = 0;
        if (Convert.ToInt32(dr["share"]) == 0)
            share = 1;
        else
            share = 0;
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " share = ?share ";
        Sql += " WHERE id = ?id ";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "share", DbType.Int32, share);
        db.ExecuteNonQuery(Sql, dt);

    }
    public void SetDateStatus(string id)
    {
        DataRow dr = this.GetInfo(id);
        if (dr == null)
            return;
        int datestatus = 0;
        if (Convert.ToInt32(dr["datestatus"]) == 0)
            datestatus = 1;
        else
            datestatus = 0;
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " datestatus = ?datestatus ";
        Sql += " WHERE id = ?id ";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "datestatus", DbType.Int32, datestatus);
        db.ExecuteNonQuery(Sql, dt);

    }
    public void SetStatus_reok(string id)
    {
        DataRow dr = this.GetInfo(id);
        if (dr == null)
            return;
        int status = 0;
        if (Convert.ToInt32(dr["status"]) == 0)
        {
            status = 4; //muc cho duyet
        }
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " status = ?status ";
        Sql += " WHERE id = ?id ";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "status", DbType.Int32, status);
        db.ExecuteNonQuery(Sql, dt);
    }
    //Status OK
    public DataTable Searching(string Keyword, string GroupID, int Status, int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;

        string Sql = string.Empty;
        Sql = "SELECT COUNT(ID) AS Total FROM " + TableName + "  ";
        Sql += "WHERE 1=1 and kind = 0 ";
        if (GroupID != null && GroupID != string.Empty && GroupID != "")
        {
            Sql += "AND groupid IN (" + GroupID + ") ";
        }
        if (_mGroupIdAllow != string.Empty)
        {
            Sql += "AND groupid IN(" + _mGroupIdAllow + ") ";
        }

        if (Status == 0)
        {
            Sql += "AND status = 0 ";
        }
        if (Status == 1)
        {
            Sql += "AND status = 1 ";
        }
        if (Status == 2)
        {
            Sql += "AND (status = 1 OR status = 0) ";
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
        Sql = "Select " + TableName + ".*, row_number() over (order by created DESC) as row_index INTO #Temp_Table ";
        Sql += "From " + TableName + " ";
        Sql += "where 1=1 and kind = 0 ";
        if (GroupID != null && GroupID != string.Empty && GroupID != "")
        {
            Sql += "AND groupid IN (" + GroupID + ") ";
        }
        if (_mGroupIdAllow != string.Empty)
        {
            Sql += "AND groupid IN(" + _mGroupIdAllow + ") ";
        }
        if (Status == 0)
        {
            Sql += "AND status = 0 ";
        }
        if (Status == 1)
        {
            Sql += "AND status = 1 ";
        }
        if (Status == 2)
        {
            Sql += "AND (status = 1 OR status = 0) ";
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
        dt = db.GetData(Sql);
        return dt;
    }
    public void DeleteGroup(string groupid)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT id FROM " + TableName + " WHERE groupid LIKE '" + groupid + "%'";
        DataTable dt = db.GetData(Sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            string id = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                id = dr["id"].ToString();
                //Xoa log
                Logfile logfile = new Logfile();
                logfile.DeleteLog(id);
                //Xoa file
                Delete(id);
            }
        }
    }

    public void UpdateSTT(string id, int ncount)
    {
        try
        {
            string Sql = "UPDATE " + TableName + " SET ";
            Sql += " idx = ?idx  ";
            Sql += "WHERE id = '" + id + "' ";
            DbTask db = new DbTask();
            DataTable dt = null;
            db.AddParameters(ref dt, "idx", DbType.Int32, ncount);
            db.AddParameters(ref dt, "id", DbType.NVarChar, id);
            db.ExecuteNonQuery(Sql, dt);
        }
        catch (Exception ae)
        {

            string error = ae.ToString();
        }
    }
    public DataTable CheckShortlinkUrl(string sshortLink)
    {
        DbTask db = new DbTask();
        string Sql = "Select shortlink from " + TableName + " where shortlink = '" + sshortLink + "' ";
        DataTable dt = db.GetData(Sql);
        if (dt.Rows.Count > 0 && dt != null) return dt;
        return null;
    }
    //
    public DataTable GetAllDataSelect(string idLienKet)
    {
        if (idLienKet.Length > 0)
        {
            DbTask db = new DbTask();
            string Sql = "Select title, id from " + TableName + " where id IN (" + idLienKet + ")";
            return db.GetData(Sql);
        }
        else { return null; }
    }
    public DataTable Searching_ForLienKet(string username, string Keyword, string GroupID, int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages, string IdLienketC)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;
        string Sql = string.Empty;
        Sql = "SELECT COUNT(ID) AS Total FROM " + TableName + "  ";
        Sql += "WHERE 1 = 1 and status = 1";
        if (username != string.Empty)
        {
            Sql += "AND userid  LIKE N'%" + username + "%' ";
        }
        if (IdLienketC.Length > 0)
        {
            Sql += "AND id not in (" + IdLienketC + ")";
        }
        if (GroupID != null && GroupID != string.Empty && GroupID != "") { Sql += "AND groupid IN (" + GroupID + ") "; }
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
        if ((int)TotalRecords % RecordPerPages > 0) TotalPages = TotalPages + 1;
        Sql = "Select " + TableName + ".*, row_number() over (order by created DESC) as row_index INTO #Temp_Table ";
        Sql += "From " + TableName + " ";
        Sql += "where 1=1 and status = 1 ";
        if (username != string.Empty)
        {
            Sql += "AND userid  LIKE N'%" + username + "%' ";
        }
        if (IdLienketC.Length > 0)
        {
            Sql += "AND id not in (" + IdLienketC + ")";
        }
        if (GroupID != null && GroupID != string.Empty && GroupID != "") { Sql += "AND groupid IN (" + GroupID + ") "; }
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
        dt = db.GetData(Sql);
        return dt;
    }
    public void UpdateIdLienket(string id, string IdSelect)
    {
        DbTask db = new DbTask();
        string Sql = "Update " + TableName + " SET ";
        Sql += " idlienket = idlienket + '" + IdSelect + "' ";
        Sql += " WHERE id= '" + id + "'";
        db.ExecuteNonQuery(Sql);
    }
    public void UpdateIdLienketCheo(string id, string IdSelect)
    {
        DbTask db = new DbTask();
        string Sql = "Update " + TableName + " SET ";
        Sql += " idlienket = '" + IdSelect + "' ";
        Sql += " WHERE id= '" + id + "'";
        db.ExecuteNonQuery(Sql);
    }
    public void RemoveIDlienket(string id, string IdNew)
    {
        DbTask db = new DbTask();
        string Sql = "Update " + TableName + " SET ";
        Sql += " idlienket = '" + IdNew + "' ";
        Sql += " WHERE id='" + id + "'";
        db.ExecuteNonQuery(Sql);
    }

}