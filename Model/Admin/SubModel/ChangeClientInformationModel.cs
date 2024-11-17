using DAL.AdditionalEntities;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM2.Model.Admin
{
    public class ChangeClientInformationModel
    {
        public ChangeClientInformationModel() { }

        public List<DiscountExtension> GetAllDiscounts()
        {
            List<DiscountExtension> discounts = new List<DiscountExtension>();
            using (HotelModel hm = new HotelModel())
            {
                var discountList = (from d in hm.Discount select d).ToList();
                foreach (var discount in discountList)
                {
                    discounts.Add(new DiscountExtension(discount));
                }
            }
            return discounts;
        }

        public void ChangeClientDiscount(UserExtension selectedUser , DiscountExtension selectedDiscount)
        {
            using (HotelModel hm = new HotelModel())
            {
                var discountList = (from u in hm.User where u.Id == selectedUser.Id select u).ToList();
                discountList.First().IdDiscount = selectedDiscount.Id;
                hm.SaveChanges();
            }
            return;
        }

        public DiscountExtension GetUserDiscount(int Id , List<DiscountExtension> discounts)
        {
            var userDiscount = discounts.FirstOrDefault(d =>
            {
                return d.Id == Id;
            });
            return userDiscount;
        }
    }
}
