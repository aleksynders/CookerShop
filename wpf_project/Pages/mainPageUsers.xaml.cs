using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
    /// Логика взаимодействия для mainPageUsers.xaml
    /// </summary>
    public partial class mainPageUsers : Page
    {
        public mainPageUsers()
        {
            InitializeComponent();
            listProduct.ItemsSource = BaseClass.BD.Products.ToList();
            Filter.SelectedIndex = 0;
            Sortirovka.SelectedIndex = 0;
        }

        private void rate_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            int rateID = 0;
            List<Products> DC = BaseClass.BD.Products.Where(x => x.product_code == index).ToList();
            foreach (Products Product in DC)
                rateID = Convert.ToInt32(Product.rate);
            List<Ratings> RN = BaseClass.BD.Ratings.Where(x => x.rate == rateID).ToList();
            foreach (Ratings Ratings in RN)
            {
                if (Ratings.rating == 5)
                    tb.Text = "★★★★★";
                else if (Ratings.rating == 4)
                    tb.Text = "★★★★";
                else if (Ratings.rating == 3)
                    tb.Text = "★★★";
                else if (Ratings.rating == 2)
                    tb.Text = "★★";
                else if (Ratings.rating == 1)
                    tb.Text = "★";
                else
                {
                    tb.Text = "Рейтинг: 0";
                    tb.Foreground = Brushes.DarkGray;
                }
                tb.Foreground = Brushes.DarkOrange;
            }
        }

        private void price_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            int disc = 0;
            int price = 0;
            List<Products> DC = BaseClass.BD.Products.Where(x => x.product_code == index).ToList();
            foreach (Products Product in DC)
            {
                disc = Convert.ToInt32(Product.discount);
                price = Convert.ToInt32(Product.price);
            }
            if (disc <= 0)
                tb.Text = "Цена: " + price.ToString() + "₽";
            if (disc > 0)
            {
                price = price * (100 - disc) / 100;
                tb.Text = "Цена: " + price.ToString() + "₽";
                tb.FontWeight = FontWeights.Bold;
                tb.Foreground = Brushes.Red;
            }
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new ProfileUser());
        }

        void SearchMainMethod()
        {
            string strSearch = Search.Text.ToLower();
            var regexSearch = new Regex($@"(^({strSearch}).*)");

            if(Sortirovka.SelectedIndex == 0)
            {
                if (cbFilter.IsChecked == true)
                {
                    if (Filter.SelectedIndex == 0)
                    {
                        listProduct.ItemsSource = BaseClass.BD.Products.ToList().Where(x => regexSearch.IsMatch(x.name.ToLower()) == true && x.rate !=null);
                    }
                    else if (Filter.SelectedIndex == 1)
                    {
                        listProduct.ItemsSource = BaseClass.BD.Products.ToList().Where(x => regexSearch.IsMatch(x.name.ToLower()) == true && x.discount > 0 && x.rate != null);
                    }
                    else
                    {
                        listProduct.ItemsSource = BaseClass.BD.Products.ToList().Where(x => regexSearch.IsMatch(x.name.ToLower()) == true && x.discount == 0 && x.rate != null);
                    }
                }
                else
                {
                    if (Filter.SelectedIndex == 0)
                    {
                        listProduct.ItemsSource = BaseClass.BD.Products.ToList().Where(x => regexSearch.IsMatch(x.name.ToLower()) == true );
                    }
                    else if (Filter.SelectedIndex == 1)
                    {
                        listProduct.ItemsSource = BaseClass.BD.Products.ToList().Where(x => regexSearch.IsMatch(x.name.ToLower()) == true && x.discount > 0);
                    }
                    else
                    {
                        listProduct.ItemsSource = BaseClass.BD.Products.ToList().Where(x => regexSearch.IsMatch(x.name.ToLower()) == true && x.discount == 0);
                    }
                }
            }
            else if(Sortirovka.SelectedIndex == 1) 
            {
                if (cbFilter.IsChecked == true)
                {
                    if (Filter.SelectedIndex == 0)
                    {
                        listProduct.ItemsSource = BaseClass.BD.Products.OrderBy(x => x.price * (100 - x.discount) / 100).ToList().Where(x => regexSearch.IsMatch(x.name.ToLower()) == true && x.rate != null);
                    }
                    else if (Filter.SelectedIndex == 1)
                    {
                        listProduct.ItemsSource = BaseClass.BD.Products.OrderBy(x => x.price * (100 - x.discount) / 100).ToList().Where(x => regexSearch.IsMatch(x.name.ToLower()) == true && x.discount > 0 && x.rate != null);
                    }
                    else
                    {
                        listProduct.ItemsSource = BaseClass.BD.Products.OrderBy(x => x.price * (100 - x.discount) / 100).ToList().Where(x => regexSearch.IsMatch(x.name.ToLower()) == true && x.discount == 0 && x.rate != null);
                    }
                }
                else
                {
                    if (Filter.SelectedIndex == 0)
                    {
                        listProduct.ItemsSource = BaseClass.BD.Products.OrderBy(x => x.price * (100 - x.discount) / 100).ToList().Where(x => regexSearch.IsMatch(x.name.ToLower()) == true);
                    }
                    else if (Filter.SelectedIndex == 1)
                    {
                        listProduct.ItemsSource = BaseClass.BD.Products.OrderBy(x => x.price * (100 - x.discount) / 100).ToList().Where(x => regexSearch.IsMatch(x.name.ToLower()) == true && x.discount > 0);
                    }
                    else
                    {
                        listProduct.ItemsSource = BaseClass.BD.Products.OrderBy(x => x.price * (100 - x.discount) / 100).ToList().Where(x => regexSearch.IsMatch(x.name.ToLower()) == true && x.discount == 0);
                    }
                }
            }
            else if (Sortirovka.SelectedIndex == 2) 
            {
                if (cbFilter.IsChecked == true)
                {
                    if (Filter.SelectedIndex == 0)
                    {
                        listProduct.ItemsSource = BaseClass.BD.Products.OrderByDescending(x => x.price * (100 - x.discount) / 100).ToList().Where(x => regexSearch.IsMatch(x.name.ToLower()) == true && x.rate != null);
                    }
                    else if (Filter.SelectedIndex == 1)
                    {
                        listProduct.ItemsSource = BaseClass.BD.Products.OrderByDescending(x => x.price * (100 - x.discount) / 100 ).ToList().Where(x => regexSearch.IsMatch(x.name.ToLower()) == true && x.discount > 0 && x.rate != null);
                    }
                    else
                    {
                        listProduct.ItemsSource = BaseClass.BD.Products.OrderByDescending(x => x.price * (100 - x.discount) / 100 ).ToList().Where(x => regexSearch.IsMatch(x.name.ToLower()) == true && x.discount == 0 && x.rate != null);
                    }
                }
                else
                {
                    if (Filter.SelectedIndex == 0)
                    {
                        listProduct.ItemsSource = BaseClass.BD.Products.OrderByDescending(x => x.price * (100 - x.discount) / 100).ToList().Where(x => regexSearch.IsMatch(x.name.ToLower()) == true);
                    }
                    else if (Filter.SelectedIndex == 1)
                    {
                        listProduct.ItemsSource = BaseClass.BD.Products.OrderByDescending(x => x.price * (100 - x.discount) / 100).ToList().Where(x => regexSearch.IsMatch(x.name.ToLower()) == true && x.discount > 0);
                    }
                    else
                    {
                        listProduct.ItemsSource = BaseClass.BD.Products.OrderByDescending(x => x.price * (100 - x.discount) / 100).ToList().Where(x => regexSearch.IsMatch(x.name.ToLower()) == true && x.discount == 0);
                    }
                }
            }
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchMainMethod();
        }

        private void cbFilter_Checked(object sender, RoutedEventArgs e)
        {
            SearchMainMethod();
        }

        private void cbFilter_Unchecked(object sender, RoutedEventArgs e)
        {
            SearchMainMethod();
        }

        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchMainMethod();
        }

        private void Sortirovka_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchMainMethod();
        }
    }
}
