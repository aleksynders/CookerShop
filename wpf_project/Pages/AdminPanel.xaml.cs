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
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Page
    {
        public AdminPanel()
        {
            InitializeComponent();
            Users searchUser = BaseClass.BD.Users.FirstOrDefault(x => x.login == FrameClass.loginAutorizate);
            LoginUserAutorizate.Text = searchUser.login;
            NameUserAutorizate.Text = searchUser.name_user + " " + searchUser.surname_user;
            DateRegUserAutorizate.Text = "Дата регистрации: " + (searchUser.date_reg).ToString("d") + "г.";
        }

        private void ListUser_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new aListUser());
        }

        private void ExitUser_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.loginAutorizate = null;
            FrameClass.MainFrame.Navigate(new MainPage());
        }

        private void ListProduct_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new aListProduct());
        }
    }
}
