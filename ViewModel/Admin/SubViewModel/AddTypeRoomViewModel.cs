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
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

        private byte[] _imageBytes;
        public byte[] ImageBytes
        {
            get
            {
                return _imageBytes;
            }
            set
            {
                _imageBytes = value;
                RaisePropertyChanged(nameof(ImageBytes));
            }
        }

        private ImageSource _picture;
        public ImageSource Picture
        {
            get
            {
                return _picture;
            }
            set
            {
                _picture = value;
                RaisePropertyChanged(nameof(Picture));
            }
        }


        public ICommand AddNewTypeRoom { get; }
        public ICommand LoadPicture { get; }
        public AddTypeRoomViewModel(WindowContext windowContext , OnWindowClose onWindowClose) 
        {
            AllCapacities = new ObservableCollection<CapacityExtension>();
            AllComforts = new ObservableCollection<ComfortExtension>();
            addTypeRoomModel = new AddTypeRoomModel();

            try
            {
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
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

            LoadPicture = new RelayCommand(_ =>
            {
                var result = addTypeRoomModel.UpdateImageSource();
                Picture = result.Item1;
                ImageBytes = result.Item2;  
            });


            AddNewTypeRoom = new RelayCommand(_ =>
            {
                try
                {
                    if (SelectedCapacity != null && SelectedComfort != null && Cost.Length != 0 && Picture != null)
                    {
                        addTypeRoomModel.AddNewTypeRoom(Cost, SelectedComfort.Id, SelectedCapacity.Id, SelectedCapacity.name, SelectedComfort.name, Description , ImageBytes);
                    }
                    windowContext.GetCurrentWindow().Close();
                    onWindowClose();
                }
                catch(Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
                
            });
        }
    }
}
