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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Windows;
using System.Windows.Input;

namespace HM2.ViewModel
{
    public class BookingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        private string _totalAmountSum;
        private WindowContext _windowContext;
        private BookingRoomsModel roomsModel;

        public string TotalAmountSum 
        { 
            get 
            { 
                return _totalAmountSum;
            } 
            set 
            {  _totalAmountSum = value; 
                RaisePropertyChanged("TotalAmountSum"); 
            } 
        }
        private DateTime _startDate;
        public DateTime StartDate 
        {   get 
            { 
                return _startDate;
            } 
            set 
            {   _startDate = value;
                if (_startDate != null)
                {
                    //booking.ArrivalDate = _startDate;
                    roomsModel.SetArrivalDate(_startDate);
                }
                RaisePropertyChanged(); 
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
                if (_endDate != null)
                {
                    //booking.DepatureDate = _endDate;
                    roomsModel.SetDepatureDate(_endDate);
                }
                RaisePropertyChanged(); 
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

                if (_selectedRoom != null)
                {
                    //booking.IdRoom = _selectedRoom.Id;
                    roomsModel.SetIdRoom(_selectedRoom.Id);
                    recalculateTotalSum();
                }
            }
        }

        private void recalculateTotalSum()
        {
            var currentUser = (UserExtension)_windowContext.GetResourse("CURRENT_USER");
            double totalAmount = roomsModel.recalculateTotalSum(StartDate , EndDate , _selectedRoom ,EnterAddServices , currentUser.DiscountSize);
            roomsModel.SetFinalCost(totalAmount);
            TotalAmountSum = totalAmount.ToString();
        }

        private ObservableCollection<RoomExtension> rooms;
        public ObservableCollection<RoomExtension> Rooms
        {
            get { return rooms; }
            set { rooms = value; RaisePropertyChanged("Rooms"); }
        }

        private ObservableCollection<TypeRoomExtension> types;

        private TypeRoomExtension _selectedType;
        public ObservableCollection<TypeRoomExtension> TypesRoom
        {
            get { return  types; }
            set { types = value; RaisePropertyChanged("TypesRoom"); }
        }

        public TypeRoomExtension SelectedType
        {
            get
            {
                return _selectedType;
            }
            set
            {
                _selectedType = value;
                RaisePropertyChanged("SelectedType");
                Debug.WriteLine(_selectedType.name);
            }
        }

        private StringServiceExtension _selectedAddService;
        public StringServiceExtension SelectedAddService
        {
            get
            {
                return _selectedAddService;
            }
            set
            {
                _selectedAddService = value;
                Debug.WriteLine(_selectedAddService);
            }
        }

        public ICommand CommandDeleteSelectedService { get; }
        public ICommand CommandConfirmBooking { get; }
        public ICommand CommandFindRooms { get; }
        public ICommand CommandSwowListServices { get; }


        private ObservableCollection<StringServiceExtension> serviceExtensions;
        public ObservableCollection<StringServiceExtension> EnterAddServices
        {
            get { return serviceExtensions; }
            set { serviceExtensions = value; RaisePropertyChanged("EnterAddServices"); }
        }

        public BookingViewModel(WindowContext windowContext , OnWindowClose onWindowClose) 
        {
            _windowContext = windowContext;
            _startDate = DateTime.Now;
            _endDate = DateTime.Now;

            types = new ObservableCollection<TypeRoomExtension>();
            serviceExtensions = new ObservableCollection<StringServiceExtension>();
            rooms = new ObservableCollection<RoomExtension>();
            roomsModel = new BookingRoomsModel();

            List <TypeRoomExtension> typesRoom = roomsModel.GetTypes();
            foreach (TypeRoomExtension roomType in typesRoom)
            {
                TypesRoom.Add(roomType);
            }

            CommandDeleteSelectedService = new RelayCommand ( _ =>
            {
                if (SelectedAddService != null)
                {
                    serviceExtensions.Remove(SelectedAddService);
                    recalculateTotalSum();
                }
            });

            CommandFindRooms = new RelayCommand(_ =>
            {
                Rooms.Clear();
                List<RoomExtension> findRooms = roomsModel.GetRooms(_selectedType, StartDate, EndDate);
                foreach (RoomExtension findRoom in findRooms)
                {
                    Rooms.Add(findRoom);
                }
            });

            CommandSwowListServices = new RelayCommand(_ =>
            {
                ShowAddServiceWindow window = new ShowAddServiceWindow(windowContext , (AddService service, int count) =>
                {
                    EnterAddServices.Add(roomsModel.GetAddService(service, count));
                    recalculateTotalSum();
                });
                window.Show();
            });

            CommandConfirmBooking = new RelayCommand(_ =>
            {
                if (_selectedRoom != null && _selectedType != null && _startDate < _endDate) 
                {
                    var currentUser = (UserExtension)_windowContext.GetResourse("CURRENT_USER");
                    roomsModel.CreateBooking(serviceExtensions , 1 , currentUser.Id , _startDate , _endDate , _selectedRoom.Id , double.Parse(TotalAmountSum));
                    onWindowClose();
                }
                else
                {
                    MessageBox.Show("Проверьте корректность введенных данных");
                }
            });
        }
    }
}