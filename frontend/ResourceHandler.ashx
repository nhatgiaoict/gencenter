<%@ WebHandler Language="C#" Class="ResourceHandler" %>
using System;
using System.IO;
using System.Web;
using System.Configuration;
/// <summary>
/// Created by Hungnm
/// </summary>
public class ResourceHandler : IHttpHandler
{
    public bool IsReusable
    {
        get
        {
            return true;
        }
    }

    public void ProcessRequest (HttpContext context)
    {
        string fileSet = context.Request.QueryString["fileSet"];

        //Kiểm tra xem có dùng Compe file ko?
        if (string.IsNullOrEmpty(fileSet))
        {
            return;
        }

        string contetType = context.Request.QueryString["type"];

        if (string.IsNullOrEmpty(contetType))
        {
            return;
        }

        string files = ConfigurationManager.AppSettings["FileSet_" + fileSet];

        if (string.IsNullOrEmpty(files))
        {
            return;
        }

       // Lấy danh sách các tập tin quy định tại các FileSet
        string[] fileNames = files.Split(',');

        if ((fileNames != null) && (fileNames.Length > 0))
        {
            //Đọc tất cả các tệp tin
            for (int i = 0; i < fileNames.Length; i++)
            {
                context.Response.Write(File.ReadAllText(context.Server.MapPath(fileNames[i])));
            }

            //Set kiểu dữ liệu là CSS hay Js
            context.Response.ContentType = contetType;

            // Cache 5 ngày
            CacheUtility.Cache(TimeSpan.FromDays(5));
        }
    }
}