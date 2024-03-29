using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.IO;
namespace filemanager
{
	/// <summary>
	/// Summary description for upload.
	/// </summary>
    public partial class upload : System.Web.UI.Page
	{
		
		protected string abspath;
		private void Page_Load(object sender, System.EventArgs e)
		{
            abspath = HttpContext.Current.Server.MapPath(filemanager.components.Globals.UrlRoot + filemanager.components.Globals.UrlRootPath);
			
		}
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
			this.UploadBtn.Click += new System.EventHandler(this.UploadBtn_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
        private void UploadBtn_Click(object sender, System.EventArgs e)
        {

            string path = Request.QueryString["Path"];
            if (fileToUpload.PostedFile.FileName == "")	// no file selected
                Response.Write("<script>alert(\"No file selected for upload\\nPlease click browse and select the file you wish to upload\");</script>");
            else
            {
                string fileExt = string.Empty;
                fileExt = System.IO.Path.GetExtension(fileToUpload.PostedFile.FileName);
                if (fileExt == ".wmv" || fileExt == ".wma" || fileExt == ".avi" || fileExt == ".flv")
                {
                    try
                    {
                        string filename = fileToUpload.PostedFile.FileName;
                        string togo = abspath + path + "/" + filename.Remove(0, filename.LastIndexOf("\\") + 1);
                        if (fileToUpload.PostedFile.ContentLength < 20971520) // 20mb 1024*1024*10
                        {
                            fileToUpload.PostedFile.SaveAs(togo);
                            //convert video thanh flv
                            if (togo.Substring(togo.Length - 4) != ".flv")
                            {
                                Convert_Video(togo);
                            }
                            /*
                        them doan code nay vao de tao anh cho file video khi lam mutimedia
                        anh nay giong file anh video nhung co duoi la jpg
                         */
                            string filename1 = filename.Remove(0, filename.LastIndexOf("\\") + 1);
                            filename1 = filename1.Substring(0, filename1.Length - 4);
                            Process ffmpeg; // creating process
                            string video;
                            string thumb;
                            video = togo.Substring(0, togo.Length - 4) + ".flv";
                            thumb = abspath + path + "/" + filename1 + ".jpg";
                            ffmpeg = new Process();
                            ffmpeg.StartInfo.Arguments = " -i \"" + video + "\" -s 108*80  -vframes 1 -f image2 -vcodec mjpeg \"" + thumb + "\""; // arguments !
                            ffmpeg.StartInfo.FileName = Page.MapPath("ffmpeg.exe");
                            ffmpeg.Start(); // start 
                            // get size of file with appropriate expression of size

                            long fileSize = fileToUpload.PostedFile.ContentLength;
                            string fileSizeStr;
                            if (fileSize > 1000000) fileSizeStr = fileSize / 1000000 + " Mb";
                            else if (fileSize > 1000) fileSizeStr = fileSize / 1000 + " Kb";
                            else fileSizeStr = fileSize + " b";

                            //xoa file goc VD: a.wma;
                            if (togo.Substring(togo.Length - 4) != ".flv")
                            {
                                string sFile = togo;
                                XoaFile(sFile);
                            }
                            // notify user of success
                            Response.Write("<script>alert(\"Upload Successful...\\nName: " + filename.Remove(0, filename.LastIndexOf("\\") + 1) + "\\nSize: " + fileSizeStr + "\"); opener.location.href=opener.location.href; window.self.close();</script>");	//document.parentWindow.location.href=\"contentPane.aspx?Path=" + Server.UrlEncode(Request["Path"]) + "\"; 
                        }
                        else
                        {
                            Response.Write("<script>alert(\"Không thể upload file lớn hơn 20MB\"); opener.location.href=opener.location.href; window.self.close();</script>");
                        }
                    }
                    catch (UnauthorizedAccessException unEx)
                    {
                        filemanager.components.FTP.ReportError("Access denied. Could not upload file", unEx.Message, "");
                    }

                    catch (Exception ex)
                    {
                        filemanager.components.FTP.ReportError("Upload failed.", ex.ToString(), "The file may be too large");
                    }
                }

                else
                {
                    // filemanager.components.FTP.ReportError("Access denied. Could not upload file", ".wma,.wmv,.avi,.flv", "");
                    try
                    {
                        string filename = fileToUpload.PostedFile.FileName;
                        string togo = abspath + path + "/" + filename.Remove(0, filename.LastIndexOf("\\") + 1);
                        fileToUpload.PostedFile.SaveAs(togo);
                        /*
                        them doan code nay vao de tao anh cho file video khi lam mutimedia
                         * anh nay giong file anh video nhung co duoi la jpg
                    
                        #region tạo ảnh cho video
                        string filename1 = filename.Remove(0, filename.LastIndexOf("\\") + 1);
                        filename1 = filename1.Substring(0, filename1.Length - 4);
                        Process ffmpeg; // creating process
                        string video;
                        string thumb;
                        video = togo;
                        thumb = abspath + path + "/" + filename1 + ".jpg";
                        ffmpeg = new Process();
                        ffmpeg.StartInfo.Arguments = " -i \"" + video + "\" -s 108*80  -vframes 1 -f image2 -vcodec mjpeg \"" + thumb + "\""; // arguments !
                        ffmpeg.StartInfo.FileName = Page.MapPath("ffmpeg.exe");
                        ffmpeg.Start(); // start 
                        #endregion
                         */

                        // get size of file with appropriate expression of size
                        long fileSize = fileToUpload.PostedFile.ContentLength;
                        string fileSizeStr;
                        if (fileSize > 1000000) fileSizeStr = fileSize / 1000000 + " Mb";
                        else if (fileSize > 1000) fileSizeStr = fileSize / 1000 + " Kb";
                        else fileSizeStr = fileSize + " b";

                        // notify user of success
                        Response.Write("<script>alert(\"Upload Successful...\\nName: " + filename.Remove(0, filename.LastIndexOf("\\") + 1) + "\\nSize: " + fileSizeStr + "\"); opener.location.href=opener.location.href; window.self.close();</script>");
                        //document.parentWindow.location.href=\"contentPane.aspx?Path=" + Server.UrlEncode(Request["Path"]) + "\"; 
                    }
                    catch (UnauthorizedAccessException unEx)
                    {
                        filemanager.components.FTP.ReportError("Access denied. Could not upload file", unEx.Message, "");
                    }
                    catch (Exception ex)
                    {
                        filemanager.components.FTP.ReportError("Upload failed.", ex.ToString(), "The file may be too large");
                    }
                }
            }

            /*string path = Request.QueryString["Path"];
			
                try
                {
                    Upload UploadResult = new Upload();					
                    String FolderToSave =  abspath + path + "/";
				
                    UploadedFile myFile = UploadResult.Files["fileToUpload"];
					
                    /*Result.Text += "<hr>File <b>" + myFile.SafeFileName + "</b> was uploaded successfully<br>";
                    Result.Text += "File size: <b>" + myFile.ContentLength + " bytes</b><br>";
                    Result.Text += "File content type: <b>" + myFile.ContentType + "</b><br>";
                    Result.Text += "File path on the client computer: <b>" + myFile.ClientFilePath + "</b><br>";
                    Result.Text += "File field name in form: <b>" + myFile.FieldName + "</b><br>";
                    Result.Text += "File temp path: <b>" + myFile.TempFileName + "</b><br>";
                    myFile.SaveAs(FolderToSave + myFile.SafeFileName, true);
                    Result.Text += "File was saved to folder <b>" + FolderToSave + "</b><br><hr>";	*/
            /*myFile.SaveAs(FolderToSave + myFile.SafeFileName, true);
            Response.Write("<script>opener.location.href=opener.location.href; window.self.close();</script>");	
        }
        catch(UnauthorizedAccessException unEx)
        {
            FTP.ReportError("Access denied. Could not upload file", unEx.Message, "");
        }
        catch(Exception ex)
        {
            FTP.ReportError("Upload failed.", ex.ToString(), "The file may be too large");
        }*/
        }
        private void Convert_Video(string togo)
        {
            string video;
            string mpg;
            video = togo;// setting video input name with path
            mpg = video.Substring(0, video.Length - 4) + ".flv";
            try
            {
                Process ffmpeg; // creating process
                ffmpeg = new Process();
                ffmpeg.StartInfo.Arguments = " -i " + video + " -s 480*360 -deinterlace -ab 32 -r 15 -ar 22050 -ac 1 " + mpg; // arguments !
                ffmpeg.StartInfo.FileName = Page.MapPath("ffmpeg.exe");

                ffmpeg.Start(); // start !
                ffmpeg.WaitForExit();
                ffmpeg.Close();

            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        private void XoaFile(string sFile)
        {
            DynamicFile sDynamicFire = new DynamicFile();
            sDynamicFire.DeleteFile(sFile);
        }
        #region code button upload cu
        //private void UploadBtn_Click(object sender, System.EventArgs e)
        //{
			
        //    string path = Request.QueryString["Path"];
        //    if(fileToUpload.PostedFile.FileName == "")	// no file selected
        //        Response.Write("<script>alert(\"No file selected for upload\\nPlease click browse and select the file you wish to upload\");</script>");
        //    else
        //    {
        //        try
        //        {
        //            string filename = fileToUpload.PostedFile.FileName;

        //            string togo = abspath + path + "/" + filename.Remove(0, filename.LastIndexOf("\\") + 1);
        //            fileToUpload.PostedFile.SaveAs(togo);
        //            /*
        //            them doan code nay vao de tao anh cho file video khi lam mutimedia
        //             * anh nay giong file anh video nhung co duoi la jpg
                    
        //            #region tạo ảnh cho video
        //            string filename1 = filename.Remove(0, filename.LastIndexOf("\\") + 1);
        //            filename1 = filename1.Substring(0, filename1.Length - 4);
        //            Process ffmpeg; // creating process
        //            string video;
        //            string thumb;
        //            video = togo;
        //            thumb = abspath + path + "/" + filename1 + ".jpg";
        //            ffmpeg = new Process();
        //            ffmpeg.StartInfo.Arguments = " -i \"" + video + "\" -s 108*80  -vframes 1 -f image2 -vcodec mjpeg \"" + thumb + "\""; // arguments !
        //            ffmpeg.StartInfo.FileName = Page.MapPath("ffmpeg.exe");
        //            ffmpeg.Start(); // start 
        //            #endregion
        //             */

        //            // get size of file with appropriate expression of size
        //            long fileSize = fileToUpload.PostedFile.ContentLength;
        //            string fileSizeStr;
        //            if (fileSize > 1000000) fileSizeStr = fileSize / 1000000 + " Mb";
        //            else if (fileSize > 1000) fileSizeStr = fileSize / 1000 + " Kb";
        //            else fileSizeStr = fileSize + " b";

        //            // notify user of success
        //            Response.Write("<script>alert(\"Upload Successful...\\nName: " + filename.Remove(0, filename.LastIndexOf("\\") + 1) + "\\nSize: " + fileSizeStr + "\"); opener.location.href=opener.location.href; window.self.close();</script>");	//document.parentWindow.location.href=\"contentPane.aspx?Path=" + Server.UrlEncode(Request["Path"]) + "\"; 
        //        }
        //        catch (UnauthorizedAccessException unEx)
        //        {
        //            filemanager.components.FTP.ReportError("Access denied. Could not upload file", unEx.Message, "");
        //        }
        //        catch (Exception ex)
        //        {
        //            filemanager.components.FTP.ReportError("Upload failed.", ex.ToString(), "The file may be too large");
        //        }
        //    }
        //    /*string path = Request.QueryString["Path"];
			
        //        try
        //        {
        //            Upload UploadResult = new Upload();					
        //            String FolderToSave =  abspath + path + "/";
				
        //            UploadedFile myFile = UploadResult.Files["fileToUpload"];
					
        //            /*Result.Text += "<hr>File <b>" + myFile.SafeFileName + "</b> was uploaded successfully<br>";
        //            Result.Text += "File size: <b>" + myFile.ContentLength + " bytes</b><br>";
        //            Result.Text += "File content type: <b>" + myFile.ContentType + "</b><br>";
        //            Result.Text += "File path on the client computer: <b>" + myFile.ClientFilePath + "</b><br>";
        //            Result.Text += "File field name in form: <b>" + myFile.FieldName + "</b><br>";
        //            Result.Text += "File temp path: <b>" + myFile.TempFileName + "</b><br>";
        //            myFile.SaveAs(FolderToSave + myFile.SafeFileName, true);
        //            Result.Text += "File was saved to folder <b>" + FolderToSave + "</b><br><hr>";	*/
        //            /*myFile.SaveAs(FolderToSave + myFile.SafeFileName, true);
        //            Response.Write("<script>opener.location.href=opener.location.href; window.self.close();</script>");	
        //        }
        //        catch(UnauthorizedAccessException unEx)
        //        {
        //            FTP.ReportError("Access denied. Could not upload file", unEx.Message, "");
        //        }
        //        catch(Exception ex)
        //        {
        //            FTP.ReportError("Upload failed.", ex.ToString(), "The file may be too large");
        //        }*/


        //}
        #endregion
    }
}