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
    
    public partial class mainPageUsers : Page
    {
        PageChange pc = new PageChange();
        List<Products> ProductsFilter = new List<Products>();

        public mainPageUsers()
        {
            InitializeComponent();
            ProductsFilter = BaseClass.BD.Products.ToList();
            listProduct.ItemsSource = BaseClass.BD.Products.ToList();
            pc.CountPage = BaseClass.BD.Products.ToList().Count;
            DataContext = pc;
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
            //var regexSearch = new Regex($@"(^({strSearch}).*)");

            List <Products> fsItog = new List<Products>();

            if (Sortirovka.SelectedIndex == 0)
            {
                if (cbFilter.IsChecked == true)
                {
                    if (Filter.SelectedIndex == 0)
                    { //Where(x => x.name.Contains(strSearch))
                        fsItog = BaseClass.BD.Products.Where(x => x.name.Contains(strSearch) && x.rate != null).ToList();
                    }
                    else if (Filter.SelectedIndex == 1)
                    {
                        fsItog = BaseClass.BD.Products.Where(x => x.name.Contains(strSearch) && x.discount > 0 && x.rate != null).ToList();
                    }
                    else
                    {
                        fsItog = BaseClass.BD.Products.Where(x => x.name.Contains(strSearch) && x.discount == 0 && x.rate != null).ToList();
                    }
                }
                else
                {
                    if (Filter.SelectedIndex == 0)
                    {
                        fsItog = BaseClass.BD.Products.Where(x => x.name.Contains(strSearch)).ToList();
                    }
                    else if (Filter.SelectedIndex == 1)
                    {
                        fsItog = BaseClass.BD.Products.Where(x => x.name.Contains(strSearch) && x.discount > 0).ToList();
                    }
                    else
                    {
                        fsItog = BaseClass.BD.Products.Where(x => x.name.Contains(strSearch) && x.discount == 0).ToList();
                    }
                }
            }
            else if(Sortirovka.SelectedIndex == 1) 
            {
                if (cbFilter.IsChecked == true)
                {
                    if (Filter.SelectedIndex == 0)
                    {
                        fsItog = BaseClass.BD.Products.OrderBy(x => x.price * (100 - x.discount) / 100).Where(x => x.name.Contains(strSearch) && x.rate != null).ToList();
                    }
                    else if (Filter.SelectedIndex == 1)
                    {
                        fsItog = BaseClass.BD.Products.OrderBy(x => x.price * (100 - x.discount) / 100).Where(x => x.name.Contains(strSearch) && x.discount > 0 && x.rate != null).ToList();
                    }
                    else
                    {
                        fsItog = BaseClass.BD.Products.OrderBy(x => x.price * (100 - x.discount) / 100).Where(x => x.name.Contains(strSearch) && x.discount == 0 && x.rate != null).ToList();
                    }
                }
                else
                {
                    if (Filter.SelectedIndex == 0)
                    {
                        fsItog = BaseClass.BD.Products.OrderBy(x => x.price * (100 - x.discount) / 100).Where(x => x.name.Contains(strSearch)).ToList();
                    }
                    else if (Filter.SelectedIndex == 1)
                    {
                        fsItog = BaseClass.BD.Products.OrderBy(x => x.price * (100 - x.discount) / 100).Where(x => x.name.Contains(strSearch) && x.discount > 0).ToList();
                    }
                    else
                    {
                        fsItog = BaseClass.BD.Products.OrderBy(x => x.price * (100 - x.discount) / 100).Where(x => x.name.Contains(strSearch) && x.discount == 0).ToList();
                    }
                }
            }
            else if (Sortirovka.SelectedIndex == 2) 
            {
                if (cbFilter.IsChecked == true)
                {
                    if (Filter.SelectedIndex == 0)
                    {
                        fsItog = BaseClass.BD.Products.OrderByDescending(x => x.price * (100 - x.discount) / 100).Where(x => x.name.Contains(strSearch) && x.rate != null).ToList();
                    }
                    else if (Filter.SelectedIndex == 1)
                    {
                        fsItog = BaseClass.BD.Products.OrderByDescending(x => x.price * (100 - x.discount) / 100 ).Where(x => x.name.Contains(strSearch) && x.discount > 0 && x.rate != null).ToList();
                    }
                    else
                    {
                        fsItog = BaseClass.BD.Products.OrderByDescending(x => x.price * (100 - x.discount) / 100 ).Where(x => x.name.Contains(strSearch) && x.discount == 0 && x.rate != null).ToList();
                    }
                }
                else
                {
                    if (Filter.SelectedIndex == 0)
                    {
                        fsItog = BaseClass.BD.Products.OrderByDescending(x => x.price * (100 - x.discount) / 100).Where(x => x.name.Contains(strSearch)).ToList();
                    }
                    else if (Filter.SelectedIndex == 1)
                    {
                        fsItog = BaseClass.BD.Products.OrderByDescending(x => x.price * (100 - x.discount) / 100).Where(x => x.name.Contains(strSearch) && x.discount > 0).ToList();
                    }
                    else
                    {
                        fsItog = BaseClass.BD.Products.OrderByDescending(x => x.price * (100 - x.discount) / 100).Where(x => x.name.Contains(strSearch) && x.discount == 0).ToList();
                    }
                }
            }

            listProduct.ItemsSource = fsItog;
            ProductsFilter = fsItog;

            string temp = txtPageCount.Text;
            txtPageCount.Text = "";
            txtPageCount.Text = temp;
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




        private void txtPageCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                pc.CountPage = Convert.ToInt32(txtPageCount.Text); // если в текстовом поле есnь значение, присваиваем его свойству объекта, которое хранит количество записей на странице
            }
            catch
            {
                pc.CountPage = ProductsFilter.Count; // если в текстовом поле значения нет, присваиваем свойству объекта, которое хранит количество записей на странице количество элементов в списке
            }
            pc.Countlist = ProductsFilter.Count;  // присваиваем новое значение свойству, которое в объекте отвечает за общее количество записей
            listProduct.ItemsSource = ProductsFilter.Skip(0).Take(pc.CountPage).ToList();  // отображаем первые записи в том количестве, которое равно CountPage
            pc.CurrentPage = 1; // текущая страница - это страница 1
        }

        private void GoPage_MouseDown(object sender, MouseButtonEventArgs e)  // обработка нажатия на один из Textblock в меню с номерами страниц
        {
            TextBlock tb = (TextBlock)sender;

            switch (tb.Uid)  // определяем, куда конкретно было сделано нажатие
            {
                case "prev":
                    pc.CurrentPage--;
                    break;
                case "next":
                    pc.CurrentPage++;
                    break;
                default:
                    pc.CurrentPage = Convert.ToInt32(tb.Text);
                    break;
            }
            listProduct.ItemsSource = ProductsFilter.Skip(pc.CurrentPage * pc.CountPage - pc.CountPage).Take(pc.CountPage).ToList();  // оображение записей постранично с определенным количеством на каждой странице
            // Skip(pc.CurrentPage* pc.CountPage - pc.CountPage) - сколько пропускаем записей
            // Take(pc.CountPage) - сколько записей отображаем на странице
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            pc.CurrentPage = 1;

            try
            {
                pc.CountPage = Convert.ToInt32(txtPageCount.Text); // если в текстовом поле есnь значение, присваиваем его свойству объекта, которое хранит количество записей на странице
            }
            catch
            {
                pc.CountPage = ProductsFilter.Count; // если в текстовом поле значения нет, присваиваем свойству объекта, которое хранит количество записей на странице количество элементов в списке
            }
            pc.Countlist = ProductsFilter.Count;  // присваиваем новое значение свойству, которое в объекте отвечает за общее количество записей
            listProduct.ItemsSource = ProductsFilter.Skip(0).Take(pc.CountPage).ToList();  // отображаем первые записи в том количестве, которое равно CountPage

        }

        private void txtPrevStart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            pc.CurrentPage = 1;
            listProduct.ItemsSource = ProductsFilter.Skip(pc.CurrentPage * pc.CountPage - pc.CountPage).Take(pc.CountPage).ToList();
        }

        private void txtNextEnd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            pc.CurrentPage = ProductsFilter.Count;
            listProduct.ItemsSource = ProductsFilter.Skip(pc.CurrentPage * pc.CountPage - pc.CountPage).Take(pc.CountPage).ToList();
        }
    }
}
