using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;


namespace MegaAuctions.Models
{
    public class PaymentModel
    {
        DBEntitiesAuction db = new DBEntitiesAuction(); 
        public IEnumerable<Payment> ListPayment()
        {
            return db.Payments.ToList();
        }

        public Payment OnePaymment(int id) 
        {
            return db.Payments.First(m => m.idpay == id);
        }

        public void save_payment(BillModel bill) 
        {
            DateTime day = DateTime.Now;
            Payment pay = new Payment();
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
            pay.idproduct = bill.idproduct;
            pay.idUser = bill.idUser;
            db.Payments.Add(pay);
            db.SaveChanges();
        }
        
    }
}