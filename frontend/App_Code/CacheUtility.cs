using System;
using System.Web;
using System.Reflection;


public static class CacheUtility
{
    public static void Cache(TimeSpan duration)
    {
        HttpCachePolicy cache = HttpContext.Current.Response.Cache;

        FieldInfo maxAgeField = cache.GetType().GetField("_maxAge", BindingFlags.Instance | BindingFlags.NonPublic);
        maxAgeField.SetValue(cache, duration);

        cache.SetCacheability(HttpCacheability.Public);
        cache.SetExpires(DateTime.Now.Add(duration));
        cache.SetMaxAge(duration);
        cache.AppendCacheExtension("must-revalidate, proxy-revalidate");
    }
}
