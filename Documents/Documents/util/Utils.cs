using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Documents.Models;
using System.Xml;
using System.Web.Security;
using System.Globalization;
using Ext.Net.MVC;
using Ext.Net;

namespace Utils
{
    public static class Util
    {
        public static List<Document> GetAllXml()
        {
            var dir = System.IO.Directory.GetFiles((System.Web.HttpContext.Current.Server.MapPath("~/XmlFiles/")));
            var docList = new List<Document>();
            foreach (var path in dir)
            {
                var doc = XmlToDocument(path);
                docList.Add(doc);
            }
            return docList;
        }

        public static Document XmlToDocument(string filePath)
        {
            var doc = new Document();
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            XmlNode node = xmlDoc.SelectSingleNode("/Document");
            doc.Name = node.ChildNodes[0].InnerText;
            //newDoc.ReleaseDate = DateTime.ParseExact(node.ChildNodes[1].InnerText, "dd.MM.yyyy HHmmss", DateTimeFormatInfo.CurrentInfo);
            doc.ReleaseDate = node.ChildNodes[1].InnerText;
            doc.Author = node.ChildNodes[2].InnerText;
            doc.Tags = "";
            for (var i = 3; i < node.ChildNodes.Count; i++)
            {
                doc.Tags += node.ChildNodes[i].InnerText;
            }
            return doc;
        }

        public static void DocumentToXml(Document document)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode headNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(headNode);

            XmlNode docNode = doc.CreateElement("Document");
            doc.AppendChild(docNode);

            XmlNode nameNode = doc.CreateElement("Name");
            nameNode.AppendChild(doc.CreateTextNode(document.Name));
            docNode.AppendChild(nameNode);

            XmlNode releaseNode = doc.CreateElement("ReleaseDate");
            var time = DateTime.Now;
            releaseNode.AppendChild(doc.CreateTextNode(time.ToString("dd.MM.yyyy HHmmss")));
            docNode.AppendChild(releaseNode);

            XmlNode authorNode = doc.CreateElement("Author");
            authorNode.AppendChild(doc.CreateTextNode(Membership.GetUser().UserName));
            docNode.AppendChild(authorNode);

            var list = document.Tags.Split(',').ToList();
            for (var i = 0; i < list.Count; i++)
            {
                XmlNode tagNode = doc.CreateElement("Tag" + i);
                tagNode.AppendChild(doc.CreateTextNode(list[i]));
                docNode.AppendChild(tagNode);
            }

            doc.Save((System.Web.HttpContext.Current.Server.MapPath("~/XmlFiles/" + time.ToString("dd.MM.yyyy HHmmss") + ".xml")));
        }

        public static void UpdateDocument(Document document)
        {
            System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath("~/XmlFiles/" + document.ReleaseDate + ".xml"));

            XmlDocument doc = new XmlDocument();
            XmlNode headNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(headNode);

            XmlNode docNode = doc.CreateElement("Document");
            doc.AppendChild(docNode);

            XmlNode nameNode = doc.CreateElement("Name");
            nameNode.AppendChild(doc.CreateTextNode(document.Name));
            docNode.AppendChild(nameNode);

            XmlNode releaseNode = doc.CreateElement("ReleaseDate");
            var time = DateTime.Now;
            releaseNode.AppendChild(doc.CreateTextNode(document.ReleaseDate));
            docNode.AppendChild(releaseNode);

            XmlNode authorNode = doc.CreateElement("Author");
            authorNode.AppendChild(doc.CreateTextNode(Membership.GetUser().UserName));
            docNode.AppendChild(authorNode);

            var list = document.Tags.Split(';').ToList();
            for (var i = 0; i < list.Count; i++)
            {
                XmlNode tagNode = doc.CreateElement("Tag" + i);
                tagNode.AppendChild(doc.CreateTextNode(list[i]));
                docNode.AppendChild(tagNode);
            }

            doc.Save((System.Web.HttpContext.Current.Server.MapPath("~/XmlFiles/" + document.ReleaseDate + ".xml")));
        }


        public static IEnumerable<Document> SearchXml(string name, string tags)
        {
            var dir = System.IO.Directory.GetFiles((System.Web.HttpContext.Current.Server.MapPath("~/XmlFiles/")));
            var docList = new List<Document>();
            foreach (var path in dir)
            {
                var doc = XmlToDocument(path);
                docList.Add(doc);
            }
            var resultList = docList.Where(s => s.Name.Contains(name) || s.Tags.Contains(tags));
            return resultList;
        }
    }
}