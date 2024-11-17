using DAL.AdditionalEntities;
using DAL.Entities;
using HM2.View.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM2.Model
{
    public class PersonalAccountModel
    {
  
        public PersonalAccountModel() 
        {
            
        }

        public ObservableCollection<UserBookingExtension> GetUserBookings(int userId)
        {
            ObservableCollection<UserBookingExtension> userData = new ObservableCollection<UserBookingExtension>();
            using (HotelModel hm = new HotelModel())
            {
                var bookings = (from booking in hm.Booking where booking.User.Id == userId select booking).ToList();
                foreach (var booking in bookings)
                {
                    userData.Add(new UserBookingExtension(booking));
                }
            }
            return userData;
        }

        public void RefuseBooking(int selectedId)
        {
            using (HotelModel hm = new HotelModel())
            {
                List<DAL.Entities.Booking> bookingList = (from booking in hm.Booking where booking.Id == selectedId select booking).ToList();
                if (bookingList.Count != 0)
                {
                    bookingList.First().IdStatus = 3;
                    hm.SaveChanges();
                }
            }
        }
    }
}
