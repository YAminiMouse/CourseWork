using DAL.AdditionalEntities;
using HM2.ViewModel.Admin;
using HM2.ViewModel.Admin.MainViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HM2.View.Admin
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private WindowContext windowContext;
        private int _currentSelectedIndex;
        public AdminWindow(WindowContext windowContext)
        {
            InitializeComponent();
            this.windowContext = windowContext;
            windowContext.SetCurrentWindow(this);
            DataContext = new AdminClientsViewModel(windowContext);
            _currentSelectedIndex = 0;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine(((TabControl)sender).SelectedIndex);
            TabControl tabControl = (TabControl)sender;
            if (_currentSelectedIndex == tabControl.SelectedIndex)
            {
                return;
            }
            _currentSelectedIndex = tabControl.SelectedIndex;
            switch (tabControl.SelectedIndex)
            {
                case 0:
                {
                    DataContext = new AdminClientsViewModel(windowContext);
                    break;
                }
                case 1:
                {
                    DataContext = new AdminTypeRoomViewModel(windowContext);
                    break;
                }
                case 2:
                {
                    DataContext = new AdminRoomsViewModel(windowContext);
                    break;
                }
                case 3:
                {
                    DataContext = new AdminBookingViewModel(windowContext);
                    break;
                }
                case 4:
                {
                    DataContext = new AdminAddServiceViewModel(windowContext);
                    break;
                }
                case 5:
                {
                    DataContext = new AdminReportsViewModel(windowContext);
                    break;
                }
                case 6:
                {
                    DataContext = new AdminReportsViewModel(windowContext);
                    break;
                }
            }
        }
    }
}