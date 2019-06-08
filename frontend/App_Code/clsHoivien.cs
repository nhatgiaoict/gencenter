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
    private static string TableHoiVien
    {
        get { return "tbl_hoivien_" + Globals.CurrentLang; }
    }
    private static string TablSanpham
    {
        get { return "tbl_news_" + Globals.CurrentLang; }
    }
    private  string tblGroup
    {
        get { return "tbl_group_" + Globals.CurrentLang; }
    }
    public string AbsHtmls
    {
        get
        {
            return Globals.AbsData + "news/htmls/";
        }
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
        if (GroupID != null && GroupID != string.Empty && GroupID != "") { Sql += "AND a.groupid like '" + GroupID + "%' "; }
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

        Sql = "Select a.*, b.title as Nganhnghe, row_number() over (order by a.created DESC) as row_index INTO #Temp_Table ";
        Sql += "From " + TableHoiVien + " as a INNER JOIN " + tblGroup + " as b ON a.groupid = b.id ";
        Sql += "where 1 = 1 ";

        if (GroupID != null && GroupID != string.Empty && GroupID != "") { Sql += "AND a.groupid like '" + GroupID + "%' "; }
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


    public DataTable GetHoiVien(int N)
    {
        DbTask db = new DbTask();
        string SQL = "Select TOP " + N + " title, logo, id, summary, created, row_number() over (order by created DESC) as row_index, groupid from " + TableHoiVien + " where status = 1";
        return db.GetData(SQL);
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

    public DataRow GetInfoByShortlink(string shortlink)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT id, title, groupid FROM " + TableHoiVien + " WHERE shortlink = '" + shortlink.Trim() + "' ";
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

    public DataTable GETSPBYIDHOIVIEN(string IdHoivien, int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;
        string Sql = string.Empty;
        Sql = "SELECT COUNT(ID) AS Total FROM " + TablSanpham + " as a  ";
        Sql += "WHERE status = 1 ";
        if (IdHoivien != null && IdHoivien != string.Empty && IdHoivien != "") { Sql += "AND a.idhoivien = " + IdHoivien + " "; }
        dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;

        TotalRecords = Convert.ToInt32(dt.Rows[0]["Total"]);
        TotalPages = (int)TotalRecords / RecordPerPages;
        if ((int)TotalRecords % RecordPerPages > 0) TotalPages = TotalPages + 1;

        Sql = "Select a.*, row_number() over (order by a.created DESC) as row_index INTO #Temp_Table ";
        Sql += "From " + TablSanpham + " as a  ";
        Sql += "where status = 1 ";

        if (IdHoivien != null && IdHoivien != string.Empty && IdHoivien != "") { Sql += "AND a.idhoivien  = " + IdHoivien + " "; }
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
}