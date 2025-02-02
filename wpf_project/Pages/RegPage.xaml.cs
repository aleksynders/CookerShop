﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }

        private void bRegistration_Click(object sender, RoutedEventArgs e)
        {
            if (tbName.Text == "") MessageBox.Show("Заполните поле имени!", "", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (tbSurname.Text == "") MessageBox.Show("Заполните поле фамилии!", "", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (tbNumber.Text == "") MessageBox.Show("Заполните поле номера телефона!", "", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (tbLogin.Text == "") MessageBox.Show("Заполните поле логина!", "", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (pbPassword.Password == "") MessageBox.Show("Заполните поле пароля!", "", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (rbMan.IsChecked != true && rbWoman.IsChecked != true) MessageBox.Show("Убедитесь, что Вы выбрали пол!", "", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (tbNumber.Text.Length != 11) MessageBox.Show("Проверьте правильность введенного номера телефона!\nПримеры правильного формата:\n  • 79101426789\n  • 89101426789", "", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                string checkPassword = pbPassword.Password;
                Users searchUser = BaseClass.BD.Users.FirstOrDefault(x => x.login == tbLogin.Text);
                if (searchUser != null) MessageBox.Show("Такой пользователь уже существует!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                else if (CheckPass(checkPassword) == false)
                    MessageBox.Show("Ваш пароль очень простой!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    int tempGender = 0;
                    if (rbMan.IsChecked == true)
                        tempGender = 1;
                    if (rbWoman.IsChecked == true)
                        tempGender = 2;
                    Users user = new Users()
                    {
                        name_user = tbName.Text,
                        surname_user = tbSurname.Text,
                        gender = tempGender,
                        login = tbLogin.Text,
                        password = pbPassword.Password.GetHashCode(),
                        phone = tbNumber.Text,
                        date_reg = Convert.ToDateTime(DateTime.Today),
                        role = 0,
                        privilege = null
                    };
                    BaseClass.BD.Users.Add(user);
                    BaseClass.BD.SaveChanges();
                    MessageBox.Show("Вы зарегистрировались!");
                    FrameClass.MainFrame.Navigate(new AutoPage());
                }

            }

        }

        private void BackMain_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new MainPage());
        }
        static bool CheckPass(string pass)
        {
            bool otv;
            var regexTwoNum = new Regex(@"(\d.*\d)");
            var regexSpecSim = new Regex(@"([!,@,#,$,%,^,&,*,?,_,~])");
            var regexStrochLat = new Regex(@"([a-z].*[a-z].*[a-z])");
            var regexZaglavLat = new Regex(@"([A-Z])");
            if (pass.Length < 8) otv = false;
            else if (!regexSpecSim.IsMatch(pass)) otv = false;
            else if (!regexTwoNum.IsMatch(pass)) otv = false;
            else if (!regexZaglavLat.IsMatch(pass)) otv = false;
            else otv = true;
            return otv;
        }
    }
}
