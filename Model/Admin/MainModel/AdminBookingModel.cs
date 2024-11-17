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
    }
}
