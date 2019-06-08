using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Text;

/// <summary>
/// Summary description for clsHoivien
/// </summary>
public class clsHoivien
{
    public static int _nLength = 2;
    private static string TableHoiVien
    {
        get { return "tbl_hoivien_" + Globals.CurrentLang; }
    }
    private  string tblGroup
    {
        get { return "tbl_group_" + Globals.CurrentLang; }
    }
    private static string _mIdAllow = string.Empty;
	public clsHoivien()
	{
		//
		// TODO: Add constructor logic here
		//
        if (Globals.CheckExist)
        {
            try
            {
                //Create Table
                string Sql = "CREATE TABLE IF NOT EXISTS " + TableHoiVien + "(";
                Sql += "id nvarchar(255) NOT NULL default '', ";
                Sql += "title nvarchar(500) default NULL, ";
                Sql += "summary mediumtext, ";
                Sql += "status tinyint(4) default 0, ";
                Sql += "inquiry tinyint(4) default 0, ";
                Sql += "idx tinyint(4) default 0, ";
                Sql += "PRIMARY KEY  (id)) ";
                Sql += "ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_bin;";
                DbTask db = new DbTask();
                db.ExecuteNonQuery(Sql);
            }
            catch
            {

            }
        }
        if (!Membertask.IsAdministrator())
            _mIdAllow = Membertask.IsChuyenmuc();
        else
            _mIdAllow = string.Empty;
	}

    public string AbsHtmls
    {
        get
        {
            return Globals.AbsData + "news/htmls/";
        }
    }
    public string InsertHoiVien(string groupid, string title, string summary, string created, int status, string logo, string capbac, string content)
    {
        string Sql = string.Empty;
        Sql += " INSERT INTO " + TableHoiVien + "(groupid, title, summary, created, status, logo, capbac, Content)";
        Sql += " VALUES(?groupid, ?title, ?summary, ?created, ?status, ?logo, ?capbac, ?Content )";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "groupid", DbType.NVarChar, groupid);
        db.AddParameters(ref dt, "title", DbType.NVarChar, title);
        db.AddParameters(ref dt, "summary", DbType.NVarChar, summary);
        db.AddParameters(ref dt, "created", DbType.NVarChar, created);
        db.AddParameters(ref dt, "status", DbType.Int32, status);
        db.AddParameters(ref dt, "logo", DbType.NVarChar, logo);
        db.AddParameters(ref dt, "Content", DbType.Ntext, content);
        db.AddParameters(ref dt, "capbac", DbType.NVarChar, capbac);
        db.ExecuteNonQuery(Sql, dt);

        Sql = "select * from " + TableHoiVien + " order by created DESC";
        DataTable dtGetID = db.GetData(Sql);
        if (dtGetID == null || dtGetID.Rows.Count == 0) return string.Empty;
        string ID = dtGetID.Rows[0]["id"].ToString();
        //
        return ID;
    }

    public void UpdateHoivien(string id, string groupid, string title, string summary, string created, string logo, string capbac, string content)
    {
        string Sql = "UPDATE " + TableHoiVien + " SET ";
        Sql += " title = ?title,  ";
        Sql += " groupid = ?groupid, ";
        Sql += " summary = ?summary,  ";
        Sql += " content = ?content,  ";
        Sql += " capbac = ?capbac,  ";
        Sql += " logo = ?logo  ";
        Sql += " WHERE id=?id";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "groupid", DbType.NVarChar, groupid);
        db.AddParameters(ref dt, "title", DbType.NVarChar, title);
        db.AddParameters(ref dt, "summary", DbType.NVarChar, summary);
        db.AddParameters(ref dt, "capbac", DbType.NVarChar, capbac);
        db.AddParameters(ref dt, "content", DbType.Ntext, content);
        db.AddParameters(ref dt, "logo", DbType.NVarChar, logo);
        db.ExecuteNonQuery(Sql, dt);
    }

    public void UpdateIndexHoiVien(string id, int idx)
    {
        try
        {
            string Sql = " UPDATE " + TableHoiVien + " ";
            Sql += " SET idx = " + idx + " ";
            Sql += " Where id = '" + id + "'";
            DbTask db = new DbTask();
            db.ExecuteNonQuery(Sql);
        }
        catch { }
    }
    public DataTable SearchHoivien(string Keyword, string GroupID, int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;
        string Sql = string.Empty;
        Sql = "SELECT COUNT(ID) AS Total FROM " + TableHoiVien + " as a  ";
        Sql += "WHERE 1 = 1 ";
        if (GroupID != null && GroupID != string.Empty && GroupID != "") { Sql += "AND a.groupid IN (" + GroupID + ") "; }
        if (Keyword != string.Empty && Keyword != null && Keyword != "")
        {
            string dau = Keyword.Trim().Substring(0, 1);
            string sau = Keyword.Trim().Substring(1);
            string abc = dau.ToUpper() + sau.ToLower();
            Sql += "AND(a.title LIKE N'%" + Keyword.Trim() + "%' OR a.title LIKE N'%" + Keyword.Trim().ToLower() + "%' OR a.title LIKE N'%" + Keyword.Trim().ToUpper() + "%' OR  a.title LIKE N'%" + abc + "%' )";
        }
        dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        TotalRecords = Convert.ToInt32(dt.Rows[0]["Total"]);
        TotalPages = (int)TotalRecords / RecordPerPages;
        if ((int)TotalRecords % RecordPerPages > 0) TotalPages = TotalPages + 1;

        Sql = "Select a.*, b.title as Nganhnghe, row_number() over (order by a.idx ASC) as row_index INTO #Temp_Table ";
        Sql += "From " + TableHoiVien + " as a INNER JOIN " + tblGroup + " as b ON a.groupid = b.id ";
        Sql += "where 1 = 1 ";
      
        if (GroupID != null && GroupID != string.Empty && GroupID != "") { Sql += "AND a.groupid IN (" + GroupID + ") "; }
        if (Keyword != string.Empty && Keyword != null && Keyword != "")
        {
            string dau = Keyword.Trim().Substring(0, 1);
            string sau = Keyword.Trim().Substring(1);
            string abc = dau.ToUpper() + sau.ToLower();
            Sql += "AND(a.title LIKE N'%" + Keyword.Trim() + "%' OR a.title LIKE N'%" + Keyword.Trim().ToLower() + "%' OR a.title LIKE N'%" + Keyword.Trim().ToUpper() + "%' OR  a.title LIKE N'%" + abc + "%' )";
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

    public DataRow GetInfo(string id)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT * FROM " + TableHoiVien + " WHERE id = '" + id.Trim() + "' ";
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
        string Sql = "UPDATE " + TableHoiVien + " SET ";
        Sql += " status = ?status ";
        Sql += " WHERE id = ?id ";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "status", DbType.Int32, status);
        db.ExecuteNonQuery(Sql, dt);

    }

    // xóa một hội viên trong trang quản lý
    public void Delete(string id)
    {
        DbTask db = new DbTask();
        string Sql = "DELETE FROM " + TableHoiVien + " WHERE id = '" + id.Trim() + "'";
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

    private void DeleteHtmlFile(string id)
    {
        string sFilename = this.AbsHtmls + id + "_" + Globals.CurrentLang + ".htm";
        if (File.Exists(sFilename))
            File.Delete(sFilename);
    }
}