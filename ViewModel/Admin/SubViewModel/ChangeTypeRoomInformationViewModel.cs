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

namespace HM2.ViewModel.Admin
{
    public class ChangeTypeRoomInformationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
        private ChangeTypeRoomInformationModel changeTypeRoomInformationModel;
        private string _selectedCost;
        public string SelectedCost
        {
            get
            {
                return _selectedCost;
            }
            set
            {
                if (value.Length == 0)
                {
                    _selectedCost = value;
                    return;
                }
                try
                {
                    int.Parse(value);
                    _selectedCost = value;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        private ObservableCollection<CapacityExtension> capacities;
        public ObservableCollection<CapacityExtension> Capacities
        {
            get
            {
                return capacities;
            }
            set
            {
                capacities = value;
                RaisePropertyChanged("Capacities");
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

        private ObservableCollection<ComfortExtension> comforts;
        public ObservableCollection<ComfortExtension> Comforts
        {
            get
            {
                return comforts;
            }
            set
            {
                comforts = value;
                RaisePropertyChanged("Comforts");
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
        private string _selectedDescription;
        public string SelectedDescription
        {
            get
            {
                return _selectedDescription;
            }
            set
            {
                _selectedDescription = value;
                RaisePropertyChanged("SelectedDescription");
            }
        }

        //private string _selectedPicture;
        //public string SelectedPicture
        //{
        //    get
        //    {
        //        return _selectedPicture;
        //    }
        //    set
        //    {
        //        _selectedPicture = value;
        //        RaisePropertyChanged("SelectedPicture");
        //    }
        //}

        public ICommand ConfirmChanges { get; }
        public ICommand LoadPicture { get; }
        private OnWindowClose _onWindowClose;
        public ChangeTypeRoomInformationViewModel(WindowContext windowContext, OnWindowClose onWindowClose)
        {
            TypeRoomExtension selectedType = null;
            try
            {
                _onWindowClose = onWindowClose;
                changeTypeRoomInformationModel = new ChangeTypeRoomInformationModel();
                Comforts = new ObservableCollection<ComfortExtension>();
                Capacities = new ObservableCollection<CapacityExtension>();

                selectedType = (TypeRoomExtension)windowContext.GetResourse("SELECTED_TYPE");
                var comfortList = changeTypeRoomInformationModel.GetAllComforts();
                foreach (var item in comfortList)
                {
                    Comforts.Add(item);
                }
                var capacityList = changeTypeRoomInformationModel.GetAllCapacities();
                foreach (var item in capacityList)
                {
                    Capacities.Add(item);
                }
                SelectedCost = selectedType.cost.ToString();
                SelectedDescription = selectedType.description;
                var comfort = changeTypeRoomInformationModel.GetComfort(selectedType.IdComfort, comfortList);
                var capacity = changeTypeRoomInformationModel.GetCapacity(selectedType.IdCapacity, capacityList);
                SelectedCapacity = capacity;
                SelectedComfort = comfort;
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

            ConfirmChanges = new RelayCommand(_ =>
            {
                try
                {
                    changeTypeRoomInformationModel.ChangeInformation(selectedType.Id, SelectedCost, SelectedDescription, SelectedCapacity.Id, SelectedComfort.Id , selectedType.data);
                    var currentWindow = windowContext.GetCurrentWindow();
                    currentWindow.Close();
                    _onWindowClose();
                }
                catch(Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
                
            });

            LoadPicture = new RelayCommand(_ =>
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string path = openFileDialog.FileName;
                        byte[] data = File.ReadAllBytes(path);
                        selectedType.data = data;
                    }
                }
            });


        }
    }
}
