<%@ Application Language="C#" %>
<%@ Import Namespace="System.IO" %> 
<%@ Import Namespace="System.IO.Compression" %>
<script runat="server">
    //sử dụng gzip để tăng tốc độ load file
    void Application_BeginRequest(object sender, EventArgs e)
    {
        //HttpApplication app = (HttpApplication)sender;
        //string acceptEncoding = app.Request.Headers["Accept-Encoding"];
        //Stream prevUncompressedStream = app.Response.Filter;
        //if (acceptEncoding == null || acceptEncoding.Length == 0)
        //    return;
        //acceptEncoding = acceptEncoding.ToLower();
        //if (acceptEncoding.Contains("gzip"))
        //{
        //    // gzip
        //    app.Response.Filter = new GZipStream(prevUncompressedStream,
        //        CompressionMode.Compress);
        //    app.Response.AppendHeader("Content-Encoding",
        //        "gzip");
        //}
        //else if (acceptEncoding.Contains("deflate"))
        //{
        //    // defalte
        //    app.Response.Filter = new DeflateStream(prevUncompressedStream,
        //        CompressionMode.Compress);
        //    app.Response.AppendHeader("Content-Encoding",
        //        "deflate");
        //}
    }
    
    void Application_Start(object sender, EventArgs e) 
    {
    }
    
    void Application_End(object sender, EventArgs e) 
    {
    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
    }

    void Session_Start(object sender, EventArgs e) 
    {
        //Session.Timeout = 150;
        //Application.Lock();
        //Application["visitors_online"] = Convert.ToInt32(Application["visitors_online"]) + 1;
        //Application.UnLock();
    }
    void Session_End(object sender, EventArgs e) 
    {
        //Application.Lock();
        //Application["visitors_online"] = Convert.ToUInt32(Application["visitors_online"]) - 1;
        //Application.UnLock();
    }
</script>
