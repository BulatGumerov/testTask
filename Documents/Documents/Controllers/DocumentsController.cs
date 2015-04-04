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
using Utils;


namespace Documents.Controllers.Content
{ 
    public class DocumentsController : Controller
    {
        public ActionResult Index()
        {
            return View(Util.GetAllXml());
        }

        public ActionResult IndexAction()
        {
            return this.Redirect(Url.Action("Index"));
        }


        public ViewResult Details(string documentName)
        {
            var document = Util.XmlToDocument(Server.MapPath("~/XmlFiles/" + documentName + ".xml"));
            return View(document);
        }


        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult Create(Document document)
        {
            if (ModelState.IsValid)
            {
                Util.DocumentToXml(document);
                return RedirectToAction("Index");
            }

            return View(document);
        }
        

        public ActionResult Edit(string documentName)
        {
            var doc = Util.XmlToDocument(Server.MapPath("~/XmlFiles/" + documentName + ".xml"));
            return View(doc);
        }


        [HttpPost]
        public ActionResult Edit(Document document)
        {
            if (ModelState.IsValid)
            {
                Util.UpdateDocument(document);
                return RedirectToAction("Index"); 
            }
            return View(document);
        }

 
        public ActionResult Delete(string documentName)
        {
            var doc = Util.XmlToDocument(Server.MapPath("~/XmlFiles/" + documentName + ".xml"));
            return View(doc);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string documentName)
        {
            System.IO.File.Delete(Server.MapPath("~/XmlFiles/" + documentName + ".xml"));
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}