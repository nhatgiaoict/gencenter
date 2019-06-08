using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Permissions;
using System.Configuration;
using System.Web;
namespace filemanager.components
{
	/// <summary>
	/// Summary description for FTP.
	/// </summary>
	public class FTP
	{
		public FTP()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public static string getParentDirectory(string path)
		{	
			// this function works for /main/db/test/ as well as /main/db/test.aspx
			string AbsRootPath = Globals.UrlRootPath;
			if(path ==  null || path == string.Empty || path == "/" || path == AbsRootPath)
				//return(AbsRootPath);	// can't go higher than root
				return string.Empty;
			else
			{
				// remove trailing "/" at end of path
				if(path.LastIndexOf("/") == path.Length-1)				
					path = path.Remove(path.LastIndexOf("/"), (path.Length - path.LastIndexOf("/")));
				
				try
				{									
					DirectoryInfo dir = new DirectoryInfo(path);
					string parentPath = dir.Parent.FullName;					
					parentPath = parentPath.Remove(0,2).Replace("\\","/");
					return parentPath;
				}
				catch
				{
					return(HttpContext.Current.Server.MapPath(AbsRootPath));	// default to root;
				}
			}			
		}

		public static void ReportError(string problem, string tech, string suggestion)
		{
			// outputs error, in english, and in tech, and with any suggestions.
			System.Web.HttpContext context = System.Web.HttpContext.Current;
			string output = "<font color=red>" + problem + "</font><hr>";
			if(suggestion != "")
				output += "Suggestion: " + suggestion + "<hr>";
			if(tech != "")
				output += "<small>Technical details: " + tech + "</small><hr>";
			context.Response.Write(output);
		}

		public static void deleteFile(string name, string path)
		{
			System.Web.HttpContext context = System.Web.HttpContext.Current;	
			
			//FileInfo file = new FileInfo(name);
			try
			{
				//	file.Delete();
				//File.Delete(name);
				
				File.Delete(name);
				context.Response.Redirect(context.Request.CurrentExecutionFilePath+"?Path="+path);
			}
			catch(UnauthorizedAccessException unEx)
			{
				FTP.ReportError("Access denied. Could not delete file", unEx.Message, "");
			}
			catch(Exception ex)
			{
				FTP.ReportError("Could not delete file", ex.Message, "If the file has just been created / edited / saved, wait a few seconds for Windows to release the file lock and try again. " + 
					"<a href=\"" + context.Request.ServerVariables["URL"].ToString() + "?" + context.Request.QueryString.ToString() + "\">&lt;refresh&gt;</a>");
			}
		}

		public static void deleteFolder(string name, string path)
		{
			System.Web.HttpContext context = System.Web.HttpContext.Current;						
			try
			{
				Directory.Delete(name, true);
			}
			catch(UnauthorizedAccessException unEx)
			{
				FTP.ReportError("Access denied. Could not delete folder (or sub-items)", unEx.Message, "");
			}
			catch(Exception ex)
			{
				FTP.ReportError("Could not delete folder", ex.Message, "");
			}
			context.Response.Redirect(context.Request.CurrentExecutionFilePath+"?Path="+path);			
		}
				
