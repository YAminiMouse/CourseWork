using DAL.AdditionalEntities;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM2.Model.Admin.MainModel
{
    public class AdminBookingModel
    {
        public AdminBookingModel() { }

        public List<UserBookingExtension> GetAllBookings()
        {
            var list = new List<Booking>();
            List<UserBookingExtension> bookings = new List<UserBookingExtension>();
            using(HotelModel hm =  new HotelModel())
            {

                list = (from b in hm.Booking select b).ToList();
                foreach(var item in list)
                {
                    bookings.Add(new UserBookingExtension(item));
                }
            }
            return bookings;

        }

        public List<Status> GetAllStatuses()
        {
            var list = new List<Status>();
            using(HotelModel hm = new HotelModel())
            {

                list = (from s in hm.Status select s).ToList();
            }
            return list;
        }

        public List<UserBookingExtension> FindClientBookings(string number , int IdSatus)
        {
            var list = new List<Booking>();
            List<UserBookingExtension> bookings = new List<UserBookingExtension>();
            using(HotelModel hm = new HotelModel())
            {

                list = (from b in hm.Booking where b.IdStatus == IdSatus && b.User.number == number select b).ToList();
                foreach (var item in list)
                {
                    bookings.Add(new UserBookingExtension(item));
                }
            }
            return bookings;
        }

        public void RefuseBooking(int selectedBookingId)
        {
            //var booking = new Booking();
            using(HotelModel hm = new HotelModel())
            {

                var booking = (from b in hm.Booking where b.Id == selectedBookingId select b).ToList().First();
                booking.IdStatus = 3;
                hm.SaveChanges();
            }
        }

        public void PopulateClientAndRecalculateDiscount(int selectedBookingId)
        {
            using(HotelModel hm = new HotelModel())
            {

                var booking = (from b in hm.Booking where b.Id == selectedBookingId select b).ToList().First();
                booking.IdStatus = 2;
                var user = booking.User;
                if (user.moneySpent == null)
                {
                    user.moneySpent = 0;
                }
                user.moneySpent = user.moneySpent + booking.FinalCost;
                if (user.moneySpent < 10000)
                {
                    user.IdDiscount = 1;
                }
                else if (user.moneySpent < 20000)
                {
                    user.IdDiscount = 2;
                }
                else if (user.moneySpent < 30000)
                {
                    user.IdDiscount = 3;
                }
                else 
                {
                    user.IdDiscount = 4;
                }
                hm.SaveChanges();
            }
        }


        public void EvictClient(int selectedBookingId)
        {
            using (HotelModel hm = new HotelModel())
            {
                throw new Exception("блаблаблаблабла");
                var booking = (from b in hm.Booking where b.Id == selectedBookingId select b).ToList().First();
                booking.IdStatus = 4;
                hm.SaveChanges();
            }
        }
    }
}
