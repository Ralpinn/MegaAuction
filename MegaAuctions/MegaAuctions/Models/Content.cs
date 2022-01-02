using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegaAuctions.Models
{
    public class Content
    {
        static DBEntitiesAuction db = new DBEntitiesAuction();
        public static string RoleUser = "0";
        public static string StatusLogin = "";
        public static string StatusSSPrice = "";   
        public static User userinformation = null;
        public static string ran = "";
        private static Random random = new Random();
        public static string mess = "";

        public static string getDate(DateTime date)
        {
            string day = date.Day.ToString();
            string mon = date.Month.ToString();
            string year = date.Year.ToString();
            return day + "/" + mon + "/" + year;
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string getDateTimeFormat(DateTime date, String time) 
        {
            string day = date.Day.ToString();
            string mon = date.Month.ToString();
            string year = date.Year.ToString();
            return mon + "/" + day + "/" + year + " " + time + ":00";
        }
       

        
        public static int FindVt(List<Auction> dsauction, int idpro)
        {
            for (int i = dsauction.Count - 1; i >=0; i--) 
            {
                if(dsauction[i].idproduct == idpro)
                {
                    return i;
                }
            }
            return 0;
        }
        public static List<int> FinddsVt(List<Auction> dsauction, List<Product> dspro)
        {                                               //11                   //15
            List<int> dsVt = new List<int>();
            for (int i = 0; i < dspro.Count; i++) //0->14
            {
                int num = 0;
                for (int j = dsauction.Count - 1; j >= 0; j--) //0->10
                {
                    if (dspro[i].idproduct == dsauction[j].idproduct && num == 0)
                    {
                        dsVt.Add(j); num=1;
                    }
                }
                if (num == 0)
                {
                    dsVt.Add(0);
                }
            }
            return dsVt; //0->14
        }
        public static int FindidUserWinner(List<Auction> dsauction, int idpro) 
        {
            for (int i = dsauction.Count - 1; i >= 0; i--)
            {
                if (dsauction[i].idproduct == idpro)
                {
                    return dsauction[i].idUser.Value;
                }
            }
            return 0;
        }

        public static string TinhThueTax(string price) //30.000.000 * 2%
        {
            string tax = "";
            string[] a = price.Split('.');
            for (int i = 0; i < a.Length; i++)
            {
                tax += a[i];
            }
            int money = int.Parse(tax); //30000000
            double moneytax = Math.Round(money * 0.02,2); //600000
            tax = moneytax.ToString();
            return tax;
        }
        public static string DoiTienUSD(string price) //30.000.000 * 2% 
        {
            string usd = "";
            string[] a = price.Split('.');
            for (int i = 0; i < a.Length; i++)
            {
                usd += a[i];
            }
            int money = int.Parse(usd); //30000000
            double moneytax = Math.Round((double)money/23000,2);  //1304.348 
            usd = moneytax.ToString();
            return usd;
        }
        public static string SumPriceUSD(string price) //30.000.000 + tax + ship
        {
            string total = "";
            //Price
            string money = DoiTienUSD(price); //1304.348 
            double money_usd = double.Parse(money); //30000000
            //Tax
            string tax = DoiTienUSD(TinhThueTax(price)); //26.087
            double tax_usd = double.Parse(tax); //26.087 
            //Total = price + tax + ship 
            double sum = money_usd + tax_usd + 1;
            total = sum.ToString();
            return total;
        }

        public static int FindIDAuction(int idproduct)
        {
            List<Auction> listauc = db.Auctions.ToList();
            for(int i = listauc.Count-1; i>=0; i++)
            {
                if(listauc[i].idproduct == idproduct)
                {
                    return listauc[i].idauction;
                }
            }
            return 0;
        }
    }
}
