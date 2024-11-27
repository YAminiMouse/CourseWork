using DAL.AdditionalEntities;
using DAL.Entities;
using HM2.Command;
using HM2.Model.Admin.SubModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HM2.ViewModel.Admin.SubViewModel
{
    public class ChangeServiceInformationViewModel
    {
        private ChangeServiceInformationModel changeServiceInformationModel;
        private string _selectedNameService;
        public string SelectedNameService
        {
            get
            {
                return _selectedNameService;
            }
            set
            {
                _selectedNameService = value;
            }
        }

        private string _selectedCostService;
        public string SelectedCostService
        {
            get
            {
                return _selectedCostService;
            }
            set
            {
                if (value.Length == 0)
                {
                    _selectedCostService = value;
                    return;
                }
                try
                {
                    int.Parse(value);
                    _selectedCostService = value;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }
        public ICommand EditAddService { get; set; }
        private OnWindowClose _onWindowClose;
        public ChangeServiceInformationViewModel(WindowContext windowContext , OnWindowClose onWindowClose)
        {
            AddServiceExtension selectedAddService = null;
            try
            {
                _onWindowClose = onWindowClose;
                changeServiceInformationModel = new ChangeServiceInformationModel();
                selectedAddService = (AddServiceExtension)windowContext.GetResourse("SELECTED_ADD_SERVICE");

                SelectedCostService = selectedAddService.cost.ToString();
                SelectedNameService = selectedAddService.name;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

            EditAddService = new RelayCommand(_ =>
            {
                try
                {
                    changeServiceInformationModel.EditAddService(selectedAddService.Id, SelectedCostService, SelectedNameService);
                    var currentWindow = windowContext.GetCurrentWindow();
                    currentWindow.Close();
                    _onWindowClose();
                }
                catch( Exception ex )
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }
    }
}