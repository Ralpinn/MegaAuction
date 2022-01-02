using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegaAuctions.Models
{
    public class AuctionModel
    {
        DBEntitiesAuction db = new DBEntitiesAuction();
        public IEnumerable<Auction> ListAuction()
        {
            return db.Auctions.ToList();
        }
        public IEnumerable<Auction> ListAuctionById(int idpro)
        {
            return db.Auctions.Where(m => m.idproduct == idpro).ToList();
        }
        public Auction OneAuction(int id)
        {
            return db.Auctions.First(m => m.idauction == id);
        }

        public void add_auction(string price, int idpro) 
        {
            DateTime day = DateTime.Now;
            string h = Convert.ToString(day.Hour);
            string m = Convert.ToString(day.Minute);
            string s = Convert.ToString(day.Second);
            string time = h + ":" + m + ":" + s;
            Auction ac = new Auction();
            ac.priceup = price;
            ac.dateauction = day;
            ac.timeauction = time;
            ac.statusauction = "Action";
            ac.statusorder = "No";
            ac.idUser = Content.userinformation.idUser;
            ac.idproduct = idpro;
            db.Auctions.Add(ac);
            db.SaveChanges();
        }

        public bool check_price_now(string priceup, string pricenow)
        {
            int priup = changeToNumber(priceup);
            int prinow = changeToNumber(pricenow);

            if(priup >= (prinow + 10000))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int changeToNumber(string pri)
        {
            string so = "";
            string[] a = pri.Split('.');
            for (int i = 0; i <= a.Length - 1; i++) 
            {
                so +=a[i];
            }
            return int.Parse(so);
        }
        public void EditEndStatusAuction(int idpro)
        {
            IEnumerable<Auction> lisauction = ListAuctionById(idpro); 
            foreach(var item in lisauction)
            {
                Auction ac = OneAuction(item.idauction);
                ac.statusauction = "End";
                db.SaveChanges();
            }            
        }

        public void setStatusAuction(int idproduct) //Payment Success
        {
            List<Auction> listauction = db.Auctions.ToList();
            for (int i = listauction.Count - 1; i >= 0; i++)
            {
                if (listauction[i].idproduct == idproduct)
                {
                    listauction[i].statusorder = "Ordered";
                    db.SaveChanges();
                    break;
                }
            }
        }
    }
}