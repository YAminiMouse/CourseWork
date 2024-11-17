using DAL.AdditionalEntities;
using DAL.Entities;
using HM2.Command;
using HM2.Model;
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
    public class AddServiceViewModel : INotifyPropertyChanged
    {
        private AddServiceModel addServicesModel;
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        public ICommand ConfirmCommand { get; }

        private ObservableCollection<AddService> addServices;
        private AddService _selectedAddService;
        
        
        public ObservableCollection<AddService> AddServices
        {
            get { return addServices; }
            set { addServices = value; RaisePropertyChanged("AddServices"); }
        }

        public AddService SelectedAddService
        {
            get
            {
                return _selectedAddService;
            }
            set
            {
                _selectedAddService = value;
                if (_selectedAddService != null)
                {
                    Cost = _selectedAddService.cost;
                }
                Debug.WriteLine(_selectedAddService);
                RaisePropertyChanged(nameof(SelectedAddService));
            }
        }

        private int _Cost;
        public int Cost
        {
            get
            {
                return _Cost;
            }
            set
            {
                _Cost = value;
                RaisePropertyChanged("Cost");
            }
        }

        private int _Count;
        public int Count
        {
            get
            {
                return _Count;
            }
            set
            {
                _Count = value;
                Debug.WriteLine(_Count);
                RaisePropertyChanged("Count");
            }
        }

        public AddServiceViewModel(Window window , ReturnEnteringData callBack)
        {
            AddServices = new ObservableCollection<AddService>();
            addServicesModel = new AddServiceModel();
            List<AddService> addServices1 = addServicesModel.GetAddServices();
            foreach (AddService addService in addServices1)
            {
                AddServices.Add(addService);
            }
            //addServicesModel.GetAddServices((List<AddService> addServices) =>
            //{
            //    foreach (AddService addService in addServices)
            //    {
            //        AddServices.Add(addService);
            //    }
            //});

            ConfirmCommand = new RelayCommand(_ =>
            {
                callBack(_selectedAddService, Count);
                window.Close();
            });
        }
    }
}
