using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using DAL.AdditionalEntities;
using DAL.Entities;
using HM2.Command;
using HM2.View.Pages;

namespace HM2.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private object _currentPage;
        public object CurrentPage
        {
            get => _currentPage;
            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    OnPropertyChanged(nameof(CurrentPage));
                }
            }
        }

        public ICommand NavigateToBookingCommand { get; }
        public ICommand NavigateToPersonalAccountCommand { get; }
        public MainViewModel(WindowContext windowContext)
        {
            var windowsBuilder = (WindowsBuilder)windowContext.GetResourse("WINDOW_BUILDER");
            NavigateToBookingCommand = new RelayCommand(_ =>  {
                CurrentPage = windowsBuilder.Build("BOOKING_PAGE" , () =>
                {
                    CurrentPage = windowsBuilder.Build("PERSONAL_ACCOUNT_PAGE");
                });
            });
            NavigateToPersonalAccountCommand = new RelayCommand(_ => {
                CurrentPage = windowsBuilder.Build("PERSONAL_ACCOUNT_PAGE");
            });
            CurrentPage = windowsBuilder.Build("PERSONAL_ACCOUNT_PAGE");
        }
    }
}