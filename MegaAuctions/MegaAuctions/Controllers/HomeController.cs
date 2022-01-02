using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Net;
using System.Web.Mvc;
using MegaAuctions.Models;


namespace MegaAuctions.Controllers
{
    public class HomeController : Controller
    {
        HomeModel homemodel = new HomeModel();
        ProductModel productmodel = new ProductModel();
        AuctionModel auctionmodel = new AuctionModel();
        NotificationModel notifimodel = new NotificationModel();
        static User userinfo = null;
        public ActionResult Index()
        {
            MegaAuctions.Models.Content.userinformation = userinfo;
            ViewBag.ListPro = productmodel.ListProduct();
            ViewBag.TableAuction = auctionmodel.ListAuction();
            ViewBag.ListNotification = notifimodel.ListNotification();
            return View(userinfo);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.ListNotification = notifimodel.ListNotification();
            return View(userinfo);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.ListNotification = notifimodel.ListNotification();
            return View(userinfo);
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
            homemodel.add_signin_user(user);
            return RedirectToAction("Index");
        }

        //FORGET PASS
        public ActionResult Resetpass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Resetpass_sendEmail(string toemail)
        {
            var listuser = homemodel.ListUser();
            foreach (var user in listuser)
            {
                if (user.email == toemail)
                {
                    string subject = "METABID Reset Password";
                    string body = "Hệ thống đã xác thực tài khoản của bạn !\nMật khẩu : " + user.password;

                    WebMail.Send(toemail, subject, body, null, null, null, true, null, null, null, headerEncoding: null, null, null);

                    MegaAuctions.Models.Content.mess = "Success";
                    return RedirectToAction("Resetpass");
                }
            }
            MegaAuctions.Models.Content.mess = "Send Email Fail";
            return RedirectToAction("Resetpass");
        }

        //LOG OUT
        public ActionResult Logout() 
        {
            MegaAuctions.Models.Content.RoleUser = "0";
            return RedirectToAction("Index");
        }

        //AUCTIONS
        public ActionResult Auctions() 
        {
            ViewBag.ListPro = productmodel.ListProduct();
            ViewBag.ListTypePro = productmodel.ListTypeProduct();
            ViewBag.ListNotification = notifimodel.ListNotification();
            return View(userinfo);
        }

        //DETAIL PRODUCT
        public ActionResult DetailPro(int id) 
        {
            ViewBag.DetailProduct = productmodel.OneProduct(id);
            ViewBag.TableAuction = auctionmodel.ListAuction();
            ViewBag.ListNotification = notifimodel.ListNotification();
            return View(userinfo);
        }
        [HttpPost]
        public ActionResult DetailPro(string priceup, int idproduct, string pricenoww)
        {
            if(priceup == String.Empty || priceup == null || priceup == "")
            {
                MegaAuctions.Models.Content.StatusSSPrice = "Auction failed, Minimum bid is 10,000 more than current price!";
            }
            else if(auctionmodel.check_price_now(priceup, pricenoww) == true)
            {
                auctionmodel.add_auction(priceup, idproduct);
                notifimodel.add_Notification(userinfo.idUser, idproduct); // 2, 1
                MegaAuctions.Models.Content.StatusSSPrice = "Auction success!";
            }
            else
            {
                MegaAuctions.Models.Content.StatusSSPrice = "Auction failed, Minimum bid is 10,000 more than current price!";
            }
            return RedirectToAction("DetailPro");
        }
        //EndResetAuction
        public ActionResult EndResetAuction(int idUser, int idproduct)
        {
            notifimodel.add_Notification_Winner(idUser, idproduct);
            productmodel.EditEndStatusPro(idproduct);
            auctionmodel.EditEndStatusAuction(idproduct);
            return RedirectToAction("DetailPro" + "/" + idproduct); 
        }
        //Blog
        public ActionResult Blogs()
        {
            ViewBag.ListNotification = notifimodel.ListNotification();
            return View(userinfo);
        }

        public ActionResult EditInfo(int id)
        {
            ViewBag.ListNotification = notifimodel.ListNotification();
            return View(homemodel.Queryuser(id));  //lay id, user đưa vào query user loc tu bang user
        }
        [HttpPost]
        public ActionResult EditInfo(User us)
        {
            homemodel.edit_info(us);
            return RedirectToAction("Index");
        }
    }
}