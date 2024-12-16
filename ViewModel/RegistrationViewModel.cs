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
                int IsLogin = registrationModel.CreateNewUser(LoginRegistrationUser , PasswordRegistrationUser , FIORegistrationUser , NumberRegistrationUser);

                switch (IsLogin)
                {
                    case 0:
                    {
                            MessageBox.Show("Пользователь с таким логином уже существует.");
                            break;
                    }
                    case 1:
                        {
                            MessageBox.Show("Длина логина менее 8 символов");
                            break;
                        }
                    case 2:
                        {
                            MessageBox.Show("Пароль менее 8 символов");
                            break;
                        }
                    case 3:
                        {
                            MessageBox.Show("Введено некорректное ФИО");
                            break;
                        }
                    case 4:
                        {
                            MessageBox.Show("Введен некорректный номер телефона");
                            break;
                        }
                   
                    case 5:
                        {
                            MessageBox.Show("Регистрация прошла успешно");
                            LoginRegistrationUser = "";
                            PasswordRegistrationUser = "";
                            FIORegistrationUser = "";
                            NumberRegistrationUser = "";
                            break;
                        }
                    case 100:
                        {
                            MessageBox.Show("Заполните все поля для завершения регистрации!");
                            break;
                        }
                }
                
            });
            BackToLoginWindow = new RelayCommand(_ =>
            {
                windowContext.GetCurrentWindow().Close();
            });
        }

    }
}
