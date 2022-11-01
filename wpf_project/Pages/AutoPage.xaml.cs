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
    /// Логика взаимодействия для AutoPage.xaml
    /// </summary>
    public partial class AutoPage : Page
    {
        public AutoPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new MainPage());
        }

        private void bRegistration_Click(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text == "") MessageBox.Show("Заполните поле логина!", "", MessageBoxButton.OK, MessageBoxImage.Error); // Проверка на заполнение Логина
            else if (pbPassword.Password == "") MessageBox.Show("Заполните поле логина!", "", MessageBoxButton.OK, MessageBoxImage.Error); // Проверка на заполнение Пароля
            else
            {
                int pass = pbPassword.Password.GetHashCode();
                Users searchUser = BaseClass.BD.Users.FirstOrDefault(x => x.login == tbLogin.Text); 
                if (searchUser == null) MessageBox.Show("Такого пользователя не существует!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    Users autoUser = BaseClass.BD.Users.FirstOrDefault(x => x.login == tbLogin.Text && x.password == pass);
                    if (autoUser == null)
                    {
                        MessageBox.Show("Неправильно введён пароль!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        switch (autoUser.role)
                        {
                            // Обычный пользователь (0)
                            case 0:

                                break;
                            // Администратор (1)
                            case 1:
                                FrameClass.loginAutorizate = tbLogin.Text;
                                FrameClass.MainFrame.Navigate(new AdminPanel());
                                break;
                            default: MessageBox.Show("Произошла неизвестная ошибка!\nПерезайдите в приложение!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); break;
                        }
                    }
                }
            }
        }
    }
}
