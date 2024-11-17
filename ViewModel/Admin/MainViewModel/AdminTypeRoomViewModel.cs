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
    public class AdminTypeRoomViewModel : AdminViewModel
    {
        private AdminTypeRoomModel adminTypeRoomModel;

        private void UpdateTypesRoom()
        {
            AllTypes.Clear();
            List<TypeRoomExtension> typesRoom = adminTypeRoomModel.GetAllTypes();
            foreach (TypeRoomExtension typeRoom in typesRoom)
            {
                AllTypes.Add(typeRoom);
            }
        }
        
        public AdminTypeRoomViewModel(WindowContext windowContext) 
        {
            adminTypeRoomModel = new AdminTypeRoomModel();


            List<TypeRoomExtension> typesRoom = adminTypeRoomModel.GetAllTypes();
            foreach (TypeRoomExtension typeRoom in typesRoom)
            {
                AllTypes.Add(typeRoom);
            }


            AddNewTypeRoom = new RelayCommand(_ =>
            {
                AddTypeRoomWindow addTypeRoomWindow = new AddTypeRoomWindow(windowContext , UpdateTypesRoom);
                addTypeRoomWindow.Show();
            });

            EditTypeRoom = new RelayCommand(_ =>
            {
                if (SelectedType != null)
                {
                    windowContext.SetResource("SELECTED_TYPE" , SelectedType);
                    ChangeTypeRoomInformationWindow changeTypeRoomInformationWindow = new ChangeTypeRoomInformationWindow(windowContext, UpdateTypesRoom);
                    changeTypeRoomInformationWindow.Show();
                }
            });
        }
    }
}
