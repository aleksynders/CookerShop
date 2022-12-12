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

namespace wpf_project
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void autorizate_Click(object sender, RoutedEventArgs e) // Login: admin Password: admin
        {
            FrameClass.MainFrame.Navigate(new AutoPage());
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new RegPage());
        }

        private void ads_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new advertising());
        }
    }
}
