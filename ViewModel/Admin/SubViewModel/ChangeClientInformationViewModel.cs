using DAL.AdditionalEntities;
using DAL.Entities;
using HM2.Command;
using HM2.Model.Admin;
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

namespace HM2.ViewModel.Admin
{
    public class ChangeClientInformationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        private ChangeClientInformationModel changeClientInformationModel;
        private string _fioUser;
        public string FIOUser
        {
            get
            {
                return _fioUser;
            }
            set
            {
                _fioUser = value;
                RaisePropertyChanged("FIOUser");
            }
        }

        private double _moneySpentUser;
        public double MoneySpentUser
        {
            get
            {
                return _moneySpentUser;
            }
            set
            {
                _moneySpentUser = value;
                RaisePropertyChanged("MoneySpentUser");
            }
        }

        private string _numberUser;
        public string NumberUser
        {
            get
            {
                return _numberUser;
            }
            set
            {
                _numberUser = value;
                RaisePropertyChanged("NumberUser");
            }
        }

        private ObservableCollection<DiscountExtension> _discounts;
        public ObservableCollection<DiscountExtension> Discounts
        {
            get
            {
                return _discounts;
            }
            set
            {
                _discounts = value;
                RaisePropertyChanged("Discounts");
            }
        }

        private DiscountExtension _selectedDiscount;
        public DiscountExtension SelectedDiscount
        {
            get
            {
                return _selectedDiscount;
            }
            set
            {
                _selectedDiscount = value;
                RaisePropertyChanged("SelectedDiscount");
            }
        }

        public ICommand ChangeClientInformation { get; }

        private DiscountExtension _previousDiscount;
        private OnWindowClose _onWindowClose;
        public ChangeClientInformationViewModel(WindowContext windowContext, OnWindowClose onWindowClose)
        {
            _onWindowClose = onWindowClose;
            changeClientInformationModel = new ChangeClientInformationModel();
            Discounts = new ObservableCollection<DiscountExtension>();

            var selectedUser = (UserExtension)windowContext.GetResourse("SELECTED_USER");
            FIOUser = selectedUser.FIO;
            MoneySpentUser = (double)selectedUser.MoneySpent;
            NumberUser = selectedUser.Number;

            List<DiscountExtension> discounts = changeClientInformationModel.GetAllDiscounts();
            foreach(var d in discounts)
            {
                Discounts.Add(d);
            }

            var userDiscount = changeClientInformationModel.GetUserDiscount(selectedUser.IdDiscount, discounts);

            _previousDiscount = userDiscount;
            SelectedDiscount = userDiscount;

            ChangeClientInformation = new RelayCommand(_ =>
            {
                if (SelectedDiscount != _previousDiscount)
                {
                    changeClientInformationModel.ChangeClientDiscount(selectedUser, _selectedDiscount);
                }
                var currentWindow = windowContext.GetCurrentWindow();
                currentWindow.Close();
                _onWindowClose();
            });
        }
    }
}