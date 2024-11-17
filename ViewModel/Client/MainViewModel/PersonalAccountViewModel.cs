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

        private double _Size;
        public double Size
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
            UserBookings = new ObservableCollection<UserBookingExtension>();
            _personalAccount = new PersonalAccountModel();

            var currentUser = (UserExtension)windowContext.GetResourse("CURRENT_USER");

            _Size = (double)(currentUser.DiscountSize * 100);
            _FIO = currentUser.FIO;
            _Number = currentUser.Number;

            var userBookings = _personalAccount.GetUserBookings(currentUser.Id);
            foreach (var item in userBookings)
            {
                UserBookings.Add(item);
                Debug.WriteLine(item.IdUser);
            }

            RefuseBooking = new RelayCommand(_ =>
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
            });

            ShowDetailAddService = new RelayCommand(_ =>
            {
                if (SelectedBooking != null)
                {
                    windowContext.SetResource("USER_BOOKING_EXTENSION", SelectedBooking);
                    var detailInformation = new UserAddServiceBookingWindow(windowContext);
                    detailInformation.Show();
                }
            });
        }
    }
}