		public static void renameFile(string name, string path, string newName)
		{
			System.Web.HttpContext context = System.Web.HttpContext.Current;	
			string rootDir = Globals.UrlRootPath;
			string sDir = string.Empty;
			sDir= HttpContext.Current.Server.MapPath(rootDir + path + name);

			FileInfo f = new FileInfo(sDir);
			try
			{
				// check for a rename to a different folder
				if(newName.IndexOf("/") > -1)
					//f.MoveTo(context.Server.MapPath(newName));
					f.MoveTo(newName);
				else
				{	// just rename the file in the current directory
					if(path == "/")
						//f.MoveTo(context.Server.MapPath(path + newName));
						f.MoveTo(path + newName);
					else
						//f.MoveTo(context.Server.MapPath(path + "/" + newName));
						sDir = rootDir +path + "/"+ newName;
						f.MoveTo(sDir);
				}
				context.Response.Redirect(context.Request.CurrentExecutionFilePath+"?Path="+path);
			}
			catch(UnauthorizedAccessException unEx)
			{
				FTP.ReportError("Access denied. Could not rename file", unEx.Message, "");
			}
			catch(Exception ex)
			{
				FTP.ReportError("Could not rename file", ex.Message, "If the file has just been created / edited / saved, wait a few seconds for Windows to release the file lock and try again. " + 
					"<a href=\"" + context.Request.ServerVariables["URL"].ToString() + "?" + context.Request.QueryString.ToString() + "\">&lt;refresh&gt;</a>");
			}
		}

		public static void renameFolder(string name, string path, string newName)
		{
			System.Web.HttpContext context = System.Web.HttpContext.Current;	
			string rootDir = Globals.UrlRootPath;
			string sDir = string.Empty;
			sDir= HttpContext.Current.Server.MapPath(rootDir + path + name);
			DirectoryInfo d = new DirectoryInfo(sDir);
			try
			{
				// check for a rename to a different folder
				if(newName.IndexOf("/") > -1)
					//d.MoveTo(context.Server.MapPath(newName));
					d.MoveTo(newName);
				else	// just rename the folder in the current directory
				{	
					if(path == "/")
						//d.MoveTo(context.Server.MapPath(path + newName));
						d.MoveTo(path + newName);
					else
						//d.MoveTo(context.Server.MapPath(path + "/" + newName));
						sDir = rootDir +path + "/"+ newName;
						d.MoveTo(sDir);
				}
				context.Response.Redirect(context.Request.CurrentExecutionFilePath+"?Path="+path);
			}
			catch(UnauthorizedAccessException unEx)
			{
				FTP.ReportError("Access denied. Could not rename folder", unEx.Message, "");
			}
			catch(Exception ex)
			{
				FTP.ReportError("Could not rename folder", ex.Message, "If the file has just been created / edited / saved, wait a few seconds for Windows to release the file lock and try again. " + 
					"<a href=\"" + context.Request.ServerVariables["URL"].ToString() + "?" + context.Request.QueryString.ToString() + "\">&lt;refresh&gt;</a>");
			}
		}

