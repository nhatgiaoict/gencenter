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
using System.Drawing;
using System.Drawing.Imaging;

/// <summary>
/// Summary description for Thumbnails
/// </summary>
public class Thumbnails
{
    private static bool AbortThumbnailGeneration()
    {
        return false;
    }
    public static string viewAsThumbnails(string path, int thumbWidth, int thumbHeight, string key, string id)
    {
        if (path.Trim().Length == 0 || path.Trim() == "")
            return string.Empty;
        try
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string urlThumb = Globals.UrlData + "Gallery/";
            string thumbDir = context.Server.MapPath(urlThumb);
            DirectoryInfo d = new DirectoryInfo(thumbDir);
            if (d.Exists == false)
            {
                try
                {
                    d.Create();
                }
                catch
                {
                    return Globals.UrlRoot + path;
                }
            }
            string sFilename = context.Server.MapPath(Globals.UrlRoot + path);
            FileInfo f = new FileInfo(sFilename);
            string sThumbFile = thumbDir + "/" + key + "_" + id + "_" + f.Name;
            string sRet = urlThumb + key + "_" + id + "_" + f.Name;
            if (File.Exists(sThumbFile))
                return sRet;

            string type = "";
            if (path.ToLower().IndexOf("jpg") > -1 || path.ToLower().IndexOf("jpeg") > -1)
                type = "jpg";
            else if (path.ToLower().IndexOf("gif") > -1)
                type = "gif";
            else if (path.ToLower().IndexOf("bmp") > -1)
                type = "bmp";
            else
                return Globals.UrlRoot + path;
            System.Drawing.Image Thumbnail;
            System.Drawing.Image img2Scale = System.Drawing.Image.FromFile(sFilename);
            System.Drawing.Image.GetThumbnailImageAbort cb = new System.Drawing.Image.GetThumbnailImageAbort(AbortThumbnailGeneration);

            float wScale = (float)thumbWidth / img2Scale.Size.Width;
            float hScale = (float)thumbHeight / img2Scale.Size.Height;
            Size newsize = new Size(thumbWidth, thumbHeight);
            if (wScale >= 1.0 && hScale >= 1.0)
            {
                newsize.Width = img2Scale.Size.Width;
                newsize.Height = img2Scale.Size.Height;
            }
            else if (wScale < hScale)
            {
                //newsize.Width = size.Width;
                newsize.Height = Convert.ToInt32(img2Scale.Size.Height * wScale);
            }
            else //wScale >= hScale
            {
                newsize.Width = Convert.ToInt32(img2Scale.Size.Width * hScale);
            }

            Thumbnail = img2Scale.GetThumbnailImage(newsize.Width, newsize.Height, cb, IntPtr.Zero);

            switch (type)
            {
                case "jpg":
                    Thumbnail.Save(sThumbFile, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
                case "gif":
                    Thumbnail.Save(sThumbFile, System.Drawing.Imaging.ImageFormat.Gif);
                    break;
                case "bmp":
                    Thumbnail.Save(sThumbFile, System.Drawing.Imaging.ImageFormat.Bmp);
                    break;
            }
            img2Scale.Dispose();
            Thumbnail.Dispose();
            return sRet;

        }
        catch
        {
            return string.Empty;
        }
    }

    public static string ThumbnailAlbum(string path, int thumbWidth, int thumbHeight, string key, string id)
    {
        if (path.Trim().Length == 0 || path.Trim() == "")
            return string.Empty;
        try
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string urlThumb = Globals.UrlData + "Gallery/";
            string thumbDir = context.Server.MapPath(urlThumb);
            DirectoryInfo d = new DirectoryInfo(thumbDir);
            if (d.Exists == false)
            {
                try
                {
                    d.Create();
                }
                catch
                {
                    return Globals.UrlRoot + path;
                }
            }
            string sFilename = context.Server.MapPath(Globals.UrlRoot + path);
            FileInfo f = new FileInfo(sFilename);
            string sThumbFile = thumbDir + "/" + key + "_" + id + "_" + f.Name;
            string sRet = urlThumb + key + "_" + id + "_" + f.Name;
            if (File.Exists(sThumbFile))
                return sRet;

            string type = "";
            if (path.ToLower().IndexOf("jpg") > -1 || path.ToLower().IndexOf("jpeg") > -1)
                type = "jpg";
            else if (path.ToLower().IndexOf("gif") > -1)
                type = "gif";
            else if (path.ToLower().IndexOf("bmp") > -1)
                type = "bmp";
            else
                return Globals.UrlRoot + path;
            System.Drawing.Image Thumbnail;
            System.Drawing.Image img2Scale = System.Drawing.Image.FromFile(sFilename);
            System.Drawing.Image.GetThumbnailImageAbort cb = new System.Drawing.Image.GetThumbnailImageAbort(AbortThumbnailGeneration);

            float wScale = (float)thumbWidth / img2Scale.Size.Width;
            float hScale = (float)thumbHeight / img2Scale.Size.Height;
            Size newsize = new Size(thumbWidth, thumbHeight);
            if (wScale >= 1.0 && hScale >= 1.0)
            {
                newsize.Width = img2Scale.Size.Width;
                newsize.Height = img2Scale.Size.Height;
            }
            else if (wScale < hScale)
            {
                //newsize.Width = size.Width;
                newsize.Height = Convert.ToInt32(img2Scale.Size.Height * wScale);
            }
            else //wScale >= hScale
            {
                newsize.Width = Convert.ToInt32(img2Scale.Size.Width * hScale);
            }

            Thumbnail = img2Scale.GetThumbnailImage(newsize.Width, newsize.Height, cb, IntPtr.Zero);

            switch (type)
            {
                case "jpg":
                    Thumbnail.Save(sThumbFile, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
                case "gif":
                    Thumbnail.Save(sThumbFile, System.Drawing.Imaging.ImageFormat.Gif);
                    break;
                case "bmp":
                    Thumbnail.Save(sThumbFile, System.Drawing.Imaging.ImageFormat.Bmp);
                    break;
            }
            img2Scale.Dispose();
            Thumbnail.Dispose();
            return sRet;

        }
        catch
        {
            return string.Empty;
        }
    }
}
