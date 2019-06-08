using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

/// <summary>
/// Summary description for Global
/// </summary>
public class Global: System.Web.HttpApplication
{
    private static string FilePath
    {
        get
        {
            string sPath = Globals.UrlCounter + "counters/";
            if (!Directory.Exists(sPath))
                Directory.CreateDirectory(sPath);
            return sPath + "counter.txt";
        }
    }
    private static double m_nVisited = 0;
    private static int m_nOnline = 0;
    public static double Visited
    {
        get { return m_nVisited; }
        set { m_nVisited = value; }
    }
    public static int nOnline
    {
        get { return m_nOnline; }
        set { m_nOnline = value; }
    }
    private System.ComponentModel.IContainer components = null;
    public Global()
    {
        InitializeComponent();
    }
    protected void Application_Start(Object sender, EventArgs e)
    {
        // Code that runs on application startup
        try
        {
            double counterValue = 0;
            if (File.Exists(FilePath))
            {
                using (StreamReader sr = new StreamReader(FilePath))
                {
                    string result = sr.ReadLine();
                    if (result != null)
                    {
                        string delimStr = "\t";
                        char[] delimiter = delimStr.ToCharArray();
                        string[] arrResult = null;
                        arrResult = result.Split(delimiter);
                        if (arrResult.Length == 2)
                        {
                            counterValue = Convert.ToDouble(arrResult[1]);
                        }
                    }
                }
            }
            m_nVisited = counterValue;
            m_nOnline = 0;
        }
        catch
        {
        }
    }

    protected void Session_Start(Object sender, EventArgs e)
    {
        // Code that runs when a new session is started
        try
        {
            m_nOnline++;
            m_nVisited++;
            
            //ghi
            StreamWriter sw = File.CreateText(FilePath);
            sw.WriteLine("Total\t" + (m_nVisited).ToString());
            sw.Close();
        }
        catch
        {

        }
    }

    protected void Application_BeginRequest(Object sender, EventArgs e)
    {

    }

    protected void Application_EndRequest(Object sender, EventArgs e)
    {

    }

    protected void Application_AuthenticateRequest(Object sender, EventArgs e)
    {

    }

    protected void Application_Error(Object sender, EventArgs e)
    {

    }

    protected void Session_End(Object sender, EventArgs e)
    {
        m_nOnline--;       
    }

    protected void Application_End(Object sender, EventArgs e)
    {

    }

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
    }
}