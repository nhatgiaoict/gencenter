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
/// Summary description for Slide
/// </summary>
public class Slide
{
    public static string TableName
    {
        get { return "tbl_slide_" + Globals.CurrentLang; }
    }
	public Slide()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetData()
    {
        string Sql = "select * from " + TableName + " where status = 1";
        DbTask db = new DbTask();
        return db.GetData(Sql);
    }
    public DataTable SearchingSlide(int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages)
    {

        DbTask db = new DbTask();
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;
        string Sql = string.Empty;
        Sql = " SELECT COUNT(ID) AS Total FROM " + TableName + " WHERE status = 1";
        dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        TotalRecords = Convert.ToInt32(dt.Rows[0]["Total"]);
        TotalPages = (int)TotalRecords / RecordPerPages;
        if ((int)TotalRecords % RecordPerPages > 0)
            TotalPages = TotalPages + 1;
        Sql = "Select " + TableName + ".*, row_number() over (order by id ASC) as row_index INTO #Temp_Table ";
        Sql += "from " + TableName + " WHERE status = 1 ";
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
