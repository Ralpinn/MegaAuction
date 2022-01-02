using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegaAuctions.Models
{
    public class HomeModel
    {
        DBEntitiesAuction db = new DBEntitiesAuction();
        public IEnumerable<User> ListUser() 
        {
            return db.Users.ToList();
        }
        public User Queryuser(int id)
        {
            return db.Users.First(m => m.idUser == id);
        }
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

        public void add_signin_user(User us)
        {
            us.idRole = 3;
            db.Users.Add(us);
            db.SaveChanges();
        }
        
        public void edit_info(User us)
        {
            User x = Queryuser(us.idUser);
            x.name = us.name;
            x.phone = us.phone;
            x.born = us.born;
            x.address = us.address;
            x.email = us.email;
            x.password = us.password;
            x.gender = us.gender;
            db.SaveChanges();
        }
    }
}