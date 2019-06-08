using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using filemanager.components;
namespace filemanager
{
	/// <summary>
	/// Summary description for _default.
	/// </summary>
    public partial class _default : System.Web.UI.Page
	{
		protected string path;
        public string PathNew;
		protected int alternate = 0;
		protected int numResults = 0;
		public string CurrentExecutionFilePath;
		
		protected string urlpath;
		protected string abspath;
        public string UrlPathUploadJquery
        {
            get {
                if (Request["Path"] == null || Request["Path"].ToString() == "")
                {
                    urlpath = filemanager.components.Globals.UrlRootPath;
                }
                else
                {
                    path = Request["Path"].ToString();

                    urlpath = filemanager.components.Globals.UrlRootPath + path;
                }
                return urlpath;
            }
        }
		private void Page_Load(object sender, System.EventArgs e)
		{	
			string txtChoice = string.Empty;
			if(Request["txtChoice"] != null)
			{
				txtChoice = Request["txtChoice"].ToString();
				Session["txtChoice"] = txtChoice;
			}            
			CurrentExecutionFilePath = Request.CurrentExecutionFilePath;
			if(Request["Path"] == null || Request["Path"].ToString() == "")
			{
				//path = ConfigurationSettings.AppSettings["root"];	//default is root directory					

                urlpath = filemanager.components.Globals.UrlRootPath;
			}	
			else 
			{
				path = Request["Path"].ToString();

                urlpath = filemanager.components.Globals.UrlRootPath + path;
			}

            abspath = HttpContext.Current.Server.MapPath(filemanager.components.Globals.UrlRoot + urlpath);

			if(Request.QueryString["newFile"] != null && Request.QueryString["newFile"].ToString() != "")
				newFile(Request.QueryString["newFile"].ToString());
			else if(Request.QueryString["newFolder"] != null && Request.QueryString["newFolder"].ToString() != "")
				newFolder(Request.QueryString["newFolder"].ToString());
			else if(Request.QueryString["toFind"] != null && Request.QueryString["toFind"].ToString() != "")
			{
				findFiles(Request.QueryString["toFind"].ToString(), path);				
				if(numResults == 0)
				{
					this.filesFoldersPanel.Visible = false;
					this.MessageLbl.Text = "No files matching your search pattern. <BR><BR>Example use: &nbsp; &nbsp; *.aspx  &nbsp; &nbsp; or &nbsp; &nbsp; *word*<BR>";
				}
				else
					this.MessageLbl.Text = numResults + " files match your search pattern<BR><BR>";
			}
			else 
				if(Request.QueryString["Delete"] != null)
			{
				if(Request.QueryString["Type"] != null && Request.QueryString["Name"] != null)
					if(Request.QueryString["Type"].ToString() == "File")
						//FTP.deleteFile(Server.MapPath(Server.UrlDecode(Request.QueryString["Name"].ToString())), path);
						FTP.deleteFile(abspath + Server.UrlDecode(Request.QueryString["Name"].ToString()), path);
					else if(Request.QueryString["Type"].ToString() == "Folder")
						//FTP.deleteFolder(Server.MapPath(Server.UrlDecode(Request.QueryString["Name"].ToString())), path);
						FTP.deleteFolder(abspath + Server.UrlDecode(Request.QueryString["Name"].ToString()), path);
			}
			else 
				if(Request.QueryString["Rename"] != null)
			{
				if(Request.QueryString["Type"] != null && Request.QueryString["Name"] != null && Request.QueryString["NewName"] != null)
					if(Request.QueryString["Type"].ToString() == "File")
						//FTP.renameFile(Server.MapPath(Server.UrlDecode(Request.QueryString["Name"].ToString())), path, Request.QueryString["NewName"].ToString());
						FTP.renameFile(Server.UrlDecode(Request.QueryString["Name"].ToString()), path, Request.QueryString["NewName"].ToString());
					else if(Request.QueryString["Type"].ToString() == "Folder" && Request.QueryString["NewName"] != null)
						//FTP.renameFolder(Server.MapPath(Server.UrlDecode(Request.QueryString["Name"].ToString())), path, Request.QueryString["NewName"].ToString());
						FTP.renameFolder(Server.UrlDecode(Request.QueryString["Name"].ToString()), path, Request.QueryString["NewName"].ToString());
			}
			else 
				if(Request.QueryString["Thumbnails"] != null)
			{
				FTP.viewAsThumbnails(path);
                string urlThumbDir = filemanager.components.Globals.UrlThumbDir;
				if(Session["FTP_image_numPics"] != null && Int32.Parse(Session["FTP_image_numPics"].ToString()) > 0)
				{				
					int numPics = Int32.Parse(Session["FTP_image_numPics"].ToString());
					for(int i=0; i<numPics; i++)
					{
						LiteralControl img = new LiteralControl();
						if(path == "/")
                            img.Text = "<a href=\"" + urlThumbDir + "/" + Session["FTP_image_" + i] + "\" title=\"" + Session["FTP_image_" + i] + "\"><img src=\"" + filemanager.components.Globals.UrlThumbDir + "/" + Session["FTP_image_" + i] + "\" border=0></a>";
						else
                            img.Text = "<a href=\"" + urlThumbDir + "/" + Session["FTP_image_" + i] + "\" title=\"" + Session["FTP_image_" + i] + "\"><img src=\"" + filemanager.components.Globals.UrlThumbDir + "/" + Session["FTP_image_" + i] + "\" border=0 hspace=10 vspace=10></a>";
						this.imagesHolder.Controls.Add(img);
					}					
					this.MessageLbl.Text = "Showing thumbnail images for " + urlpath + "<BR>";
				}
				else
				{
					this.MessageLbl.Text = "No thumbnails available for this directory<BR>";					
				}											
				this.filesFoldersPanel.Visible = false;
			}
			else 
			{	// load the files for the 'path' parameter		
				
				if(!IsPostBack)
				{
					showFiles(); // list the files and folders in the current directory
				}
			}			
		}

