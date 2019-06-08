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
using System.Collections;
using System.Text.RegularExpressions;
using System.Web.Caching;

/// <summary>
/// Summary description for clsCache
/// </summary>
public class clsCache
{
    private static readonly Cache _cache;

    static clsCache()
    {
        HttpContext current = HttpContext.Current;
        if (current != null)
        {
            _cache = current.Cache;
        }
        else
        {
            _cache = HttpRuntime.Cache;
        }
    }

    private clsCache()
    {
    }

    public static void Clear()
    {
        IDictionaryEnumerator enumerator = _cache.GetEnumerator();
        while (enumerator.MoveNext())
        {
            _cache.Remove(enumerator.Key.ToString());
        }
    }

    public static object Get(string key)
    {
        return _cache[key];
    }

    public static void Max(string key, object obj)
    {
        Max(key, obj, null);
    }

    public static void Max(string key, object obj, CacheDependency dep)
    {
        if (IsEnabled && (obj != null))
        {
            // _cache.Insert(key, obj, dep, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.High, null);
            _cache.Insert(key, obj, dep, DateTime.Now.AddSeconds(30), TimeSpan.Zero, CacheItemPriority.High, null);
        }
    }
    //Cache với maxvalue
    public static void Max_Value(string key, object obj)
    {
        Max_Value(key, obj, null);
    }
    public static void Max_Value(string key, object obj, CacheDependency dep)
    {
        if (IsEnabled && (obj != null))
        {
            _cache.Insert(key, obj, dep, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.High, null);
        }
    }
    //end
    public static void Remove(string key)
    {
        _cache.Remove(key);
    }

    public static void RemoveByPattern(string pattern)
    {
        IDictionaryEnumerator enumerator = _cache.GetEnumerator();
        Regex regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        while (enumerator.MoveNext())
        {
            if (regex.IsMatch(enumerator.Key.ToString()))
            {
                _cache.Remove(enumerator.Key.ToString());
            }
        }
    }

    public static bool IsEnabled
    {
        get
        {
            return true;
        }
    }
}

