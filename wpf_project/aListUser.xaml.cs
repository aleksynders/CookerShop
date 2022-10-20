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
            VozrastYbivan.SelectedIndex = 0;
        }

        private void BackMain_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new AdminPanel());
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchMainMethod();
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            rbWoman.IsChecked = false;
            rbMan.IsChecked = false;
            Search.Clear();
            VozrastYbivan.SelectedIndex = 0;
            typeSearch.SelectedIndex = 0;
            dgUser.ItemsSource = BaseClass.BD.Users.ToList();
        }

        private void rbMan_Checked(object sender, RoutedEventArgs e)
        {
            rbWoman.IsChecked = false;
            SearchMainMethod();
        }

        private void rbWoman_Checked(object sender, RoutedEventArgs e)
        {
            rbMan.IsChecked = false;
            SearchMainMethod();
        }

        private void rbMan_Unchecked(object sender, RoutedEventArgs e)
        {
            SearchMainMethod();
        }

        private void rbWoman_Unchecked(object sender, RoutedEventArgs e)
        {
            SearchMainMethod();
        }

        private void VozrastYbivan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           SearchMainMethod();
        }

        private void typeSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchMainMethod();
        }

        void SearchMainMethod()
        {
            string strSearch = Search.Text;
            var regexSearch = new Regex($@"(^({strSearch}).*)");
            if (VozrastYbivan.SelectedIndex == 1)
            {
                if (typeSearch.SelectedIndex == 0)
                {
                    if (rbMan.IsChecked == true)
                    {
                        dgUser.ItemsSource = BaseClass.BD.Users.OrderBy(x => x.surname_user).ToList().Where(x => regexSearch.IsMatch(x.name_user) == true && x.gender == 1);
                    }
                    else if (rbWoman.IsChecked == true)
                    {
                        dgUser.ItemsSource = BaseClass.BD.Users.OrderBy(x => x.surname_user).ToList().Where(x => regexSearch.IsMatch(x.name_user) == true && x.gender == 2);
                    }
                    else
                    {
                        dgUser.ItemsSource = BaseClass.BD.Users.OrderBy(x => x.surname_user).ToList().Where(x => regexSearch.IsMatch(x.name_user) == true);
                    }
                }
                if (typeSearch.SelectedIndex == 1)
                {
                    if (rbMan.IsChecked == true)
                    {
                        dgUser.ItemsSource = BaseClass.BD.Users.OrderBy(x => x.surname_user).ToList().Where(x => regexSearch.IsMatch(x.surname_user) == true && x.gender == 1);
                    }
                    else if (rbWoman.IsChecked == true)
                    {
                        dgUser.ItemsSource = BaseClass.BD.Users.OrderBy(x => x.surname_user).ToList().Where(x => regexSearch.IsMatch(x.surname_user) == true && x.gender == 2);
                    }
                    else
                    {
                        dgUser.ItemsSource = BaseClass.BD.Users.OrderBy(x => x.surname_user).ToList().Where(x => regexSearch.IsMatch(x.surname_user) == true);
                    }
                }
            }
            else if (VozrastYbivan.SelectedIndex == 2)
            {
                if (typeSearch.SelectedIndex == 0)
                {
                    if (rbMan.IsChecked == true)
                    {
                        dgUser.ItemsSource = BaseClass.BD.Users.OrderByDescending(x => x.surname_user).ToList().Where(x => regexSearch.IsMatch(x.name_user) == true && x.gender == 1);
                    }
                    else if (rbWoman.IsChecked == true)
                    {
                        dgUser.ItemsSource = BaseClass.BD.Users.OrderByDescending(x => x.surname_user).ToList().Where(x => regexSearch.IsMatch(x.name_user) == true && x.gender == 2);
                    }
                    else
                    {
                        dgUser.ItemsSource = BaseClass.BD.Users.OrderByDescending(x => x.surname_user).ToList().Where(x => regexSearch.IsMatch(x.name_user) == true);
                    }
                }
                if (typeSearch.SelectedIndex == 1)
                {
                    if (rbMan.IsChecked == true)
                    {
                        dgUser.ItemsSource = BaseClass.BD.Users.OrderByDescending(x => x.surname_user).ToList().Where(x => regexSearch.IsMatch(x.surname_user) == true && x.gender == 1);
                    }
                    else if (rbWoman.IsChecked == true)
                    {
                        dgUser.ItemsSource = BaseClass.BD.Users.OrderByDescending(x => x.surname_user).ToList().Where(x => regexSearch.IsMatch(x.surname_user) == true && x.gender == 2);
                    }
                    else
                    {
                        dgUser.ItemsSource = BaseClass.BD.Users.OrderByDescending(x => x.surname_user).ToList().Where(x => regexSearch.IsMatch(x.surname_user) == true);
                    }
                }
            }
            else
            {
                if (typeSearch.SelectedIndex == 0)
                {
                    if (rbMan.IsChecked == true)
                    {
                        dgUser.ItemsSource = BaseClass.BD.Users.ToList().Where(x => regexSearch.IsMatch(x.name_user) == true && x.gender == 1);
                    }
                    else if (rbWoman.IsChecked == true)
                    {
                        dgUser.ItemsSource = BaseClass.BD.Users.ToList().Where(x => regexSearch.IsMatch(x.name_user) == true && x.gender == 2);
                    }
                    else
                    {
                        dgUser.ItemsSource = BaseClass.BD.Users.ToList().Where(x => regexSearch.IsMatch(x.name_user) == true);
                    }
                }
                if (typeSearch.SelectedIndex == 1)
                {
                    if (rbMan.IsChecked == true)
                    {
                        dgUser.ItemsSource = BaseClass.BD.Users.ToList().Where(x => regexSearch.IsMatch(x.surname_user) == true && x.gender == 1);
                    }
                    else if (rbWoman.IsChecked == true)
                    {
                        dgUser.ItemsSource = BaseClass.BD.Users.ToList().Where(x => regexSearch.IsMatch(x.surname_user) == true && x.gender == 2);
                    }
                    else
                    {
                        dgUser.ItemsSource = BaseClass.BD.Users.ToList().Where(x => regexSearch.IsMatch(x.surname_user) == true);
                    }
                }
            }
        }
    }
}
