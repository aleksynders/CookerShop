using Microsoft.Win32;
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
    /// Логика взаимодействия для UpdateProduct.xaml
    /// </summary>
    public partial class UpdateProduct : Page
    {

        Products PRODUCT;
        bool flagUpdate = false;
        string path;

        public void uploadFields()
        {
            cbManufacturer.ItemsSource = BaseClass.BD.Manufacturers.ToList();
            cbManufacturer.SelectedValuePath = "manufacturer_code";
            cbManufacturer.DisplayMemberPath = "manufacturer_name";
            cbManufacturer.SelectedIndex = 0;
            tbDiscount.Text = "0";
        }

        public UpdateProduct()
        {
            InitializeComponent();
            uploadFields();
        }

        public UpdateProduct(Products product)
        {
            InitializeComponent();
            uploadFields();
            flagUpdate = true;
            PRODUCT = product;
            tbName.Text = PRODUCT.name;
            tbPrice.Text = Convert.ToString(PRODUCT.price);
            tbAmount.Text = Convert.ToString(PRODUCT.amount);
            cbManufacturer.SelectedIndex = PRODUCT.manufacturer_code - 1;
            tbDescription.Text = PRODUCT.description;
            tbDiscount.Text = Convert.ToString(PRODUCT.discount);

            if (product.image_product != null)
            {
                BitmapImage img = new BitmapImage(new Uri(product.image_product, UriKind.RelativeOrAbsolute));
                imgProduct.Source = img;
            }
        }

        private void BackMain_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new aListProduct());
        }

        private void bRegistration_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (flagUpdate == false)
                {
                    PRODUCT = new Products();
                }
                PRODUCT.name = tbName.Text;
                PRODUCT.description = tbDescription.Text;
                PRODUCT.price = Convert.ToInt32(tbPrice.Text);
                PRODUCT.amount = Convert.ToInt32(tbAmount.Text);
                PRODUCT.manufacturer_code = cbManufacturer.SelectedIndex + 1;
                PRODUCT.discount = Convert.ToInt32(tbDiscount.Text);
                if (path!= null)
                {
                    PRODUCT.image_product = path;
                }
                if (flagUpdate == false)
                {
                    BaseClass.BD.Products.Add(PRODUCT);
                }
                BaseClass.BD.SaveChanges();
                MessageBox.Show("Информация добавлена");

            }
            catch
            {
                MessageBox.Show("Что-то пошло не по плану");
            }
        }

        private void bSavePhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog OFD = new OpenFileDialog();
                OFD.ShowDialog();
                path = OFD.FileName;
                string[] arrayPath = path.Split('\\');
                path = "\\" + arrayPath[arrayPath.Length - 2] + "\\" + arrayPath[arrayPath.Length - 1];
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri("\\Resources\\icons8-new-100.png", UriKind.Relative);
                bi3.EndInit();
                imgProduct.Source = bi3;
            }
            catch { }
        }
    }
}
