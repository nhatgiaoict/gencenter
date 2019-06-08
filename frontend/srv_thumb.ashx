<%@ WebHandler Language="C#" Class="srv_thumb_photo" %>

using System;
using System.IO;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.Caching;
using System.Configuration;

public class srv_thumb_photo : IHttpHandler
{

    //  ----- Copyright Philipos Sakellaropoulos 2002 -------
    Image _img;
    ImageResize _ImageResize = new ImageResize();
    String _path;
    String _pathnoimage;
    bool _bStretch, _bBevel, _bUseCOMobject;

    public void ProcessRequest(HttpContext context)
    {

        int _width = 0;
        int _height = 0;
        int nPercent = 100;
        String sCacheKey;
        bool bFoundInCache = true; // by default
        // create our COM thumbnail generator
        // get width and height
        if (context.Request["w"] != null) _width = Int32.Parse(context.Request["w"]);
        if (context.Request["h"] != null) _height = Int32.Parse(context.Request["h"]);
        _path = context.Server.MapPath(Globals.UrlRoot);


        _pathnoimage = context.Server.MapPath(Globals.UrlUploads) + "\\noimage.jpg";
        _path = context.Server.MapPath(context.Request["f"].Trim());

        // allow stretch of thumbnails
        _bStretch = (context.Request["AllowStretch"] == "true");
        // bevel thumbnails
        _bBevel = (context.Request["Bevel"] == "true");
        _bUseCOMobject = (context.Request["UseCOMobj"] == "true");
        // put parameters for thumbnail requested

        // get a reference to the cache object
        Cache MyCache = context.Cache;
        sCacheKey = _ImageResize.GetUniqueThumbName(_path, _width, _height);

        // --- remove from cache when we want to refresh
        bool bRefresh = (context.Request["Refresh"] == "true");
        if (bRefresh)
        {
            MyCache.Remove(sCacheKey);
        }
        if (MyCache[sCacheKey] == null)
        {
            // the thumbnail does not exist in cache, create it...
            // Create a bitmap of the thumbnail and show it
            //      bitmap = _oGenerator.ExtractThumbnail();    //chỗ này tự khóa
            Image orgImage = null;
            try
            {
                orgImage = Image.FromFile(_path);
            }
            catch (Exception ex)
            {
                orgImage = Image.FromFile(_pathnoimage);
            }


            if (_height > 0)
            {

                _img = _ImageResize.Crop(orgImage, _width, _height, ImageResize.AnchorPosition.Center);
            }
            else
            {
                nPercent = (int)((float)_width / ((float)orgImage.Width / 100));
                if (nPercent > 100) nPercent = 100;
                _img = _ImageResize.ScaleByPercent(orgImage, nPercent);
            }

            orgImage.Dispose();

            bFoundInCache = false;
        }
        else
        { // bitmap is in cache
            _img = (Image)MyCache[sCacheKey];
        }
        // let's cache this for 1 Year
        context.Response.ContentType = "image/jpeg";
        context.Response.Cache.SetCacheability(HttpCacheability.Public);
        if (context.Request["p"] != null)
        {
            context.Response.Cache.SetExpires(DateTime.Now.AddHours(12));
        }
        else
        {
            context.Response.Cache.SetExpires(DateTime.Now.AddYears(1));
        }

        _img.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);

        //if(bFoundInCache) LogMessage("Found in cache");
        // else LogMessage("NOT Found in cache");
        //cache thumbnail, make it dependent upon the file and thumbnail size
        bool bUseCache = !(ConfigurationManager.AppSettings["UseCache"] == "false");
        if (!bFoundInCache && bUseCache)
        {
            CacheDependency dependency = new CacheDependency(_path);

            int mins;
            try
            {
                mins = int.Parse(ConfigurationManager.AppSettings["SlidingExpireMinutes"]);
            }
            catch (ArgumentException ex)
            {
                mins = 20;
            }
            MyCache.Insert(sCacheKey, _img, dependency,
               Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(mins),
               CacheItemPriority.Default, new CacheItemRemovedCallback(RemovedCallback));
            dependency.Dispose();
        }
        // bitmap in cache, dont dispose yet
        //img.Dispose ();



    }

    static public void RemovedCallback(String k, Object item, CacheItemRemovedReason r)
    {
        ((Bitmap)item).Dispose();
        //LogMessage("Callback");
    }

    // for custom tracing, normal tracing does not work with WebHandlers
    static void LogMessage(String mess)
    {
        StreamWriter sw = new StreamWriter("c:\\ASP.NET_log.txt", true);
        sw.WriteLine(mess);
        sw.Close();
    }

    public bool IsReusable
    {
        get { return true; }
    }
}