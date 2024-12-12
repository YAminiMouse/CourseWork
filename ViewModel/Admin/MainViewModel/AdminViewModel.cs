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

        private string _countRooms;
        public string CountRooms
        {
            get
            {
                return _countRooms;
            }
            set 
            { 
                _countRooms = value; 
                RaisePropertyChanged("CountRooms"); 
            }
        }

        private string _countBusyRooms;
        public string CountBusyRooms
        {
            get
            {
                return _countBusyRooms;
            }
            set
            {
                _countBusyRooms = value; 
                RaisePropertyChanged("CountBusyRooms");
            }
        }

        private string _countBookings;
        public string CountBookings
        {
            get
            {
                return _countBookings;
            }
            set
            {
                _countBookings = value;
                RaisePropertyChanged("CountBookings");
            }
        }

        private string _revenueRooms;
        public string RevenueRooms
        {
            get
            {
                return _revenueRooms;
            }
            set
            {
                _revenueRooms = value;
                RaisePropertyChanged("RevenueRooms");
            }
        }

        private string _revenueAddService;
        public string RevenueAddService
        {
            get
            {
                return _revenueAddService;
            }
            set
            {
                _revenueAddService = value;
                RaisePropertyChanged("RevenueAddService");
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
                RaisePropertyChanged("StartDate");
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
                RaisePropertyChanged("EndDate");
            }
        }

        private ObservableCollection<UserBookingExtension> _busyRoomsBooking;
        public ObservableCollection<UserBookingExtension> BusyRoomsBooking
        {
            get
            {
                return _busyRoomsBooking;
            }
            set
            {
                _busyRoomsBooking = value;
                RaisePropertyChanged("BusyRoomsBooking");
            }
        }

        private ObservableCollection<UserBookingExtension> _waitRoomsBooking;
        public ObservableCollection<UserBookingExtension> WaitRoomsBooking
        {
            get
            {
                return _waitRoomsBooking;
            }
            set
            {
                _waitRoomsBooking = value;
                RaisePropertyChanged("WaitRoomsBooking");
            }
        }
        private ObservableCollection<RoomExtension> _allRooms;
        public ObservableCollection<RoomExtension> AllRooms
        {
            get
            {
                return _allRooms;
            }
            set
            {
                _allRooms = value;
                RaisePropertyChanged("AllRooms");
            }
        }
        private RoomExtension _selectedRoom;
        public RoomExtension SelectedRoom
        {
            get
            {
                return _selectedRoom;
            }
            set
            {
                _selectedRoom = value;
            }
        }

        private string service1Name;
        public string Service1Name
        {
            get
            {
                return service1Name;
            }
            set
            {
                service1Name = value;
                RaisePropertyChanged("Service1Name");
            }
        }

        private string service2Name;
        public string Service2Name
        {
            get
            {
                return service2Name;
            }
            set
            {
                service2Name = value;
                RaisePropertyChanged("Service2Name");
            }
        }

        private string service3Name;
        public string Service3Name
        {
            get
            {
                return service3Name;
            }
            set
            {
                service3Name = value;
                RaisePropertyChanged("Service3Name");
            }
        }

        private string service1Earnings;
        public string Service1Earnings
        {
            get
            {
                return service1Earnings;
            }
            set
            {
                service1Earnings = value;
                RaisePropertyChanged("Service1Earnings");
            }
        }

        private string service2Earnings;
        public string Service2Earnings
        {
            get
            {
                return service2Earnings;
            }
            set
            {
                service2Earnings = value;
                RaisePropertyChanged("Service2Earnings");
            }
        }

        private string service3Earnings;
        public string Service3Earnings
        {
            get
            {
                return service3Earnings;
            }
            set
            {
                service3Earnings = value;
                RaisePropertyChanged("Service3Earnings");
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
        public ICommand DeleteAddService { get; set; }
        public ICommand DeleteTypeRoom { get; set; }
        public ICommand PopulateClient { get; set; }
        public ICommand EvictClient { get; set; }
        public ICommand CreateReport { get; set; }
        public ICommand ExportReport { get; set; }
        public ICommand CreateReportCommand { get; set; }
        public ICommand ExportReportCommand { get; set; }
        public ICommand DeleteRoom { get; set; }
        public ICommand EditRoom { get; set; }
        public ICommand AddNewRoom { get; set; }
        public AdminViewModel() 
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            AllUsers = new ObservableCollection<UserExtension>();
            AllTypes = new ObservableCollection<TypeRoomExtension>();
            BookingList = new ObservableCollection<UserBookingExtension>();
            AddServices = new ObservableCollection<AddServiceExtension>();
            AllStatusBooking = new ObservableCollection<Status>();
            BusyRoomsBooking = new ObservableCollection<UserBookingExtension>();
            WaitRoomsBooking = new ObservableCollection<UserBookingExtension>();
            AllRooms = new ObservableCollection<RoomExtension>();
        }
    }
}