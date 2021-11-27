using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MegaAuctions.Controllers
{
    public class BlogController : Controller
    {
        //Manage Blogs
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit() 
        {
            return View();
        }
    }
}