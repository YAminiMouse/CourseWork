using DAL.AdditionalEntities;
using HM2.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

namespace HM2.View
{
    /// <summary>
    /// Логика взаимодействия для ShowAddServiceWindow.xaml
    /// </summary>
    public partial class ShowAddServiceWindow : Window
    {
        private WindowContext _windowContext;
        public ShowAddServiceWindow(WindowContext windowContext , ReturnEnteringData callBack)
        {
            InitializeComponent();
            _windowContext = windowContext;
            windowContext.SetSubWindow(this);
            _windowContext.SetCurrentWindow(this);
            DataContext = new AddServiceViewModel(windowContext , callBack);
        }

        public void OnWindowClosed(object sender, CancelEventArgs e)
        {
            _windowContext.DropSubWindow();
        }
    }
}
