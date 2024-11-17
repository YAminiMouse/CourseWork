using DAL.AdditionalEntities;
using DAL.Entities;
using HM2.Command;
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

namespace HM2.ViewModel.Admin
{
    public abstract class AdminViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        private ObservableCollection<UserExtension> _allUsers;
        public ObservableCollection<UserExtension> AllUsers
        {
            get { return _allUsers; }
            set
            {
                _allUsers = value;
                RaisePropertyChanged("AllUsers");
            }
        }

        private UserExtension _selectedUser;
        public UserExtension SelectedUser
        {
            get
            {
                return _selectedUser;
            }
            set
            {
                _selectedUser = value;
            }
        }

        private ObservableCollection<TypeRoomExtension> _allTypes;
        public ObservableCollection<TypeRoomExtension> AllTypes
        {
            get
            {
                return _allTypes;
            }
            set
            {
                _allTypes = value;
                RaisePropertyChanged("AllTypes");
            }
        }

        private TypeRoomExtension _selectedType;
        public TypeRoomExtension SelectedType
        {
            get
            {
                return _selectedType;
            }
            set
            {
                _selectedType = value;
            }
        }

        private ObservableCollection<UserBookingExtension> _bookingList;
        public ObservableCollection<UserBookingExtension> BookingList
        {
            get
            {
                return _bookingList;
            }
            set
            {
                _bookingList = value;
                RaisePropertyChanged("BookingList");
            }
        }

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

        private ObservableCollection<AddServiceExtension> _addServices;
        public ObservableCollection<AddServiceExtension> AddServices
        {
            get
            {
                return _addServices;
            }
            set
            {
                _addServices = value;
                RaisePropertyChanged("AddServices");
            }
        }

        private AddServiceExtension _selectedAddService;
        public AddServiceExtension SelectedAddservice
        {
            get
            {
                return _selectedAddService;
            }
            set
            {
                _selectedAddService = value;
            }
        }

        private string _selectedPhoneClient;
        public string SelectedPhoneClient
        {
            get
            {
                return _selectedPhoneClient;
            }
            set
            {
                _selectedPhoneClient = value;
                RaisePropertyChanged("SelectedPhoneClient");
            }
        }

        private string _selectedPhoneClientBooking;
        public string SelectedPhoneClientBooking
        {
            get
            {
                return _selectedPhoneClientBooking;
            }
            set
            {
                _selectedPhoneClientBooking = value;            
            }
        }

        private ObservableCollection<Status> _allStatusBooking;
        public ObservableCollection<Status> AllStatusBooking
        {
            get
            {
                return _allStatusBooking;
            }
            set 
            { 
                _allStatusBooking = value;
                RaisePropertyChanged("AllStatusBooking");
            }
        }

        private Status _selectedStatusBooking;
        public Status SelectedStatusBooking
        {
            get
            {
                return _selectedStatusBooking;
            }
            set 
            {
                _selectedStatusBooking = value;
            }
        }

        public ICommand FindClient { get; set; }
        public ICommand ChangeClientInformation { get; set; }
        public ICommand AddNewTypeRoom { get; set; }
        public ICommand EditTypeRoom { get; set; }
        public ICommand FindClientBooking { get; set; }
        public ICommand RefuseBooking { get; set; }
        public ICommand AddNewService { get; set; }
        public ICommand EditAddService { get; set; }
        public AdminViewModel() 
        {
            AllUsers = new ObservableCollection<UserExtension>();
            AllTypes = new ObservableCollection<TypeRoomExtension>();
            BookingList = new ObservableCollection<UserBookingExtension>();
            AddServices = new ObservableCollection<AddServiceExtension>();
            AllStatusBooking = new ObservableCollection<Status>();
        }
    }
}