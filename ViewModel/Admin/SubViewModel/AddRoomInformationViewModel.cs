using DAL.AdditionalEntities;
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
    public class AddRoomInformationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        private AddRoomModel addRoomModel;
        private string floor;
        public string Floor
        {
            get
            {
                return floor;
            }
            set
            {
                if (value.Length == 0)
                {
                    floor = value;
                    RaisePropertyChanged("Floor");
                    return;
                }
                try
                {
                    int.Parse(value);
                    floor = value;
                    RaisePropertyChanged("Floor");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        private string number;
        public string Number
        {
            get
            {
                return number;
            }
            set
            {
                if (value.Length == 0)
                {
                    number = value;
                    RaisePropertyChanged("Number");
                    return;
                }
                try
                {
                    int.Parse(value);
                    number = value;
                    RaisePropertyChanged("Number");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

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

        private TypeRoomExtension selectedType;
        public TypeRoomExtension SelectedType
        {
            get
            {
                return selectedType;
            }
            set
            {
                selectedType = value;
            }
        }

        public ICommand AddNewRoom { get; }
        public AddRoomInformationViewModel(WindowContext windowContext , OnWindowClose onWindowClose) 
        {
            AllTypes = new ObservableCollection<TypeRoomExtension>();
            addRoomModel = new AddRoomModel();

            try
            {
                var types = addRoomModel.GetTypes();
                foreach (var item in types)
                {
                    AllTypes.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            AddNewRoom = new RelayCommand(_ =>
            {
                try
                {
                    if (SelectedType != null && Number.Length != 0 && Floor.Length != 0)
                    {
                        addRoomModel.AddNewRoom(Number , Floor , SelectedType.Id);
                    }
                    windowContext.GetCurrentWindow().Close();
                    onWindowClose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }
    }
}
