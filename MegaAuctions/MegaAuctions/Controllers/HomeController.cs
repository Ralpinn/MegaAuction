using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MegaAuctions.Models;


namespace MegaAuctions.Controllers
{
    public class HomeController : Controller
    {
        HomeModel homemodel = new HomeModel();
        ProductModel productmodel = new ProductModel();
        static User userinfo = null;
        public ActionResult Index()
        {
            MegaAuctions.Models.Content.userinformation = userinfo;
            ViewBag.ListPro = productmodel.ListProduct();
            return View(userinfo);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        //LOGIN
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string gmail, string pass)  
        {
            MegaAuctions.Models.Content.StatusLogin = "";
            if (homemodel.checkUser(gmail, pass))
            {
                userinfo = homemodel.getUser(gmail, pass);
                MegaAuctions.Models.Content.RoleUser = userinfo.idRole.ToString();
                MegaAuctions.Models.Content.StatusLogin = "Login Success !";
                return RedirectToAction("Index");
            }
            else
            {
                MegaAuctions.Models.Content.StatusLogin = "Login Failed !";
                return RedirectToAction("Login");
            }
        }
        //REGISTER
        public ActionResult Register() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)  
        {
            return RedirectToAction("Index");
        }

        //AUCTIONS
        public ActionResult Auctions() 
        {
            ViewBag.ListPro = productmodel.ListProduct();
            ViewBag.ListTypePro = productmodel.ListTypeProduct();
            return View(userinfo);
        }

        //DETAIL PRODUCT
        public ActionResult DetailPro(int id) 
        {
            ViewBag.DetailProduct = productmodel.OneProduct(id);
            return View(userinfo);
        }
        //Blog
        public ActionResult Blogs()
        {
            return View(userinfo);
        }
    }
}