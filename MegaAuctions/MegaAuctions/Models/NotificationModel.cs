using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;

namespace MegaAuctions.Models
{
    public class NotificationModel
    {
        DBEntitiesAuction db = new DBEntitiesAuction();
        List<int> dsID = new List<int>();
        public IEnumerable<Notification> ListNotification()
        {
            return db.Notifications.ToList();
        }
        public IEnumerable<Notification> ListNotifiOfPro(int idpro) 
        {
            return db.Notifications.Where(m => m.idNotif == idpro).ToList();
        }
        public Notification OneNotification(int id)
        {
            return db.Notifications.First(m => m.idNotif == id);
        }

        public void displayCount(int iduser, int idproduct) //   2,  1
        {
            int x = 1;
            List<Auction> dsAuction = db.Auctions.Where(m => m.idproduct == idproduct).ToList(); //1
            dsID.Add(dsAuction[0].idUser.Value);
            for (int i = 1; i < dsAuction.Count; i++) // 7
            {
                int dem = 0;
                for (int j = 0; j < x; j++)
                {
                    if (dsAuction[i].idUser == dsID[j])
                        dem++;
                }
                if (dem == 0 && dsAuction[i].idUser != iduser)
                {
                    dsID.Add(dsAuction[i].idUser.Value);
                    x++;
                }
            }
            for (int i = 0; i < dsID.Count; i++)
            {
                if(dsID[i] == iduser)
                {
                    dsID.Remove(dsID[i]);
                }
            }
        }

        public void add_Notification(int _idUser, int _idproduct)  //  2, 1
        {
            displayCount(_idUser,_idproduct);
            DateTime day = DateTime.Now;
            for (int i = 0; i< dsID.Count; i++)
            {
                Notification n1 = new Notification();
                n1.titleN = "Auction Fail";
                n1.contentN = "Đấu giá thất bại, đã có người đấu giá cao hơn bạn !";
                n1.dateN = day;
                n1.statusNotif = "Fail";
                n1.idproduct = _idproduct;
                n1.idUser = dsID[i];
                db.Notifications.Add(n1);
                db.SaveChanges();
            }
        }

        public void add_Notification_Winner(int _idUser, int _idproduct)
        {
            displayCount(_idUser, _idproduct);
            DateTime day = DateTime.Now;
            Notification n1 = new Notification();
            n1.titleN = "Auction Success";
            n1.contentN = "Đấu giá thành công. Thanh toán ngay!";
            n1.dateN = day;
            n1.statusNotif = "Success";
            n1.idproduct = _idproduct;
            n1.idUser = _idUser;
            db.Notifications.Add(n1);
            db.SaveChanges();
        }

        public void setStatusNoti(int idproduct) //Payment Success
        {
            List<Notification> listNoti = db.Notifications.ToList();
            for (int i = listNoti.Count - 1; i >= 0; i++)
            {
                if(listNoti[i].idproduct == idproduct)
                {
                    listNoti[i].statusNotif = "Paid";
                    db.SaveChanges();
                    break;
                }
            }
        }
    }
}