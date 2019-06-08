using System;
using System.Collections;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Security.Principal;
using System.Configuration;
using System.Collections.Specialized;
using System.ComponentModel;

namespace filemanager.components
{
	/// <summary>
	/// Summary description for Globals.
	/// </summary>
	public class Globals
	{
		public Globals()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		private static string GetConfigString(string configSection, string configKey, string defaultValue) 
		{
			NameValueCollection configSettings = ConfigurationManager.GetSection(configSection) as NameValueCollection;
			if ( configSettings != null ) 
			{
				string configValue = configSettings[configKey] as string;
				if ( configValue != null ) 
				{
					return configValue;
				}
			}
			return defaultValue;			
		}
		//
		public static string UrlRoot
		{
			get 
			{
				string sRet = System.Web.HttpContext.Current.Request.ApplicationPath;

				if(!sRet.EndsWith("/"))
					sRet = sRet+"/";
				return sRet;
			}
		}

		public static string UrlRootPath
		{
			get
			{
				return (GetConfigString("FileManagerSettings","urlRootFileManager",""));
			}
		}
		public static string AbsThumbDir
		{
			get
			{
				return (UrlRoot + GetConfigString("FileManagerSettings","absThumbDirFileManager",""));
			}
		}
		public static string UrlThumbDir
		{
			get
			{
				return GetConfigString("FileManagerSettings","urlThumbDirFileManager","");
			}
		}
		public static string editableFileTypes
		{
			get
			{
				return GetConfigString("FileManagerSettings","editableFileTypesFileManager","");

			}
		}
		public static string imageFileTypes
		{
			get
			{
				return GetConfigString("FileManagerSettings","imageFileTypesFileManager","");

			}
		}
		public static string musicFileTypes
		{
			get
			{
				return GetConfigString("FileManagerSettings","musicFileTypesFileManager","");

			}
		}
	
	}
}
