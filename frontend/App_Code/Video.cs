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
/// Summary description for Video
/// </summary>
public class Video
{
	public Video()
	{
	
	}
    public static string TableName
    {
        get { return "tbl_video_" + Globals.CurrentLang; }
    }

    public DataRow GetVideo()
    {
        DbTask db = new DbTask();
        string Sql = "SELECT TOP 1 filename FROM " + TableName + "";
        Sql += " WHERE status=1";
        DataTable dt = null;
        dt=db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
}

