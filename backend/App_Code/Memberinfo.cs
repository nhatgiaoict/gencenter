using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using uss.utils;
using System.Configuration;

/// <summary>
/// Summary description for Memberinfo
/// </summary>
public class Memberinfo
{
      //Các biến cục bộ
        private string m_Name = "";

        private string m_AbsConfPathPrefix = "";

        private CustomConfig m_Config = null;
        //private uss.login.Login oLogin=null;

        //Các thuộc tính
        //---------------------------------------------
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        public Memberinfo()
        {
            m_AbsConfPathPrefix = Globals.AbsConfs;
        }
        //-------------------------------------------------------------------------

        public string GetConfigItem(string Session, string ConfigKey)
        {
            string FileName = m_AbsConfPathPrefix + m_Name;
            m_Config = new CustomConfig(FileName);
            return m_Config.ReadItem(Session, ConfigKey);
        }
        //-------------------------------------------------------------------------
        public void SetConfigItem(string Session, string ConfigKey, string Value)
        {
            //Lấy giá trị của một Key trong file config
            string FileName = "";
            if (m_Name == "") return;
            FileName = m_AbsConfPathPrefix + m_Name;
            m_Config = new CustomConfig(FileName);
            m_Config.WriteItem(Session, ConfigKey, Value);
            m_Config.Save();
        }
        public void RemoveSessionItem(string Session)
        {
            string FileName = "";

            FileName = m_AbsConfPathPrefix + m_Name;
            m_Config = new CustomConfig(FileName);
            m_Config.RemoveSession(Session);
            m_Config.Save();
        }
    
}