		private void newFile(string name)
		{
			try
			{
				FileInfo f;
				if(path == "/")
					f = new FileInfo(abspath + name);
				else
					f = new FileInfo(abspath + "/" + name);
				f.Create();
				Response.Redirect(CurrentExecutionFilePath+"?Path="+path);
			}
			catch(UnauthorizedAccessException unEx)
			{
				FTP.ReportError("Access denied. Could not create file", unEx.Message, "");
			}
			catch(Exception ex)
			{
				FTP.ReportError("Could not create file: " + name, ex.Message, "Make sure there are no invalid characters");
			}
		}

		private void newFolder(string name)
		{
			try
			{
				if(path == "/")
					Directory.CreateDirectory(abspath + name);
				else
					Directory.CreateDirectory(abspath + "/" + name);
				Response.Redirect(CurrentExecutionFilePath+"?Path="+path);
			}
			catch(UnauthorizedAccessException unEx)
			{
				FTP.ReportError("Access denied. Could not create folder", unEx.Message, "");
			}
			catch(Exception ex)
			{
				FTP.ReportError("Could not create file: " + name, ex.Message, "Make sure there are no invalid characters");
			}
		}

		public void showFiles()
		{					
			// safety checks handled by checkValidPath function
			try
			{
				DirectoryInfo mainDir = new DirectoryInfo(abspath);
				if(mainDir == null)	
				{
					FTP.ReportError("Could not get directory information for " + path, "Null value returned from new DirectoryInfo(path)", "Refresh the default.aspx file to get a fresh folder listing and try again.");
					return;
				}
				try
				{
					foreach(DirectoryInfo d in mainDir.GetDirectories())	
					{									
						addDir(d, path, false);
						numResults++;
					}
					foreach(FileInfo f in mainDir.GetFiles())
					{
						addFile(f, path, false);
						numResults++;
					}
				}
				catch(DirectoryNotFoundException dnfEx)
				{
					FTP.ReportError("Directory does not exist: " + path, dnfEx.Message, "Make sure the 'root' key in web.config is a valid path such as: / or /folder");
				}
				catch(UnauthorizedAccessException)
				{
					FTP.ReportError("You do not have permissions to view " + path, "", "");
				}
				catch(ArgumentException ArgEx)
				{
					FTP.ReportError("The path has invalid characters: " + path, ArgEx.Message,  "Try renaming the folder and removing non-standard characters.");
				}
				catch(Exception Ex)
				{
					FTP.ReportError("Could not get Directory Information for " + path, Ex.Message,  "Make sure the 'root' key in web.config is set to / unless you don't have permissions for the root.");
				}
			}
			catch(ArgumentException ex)
			{
				FTP.ReportError("The path has invalid characters: " + path, ex.Message,  "Make sure all the folders on the site don't have non-standard characters.");
			}
			if(numResults == 0)
			{
				this.filesFoldersPanel.Visible = false;
				this.MessageLbl.Text = "No files in this folder<BR>";
			}
		}		