		public static void viewAsThumbnails(string path)
		{
			System.Web.HttpContext context = System.Web.HttpContext.Current;	
			
			// check the thumbDir from web.config
			string thumbDir = Globals.AbsThumbDir;
			/*if(thumbDir == null)
			{
				FTP.ReportError("No thumbDir key in web.config, cannot continue", "", "");
				return;
			}
			else
				thumbDir = ConfigurationSettings.AppSettings["thumbDir"].ToString();*/

			// if the folder doesn't exist, create it
			//DirectoryInfo d = new DirectoryInfo(context.Server.MapPath(thumbDir));
			DirectoryInfo d = new DirectoryInfo(thumbDir);
			if(d.Exists == false)
			{
				try
				{
					d.Create();
				}
				catch
				{
					FTP.ReportError("Could not create '" + thumbDir + "' folder for temp storage of thumbnail images", "", "");
					return;
				}
			}
			else
			{
				/*	*** delete all the files in the thumbDir directory ***
					note to users: uncomment this section if you like, but exercise caution! 
					if you accidentally set the thumbDir to "/" in web.config, 
					it will empty your root directory. this is too dangerous to
					have it in by default, but it does work and will keep the 
					directory tidy.
				*/
				
				/*try
				{
					foreach(FileInfo f in d.GetFiles())
						f.Delete();
				}
				catch(Exception ex)
				{
					FTP.ReportError("Could not delete old files from thumbDir", ex.ToString(), "");
				}*/
			}

			// get all the files in the 'path' directory
			//DirectoryInfo picsDir = new DirectoryInfo(context.Server.MapPath(path));
			path = HttpContext.Current.Server.MapPath( Globals.UrlRootPath + path);
			DirectoryInfo picsDir = new DirectoryInfo(path);
			try
			{
				int numPics = 0;
				foreach(FileInfo f in picsDir.GetFiles())
				{
					// match the .gif and .jpg extensions
					string type = "";
					if(f.Extension.ToLower().IndexOf("jpg") > -1 || f.Extension.ToLower().IndexOf("jpeg") > -1)
						type = "jpg";
					else if(f.Extension.ToLower().IndexOf("gif") > -1)
						type = "gif";
					else if(f.Extension.ToLower().IndexOf("bmp") > -1)
						type = "bmp";
					else
						continue;
					

					// save thumbnail in thumbDir folder					
					switch(type)
					{
						case "jpg":
							try
							{
								//Image.FromFile(f.FullName).GetThumbnailImage(100, (int)(100/((double)Image.FromFile(f.FullName).Width / (double)Image.FromFile(f.FullName).Height)), null, IntPtr.Zero).Save(context.Server.MapPath(thumbDir + "/" + f.Name), System.Drawing.Imaging.ImageFormat.Jpeg);					
								Image.FromFile(f.FullName).GetThumbnailImage(100, (int)(100/((double)Image.FromFile(f.FullName).Width / (double)Image.FromFile(f.FullName).Height)), null, IntPtr.Zero).Save(thumbDir + "/" + f.Name, System.Drawing.Imaging.ImageFormat.Jpeg);					
							}
							catch{FTP.ReportError("Couldn't save thumbnail for " + f.Name, "", "");}
							break;
						case "gif":
							try
							{
								//Image.FromFile(f.FullName).GetThumbnailImage(100, (int)(100/((double)Image.FromFile(f.FullName).Width / (double)Image.FromFile(f.FullName).Height)), null, IntPtr.Zero).Save(context.Server.MapPath(thumbDir + "/" + f.Name), System.Drawing.Imaging.ImageFormat.Gif);					
								Image.FromFile(f.FullName).GetThumbnailImage(100, (int)(100/((double)Image.FromFile(f.FullName).Width / (double)Image.FromFile(f.FullName).Height)), null, IntPtr.Zero).Save(thumbDir + "/" + f.Name, System.Drawing.Imaging.ImageFormat.Gif);					
							}
							catch{FTP.ReportError("Couldn't save thumbnail for " + f.Name, "", "");}
							break;
						case "bmp":
							try
							{
								//Image.FromFile(f.FullName).GetThumbnailImage(100, (int)(100/((double)Image.FromFile(f.FullName).Width / (double)Image.FromFile(f.FullName).Height)), null, IntPtr.Zero).Save(context.Server.MapPath(thumbDir + "/" + f.Name), System.Drawing.Imaging.ImageFormat.Bmp);					
								Image.FromFile(f.FullName).GetThumbnailImage(100, (int)(100/((double)Image.FromFile(f.FullName).Width / (double)Image.FromFile(f.FullName).Height)), null, IntPtr.Zero).Save(thumbDir + "/" + f.Name, System.Drawing.Imaging.ImageFormat.Bmp);					
							}
							catch{FTP.ReportError("Couldn't save thumbnail for " + f.Name, "", "");}
							break;
					}
					context.Session["FTP_image_" + numPics] = f.Name;
					numPics++;
				}	
				context.Session["FTP_image_numPics"] = numPics;
			}
			catch(Exception ex)
			{
				FTP.ReportError("Couldn't get file information in the thumbnails directory", ex.ToString(), "");
			}
			
		}

		/*public static bool checkUseTreeView()
		{
			string tree = ConfigurationSettings.AppSettings["useTreeView"];
			if(tree == null)			
				return false;							
			else if(tree.ToLower() == "true")
				return true;
			return false;
		}*/
		
	}
}
