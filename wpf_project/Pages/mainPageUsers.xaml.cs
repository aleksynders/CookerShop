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
    /// Логика взаимодействия для mainPageUsers.xaml
    /// </summary>
    public partial class mainPageUsers : Page
    {
        public mainPageUsers()
        {
            InitializeComponent();
            listProduct.ItemsSource = BaseClass.BD.Products.ToList();
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
    }
}
