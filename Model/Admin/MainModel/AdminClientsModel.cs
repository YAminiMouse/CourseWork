using DAL.AdditionalEntities;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM2.Model.Admin
{
    public class AdminClientsModel
    {
        public AdminClientsModel() 
        {

        }

        public List<UserExtension> GetUserByNumber(string selectedPhoneClient)
        {
            List<UserExtension> users = new List<UserExtension>();

            using (HotelModel hm = new HotelModel())
            {
                List<User> list = (from u in hm.User where u.IdRole == 2 && u.number == selectedPhoneClient select u).ToList();
                foreach (User u in list)
                {
                   users.Add(new UserExtension(u));
                }
            }

            return users;
        }

        public List<UserExtension> GetAllUsers()
        {
            List<UserExtension> users = new List<UserExtension>();
            using (HotelModel hm = new HotelModel())
            {
                List<User> list = (from u in hm.User where u.IdRole == 2 select u).ToList();
                foreach (User u in list)
                {
                    users.Add(new UserExtension(u));
                }
            }
            return users;
        }
    }
}
