using DAL.AdditionalEntities;
using HM2.ViewModel;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            WindowContext windowContext= new WindowContext();
            var windowBuilder = new WindowsBuilder();
            windowBuilder.WindowContext = windowContext;
            windowContext.SetCurrentWindow(this);
            windowContext.SetResource("WINDOW_BUILDER", windowBuilder);
            DataContext = new LoginViewModel(windowContext);
        }
    }
}
