using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
    /// Логика взаимодействия для ProfileUser.xaml
    /// </summary>
    /// 
    public partial class ProfileUser : Page
    {




        Users searchUser = BaseClass.BD.Users.FirstOrDefault(x => x.login == FrameClass.loginAutorizate);


        void showImage(byte[] Barray, System.Windows.Controls.Image img)
        {
            BitmapImage BI = new BitmapImage();  // создаем объект для загрузки изображения
            using (MemoryStream m = new MemoryStream(Barray))  // для считывания байтового потока
            {
                BI.BeginInit();  // начинаем считывание
                BI.StreamSource = m;  // задаем источник потока
                BI.CacheOption = BitmapCacheOption.OnLoad;  // переводим изображение
                BI.EndInit();  // заканчиваем считывание
            }
            img.Source = BI;  // показываем картинку на экране (imUser – имя картиник в разметке)
            img.Stretch = Stretch.Uniform;
        }


        public ProfileUser()
        {
            InitializeComponent();
            LoginUser.Text = searchUser.login;
            IDUser.Text = searchUser.ID.ToString();
            NameUser.Text = "Имя: " + searchUser.name_user;
            SurnameUser.Text = "Фамилия: " + searchUser.surname_user;
            PhoneUser.Text = "Номер телефона: " + searchUser.phone;
            string Privilage = " ";
            var tempPrivilage = searchUser.privilege;
            if (tempPrivilage == null)
            {
                Privilage = "Отсутствует";
                
                PrivilageUser.Foreground = System.Windows.Media.Brushes.Red;
            }
            else
            {
                Privilage privilageForUser = BaseClass.BD.Privilage.FirstOrDefault(x => x.privilage1== tempPrivilage);
                Privilage = privilageForUser.name_privilage;
            }
            PrivilageUser.Text = Privilage;
            try
            {
                List<Photos> u = BaseClass.BD.Photos.Where(x => x.IdUser == searchUser.ID).ToList(); // для загрузки картинки находим все фото пользователя в таблице, где хранятся фото
                if (u != null)  // если список с фото не пустой, начинает переводить байтовый массив в изображение
                {

                    byte[] Bar = u[u.Count - 1].PhotoBinary;   // считываем изображение из базы (считываем байтовый массив двоичных данных) - выбираем последнее добавленное изображение
                    showImage(Bar, imgUserMain);  // отображаем картинку
                }
            }
            catch { }

        }

        private void BackMain_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new mainPageUsers());
        }

        private void ResetInfo_Click(object sender, RoutedEventArgs e)
        {
            WindowChangeInfo windowChangeInfo = new WindowChangeInfo(searchUser);
            windowChangeInfo.ShowDialog();
            FrameClass.MainFrame.Navigate(new ProfileUser());
        }

        private void ResetPass_Click(object sender, RoutedEventArgs e)
        {
            ChangePass changePass = new ChangePass();
            changePass.ShowDialog();
            FrameClass.MainFrame.Navigate(new ProfileUser());
        }

        private void ResetPhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Photos u = new Photos();  // создание объекта для добавления записи в таблицу, где хранится фото
                u.IdUser = searchUser.ID;  // присваиваем значение полю idUser (id авторизованного пользователя)

                OpenFileDialog OFD = new OpenFileDialog();  // создаем диалоговое окно
                //OFD.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);  // выбор папки для открытия
                OFD.ShowDialog();  // открываем диалоговое окно             
                string path = OFD.FileName;  // считываем путь выбранного изображения
                System.Drawing.Image SDI = System.Drawing.Image.FromFile(path);  // создаем объект для загрузки изображения в базу
                ImageConverter IC = new ImageConverter();  // создаем конвертер для перевода картинки в двоичный формат
                byte[] Barray = (byte[])IC.ConvertTo(SDI, typeof(byte[]));  // создаем байтовый массив для хранения картинки
                u.PhotoBinary = Barray;  // заполяем поле photoBinary полученным байтовым массивом
                BaseClass.BD.Photos.Add(u);  // добавляем объект в таблицу БД
                BaseClass.BD.SaveChanges();  // созраняем изменения в БД
                MessageBox.Show("Фото добавлено!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                FrameClass.MainFrame.Navigate(new ProfileUser()); // перезагружаем страницу
            }
            catch
            {
            }
        }

        int n = 0;
        private void DeletePhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Photos> u = BaseClass.BD.Photos.Where(x => x.IdUser == searchUser.ID).ToList();
                if (u != null)  // если объект не пустой, начинает переводить байтовый массив в изображение
                {

                    byte[] Bar = u[n].PhotoBinary;   // считываем изображение из базы (считываем байтовый массив двоичных данных)
                    showImage(Bar, imgGallery);  // отображаем картинку
                }
                spGallery.Visibility = Visibility.Visible;
            }
            catch {
                MessageBox.Show("Возможно у вас нет старых фото!", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Photos> u = BaseClass.BD.Photos.Where(x => x.IdUser == searchUser.ID).ToList();
                n++;
                if (Back.IsEnabled == false)
                {
                    Back.IsEnabled = true;
                }
                if (u != null)  // если объект не пустой, начинает переводить байтовый массив в изображение
                {

                    byte[] Bar = u[n].PhotoBinary;   // считываем изображение из базы (считываем байтовый массив двоичных данных)
                    showImage(Bar, imgGallery);
                }
                if (n == u.Count - 1)
                {
                    Next.IsEnabled = false;
                }
            }
            catch { }

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Photos> u = BaseClass.BD.Photos.Where(x => x.IdUser == searchUser.ID).ToList();
                n--;
                if (Next.IsEnabled == false)
                {
                    Next.IsEnabled = true;
                }
                if (u != null)  // если объект не пустой, начинает переводить байтовый массив в изображение
                {

                    byte[] Bar = u[n].PhotoBinary;   // считываем изображение из базы (считываем байтовый массив двоичных данных)
                    BitmapImage BI = new BitmapImage();  // создаем объект для загрузки изображения
                    showImage(Bar, imgGallery);
                }
                if (n == 0)
                {
                    Back.IsEnabled = false;
                }
            }
            catch { }
        }

        private void btnOld_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Photos> u = BaseClass.BD.Photos.Where(x => x.IdUser == searchUser.ID).ToList();
                byte[] Bar = u[n].PhotoBinary;   // считываем изображение из базы (считываем байтовый массив двоичных данных)
                showImage(Bar, imgUserMain);  // отображаем картинку
                spGallery.Visibility = Visibility.Collapsed;
            }
            catch
            {

            }
        }

        private void ChangeAllPhoto(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog OFD = new OpenFileDialog();  // создаем диалоговое окно
                OFD.Multiselect = true;  // открытие диалогового окна с возможностью выбора нескольких элементов
                if (OFD.ShowDialog() == true)  // пока диалоговое окно открыто, будет в цикле записывать каждое выбранное изображение в БД
                {
                    foreach (string file in OFD.FileNames)  // цикл организован по именам выбранных файлов
                    {
                        Photos u = new Photos();  // создание объекта для добавления записи в таблицу, где хранится фото
                        u.IdUser = searchUser.ID;  // присваиваем значение полю idUser (id авторизованного пользователя)
                        string path = file;  // считываем путь выбранного изображения
                        System.Drawing.Image SDI = System.Drawing.Image.FromFile(file);  // создаем объект для загрузки изображения в базу
                        ImageConverter IC = new ImageConverter();  // создаем конвертер для перевода картинки в двоичный формат
                        byte[] Barray = (byte[])IC.ConvertTo(SDI, typeof(byte[]));  // создаем байтовый массив для хранения картинки
                        u.PhotoBinary = Barray;  // заполяем поле photoBinary полученным байтовым массивом
                        BaseClass.BD.Photos.Add(u);  // добавляем объект в таблицу БД
                    }
                    BaseClass.BD.SaveChanges();
                    MessageBox.Show("Фото добавлены!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch { }
        }
    }
}
