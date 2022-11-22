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
using System.Windows.Shapes;

namespace wpf_project
{
    /// <summary>
    /// Логика взаимодействия для WindowChangeInfo.xaml
    /// </summary>
    public partial class WindowChangeInfo : Window
    {
        Users user;
        public WindowChangeInfo(Users user)
        {
            InitializeComponent();
            this.user = user;
            tbName.Text = user.name_user;
            tbSurname.Text = user.surname_user;
            tbPhone.Text = user.phone;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            user.name_user = tbName.Text; 
            user.surname_user = tbSurname.Text;  
            user.phone = tbPhone.Text;
            BaseClass.BD.SaveChanges(); 
            this.Close();
        }
    }
}
