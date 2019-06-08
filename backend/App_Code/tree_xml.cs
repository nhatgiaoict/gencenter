using System;
using System.Web;
using System.Collections;
using System.Xml;
using uss.utils;
using System.Data;

/// <summary>
/// Summary description for tree_xml
/// </summary>
    public class tree_xml
    {
        //========================================================================================================
        public tree_xml()
        {
        }
        //========================================================================================================
        virtual protected Hashtable GetItem(string level)
        {
            return null;
        }
        //========================================================================================================
        public string GetXmlString(string level)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<tree></tree>");

            for (int i = 1; ; i++)
            {
                string ChildLevel = string.Format("{0}.{1}", level, i);
                Hashtable NodeAttributes = GetItem(ChildLevel);
                if (NodeAttributes == null)
                    break;

                XmlNode node = doc.CreateElement("tree");
                IDictionaryEnumerator Enumerator = NodeAttributes.GetEnumerator();
                while (Enumerator.MoveNext())
                {
                    XmlAttribute Attr = doc.CreateAttribute((string)Enumerator.Key);
                    Attr.Value = (string)Enumerator.Value;
                    node.Attributes.Append(Attr);
                }

                doc.DocumentElement.AppendChild(node);
            }
            return doc.InnerXml;
        }
        //========================================================================================================
        virtual protected Hashtable[] GetItemList(string parent)
        {
            return null;
        }
        //========================================================================================================
        public string GetXmlString2(string parent)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<tree></tree>");

            Hashtable[] itemlist = GetItemList(parent);
            if (itemlist == null)
                return "";
            foreach (Hashtable NodeAttributes in itemlist)
            {
                XmlNode node = doc.CreateElement("tree");
                IDictionaryEnumerator Enumerator = NodeAttributes.GetEnumerator();
                while (Enumerator.MoveNext())
                {
                    XmlAttribute Attr = doc.CreateAttribute((string)Enumerator.Key);
                    Attr.Value = (string)Enumerator.Value;
                    node.Attributes.Append(Attr);
                }

                doc.DocumentElement.AppendChild(node);
            }
            return doc.InnerXml;
        }
        //========================================================================================================
        public string GetItemsXmlString(string itemsString)
        {
            string delimStr = ",";
            string[] items = itemsString.Split(delimStr.ToCharArray());

            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<tree></tree>");

            foreach (string item in items)
            {
                Hashtable NodeAttributes = GetItem(item);
                if (NodeAttributes == null)
                    break;

                XmlNode node = doc.CreateElement("tree");
                IDictionaryEnumerator Enumerator = NodeAttributes.GetEnumerator();
                while (Enumerator.MoveNext())
                {
                    XmlAttribute Attr = doc.CreateAttribute((string)Enumerator.Key);
                    Attr.Value = (string)Enumerator.Value;
                    node.Attributes.Append(Attr);
                }

                doc.DocumentElement.AppendChild(node);
            }
            return doc.InnerXml;
        }
    }
    public class TreeItem
    {
        public string Text;
        public string LinkParam;
        public string TreeParam = "";

        public TreeItem()
        {
        }
        public TreeItem(string text, string linkparam)
        {
            Text = text;
            LinkParam = linkparam;
        }
        public TreeItem(string text, string linkparam, string treeparam)
        {
            Text = text;
            LinkParam = linkparam;
            TreeParam = treeparam;
        }
    }
