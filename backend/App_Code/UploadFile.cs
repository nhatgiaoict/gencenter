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
using System.Drawing.Imaging;
using System.IO;

/// <summary>
/// Summary description for UploadFile
/// </summary>
public class UploadFile
{
	public UploadFile()
	{
	}
    private HttpPostedFile mPostedFile;
    public HttpPostedFile PostedFile
    {
        get { return mPostedFile; }
        set { mPostedFile = value; }
    }
    /// <summary>
    /// Các thông báo về Upload File
    /// </summary>
    private string message = string.Empty;
    /// <summary>
    /// Các thông báo về Upload File
    /// </summary>

    /// <summary>
    /// Đường dẫn tuyệt đối vật lý của file đã được Upload
    /// </summary>
    private string fullFileName_Uploaded = string.Empty;
    /// <summary>
    /// Đường dẫn tuyệt đối vật lý của file đã được Upload
    /// </summary>
    public string FullFileName_Uploaded
    {
        get { return fullFileName_Uploaded; }
    }
    public string Message
    {
        get { return message; }
        set { message = value; }
    }
    public bool CreateThumbnail(int iWidth, int iHeight, string FullFileName, ref string str_FullFileName_Thumb)
    {
        try
        {
            string str_Extension = Path.GetExtension(FullFileName);
            str_FullFileName_Thumb = FullFileName.Replace(str_Extension, "") + "_Thumb" + str_Extension;
            System.Drawing.Image fullSizeImg = System.Drawing.Image.FromFile(FullFileName);
            System.Drawing.Image.GetThumbnailImageAbort dummyCallBack = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
            System.Drawing.Image thumbNailImg = fullSizeImg.GetThumbnailImage(iWidth, iHeight, dummyCallBack, IntPtr.Zero);
            thumbNailImg.Save(str_FullFileName_Thumb, ImageFormat.Png);
            fullSizeImg.Dispose();
            thumbNailImg.Dispose();
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private bool ThumbnailCallback()
    {
        return false;
    }
    /// <summary>
    /// Định dạng file ảnh
    /// </summary>
    public static readonly string ImageExtension = ".JPEG/.JPG/.GIF/.PNG";
    /// <summary>
    /// Xóa file
    /// </summary>
    /// <param name="FullFileName">Đường dẫn vật ly tuyệt đối</param>
    /// <returns></returns>
    public bool DeleteFile(string FullFileName)
    {
        try
        {
            FileInfo mFileInfo = new FileInfo(FullFileName);
            if (mFileInfo.Exists)
            {
                File.Delete(FullFileName);
                Message = "Xóa file thành công";
                return true;
            }
            else
            {
                Message = "File không tồn tại";
                return false;
            }
        }
        catch(IOException ex)
        {
            throw new IOException("lỗi xóa file trên server", ex);
        }
    }

    public bool CheckFileExists()
    {
        try
        {
            if (PostedFile.FileName == string.Empty)
            {
                Message += "Chưa chọn file cần upload";
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Lỗi trong quá trình chech file Empty", ex);
        }
    }

    public bool CheckExtention()
    {

        try
        {
            int iOldLenght;
            string ConfigExtension = "", CurrentExtension = "";
            ConfigExtension = UploadFile.ImageExtension;
            iOldLenght = ConfigExtension.Length;
            CurrentExtension = Path.GetExtension(PostedFile.FileName).ToUpper();
            ConfigExtension = ConfigExtension.Replace(CurrentExtension, "");
            if (iOldLenght == ConfigExtension.Length)
            {
                message = "Dịnh dạng file ảnh không hợp lệ";
                return false;
            }
            else
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("lỗi xảy ra khi upload ảnh", ex);
        }
    }
    private string thumbnailFullFileName = string.Empty;
    public string ThumbnailFullFileName
    {
        get { return thumbnailFullFileName; }
        set { thumbnailFullFileName = value; }
    }
    private string relativeFileName_Uploaded = string.Empty;
    /// <summary>
    /// Đường dẫn tương đối vật lý của file đã được Upload
    /// </summary>
    public string RelativeFileName_Upload
    {
        get { return relativeFileName_Uploaded; }
    }
    public bool SaveImageToHardDisk_Thumb()
    {
        try
        {
            if (!CheckFileExists() || !CheckExtention())
                return false;
            Page currentPage = (Page)HttpContext.Current.Handler;
            string FullFileName = string.Empty,
            //Tên file của file upload 
            fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + Path.GetExtension(PostedFile.FileName);
            string path1 = currentPage.Request.PhysicalApplicationPath.Replace("backend\\", "");
            // kiem tra xem co thu muc de chua anh upload chua; 
            string pathUploadImg = path1 + "data\\img\\";
            DirectoryInfo d = new DirectoryInfo(pathUploadImg);
            if (d.Exists == false)
            {
                d.Create(); // chua co thi creat; 
            }
            FullFileName = path1 + "data\\img\\" + fileName;
            relativeFileName_Uploaded = "data\\img\\" + fileName;
            PostedFile.SaveAs(FullFileName);
            Message = "Upload file thành công";
            fullFileName_Uploaded = FullFileName;
            CreateThumbnail(80, 80, FullFileName, ref thumbnailFullFileName);
            return true;
        }
        catch (IOException ex)
        {
            relativeFileName_Uploaded = string.Empty;
            throw new IOException("lồi khi lưa file lên server", ex);
        }
    }
}
