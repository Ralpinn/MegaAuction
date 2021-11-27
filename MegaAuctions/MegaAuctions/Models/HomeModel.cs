using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegaAuctions.Models
{
    public class HomeModel
    {
        DBEntitiesAuction db = new DBEntitiesAuction();
        public bool checkUser(string gmail, string pass)
        {
            List<User> listuser = db.Users.ToList();
            foreach(var user in listuser)
            {
                if(gmail==user.email && pass == user.password)
                {
                    return true;
                }                
            }
            return false;
        }

        public User getUser(string gmail, string pass)
        {            
            User user = db.Users.First(a => a.email == gmail && a.password == pass);
            return user;
        }
    }
}