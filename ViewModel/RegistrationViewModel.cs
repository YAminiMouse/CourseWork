using DAL.AdditionalEntities;
using HM2.Command;
using HM2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HM2.ViewModel
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
        private RegistrationModel registrationModel;
        private string loginRegistrationUser;
        public string LoginRegistrationUser
        {
            get
            {
                return loginRegistrationUser;
            }
            set
            {
                loginRegistrationUser = value;
                RaisePropertyChanged("LoginRegistrationUser");
            }
        }

        private string passwordRegistrationUser;
        public string PasswordRegistrationUser
        {
            get
            {
                return passwordRegistrationUser;
            }
            set
            {
                passwordRegistrationUser = value;
                RaisePropertyChanged("PasswordRegistrationUser");
            }
        }

        private string fIORegistrationUser;
        public string FIORegistrationUser
        {
            get
            {
                return fIORegistrationUser;
            }
            set
            {
                fIORegistrationUser = value;
                RaisePropertyChanged("FIORegistrationUser");
            }
        }

        private string numberRegistrationUser;
        public string NumberRegistrationUser
        {
            get
            {
                return numberRegistrationUser;
            }
            set
            {
                numberRegistrationUser = value;
                RaisePropertyChanged("NumberRegistrationUser");
            }
        }

        public ICommand AddNewUser { get; }
        public ICommand BackToLoginWindow { get; }
        public RegistrationViewModel(WindowContext windowContext) 
        {
            registrationModel = new RegistrationModel();
            AddNewUser = new RelayCommand(_ =>
            {
                bool IsLogin = registrationModel.CreateNewUser(LoginRegistrationUser , PasswordRegistrationUser , FIORegistrationUser , NumberRegistrationUser);
                if (!IsLogin)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует.");
                }
                else
                {
                    MessageBox.Show("Регистрация прошла успешно");
                    LoginRegistrationUser = "";
                    PasswordRegistrationUser = "";
                    FIORegistrationUser = "";
                    NumberRegistrationUser = "";
                }
                
            });
            BackToLoginWindow = new RelayCommand(_ =>
            {
                windowContext.GetCurrentWindow().Close();
            });
        }

    }
}
