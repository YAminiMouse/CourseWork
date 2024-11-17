using DAL.AdditionalEntities;
using DAL.Entities;
using HM2.Command;
using HM2.Model.Admin;
using HM2.View.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HM2.ViewModel.Admin
{
    public class AdminAddServiceViewModel : AdminViewModel
    {   
        private void UpdateAddServices()
        {
            AddServices.Clear();
            using (HotelModel hm = new HotelModel())
            {
                List<AddService> list = (from a in hm.AddService select a).ToList();
                foreach(var item in list)
                {
                    AddServices.Add(new AddServiceExtension(item));
                }
            }
        }
        public AdminAddServiceViewModel(WindowContext windowContext) 
        {
            using(HotelModel hm = new HotelModel())
            {
                List<AddService> list = (from addService in hm.AddService select addService).ToList();
                foreach(AddService addService in list)
                {
                    AddServices.Add(new AddServiceExtension(addService));
                }
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
        }
    }
}
