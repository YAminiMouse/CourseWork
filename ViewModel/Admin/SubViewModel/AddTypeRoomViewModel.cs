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
using System.Windows.Input;

namespace HM2.ViewModel.Admin
{
    public class AddTypeRoomViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
        private AddTypeRoomModel addTypeRoomModel;
        private ObservableCollection<ComfortExtension> comforts;
        public ObservableCollection<ComfortExtension> AllComforts
        {
            get
            {
                return comforts;
            }
            set
            {
                comforts = value;
                RaisePropertyChanged("AllComforts");
            }
        }

        private ComfortExtension _selectedComfort;
        public ComfortExtension SelectedComfort
        {
            get
            {
                return _selectedComfort;
            }
            set
            {
                _selectedComfort = value;
            }
        }

        private ObservableCollection<CapacityExtension> capacities;
        public ObservableCollection<CapacityExtension> AllCapacities
        {
            get
            {
                return capacities;
            }
            set
            {
                capacities = value;
                RaisePropertyChanged("AllCapacities");
            }
        }

        private CapacityExtension _selectedCapacity;
        public CapacityExtension SelectedCapacity
        {
            get
            {
                return _selectedCapacity;
            }
            set
            {
                _selectedCapacity = value;
            }
        }
        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                RaisePropertyChanged("Description");
            }
        }

        private string cost;
        public string Cost
        {
            get
            {
                return cost;
            }
            set
            {
                if (value.Length == 0)
                {
                    cost = value;
                    RaisePropertyChanged("Cost");
                    return;
                }
                try
                {
                    int.Parse(value);
                    cost = value;
                    RaisePropertyChanged("Cost");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        public ICommand AddNewTypeRoom { get; }
        public AddTypeRoomViewModel(WindowContext windowContext , OnWindowClose onWindowClose) 
        {
            AllCapacities = new ObservableCollection<CapacityExtension>();
            AllComforts = new ObservableCollection<ComfortExtension>();
            addTypeRoomModel = new AddTypeRoomModel();

            var capacities = addTypeRoomModel.GetAllCapacities();
            foreach (var item in capacities)
            {
               AllCapacities.Add(item);
            }

            var comforts = addTypeRoomModel.GetAllComforts();
            foreach (var item in comforts)
            {
                AllComforts.Add(item);
            }

            AddNewTypeRoom = new RelayCommand(_ =>
            {
                Debug.WriteLine("btn Add is pressed");
                if (SelectedCapacity != null && SelectedComfort != null && Cost.Length != 0)
                {
                    addTypeRoomModel.AddNewTypeRoom(Cost , SelectedComfort.Id , SelectedCapacity.Id , SelectedCapacity.name , SelectedComfort.name , Description);
                }
                windowContext.GetCurrentWindow().Close();
                onWindowClose();
            });
        }
    }
}
