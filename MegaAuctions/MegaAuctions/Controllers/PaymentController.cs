using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PayPal.Api;
using System.Web.Mvc;
using MegaAuctions.Models;


namespace MegaAuctions.Controllers
{
    public class PaymentController:Controller
    {
        User userseller = MegaAuctions.Models.Content.userinformation;
        ProductModel promodel = new ProductModel();
        NotificationModel notifimodel = new NotificationModel();
        AuctionModel aucmodel = new AuctionModel();
        PaymentModel paymodel = new PaymentModel();
        static int flag = 0; 
        static BillModel billset = new BillModel();
        public ActionResult Index(int idproduct)
        {
            ViewBag.PayProduct = promodel.OneProduct(idproduct);
            ViewBag.ListNotification = notifimodel.ListNotification();
            return View(userseller);
        }
        public ActionResult Auction(int idauction)  
        {
            ViewBag.PayAuction = aucmodel.OneAuction(idauction);
            ViewBag.ListNotification = notifimodel.ListNotification();
            return View(userseller);
        }
        public ActionResult Failure() 
        {
            ViewBag.ListNotification = notifimodel.ListNotification();
            return View(userseller);
        }
        public ActionResult Success()
        {
            ViewBag.ListNotification = notifimodel.ListNotification();
            return View(userseller);
        }
        public ActionResult PaymentWithPaypal(BillModel bill)
        {
            //Goi flag dam bao thuc hien 1 lan tranh chong du lieu, mat du lieu khi save db
            if(flag == 0)
            {
                billset = bill;
                flag++;
            }            
            //
            ViewBag.ListNotification = notifimodel.ListNotification();
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Payment/PaymentWithPaypal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url                      
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, bill);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("Failure", userseller);
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Failure", userseller);
            }
            //save bill in payment  
            paymodel.save_payment(billset);
            //change status Notification
            notifimodel.setStatusNoti(billset.idproduct);
            //change status Auctions
            aucmodel.setStatusAuction(billset.idproduct);
            //Set flag
            flag = 0;
            // Create a payment using a APIContext              
            //on successful payment, show success page to user.             
            return View("Success", userseller);
        }
        private PayPal.Api.Payment payment;
        private PayPal.Api.Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new PayPal.Api.Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        // 10.000.000 vnd
        private PayPal.Api.Payment CreatePayment(APIContext apiContext, string redirectUrl, BillModel bill)
        {
            
            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };            
            //Adding Item Details like name, currency, price etc  
            itemList.items.Add(new Item()
            {
                name = bill.nameproduct,   //Name product
                currency = "USD",
                price = bill.price,                     //Price product by USD
                quantity = "1",
                sku = "SKU-PAYPAL-" + MegaAuctions.Models.Content.RandomString(5)
            });
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = bill.tax, //thue
                shipping = bill.ship, //ship
                subtotal = bill.price  // tổng giá product
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = bill.total, // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "Pay with MetaBid by paypal wallet",
                invoice_number = MegaAuctions.Models.Content.RandomString(10),
                amount = amount,
                item_list = itemList
            });
            this.payment = new PayPal.Api.Payment()
            {
                intent = "Sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };            
            return this.payment.Create(apiContext);
        }
        DBEntitiesAuction db = new DBEntitiesAuction();
        public void save_payment(BillModel bill)
        {
            DateTime day = DateTime.Now;
            Models.Payment pay = new Models.Payment();
            pay.method = "Paypal";
            pay.currency = "USD";
            pay.date = day;
            pay.price = bill.price;
            pay.tax = bill.tax;
            pay.ship = bill.ship;
            pay.namecontact = bill.namect;
            pay.phonecontact = bill.phonect;
            pay.addresscontact = bill.addressct;
            pay.subtotal = bill.total;
            pay.quantity = 1;
            pay.idproduct = 2;
            pay.idUser = 4;
            db.Payments.Add(pay);
            db.SaveChanges();
        }
    }
}