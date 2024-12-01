using DAL.AdditionalEntities;
using DAL.Entities;
using HM2.Command;
using HM2.Model.Admin;
using HM2.Model.Admin.MainModel;
using HM2.View.Admin;
using HM2.View.Admin.SubWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HM2.ViewModel.Admin.MainViewModel
{
    public class AdminRoomsViewModel : AdminViewModel
    {
        private AdminRoomsModel adminRoomsModel;

        private void UpdateListRooms()
        {
            AllRooms.Clear();
            var rooms = adminRoomsModel.GetRooms();
            foreach (var item in rooms)
            {
                AllRooms.Add(item);
            }
        }

        public AdminRoomsViewModel(WindowContext windowContext) 
        {
            adminRoomsModel = new AdminRoomsModel();
            UpdateListRooms();

            AddNewRoom = new RelayCommand(_ =>
            {
                AddNewRoom addRoomWindow = new AddNewRoom(windowContext, UpdateListRooms);
                addRoomWindow.Show();
            });

            EditRoom = new RelayCommand(_ =>
            {
                if (SelectedRoom != null)
                {
                    windowContext.SetResource("SELECTED_ROOM", SelectedRoom);
                    ChangeRoomInformationWindow changeRoomInformationWindow = new ChangeRoomInformationWindow(windowContext, UpdateListRooms);
                    changeRoomInformationWindow.Show();
                }
            });

            DeleteRoom = new RelayCommand(_ => {
                if (SelectedRoom != null)
                {
                    try
                    {
                        adminRoomsModel.DeleteSelectedRoom(SelectedRoom.Id);
                        UpdateListRooms();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            });
        }
    }
}
