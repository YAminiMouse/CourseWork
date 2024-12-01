using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HM2.Model
{
    public class RegistrationModel
    {
        public RegistrationModel() { }

        // 0 - пользователь с таким логином есть
        // 1 - логин менее 8 символов
        // 2 - Пароль содержит менее 8 символов
        // 3 - Некорректный номер
        // 4 - Некорректное ФИО
        // 5  - все данные корректны
        public int CreateNewUser(string login, string password , string fio , string number)
        {
            Regex regexNumber = new Regex(@"^((\+7|7|8)+([0-9]){10})$");
            Regex regexFIO = new Regex(@"^[а-яА-ЯёЁa-zA-Z]+ [а-яА-ЯёЁa-zA-Z]+ [а-яА-ЯёЁa-zA-Z]+$");

            if (login.Count() < 8)
            {
                return 1;
            }
            if (password.Count() < 8)
            {
                return 2;
            }
            if (!regexFIO.IsMatch(fio))
            {
                return 3;
            }
            if (!regexNumber.IsMatch(number))
            {
                return 4;
            }
            
            using (HotelModel hm = new HotelModel())
            {
                var users = (from u in hm.User where u.login == login select u).ToList();
                if (users.Count > 0)
                {
                    return 0; 
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
            return 5;
        }
    }
}
