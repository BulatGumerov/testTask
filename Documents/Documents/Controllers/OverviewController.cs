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


        public ActionResult SearchIndexAction()
        {
            return this.Redirect(Url.Action("SearchIndex"));
        }

        public ActionResult SearchIndex(string name, string tags)
        {
            var docList = Util.GetAllXml();
            if (name == null && tags == null)
            {
                return View(docList);
            }
            else
            {
                var resultDoc = docList.Where(a => a.Name.Contains(name) && a.Tags.Contains(tags));
                return View(resultDoc);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