		private void addDir(DirectoryInfo d, string path, bool isSearchItem)
		{
			// display a folder icon
			Literal picColumn = new Literal();							
			picColumn.Text = "<img src=pics/icons/folder.gif>";

			// this link changes the path to the directory being clicked
			HyperLink goTo = new HyperLink();
			goTo.Text = d.Name.ToString();
			
			// this link deletes the directory, but it must be empty for the delete to be successful
			HyperLink deleteBtn = new HyperLink();
			deleteBtn.ImageUrl = "pics/icons/delete.gif";
			deleteBtn.ToolTip = "Delete '" + d.Name.ToString() + "'";

			// this link opens a js prompt to rename the folder
			HyperLink renameBtn = new HyperLink();
			renameBtn.ImageUrl = "pics/icons/rename.gif";
			renameBtn.ToolTip = "Rename '" + d.Name.ToString() + "'";

			// if a search is being performed, add a 'path' control
			HyperLink goToPath = null;
			if(isSearchItem)
			{
				goToPath = new HyperLink();
				if(path == "/")
				{
					goToPath.NavigateUrl = CurrentExecutionFilePath+"?Path="+path + d.Name.ToString();
					goToPath.Text = path + d.Name.ToString();
				}
				else
				{
					goToPath.NavigateUrl = CurrentExecutionFilePath+"?Path="+path +"/"+ d.Name.ToString();
					goToPath.Text = path + "/" + d.Name.ToString();
				}
			}

			if(path == "/")
			{
				goTo.NavigateUrl = CurrentExecutionFilePath+"?Path="+path + d.Name.ToString();
				renameBtn.NavigateUrl = "javascript:rename('"+CurrentExecutionFilePath+"?Rename=1&Path=" + path + "&Type=Folder&Name=" + Server.UrlEncode(d.Name.ToString()) + "'," + d.Name.ToString()+");";	
				deleteBtn.NavigateUrl = "javascript:confirmDeleteFolder('"+CurrentExecutionFilePath+"?Delete=1&Path=" + path + "&Type=Folder&Name=" + Server.UrlEncode(d.Name.ToString()) + "','" + d.Name.ToString()+ "');";	// use client-side confirm function for deleting			
			}
			else
			{
				goTo.NavigateUrl = CurrentExecutionFilePath+"?Path="+path +"/"+ d.Name.ToString();
				renameBtn.NavigateUrl = "javascript:rename('"+CurrentExecutionFilePath+"?Rename=1&Path=" + path + "&Type=Folder&Name=" + Server.UrlEncode("/" + d.Name.ToString()) + "','" + d.Name.ToString()+ "');";	
				deleteBtn.NavigateUrl = "javascript:confirmDeleteFolder('"+CurrentExecutionFilePath+"?Delete=1&Path=" + path + "&Type=Folder&Name=" + Server.UrlEncode( "/" + d.Name.ToString()) + "','" + d.Name.ToString()+ "');";	// use client-side confirm function for deleting			
			}

			HtmlTableRow row = new HtmlTableRow();
			row.VAlign = "middle";
			if(alternate%2 == 1)	// alternate back-colour of rows
				row.BgColor = "#D5D8E1";
			row.Cells.Add(new HtmlTableCell());
			row.Cells[0].Controls.Add(picColumn);
			row.Cells.Add(new HtmlTableCell());
			row.Cells[1].Controls.Add(goTo);			
			row.Cells.Add(new HtmlTableCell());
			if(goToPath != null)
			{
				this.pathHeading.Text = " &nbsp; &nbsp; Path";
				row.Cells[2].Controls.Add(new LiteralControl(" &nbsp; &nbsp; "));
				row.Cells[2].Controls.Add(goToPath);
			}
			row.Cells[2].ColSpan = 2;
			row.Cells.Add(new HtmlTableCell());
			row.Cells[3].Controls.Add(deleteBtn);
			row.Cells.Add(new HtmlTableCell());
			row.Cells[4].Controls.Add(renameBtn);
			row.Cells[4].ColSpan = 3;
			this.FilesFolders.Controls.Add(row);

			alternate++;	// bump the int for back-colour alternation	
		}

