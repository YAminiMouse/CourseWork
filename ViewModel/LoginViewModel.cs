using DAL.AdditionalEntities;
using DAL.Entities;

using HM2.Command;
using HM2.Model;
using HM2.View;
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

namespace HM2.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _loginUser;
        private string _passwordUser;
        private readonly LoginUserModel _userModel;

        public string LoginUser
        {
            get
            {
                return _loginUser;
            }
            set
            {
                _loginUser = value;
                RaisePropertyChanged("LoginUser");
            }
            
        }

        public string PasswordUser
        {
            get
            {
                return _passwordUser;
            }
            set
            {
                _passwordUser = value;
                RaisePropertyChanged("PasswordUser");
            }
        }

        private void RaisePropertyChanged([CallerMemberName] string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        public ICommand NavigateToMainWindow { get; } 
        public LoginViewModel(WindowContext windowContext)
        {
            _userModel = new LoginUserModel();

            NavigateToMainWindow = new RelayCommand(_ =>
            {
                try
                {
                    UserExtension user = _userModel.GetAuthenticatedUser(LoginUser, PasswordUser);
                    windowContext.SetResource("CURRENT_USER", user);
                    var currentWindow = windowContext.GetCurrentWindow(); // окно LoginWindow
                    if (user != null)
                    {
                        var windowBuilder = (WindowsBuilder)windowContext.GetResourse("WINDOW_BUILDER");
                        Window mainWindow = (Window)windowBuilder.Build(_userModel.GetWindowId(user));
                        mainWindow.Show();
                        currentWindow.Close();
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