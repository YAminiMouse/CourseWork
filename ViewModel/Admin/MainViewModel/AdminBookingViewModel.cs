using DAL.AdditionalEntities;
using DAL.Entities;
using HM2.Command;
using HM2.Model.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HM2.ViewModel.Admin
{
    public class AdminBookingViewModel : AdminViewModel
    {
        public AdminBookingViewModel(WindowContext windowContext) 
        {
            using(HotelModel hm = new HotelModel())
            {
                List<Booking> bookings = (from b in hm.Booking select b).ToList();
                foreach(Booking booking in bookings)
                {
                    BookingList.Add(new UserBookingExtension(booking));
                }
            }

            using (HotelModel hm = new HotelModel())
            {
                var list = (from s in hm.Status select s).ToList();
                foreach(var item in list)
                {
                    AllStatusBooking.Add(item);
                }
            }

            FindClientBooking = new RelayCommand(_ =>
            {
                BookingList.Clear();
                if (SelectedStatusBooking != null && SelectedPhoneClientBooking.Length != 0)
                {
                    using (HotelModel hm = new HotelModel())
                    {
                        var list = (from b in hm.Booking where b.IdStatus == SelectedStatusBooking.Id && b.User.number == SelectedPhoneClientBooking select b).ToList();
                        foreach(var item in list)
                        {
                            BookingList.Add(new UserBookingExtension(item));
                        }
                    }

                }
            });

            RefuseBooking = new RelayCommand(_ =>
            {
                if (SelectedStatusBooking != null && SelectedPhoneClientBooking.Length != 0)
                {
                    using (HotelModel hm = new HotelModel())
                    {
                        var booking = (from b in hm.Booking where b.Id == SelectedBooking.Id select b).ToList().First();
                        booking.IdStatus = 3;
                        hm.SaveChanges();
                        BookingList.Clear();
                        var bookings = (from b in hm.Booking where b.IdStatus == SelectedStatusBooking.Id && b.User.number == SelectedPhoneClientBooking select b).ToList();
                        foreach(var item in bookings)
                        {
                            BookingList.Add(new UserBookingExtension(item));
                        }
                    }

                }
            });
        }
    }
}
