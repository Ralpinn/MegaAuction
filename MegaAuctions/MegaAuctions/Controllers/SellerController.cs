using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MegaAuctions.Models;

namespace MegaAuctions.Controllers
{
    public class SellerController : Controller
    {
        User userseller = MegaAuctions.Models.Content.userinformation;
        public ActionResult Index()
        {
            return View(userseller);
        }
        public ActionResult Profiles()  
        { 
            return View(userseller);
        }
    }
}