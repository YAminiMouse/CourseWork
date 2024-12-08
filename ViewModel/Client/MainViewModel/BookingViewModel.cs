using DAL.AdditionalEntities;
using DAL.Entities;
using HM2.Command;
using HM2.Model;
using HM2.View;
using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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
            {
                try
                {
                    _startDate = value;
                    if (_startDate != null)
                    {
                        roomsModel.SetArrivalDate(_startDate);
                    }
                    RaisePropertyChanged();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
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
                try
                {
                    _endDate = value;
                    if (_endDate != null)
                    {
                        roomsModel.SetDepatureDate(_endDate);
                    }
                    RaisePropertyChanged();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
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
                try
                {
                    _selectedRoom = value;
                    if (_selectedRoom != null)
                    {
                        roomsModel.SetIdRoom(_selectedRoom.Id);
                        recalculateTotalSum();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
        }

        private void recalculateTotalSum()
        {
            try
            {
                var currentUser = (UserExtension)_windowContext.GetResourse("CURRENT_USER");
                double totalAmount = roomsModel.recalculateTotalSum(StartDate, EndDate, _selectedRoom, EnterAddServices, currentUser.DiscountSize);
                roomsModel.SetFinalCost(totalAmount);
                TotalAmountSum = totalAmount.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
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
            }
        }

        private BitmapImage _imagePath;
        public BitmapImage ImagePath
        {
            get
            {
                return _imagePath;
            }
            set
            {
                _imagePath = value;
                RaisePropertyChanged("ImagePath");
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
            //ImagePath = new BitmapImage();
            //string path = "C:\\Users\\samoy\\Desktop\\Учеба\\3 курс\\Конструирование ПО\\Курсовая\\Test\\CourseWork\\Photos\\photo1.jpg";
            //byte[] data = File.ReadAllBytes(path);
            //ImagePath.BeginInit();
            //ImagePath.StreamSource = new MemoryStream(data);
            //ImagePath.EndInit();

            try
            {
                _windowContext = windowContext;
                _startDate = DateTime.Now;
                _endDate = DateTime.Now;

                types = new ObservableCollection<TypeRoomExtension>();
                serviceExtensions = new ObservableCollection<StringServiceExtension>();
                rooms = new ObservableCollection<RoomExtension>();
                roomsModel = new BookingRoomsModel();

                List<TypeRoomExtension> typesRoom = roomsModel.GetTypes();
                foreach (TypeRoomExtension roomType in typesRoom)
                {
                    TypesRoom.Add(roomType);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           

            CommandDeleteSelectedService = new RelayCommand ( _ =>
            {
                try
                {
                    if (SelectedAddService != null)
                    {
                        serviceExtensions.Remove(SelectedAddService);
                        recalculateTotalSum();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            });

            CommandFindRooms = new RelayCommand(_ =>
            {
                try
                {
                    Rooms.Clear();
                    List<RoomExtension> findRooms = roomsModel.GetRooms(_selectedType, StartDate, EndDate);
                    foreach (RoomExtension findRoom in findRooms)
                    {
                        //findRoom.ImagePath = ImagePath;
                        Rooms.Add(findRoom);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            });

            CommandSwowListServices = new RelayCommand(_ =>
            {
                try
                {
                    ShowAddServiceWindow window = new ShowAddServiceWindow(windowContext, (AddService service, int count) =>
                    {
                        EnterAddServices.Add(roomsModel.GetAddService(service, count));
                        recalculateTotalSum();
                    });
                    window.Show();
                }
                catch( Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            });

            CommandConfirmBooking = new RelayCommand(_ =>
            {
                try
                {
                    if (_selectedRoom != null && _selectedType != null && _startDate < _endDate)
                    {
                        var currentUser = (UserExtension)_windowContext.GetResourse("CURRENT_USER");
                        roomsModel.CreateBooking(serviceExtensions, 1, currentUser.Id, _startDate, _endDate, _selectedRoom.Id, double.Parse(TotalAmountSum));
                        onWindowClose();
                    }
                    else
                    {
                        MessageBox.Show("Проверьте корректность введенных данных");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            });
        }
    }
}