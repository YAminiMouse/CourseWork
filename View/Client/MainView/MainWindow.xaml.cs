﻿using DAL.AdditionalEntities;
using DAL.Entities;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HM2.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(WindowContext windowContext)
        {
            InitializeComponent();
            //windowContext.CurrentWindow = this;
            windowContext.SetResource("CURRENT_WINDOW", this);
            DataContext = new MainViewModel(windowContext);
        }
    }
}
