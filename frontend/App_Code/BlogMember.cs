using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for BlogMember
/// </summary>
public class BlogMember
{
    public static string TableName
    {
        get { return "tbl_blog_" + Globals.CurrentLang; }
    }

    public BlogMember()
	{
		
	}
    public DataTable GetBlogMember()
    {
        DbTask db = new DbTask();
        string Sql = "SELECT * FROM " + TableName + "";
        Sql += " WHERE status=1";
       return db.GetData(Sql);
    }
    public DataTable GetALLBlogMember()
    {
        DbTask db = new DbTask();
        string Sql = "SELECT * FROM " + TableName + "";
        return db.GetData(Sql);
    }
    public DataTable Searching(int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;
        string dtNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string Sql = string.Empty;
        Sql = "SELECT COUNT(a.ID) AS Total FROM " + TableName + " as a ";
        dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        TotalRecords = Convert.ToInt32(dt.Rows[0]["Total"]);
        TotalPages = (int)TotalRecords / RecordPerPages;
        if ((int)TotalRecords % RecordPerPages > 0)
            TotalPages = TotalPages + 1;

        Sql = "SELECT  distinct a.*, row_number() over (order by created DESC) as row_index INTO #Temp_Table FROM " + TableName + " as a ";
        Sql += " WHERE a.created <= '" + dtNow + "'";
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
