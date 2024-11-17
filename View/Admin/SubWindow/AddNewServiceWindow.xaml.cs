﻿using DAL.AdditionalEntities;
using HM2.ViewModel;
using HM2.ViewModel.Admin.SubViewModel;
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

namespace HM2.View.Admin
{
    /// <summary>
    /// Логика взаимодействия для AddNewServiceWindow.xaml
    /// </summary>
    public partial class AddNewServiceWindow : Window
    {
        private WindowContext _windowContext;
        public AddNewServiceWindow(WindowContext windowContext , OnWindowClose onWindowClose)
        {
            InitializeComponent();
            _windowContext = windowContext;
            _windowContext = windowContext;
            windowContext.SetSubWindow(this);
            DataContext = new AddNewAddServiceViewModel(windowContext, onWindowClose);
        }

        public void OnWindowClosed(object sender, CancelEventArgs e)
        {
            _windowContext.DropSubWindow();
        }
    }
}
