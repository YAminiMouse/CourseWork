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
using System.Windows;

namespace HM2.ViewModel.Admin
{
    public class AdminBookingViewModel : AdminViewModel
    {
        private AdminBookingModel _adminBookingModel;

        private void UpdateBookingList()
        {
            try
            {
                BookingList.Clear();
                var allBookings = _adminBookingModel.GetAllBookings();
                foreach (var item in allBookings)
                {
                    BookingList.Add(item);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        public AdminBookingViewModel(WindowContext windowContext) 
        {
            _adminBookingModel = new AdminBookingModel();

            UpdateBookingList();

            try
            {
                var statuses = _adminBookingModel.GetAllStatuses();
                foreach (var item in statuses)
                {
                    AllStatusBooking.Add(item);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

            FindClientBooking = new RelayCommand(_ =>
            {
                try
                {
                    BookingList.Clear();
                    if (SelectedStatusBooking != null && SelectedPhoneClientBooking.Length != 0)
                    {
                        var bookings = _adminBookingModel.FindClientBookings(SelectedPhoneClientBooking, SelectedStatusBooking.Id);
                        foreach (var item in bookings)
                        {
                            BookingList.Add(item);
                        }

                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            });

            RefuseBooking = new RelayCommand(_ =>
            {
                try
                {
                    if (SelectedBooking != null)
                    {
                        _adminBookingModel.RefuseBooking(SelectedBooking.Id);
                        UpdateBookingList();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            });

            PopulateClient = new RelayCommand(_ =>
            {
                try
                {
                    if (SelectedBooking != null)
                    {
                        _adminBookingModel.PopulateClientAndRecalculateDiscount(SelectedBooking.Id);
                        UpdateBookingList();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            });

            EvictClient = new RelayCommand(_ =>
            {
                try
                {
                    if (SelectedBooking != null)
                    {
                        _adminBookingModel.EvictClient(SelectedBooking.Id);
                        UpdateBookingList();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            });
        }
    }
}
