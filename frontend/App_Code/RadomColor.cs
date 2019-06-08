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
/// Summary description for RadomColor
/// </summary>
public class RadomColor
{
	public RadomColor()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string RandomColor_Line()
    {
        string allChar = "Red,#FFCC99,Blue,Green,Aqua,#000099,Purple,Navy,#87b9fe,#ff9b42,#666666,#0099FF,#CC3366,#FF0066,#FFCC66,#009966,#F7B519,#339933";
        string[] allCharArray = allChar.Split(',');
        string randomCode = "";        

        Random rand =new Random();
        int c = rand.Next(18);
        randomCode = allCharArray[c].ToString();
        return randomCode;
    }
    public string RandomColor_BackGround()
    {
        string allChar = "#fddbe3,#d8fde4,#e7fdd9,#d9e2fd,#fdfad9,#d9fdf9,#fcd6f8,#d9fde3,#e2dafd,#fcf0d6";
        string[] allCharArray = allChar.Split(',');
        string randomCode = "";

        Random rand = new Random();
        int c = rand.Next(10);
        randomCode = allCharArray[c].ToString();
        return randomCode;
    }

}
