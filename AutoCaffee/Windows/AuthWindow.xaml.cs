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
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Windows.Media.Animation;

namespace AutoCaffee.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        void ShowError(string Error)
        {
            ColorAnimation ErrorAnim = new ColorAnimation(Color.FromRgb(255, 255, 255), Color.FromRgb(200, 0, 0), TimeSpan.FromSeconds(1));
            errorTextBlock.Visibility = Visibility.Visible;
            ErrorAnim.AutoReverse = true;
            ErrorAnim.Completed += ErrorAnim_Completed;
            errorTextBlock.Foreground = new SolidColorBrush(Colors.Orange);
            errorTextBlock.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, ErrorAnim);
        }
        private void ErrorAnim_Completed(object sender, EventArgs e)
        {
            errorTextBlock.Visibility = Visibility.Collapsed;
        }


        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {

            ShowError("Какая-то ошибка");
            /*
            var optionsBuilder = new DbContextOptionsBuilder<AutoCaffeeBDContext>();
            var options = optionsBuilder.UseSqlServer(ConfigurationHelper.getInstance().conString).Options;

            using (AutoCaffeeBDContext db = new AutoCaffeeBDContext(options))
            {
                var dolgs2 = db.Dolgs.ToList();


                db.Dolgs.Add(new Dolg() { Title = "Тест контекста" });


                MessageBox.Show(db.Dolgs.Count().ToString());

                foreach (Dolg d in dolgs2)
                {
                    MessageBox.Show($"{d.Title}");
                }
                db.SaveChanges();
            }
            */
        }

        private void tbNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbNumber.Text)) tbNumber.Text = "+";
            tbNumber.Text = "+" + tbNumber.Text.Replace("+","");


            
        }
    }
}
