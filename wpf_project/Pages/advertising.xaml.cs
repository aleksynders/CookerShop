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
using System.Windows.Media.Animation;
using System.Drawing;
using System.Diagnostics;

namespace wpf_project
{
    /// <summary>
    /// Логика взаимодействия для advertising.xaml
    /// </summary>
    public partial class advertising : Page
    {
        public advertising()
        {
            InitializeComponent();
            DoubleAnimation but = new DoubleAnimation();
            but.From = 100; 
            but.To = 150; 
            but.Duration = TimeSpan.FromSeconds(2); 
            but.RepeatBehavior = RepeatBehavior.Forever; 
            but.AutoReverse = true; 
            butGo.BeginAnimation(WidthProperty, but); 
        }

        private void BackMain_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new MainPage());
        }

        private void butGo_MouseEnter(object sender, MouseEventArgs e)
        {
            DoubleAnimation FSA = new DoubleAnimation();
            FSA.From = 18;
            FSA.To = 24;
            FSA.Duration = TimeSpan.FromSeconds(0.1);
            FSA.AutoReverse = false;
            text1.BeginAnimation(FontSizeProperty, FSA);
            text2.BeginAnimation(FontSizeProperty, FSA);
            text3.BeginAnimation(FontSizeProperty, FSA);

            DoubleAnimation button = new DoubleAnimation();
            button.From = 30;
            button.To = 35;
            button.Duration = TimeSpan.FromSeconds(0.1);
            button.AutoReverse = false;
            butGo.BeginAnimation(HeightProperty, button);
        }

        private void butGo_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation FSA = new DoubleAnimation();
            FSA.From = 24;
            FSA.To = 18;
            FSA.Duration = TimeSpan.FromSeconds(0.1);
            FSA.AutoReverse = false;
            text1.BeginAnimation(FontSizeProperty, FSA);
            text2.BeginAnimation(FontSizeProperty, FSA);
            text3.BeginAnimation(FontSizeProperty, FSA);

            DoubleAnimation button = new DoubleAnimation();
            button.From = 35;
            button.To = 30;
            button.Duration = TimeSpan.FromSeconds(0.1);
            button.AutoReverse = false;
            butGo.BeginAnimation(HeightProperty, button);
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            DoubleAnimation WA = new DoubleAnimation();
            WA.From = 150; 
            WA.To = 170; 
            WA.Duration = TimeSpan.FromSeconds(0.1); 
            WA.AutoReverse = false; 
            imgDel.BeginAnimation(WidthProperty, WA);
            imgDel.BeginAnimation(HeightProperty, WA);
        }

        private void imgDel_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation WA = new DoubleAnimation();
            WA.From = 170;
            WA.To = 150;
            WA.Duration = TimeSpan.FromSeconds(0.1);
            WA.AutoReverse = false;
            imgDel.BeginAnimation(WidthProperty, WA);
            imgDel.BeginAnimation(HeightProperty, WA);
        }

        private void butGo_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://niz.gruzovichkof.ru/perevozka-mebeli"); 
        }
    }
}
