using DAL.AdditionalEntities;
using DAL.Entities;
using HM2.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM2.Model
{
    public class LoginUserModel
    {
        public UserExtension GetAuthenticatedUser(string login, string password)
        {
            using (HotelModel hm = new HotelModel())
            {
                var user = hm.User.FirstOrDefault(u => u.login == login);

                if (user != null && user.password == password)
                {   
                    return new UserExtension(user);
                }

                return null;
            }
        }
        
        public string GetWindowId(UserExtension userExtension)
        {
            if(IsClient(userExtension))
            {
                return "MAIN_WINDOW";
            }
            return "ADMIN_WINDOW";
        }
        public bool IsClient(UserExtension user)
        {
            if (user.IdRole == 2)
            {
                return true;
            }
            return false;
        }

    }
}
