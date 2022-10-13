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
                privilege = null,
                black = null
            };
            BaseClass.BD.Users.Add(user);
            BaseClass.BD.SaveChanges();
            MessageBox.Show("Вы зарегистрировались!");
        }

    }
}
