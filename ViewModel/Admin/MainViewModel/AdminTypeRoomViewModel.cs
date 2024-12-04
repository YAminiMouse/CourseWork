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
            try
            {
                AllTypes.Clear();
                List<TypeRoomExtension> typesRoom = adminTypeRoomModel.GetAllTypes();
                foreach (TypeRoomExtension typeRoom in typesRoom)
                {
                    AllTypes.Add(typeRoom);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        public AdminTypeRoomViewModel(WindowContext windowContext) 
        {
            adminTypeRoomModel = new AdminTypeRoomModel();

            try
            {
                List<TypeRoomExtension> typesRoom = adminTypeRoomModel.GetAllTypes();
                foreach (TypeRoomExtension typeRoom in typesRoom)
                {
                    AllTypes.Add(typeRoom);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                else
                {
                    System.Windows.MessageBox.Show("Выберите тип комнаты из таблицы для редактирования!");
                }
            });

            DeleteTypeRoom = new RelayCommand(_ =>
            {
                if (SelectedType != null)
                {
                    try
                    {
                        adminTypeRoomModel.DeleteSelectedType(SelectedType.Id);
                        UpdateTypesRoom();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Выберите тип комнаты из таблицы для удаления!");
                }
            });
        }
    }
}
