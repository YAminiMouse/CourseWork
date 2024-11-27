using DAL.AdditionalEntities;
using DAL.Entities;
using HM2.Command;
using HM2.Model.Admin;
using HM2.Model.Admin.MainModel;
using HM2.View.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HM2.ViewModel.Admin
{
    public class AdminAddServiceViewModel : AdminViewModel
    {   
        private AdminAddServiceModel _adminAddServiceModel;
        private void UpdateAddServices()
        {
            try
            {
                AddServices.Clear();
                var addServices = _adminAddServiceModel.GetAllGetService();
                foreach (var item in addServices)
                {
                    AddServices.Add(new AddServiceExtension(item));
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public AdminAddServiceViewModel(WindowContext windowContext) 
        {
            
            try
            {
                _adminAddServiceModel = new AdminAddServiceModel();
                var addServices = _adminAddServiceModel.GetAllGetService();
                foreach (var item in addServices)
                {
                    AddServices.Add(new AddServiceExtension(item));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            


            AddNewService = new RelayCommand(_ =>
            {
                Debug.WriteLine("btn Add is pressed");
                AddNewServiceWindow addTypeRoomWindow = new AddNewServiceWindow(windowContext, UpdateAddServices);
                addTypeRoomWindow.Show();
            });

            EditAddService = new RelayCommand(_ =>
            {
                Debug.WriteLine("btn Edit is pressed");
                if (SelectedAddservice != null)
                {
                    windowContext.SetResource("SELECTED_ADD_SERVICE", SelectedAddservice);
                    ChangeServiceInformationWindow addTypeRoomWindow = new ChangeServiceInformationWindow(windowContext, UpdateAddServices);
                    addTypeRoomWindow.Show();
                }
            });

            DeleteAddService = new RelayCommand(_ =>
            {
                try
                {
                    if (SelectedAddservice != null)
                    {
                        _adminAddServiceModel.DeleteSelectedService(SelectedAddservice.Id);
                        UpdateAddServices();
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
