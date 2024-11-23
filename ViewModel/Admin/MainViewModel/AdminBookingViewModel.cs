using DAL.AdditionalEntities;
using DAL.Entities;
using HM2.Command;
using HM2.Model.Admin;
using HM2.Model.Admin.MainModel;
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
        private AdminBookingModel _adminBookingModel;

        private void UpdateBookingList()
        {
            BookingList.Clear();
            var allBookings = _adminBookingModel.GetAllBookings();
            foreach (var item in allBookings)
            {
                BookingList.Add(item);
            }
        }

        public AdminBookingViewModel(WindowContext windowContext) 
        {
            _adminBookingModel = new AdminBookingModel();

            UpdateBookingList();

            var statuses = _adminBookingModel.GetAllStatuses();
            foreach(var item in statuses)
            {
                AllStatusBooking.Add(item);
            }

            FindClientBooking = new RelayCommand(_ =>
            {
                BookingList.Clear();
                if (SelectedStatusBooking != null && SelectedPhoneClientBooking.Length != 0)
                {
                    var bookings = _adminBookingModel.FindClientBookings(SelectedPhoneClientBooking, SelectedStatusBooking.Id);
                    foreach(var item in bookings)
                    {
                        BookingList.Add(item);
                    }

                }
            });

            RefuseBooking = new RelayCommand(_ =>
            {
                if (SelectedBooking != null)
                {
                    _adminBookingModel.RefuseBooking(SelectedBooking.Id);
                    BookingList.Clear();
                    var bookings = _adminBookingModel.FindClientBookings(SelectedPhoneClientBooking,SelectedStatusBooking.Id);
                    foreach(var item in bookings)
                    {
                        BookingList.Add(item);
                    }
                }
            });

            PopulateClient = new RelayCommand(_ =>
            {
                if (SelectedBooking != null)
                {
                    _adminBookingModel.PopulateClientAndRecalculateDiscount(SelectedBooking.Id);
                    UpdateBookingList();
                }
            });

            EvictClient = new RelayCommand(_ =>
            {
                if (SelectedBooking != null)
                {
                    _adminBookingModel.EvictClient(SelectedBooking.Id);
                    UpdateBookingList();
                }
            });
        }
    }
}