		private void addFile(FileInfo f, string path, bool isSearchItem)
		{
			// this link goes directly to the file (not edit mode)
			HyperLink goToBtn = new HyperLink();
			goToBtn.Text = f.Name.ToString();
			goToBtn.Target = "_blank";
			

			// display a file icon
			Literal picColumn = new Literal();				
				
			// this link edits the file when clicked				
			HyperLink editBtn = new HyperLink();

			string ext = f.Extension.ToLower();

            string editableFileTypes = filemanager.components.Globals.editableFileTypes;
            string picFileTypes = filemanager.components.Globals.imageFileTypes;
            string musicFileTypes = filemanager.components.Globals.musicFileTypes;

			// check which edit icon to display, normal or alpha'd
			if(editableFileTypes.IndexOf(ext) >= 0)
			{ 
				editBtn.ImageUrl = "pics/icons/edit.gif";					
				editBtn.ToolTip = "Edit '" + f.Name.ToString() + "'";				
			}
			else
			{
				editBtn.ImageUrl = "pics/icons/editAnyway.gif";					
				editBtn.ToolTip = "Not registered as text file - edit anyway: '" + f.Name.ToString() + "'";
			}

			// check for image file type			
			if(ext == "" || ext == null)
				picColumn.Text = "<img src=pics/icons/unknown.gif>";
			else if(picFileTypes.IndexOf(ext) >= 0)
				picColumn.Text = "<img src=pics/icons/gif.gif>";
			else if(editableFileTypes.IndexOf(ext) >= 0)
				picColumn.Text = "<img src=pics/icons/textFile.gif>";
			else if(musicFileTypes.IndexOf(ext) >= 0)
				picColumn.Text = "<img src=pics/icons/music.gif>";
			else
				picColumn.Text = "<img src=pics/icons/unknown.gif>";

			// work out what string to display for the file, e.g. bytes, kilobytes, or megabytes
			long fileSize = f.Length;
			string fileSizeStr;
			if(fileSize > 1000000) fileSizeStr = fileSize/1000000 + " Mb";
			else if(fileSize > 1000) fileSizeStr = fileSize/1000 + " Kb";
			else fileSizeStr = fileSize + " b";

			// size of file
			Label sizeLbl = new Label();
			sizeLbl.Text = fileSizeStr;

			// last modified datetime of file
			Label modLbl = new Label();
			modLbl.Text = f.LastWriteTime.ToString();
				
			// set up delete button
			HyperLink deleteBtn = new HyperLink();
			deleteBtn.ImageUrl = "pics/icons/delete.gif";
			deleteBtn.ToolTip = "Delete '" + f.Name.ToString() + "'";
			
				
				
			// this link opens a js prompt to rename the folder
			HyperLink renameBtn = new HyperLink();
			renameBtn.ImageUrl = "pics/icons/rename.gif";
			renameBtn.ToolTip = "Rename '" + f.Name.ToString() + "'";

			if(path == "/")
			{
				deleteBtn.NavigateUrl = "javascript:confirmDeleteFile('"+CurrentExecutionFilePath+"?Delete=1&Path=" + path + "&Type=File&Name=" + Server.UrlEncode(f.Name.ToString())+"', '"+ f.Name.ToString()+"');";
				//goToBtn.NavigateUrl = "http://" + Request.ServerVariables["SERVER_NAME"].ToString() + path + f.Name.ToString();
				string urlFile = urlpath +"/"+ f.Name.ToString();
				//goToBtn.NavigateUrl = urlFile;
				goToBtn.Attributes.Add("style","cursor:hand;cursor:pointer");
				goToBtn.Attributes.Add("onmouseover","doTooltip(event,'"+ Globals.UrlRoot+urlFile +"','"+ f.Name.ToString() +"','#ffffff','#ff0000')");
				goToBtn.Attributes.Add("onmouseout","hideTip()");
				goToBtn.Attributes.Add("onclick","ReturnValue('"+Session["txtChoice"].ToString()+"', '"+urlFile+"')");
				renameBtn.NavigateUrl = "javascript:rename('"+CurrentExecutionFilePath+"?Rename=1&Path=" + path + "&Type=File&Name=" + Server.UrlEncode(f.Name.ToString()) + "','" + f.Name.ToString()+ "');";			
				editBtn.NavigateUrl = "edit.aspx?File=" + Server.UrlEncode(path + f.Name.ToString()) + "&Path=" + path;			
			}
			else
			{
				deleteBtn.NavigateUrl = "javascript:confirmDeleteFile('"+CurrentExecutionFilePath+"?Delete=1&Path=" + path + "&Type=File&Name=" + Server.UrlEncode("/" + f.Name.ToString()) + "','" + f.Name.ToString()+ "');";
				//goToBtn.NavigateUrl = "http://" + Request.ServerVariables["SERVER_NAME"].ToString() + path + "/" + f.Name.ToString();
				string urlFile = urlpath +"/"+ f.Name.ToString();
				//goToBtn.NavigateUrl = urlFile;
				goToBtn.Attributes.Add("style","cursor:hand;cursor:pointer");
				goToBtn.Attributes.Add("onmouseover","doTooltip(event,'"+ Globals.UrlRoot+urlFile +"','"+ f.Name.ToString() +"','#ffffff','#ff0000')");
				goToBtn.Attributes.Add("onmouseout","hideTip()");
				goToBtn.Attributes.Add("onclick","ReturnValue('"+Session["txtChoice"].ToString()+"', '"+urlFile+"')");
				renameBtn.NavigateUrl = "javascript:rename('"+CurrentExecutionFilePath+"?Rename=1&Path=" + path + "&Type=File&Name=" + Server.UrlEncode("/" + f.Name.ToString()) + "','" + f.Name.ToString()+ "');";			
				editBtn.NavigateUrl = "edit.aspx?File=" + Server.UrlEncode(path + "/" + f.Name.ToString()) + "&Path=" + path;			
			}

			// if a search is being performed, add a 'path' control
			HyperLink goToPath = null;
			if(isSearchItem)
			{
				goToPath = new HyperLink();
				goToPath.Text = path;
				goToPath.NavigateUrl = CurrentExecutionFilePath+"?Path=" + path;
			}

			// add controls to container
			addFileRow(picColumn, goToBtn, editBtn, deleteBtn, renameBtn, sizeLbl, modLbl, goToPath);			
		}	

