using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HM2.Model
{
    public class RegistrationModel
    {
        public RegistrationModel() { }

        public bool CreateNewUser(string login, string password , string fio , string number)
        {
            using (HotelModel hm = new HotelModel())
            {
                var users = (from u in hm.User where u.login == login select u).ToList();
                if (users.Count > 0)
                {
                    return false;
                }
                User user = new User();
                user.number = number;
                user.login = login;
                user.password = password;
                user.FIO = fio;
                user.IdRole = 2;
                hm.User.Add(user);
                hm.SaveChanges();
            }
            return true;
        }
    }
}
