using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ext.Net;
using Documents.Models;
using System.Xml;
using System.Web.Security;
using Utils;

namespace Documents.Controllers
{
    public class OverviewController : Controller
    {
        public ActionResult Index()
        {
            return View(Util.GetAllXml());
        }

        public ActionResult IndexAction()
        {
            return this.Redirect(Url.Action("Index"));
        }

        public ActionResult CreateAction()
        {
            return this.Redirect(Url.Action("Create"));
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

        [HttpPost]
        public void Open(string path)
        {
            System.Diagnostics.Process.Start(Server.MapPath("~/XmlFiles/" + path + ".xml"));
        }

        [DirectMethod, HttpPost]
        public ActionResult Find(string name, string tags)
        {
            var docList = Util.SearchXml(name, tags);
            return View(docList);
        }

        public ActionResult Find()
        {
            return View();
        }

        public ActionResult FindAction()
        {
            return this.Redirect(Url.Action("Find"));
        }


        public ActionResult SearchIndexAction()
        {
            return this.Redirect(Url.Action("SearchIndex"));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
