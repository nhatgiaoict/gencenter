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
/// Summary description for RandomCodes
/// </summary>
public class RandomCodes
{
	public RandomCodes()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string CreateRandomCode(int codeCount)
    {
        string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
        string[] allCharArray = allChar.Split(',');
        string randomCode = "";
        int temp = -1;

        Random rand = new Random();
        for (int i = 0; i < codeCount; i++)
        {
            if (temp != -1)
            {
                rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
            }
            int t = rand.Next(36);
            if (temp != -1 && temp == t)
            {
                return CreateRandomCode(codeCount);
            }
            temp = t;
            randomCode += allCharArray[t];
        }
        return randomCode;
    }
}
