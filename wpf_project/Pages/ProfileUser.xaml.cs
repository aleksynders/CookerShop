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
    /// Логика взаимодействия для ProfileUser.xaml
    /// </summary>
    /// 
    public partial class ProfileUser : Page
    {
        Users searchUser = BaseClass.BD.Users.FirstOrDefault(x => x.login == FrameClass.loginAutorizate);
        public ProfileUser()
        {
            InitializeComponent();
            LoginUser.Text = searchUser.login;
            IDUser.Text = searchUser.ID.ToString();
            NameUser.Text = "Имя: " + searchUser.name_user;
            SurnameUser.Text = "Фамилия: " + searchUser.surname_user;
            PhoneUser.Text = "Номер телефона: " + searchUser.phone;
            string Privilage = " ";
            var tempPrivilage = searchUser.privilege;
            if (tempPrivilage == null)
            {
                Privilage = "Отсутствует";
                PrivilageUser.Foreground = Brushes.Black;
            }
            else
            {
                Privilage privilageForUser = BaseClass.BD.Privilage.FirstOrDefault(x => x.privilage1== tempPrivilage);
                Privilage = privilageForUser.name_privilage;
            }
            PrivilageUser.Text = Privilage;
        }

        private void BackMain_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new mainPageUsers());
        }

        private void ResetInfo_Click(object sender, RoutedEventArgs e)
        {
            WindowChangeInfo windowChangeInfo = new WindowChangeInfo(searchUser);
            windowChangeInfo.ShowDialog();
            FrameClass.MainFrame.Navigate(new ProfileUser());
        }

        private void ResetPass_Click(object sender, RoutedEventArgs e)
        {
            ChangePass changePass = new ChangePass();
            changePass.ShowDialog();
            FrameClass.MainFrame.Navigate(new ProfileUser());
        }
    }
}
