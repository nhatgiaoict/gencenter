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
/// Summary description for Enum
/// </summary>
public class Enum
{
	public Enum()
	{
	}
    /// <summary>
    /// Đường dẫn lưu files xml
    /// </summary>
    public static string PathXMLS
    {
        get
        {
            //return "Data\\data\\xmls\\";
            return "data\\";
        }
    }
    /// <summary>
    /// Đường dẫn lưu ảnh upload lên
    /// </summary>
    public static string PathImages
    {
        get
        {
            return "Data\\img\\";
        }
    }
    public static string PathImagesNew
    {
        get
        {
            return "Data\\";
        }
    }
}
