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
                try
                {
                    _selectedAddService = value;
                    if (_selectedAddService != null)
                    {
                        Cost = _selectedAddService.cost;
                    }
                    RaisePropertyChanged(nameof(SelectedAddService));
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
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
                RaisePropertyChanged("Count");
            }
        }

        public AddServiceViewModel(WindowContext windowContext , ReturnEnteringData callBack)
        {
            try
            {
                AddServices = new ObservableCollection<AddService>();
                addServicesModel = new AddServiceModel();
                List<AddService> addServices1 = addServicesModel.GetAddServices();
                foreach (AddService addService in addServices1)
                {
                    AddServices.Add(addService);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

            ConfirmCommand = new RelayCommand(_ =>
            {
                try
                {
                    callBack(_selectedAddService, Count);
                    windowContext.GetCurrentWindow().Close();
                    //window.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            });
        }
    }
}
