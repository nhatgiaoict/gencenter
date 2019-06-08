using System;
using System.Web;
using System.Collections;
using System.Xml;
using uss.utils;
using System.Data;
/// <summary>
/// Summary description for tree_group_xml
/// </summary>
public class tree_group_xml : tree_xml
    {
        private bool _SelectParent = true;
        private int _kind = 2;
        public tree_group_xml(int kind, bool b)
        {
            _SelectParent = b;
            _kind = kind;
        }
        override protected Hashtable GetItem(string parent)
        {
            Hashtable hash = new Hashtable(3);
            Groups group = new Groups();
            DataRow dr = group.GetInfo(parent);

            hash.Add("text", dr["title"].ToString());
            hash.Add("action", dr["id"].ToString());
            //if(Topics.NextChild(faction.ID))
            hash.Add("src", dr["id"].ToString());
            return hash;
        }
        //========================================================================================================
        override protected Hashtable[] GetItemList(string parent)
        {
            Groups group = new Groups();
            return group.GetChildGroup(_kind, parent, _SelectParent);
        }
    }

