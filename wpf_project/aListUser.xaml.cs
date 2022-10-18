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
    /// Логика взаимодействия для aListUser.xaml
    /// </summary>
    public partial class aListUser : Page
    {
        public aListUser()
        {
            InitializeComponent();
            dgUser.ItemsSource = BaseClass.BD.Users.ToList();
            typeSearch.SelectedIndex = 0;
        }

        private void BackMain_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new AdminPanel());
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (typeSearch.SelectedIndex == 0)
            {
                dgUser.ItemsSource = BaseClass.BD.Users.ToList().Where((x => x.name_user == Search.Text));
            }
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            rbWoman.IsChecked = false;
            rbMan.IsChecked = false;
            Search.Clear();
            typeSearch.SelectedIndex = 0;
            dgUser.ItemsSource = BaseClass.BD.Users.ToList();
        }

        private void rbMan_Checked(object sender, RoutedEventArgs e)
        {
            rbWoman.IsChecked = false;
            dgUser.ItemsSource = BaseClass.BD.Users.ToList().Where((x => x.gender == 1));
        }

        private void rbWoman_Checked(object sender, RoutedEventArgs e)
        {
            rbMan.IsChecked = false;
            dgUser.ItemsSource = BaseClass.BD.Users.ToList().Where((x => x.gender == 2));
        }
    }
}
