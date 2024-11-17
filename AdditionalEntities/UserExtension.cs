using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AdditionalEntities
{
    public class UserExtension
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public string Number { get; set; }
        public double? DiscountSize { get; set; }
        public int IdRole { get; set; }
        public double? MoneySpent { get; set; }
        public int IdDiscount { get; set; }

        public UserExtension(User user) 
        {
            Id = user.Id;
            FIO = user.FIO;
            Number = user.number;
            if (user.Discount != null)
            {
                DiscountSize = user.Discount.size;
            }
            else
            {
                DiscountSize = 1.0;
            }
            IdRole = user.Role.Id;
            if (user.moneySpent != null)
            {
                MoneySpent = (double)user.moneySpent;
            }
            else
            {
                MoneySpent = 0;
            }
            if (user.IdDiscount != null)
            {
                IdDiscount = (int)user.IdDiscount;
            }
        }
    }
}
