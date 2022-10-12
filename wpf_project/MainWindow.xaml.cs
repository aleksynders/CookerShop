﻿using System;
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

namespace wpf_project
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BaseClass.BD = new ShopEntities();
            FrameClass.MainFrame = fMain;
            FrameClass.MainFrame.Navigate(new MainPage());
        }

        private void autorizate_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new AutoPage());
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}