using DAL.AdditionalEntities;
using DAL.Entities;
using HM2.Command;
using HM2.Model;
using HM2.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HM2.ViewModel
{
    public class PersonalAccountViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        private PersonalAccountModel _personalAccount;

        private string _FIO;
        public string FIO
        {
            get
            {
                return _FIO;
            }
            set
            {
                _FIO = value;
                RaisePropertyChanged("FIO");
            }
        }

        private string _Number;
        public string Number
        {
            get
            {
                return _Number;
            }
            set
            {
                _Number = value; 
                RaisePropertyChanged("Number");
            }
        }

        private string _Size;
        public string Size
        {
            get
            {
                return _Size;
            }
            set
            {
                _Size = value;
                RaisePropertyChanged("Size");
            }
        }

        private ObservableCollection<UserBookingExtension> bookings;
        public ObservableCollection<UserBookingExtension> UserBookings
        {
            get
            {
                return bookings;
            }
            set
            {
                bookings = value;
            }
        }

        public ICommand RefuseBooking { get; }
        public ICommand ShowDetailAddService { get; }

        private UserBookingExtension _selectedBooking;
        public UserBookingExtension SelectedBooking
        {
            get
            {
                return _selectedBooking;
            }
            set
            {
                _selectedBooking = value;
            }
        }
        public PersonalAccountViewModel(WindowContext windowContext)
        {
            UserExtension currentUser = null;
            try
            {
                UserBookings = new ObservableCollection<UserBookingExtension>();
                _personalAccount = new PersonalAccountModel();

                currentUser = (UserExtension)windowContext.GetResourse("CURRENT_USER");
                if (currentUser.DiscountSize == 1)
                {
                    _Size = "Нет скидки";
                }
                else
                {
                    _Size = ((double)(currentUser.DiscountSize * 100)).ToString() + "%";
                }
                _FIO = currentUser.FIO;
                _Number = currentUser.Number;

                var userBookings = _personalAccount.GetUserBookings(currentUser.Id);
                foreach (var item in userBookings)
                {
                    UserBookings.Add(item);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

            RefuseBooking = new RelayCommand(_ =>
            {
                try
                {
                    if (SelectedBooking != null)
                    {
                        _personalAccount.RefuseBooking(_selectedBooking.Id);
                        UserBookings.Clear();
                        var userBookings1 = _personalAccount.GetUserBookings(currentUser.Id);
                        foreach (var item in userBookings1)
                        {
                            UserBookings.Add(item);
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            });

            ShowDetailAddService = new RelayCommand(_ =>
            {
                try
                {
                    if (SelectedBooking != null)
                    {
                        windowContext.SetResource("USER_BOOKING_EXTENSION", SelectedBooking);
                        var detailInformation = new UserAddServiceBookingWindow(windowContext);
                        detailInformation.Show();
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
