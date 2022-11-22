using System;
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
using System.Windows.Shapes;

namespace wpf_project
{
    /// <summary>
    /// Логика взаимодействия для ChangePass.xaml
    /// </summary>
    public partial class ChangePass : Window
    {
        public ChangePass()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Users searchUser = BaseClass.BD.Users.FirstOrDefault(x => x.login == FrameClass.loginAutorizate);
            int pass = tbLastPass.Password.GetHashCode();
            if (searchUser.password != pass)
                MessageBox.Show("Неверный старый пароль!", "", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if(tbNewPass.Password != tbNewPass2.Password)
                    MessageBox.Show("Пароли не совпадают!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    if(!CheckPass(tbNewPass.Password))
                        MessageBox.Show("Новый пароль не надёжный!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    {
                        searchUser.password = tbNewPass.GetHashCode();
                        BaseClass.BD.SaveChanges();
                        MessageBox.Show("Пароль успешно изменён!", "", MessageBoxButton.OK);
                        this.Close();
                    }
                }
            }
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