		private void addFileRow(Control pic, Control goTo, Control edit, Control del, Control ren, Control size, Control mod, Control pathLink)
		{
			HtmlTableRow row = new HtmlTableRow();
			row.VAlign = "middle";
			if(alternate%2 == 1) 
				//row.BgColor = "#D5D8E1";
				row.Attributes.Add("class","item");
			else
				row.Attributes.Add("class","alter");

			row.Cells.Add(new HtmlTableCell());
			row.Cells[0].Controls.Add(pic);
			row.Cells.Add(new HtmlTableCell());
			row.Cells[1].Controls.Add(goTo);
			row.Cells.Add(new HtmlTableCell());
			if(pathLink != null)
			{
				this.pathHeading.Text = " &nbsp; &nbsp; Path";
				row.Cells[2].Controls.Add(new LiteralControl(" &nbsp; &nbsp; "));
				row.Cells[2].Controls.Add(pathLink);
			}
			row.Cells.Add(new HtmlTableCell());
			row.Cells[3].Align = "right";
			row.Cells[3].Controls.Add(edit);
			row.Cells.Add(new HtmlTableCell());
			row.Cells[4].Controls.Add(del);				
			row.Cells.Add(new HtmlTableCell());
			row.Cells[5].Controls.Add(ren);
			row.Cells.Add(new HtmlTableCell());
			row.Cells[6].Controls.Add(size);
			row.Cells.Add(new HtmlTableCell());
			row.Cells[7].Controls.Add(mod);
			
			this.FilesFolders.Controls.Add(row);
			alternate++;
		}

		
		private void findFiles(string toFind, string path)
		{
			
			DirectoryInfo mainDir = new DirectoryInfo(Server.MapPath(path));
			try
			{				
				foreach(DirectoryInfo d in mainDir.GetDirectories())
				{
					if(abspath == "/")
						findFiles(toFind, abspath + d.Name);
					else
						findFiles(toFind, abspath + "/" + d.Name);													
				}
				foreach(FileInfo f in mainDir.GetFiles(toFind))
				{
					addFile(f, abspath, true);
					numResults++;
				}
				foreach(DirectoryInfo d in mainDir.GetDirectories(toFind))
				{
					addDir(d, abspath, true);
					numResults++;
				}
			}
			catch(DirectoryNotFoundException dnfEx)
			{
				FTP.ReportError("Directory does not exist: " + path, dnfEx.Message, "Make sure the 'root' key in web.config is a valid path such as: / or /folder");
			}
			catch(UnauthorizedAccessException)
			{
				FTP.ReportError("You do not have permissions for " + path + ", continuing search..", "", "");
			}
			catch(ArgumentException ArgEx)
			{
				FTP.ReportError("The path has invalid characters: " + path, ArgEx.Message,  "Try renaming the folder and removing non-standard characters.");
			}
			catch(Exception Ex)
			{
				FTP.ReportError("Could not get Directory Information for " + path, Ex.Message,  "Make sure the 'root' key in web.config is set to / unless you don't have permissions for the root.");
			}
		}

