using DAL.AdditionalEntities;
using DAL.Entities;
using HM2.Command;
using HM2.Model.Admin.SubModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HM2.ViewModel.Admin.SubViewModel
{
    public class AddNewAddServiceViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
        private AddNewServiceModel addNewServiceModel;
        private string _nameservice;
        public string NameService
        {
            get
            {
                return _nameservice;
            }
            set
            {
                _nameservice = value;
                RaisePropertyChanged("NameService");
            }
        }

        private string _costService;
        public string CostService
        {
            get
            {
                return _costService;
            }
            set
            {
                if(value.Length == 0)
                {
                    _costService = value;
                    RaisePropertyChanged("CostService");
                    return;
                }
                try
                {
                    int.Parse(value);
                    _costService = value;
                    RaisePropertyChanged("CostService");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        public ICommand ConfirmService { get; set; }
        private OnWindowClose _onWindowClose;
        public AddNewAddServiceViewModel(WindowContext windowContext , OnWindowClose onWindowClose) 
        {
            _onWindowClose = onWindowClose;
            addNewServiceModel = new AddNewServiceModel();
            ConfirmService = new RelayCommand(_ =>
            {
                try
                {
                    if (CostService.Length != 0 && NameService.Length != 0)
                    {
                        addNewServiceModel.ConfirmService(CostService, NameService);
                    }
                    windowContext.GetCurrentWindow().Close();
                    _onWindowClose();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            });
        }
    }
}
