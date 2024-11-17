using DAL.AdditionalEntities;
using DAL.Entities;
using HM2.Command;
using HM2.Model.Admin;
using HM2.View.Admin;
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
    public class AdminClientsViewModel : AdminViewModel
    {
        private AdminClientsModel adminClientsModel;
        private void GetUpdatedList()
        {
            AllUsers.Clear();
            FillAllUsers();
        }

        private void FillAllUsers()
        {
            List<UserExtension> users = adminClientsModel.GetAllUsers();
            foreach (UserExtension user in users)
            {
                AllUsers.Add(user);
            }
        }

        public AdminClientsViewModel(WindowContext windowContext)
        {
            adminClientsModel = new AdminClientsModel();
            FillAllUsers();
            FindClient = new RelayCommand(_ =>
            {
                AllUsers.Clear();
                if (SelectedPhoneClient.Length != 0)
                {
                    List<UserExtension> selectedUsers = adminClientsModel.GetUserByNumber(SelectedPhoneClient);
                    foreach (UserExtension user in selectedUsers)
                    {
                        AllUsers.Add(user);
                    }
                }
                else
                {
                    FillAllUsers();
                }
            });

            ChangeClientInformation = new RelayCommand(_ =>
            {
                Debug.WriteLine("btn edit is pressed");
                if (SelectedUser != null)
                {
                    windowContext.SetResource("SELECTED_USER", SelectedUser);
                    Window EditClientInformation = new ChangeClientInformationWindow(windowContext, GetUpdatedList);
                    EditClientInformation.Show();
                }
            });
        }
    }
}