		private void ChangePath(object sender, CommandEventArgs e)
		{			
			if(e.CommandArgument.ToString() == "/")	// goto root
				path = ConfigurationSettings.AppSettings["root"];
			else if(e.CommandArgument.ToString() == "../") // go one level up in directory tree
				path = FTP.getParentDirectory(abspath);
			else
			{	// add the directory name to the end of the current path
				if(path == "/")
					path += e.CommandArgument.ToString();
				else
					path += "/" + e.CommandArgument.ToString();
			}
			Response.Redirect(CurrentExecutionFilePath+"?Path="+path);	// prevent annoying repost of data when page refreshed
		}		


		/*public void ManualChangeAddress()
		{
			string curPath = this.currentPathTxt.Text;			
			// check if the textbox contains a file
			if(File.Exists(Server.MapPath(curPath)))
				Response.Redirect(curPath);
			else
			{ 
				// prevent users trying to go above /
				curPath = curPath.Replace(".", "");
				// remove trailing / from path
				if(curPath.Length > 1 && curPath.LastIndexOf("/") == (curPath.Length - 1))
					curPath = curPath.Remove(curPath.Length-1, 1);
				// go to the folder
				Trace.Write("CURPATH: " + curPath);
				Response.Redirect(CurrentExecutionFilePath+"?Path=" + curPath);
			}			
		}*/

		/*private void GoButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			ManualChangeAddress();
		}
		
		public void pathBoxChange(object sender, EventArgs e)
		{
			ManualChangeAddress();
		}*/


		//this.Load += new System.EventHandler(this.Page_Load);
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.UpBtn.Click += new System.Web.UI.ImageClickEventHandler(this.UpBtn_Click);
			this.GoRoot.Click += new System.Web.UI.ImageClickEventHandler(this.GoRoot_Click);
			this.RefreshBtn.Click += new System.Web.UI.ImageClickEventHandler(this.Refresh_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		private void GoRoot_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect(CurrentExecutionFilePath+"?Path=" + Globals.UrlRootPath);
			Response.Redirect(CurrentExecutionFilePath);
		}
		private void UpBtn_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			string sPath = FTP.getParentDirectory(path);
			if(sPath != string.Empty)
			{
				Response.Redirect(CurrentExecutionFilePath+"?Path="+sPath);
			}
			else
			{
				Response.Redirect(CurrentExecutionFilePath);
			}
		}
		private void Refresh_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(Request.RawUrl);
		}
	}
}
