using DAL.AdditionalEntities;
using DAL.Entities;
using HM2.AdditionalEntities;
using HM2.Command;
using HM2.Model.Admin.SubModel;
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

namespace HM2.ViewModel.Admin.SubViewModel
{
    public class ChangeRoomInformationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
        private ChangeRoomInformationModel changeRoomInformationModel;

        private ObservableCollection<TypeRoomExtension> allTypes;
        public ObservableCollection<TypeRoomExtension> AllTypes
        {
            get
            {
                return allTypes;
            }
            set
            {
                allTypes = value;
                RaisePropertyChanged("AllTypes");
            }
        }

        private TypeRoomExtension _selectedType;
        public TypeRoomExtension SelectedType
        {
            get
            {
                return _selectedType;
            }
            set
            {
                _selectedType = value;
            }
        }

        private string _selectedFloor;
        public string SelectedFloor
        {
            get
            {
                return _selectedFloor;
            }
            set
            {
                if (value.Length == 0)
                {
                    _selectedFloor = value;
                    return;
                }
                try
                {
                    int.Parse(value);
                    _selectedFloor = value;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        private string _selectedNumber;
        public string SelectedNumber
        {
            get
            {
                return _selectedNumber;
            }
            set
            {
                if (value.Length == 0)
                {
                    _selectedNumber = value;
                    return;
                }
                try
                {
                    int.Parse(value);
                    _selectedNumber = value;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }
        public ICommand EditRoom { get; }
        private OnWindowClose _onWindowClose;
        public ChangeRoomInformationViewModel(WindowContext windowContext , OnWindowClose onWindowClose)
        {
            RoomExtension selectedRoom = null;
            try
            {
                _onWindowClose = onWindowClose;
                changeRoomInformationModel = new ChangeRoomInformationModel();
                AllTypes = new ObservableCollection<TypeRoomExtension>();
                selectedRoom = (RoomExtension)windowContext.GetResourse("SELECTED_ROOM");
                var typesList = changeRoomInformationModel.GetTypes();
                foreach (var item in typesList)
                {
                    AllTypes.Add(item);
                }
                SelectedFloor = selectedRoom.floor.ToString();
                SelectedNumber = selectedRoom.number.ToString();
                var type = changeRoomInformationModel.GetType(selectedRoom.IdTypeRoom, typesList);
                SelectedType = type;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            EditRoom = new RelayCommand(_ =>
            {
                try
                {
                    changeRoomInformationModel.ChangeInformation(selectedRoom.Id , SelectedType.Id , SelectedNumber , SelectedFloor);
                    var currentWindow = windowContext.GetCurrentWindow();
                    currentWindow.Close();
                    _onWindowClose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }
    }
}
