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
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for AutoTag
/// </summary>
public class AutoTag
{
	public AutoTag()
	{
	}
    public static String UCS2_Convert(String sContent)
    {
        sContent = sContent.Trim();
        String sUTF8Lower = "a|á|à|ả|ã|ạ|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ|đ|e|é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ|i|í|ì|ỉ|ĩ|ị|o|ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ|u|ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự|y|ý|ỳ|ỷ|ỹ|ỵ";

        String sUTF8Upper = "A|Á|À|Ả|Ã|Ạ|Ă|Ắ|Ằ|Ẳ|Ẵ|Ặ|Â|Ấ|Ầ|Ẩ|Ẫ|Ậ|Đ|E|É|È|Ẻ|Ẽ|Ẹ|Ê|Ế|Ề|Ể|Ễ|Ệ|I|Í|Ì|Ỉ|Ĩ|Ị|O|Ó|Ò|Ỏ|Õ|Ọ|Ô|Ố|Ồ|Ổ|Ỗ|Ộ|Ơ|Ớ|Ờ|Ở|Ỡ|Ợ|U|Ú|Ù|Ủ|Ũ|Ụ|Ư|Ứ|Ừ|Ử|Ữ|Ự|Y|Ý|Ỳ|Ỷ|Ỹ|Ỵ";

        String sUCS2Lower = "a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|d|e|e|e|e|e|e|e|e|e|e|e|e|i|i|i|i|i|i|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|u|u|u|u|u|u|u|u|u|u|u|u|y|y|y|y|y|y";

        String sUCS2Upper = "A|A|A|A|A|A|A|A|A|A|A|A|A|A|A|A|A|A|D|E|E|E|E|E|E|E|E|E|E|E|E|I|I|I|I|I|I|O|O|O|O|O|O|O|O|O|O|O|O|O|O|O|O|O|O|U|U|U|U|U|U|U|U|U|U|U|U|Y|Y|Y|Y|Y|Y";

        String[] aUTF8Lower = sUTF8Lower.Split(new Char[] { '|' });

        String[] aUTF8Upper = sUTF8Upper.Split(new Char[] { '|' });

        String[] aUCS2Lower = sUCS2Lower.Split(new Char[] { '|' });

        String[] aUCS2Upper = sUCS2Upper.Split(new Char[] { '|' });

        Int32 nLimitChar;

        nLimitChar = aUTF8Lower.GetUpperBound(0);

        for (int i = 1; i <= nLimitChar; i++)
        {

            sContent = sContent.Replace(aUTF8Lower[i], aUCS2Lower[i]);

            sContent = sContent.Replace(aUTF8Upper[i], aUCS2Upper[i]);

        }
        string sUCS2regex = @"[A-Za-z0-9- ]";
        string sEscaped = new Regex(sUCS2regex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture).Replace(sContent, string.Empty);
        if (string.IsNullOrEmpty(sEscaped))
            return sContent;
        sEscaped = sEscaped.Replace("[", "\\[");
        sEscaped = sEscaped.Replace("]", "\\]");
        sEscaped = sEscaped.Replace("^", "\\^");
        string sEscapedregex = @"[" + sEscaped + "]";

        return new Regex(sEscapedregex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture).Replace(sContent, string.Empty);
    }
    public static string Auto_TagHTML(string source)
    {
        try
        {
            string result;
            result = source.ToLower().Replace("\"", ",");
            result = result.Replace("\"", ",");
            result = result.Replace("\"", ",");
            result = result.Replace("<br>", "");
            result = result.Replace("'", ",");
            result = result.Replace(".", ",");
            result = result.Replace("?", ",");
            result = result.Replace("!", ",");
            do
            {
                result = result.Replace("  ", " ");
            } while (result.IndexOf("  ") > 0);

            string[] aresult = result.Split(" ".ToCharArray());
            string sDon_Am = "cần|về|quá|vì|bị|do|làm|nhưng|cùng|một|hai|ba|như|sau|không|mà|các|lên|hoặc|giành|này|nhận|ngày|từ|thay|đều|vừa|gì|theo|cho|mới|của|sẽ|trên|và|đang|theo|của|rất|muốn|có|được|với|cả|đến|những|tại|ở|là|của|khi|còn|cũng|vì|có|trong|theo|tại|vào|";
            for (int i = 0; i < aresult.Length; i++)
            {
                if ((sDon_Am.IndexOf(aresult[i] + "|") >= 0))
                {
                    aresult[i] = ",";
                }
            }

            result = "";
            for (int i = 0; i < aresult.Length; i++)
            {
                result = result + " " + aresult[i];
            }
            aresult = result.Split(",".ToCharArray());

            result = "";
            string sTmp = "";
            for (int i = aresult.Length - 1; i > 0; i--)
            {
                sTmp = aresult[i].Trim();
                while (sTmp.StartsWith(","))
                {
                    sTmp = sTmp.Remove(0, 1);
                }
                while (sTmp.EndsWith(","))
                {
                    sTmp = sTmp.Remove(sTmp.Length - 1, 1);
                }
                if (sTmp.Trim().Length > 2) result = result + ", " + sTmp.Trim();
            }
            while (result.StartsWith(","))
            {
                result = result.Remove(0, 1);
            }
            while (result.EndsWith(","))
            {
                result = result.Remove(result.Length - 1, 1);
            }
            return result.Trim();
        }
        catch
        {
            return source;
        }
    }
    public static string bodau(string sourse)
    {
        string result = string.Empty;
        result = sourse.ToLower();
        do
        {
            result = result.Replace("  ", " ");
        } 
        while (result.IndexOf("  ") > 0);
        result = result.Replace("ấ", "a");
        result = result.Replace("ầ", "a");
        result = result.Replace("ẩ", "a");
        result = result.Replace("ẫ", "a");
        result = result.Replace("ậ", "a");
        result = result.Replace("ắ", "a");
        result = result.Replace("ằ", "a");
        result = result.Replace("ẳ", "a");
        result = result.Replace("ẵ", "a");
        result = result.Replace("ặ", "a");
        result = result.Replace("à", "a");
        result = result.Replace("á", "a");
        result = result.Replace("ả", "a");
        result = result.Replace("ã", "a");
        result = result.Replace("ạ", "a");
        result = result.Replace("â", "a");
        result = result.Replace("ă", "a");
        result = result.Replace("ế", "e");
        result = result.Replace("ề", "e");
        result = result.Replace("ể", "e");
        result = result.Replace("ễ", "e");
        result = result.Replace("ệ", "e");
        result = result.Replace("é", "e");
        result = result.Replace("è", "e");
        result = result.Replace("ẻ", "e");
        result = result.Replace("ẽ", "e");
        result = result.Replace("ẹ", "e");
        result = result.Replace("ê", "e");
        result = result.Replace("í", "i");
        result = result.Replace("ì", "i");
        result = result.Replace("ỉ", "i");
        result = result.Replace("ĩ", "i");
        result = result.Replace("ị", "i");
        result = result.Replace("ố", "o");
        result = result.Replace("ồ", "o");
        result = result.Replace("ổ", "o");
        result = result.Replace("ỗ", "o");
        result = result.Replace("ộ", "o");
        result = result.Replace("ớ", "o");
        result = result.Replace("ờ", "o");
        result = result.Replace("ở", "o");
        result = result.Replace("ỡ", "o");
        result = result.Replace("ợ", "o");
        result = result.Replace("ứ", "u");
        result = result.Replace("ừ", "u");
        result = result.Replace("ử", "u");
        result = result.Replace("ữ", "u");
        result = result.Replace("ự", "u");
        result = result.Replace("ý", "y");
        result = result.Replace("ỳ", "y");
        result = result.Replace("ỷ", "y");
        result = result.Replace("ỹ", "y");
        result = result.Replace("ỵ", "y");
        result = result.Replace("đ", "d");
        result = result.Replace("ó", "o");
        result = result.Replace("ò", "o");
        result = result.Replace("ỏ", "o");
        result = result.Replace("õ", "o");
        result = result.Replace("ọ", "o");
        result = result.Replace("ô", "o");
        result = result.Replace("ơ", "o");
        result = result.Replace("ú", "u");
        result = result.Replace("ù", "u");
        result = result.Replace("ủ", "u");
        result = result.Replace("ũ", "u");
        result = result.Replace("ụ", "u");
        result = result.Replace("ư", "u");
        return result;
    }
    public static object[] StringToArray(string input, string separator, Type type)
    {
        string[] stringList = input.Split(separator.ToCharArray(),
                                          StringSplitOptions.RemoveEmptyEntries);
        object[] list = new object[stringList.Length];

        for (int i = 0; i < stringList.Length; i++)
        {
            list[i] = Convert.ChangeType(stringList[i], type);
        }

        return list;
    }
}
