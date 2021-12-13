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
using System.Threading;
using System.ComponentModel;
using System.Text.RegularExpressions;

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
            AuthButton.IsEnabled = false;
            errorTextBlock.Text = Error;
            errorTextBlock.Visibility = Visibility.Visible;
            ColorAnimation ErrorShowAnim = new ColorAnimation(Color.FromRgb(255, 255, 255), Color.FromRgb(200, 0, 0), TimeSpan.FromSeconds(1));
            ErrorShowAnim.Completed += ErrorShowAnim_Completed;
            errorTextBlock.Foreground = new SolidColorBrush(Colors.Orange);
            errorTextBlock.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, ErrorShowAnim);
        }
        private void ErrorShowAnim_Completed(object sender, EventArgs e)
        {
            ColorAnimation ErrorHideAnim = new ColorAnimation(Color.FromRgb(200, 0, 0), Color.FromRgb(255, 255, 255), TimeSpan.FromSeconds(1));
            ErrorHideAnim.Completed += ErrorHideAnim_Completed;
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.RunWorkerAsync();

            backgroundWorker.DoWork += (s, e) => { Thread.Sleep(TimeSpan.FromSeconds(2)); };
            backgroundWorker.RunWorkerCompleted += (s, e) =>
            {
                errorTextBlock.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, ErrorHideAnim);
            };
        }
        private void ErrorHideAnim_Completed(object sender, EventArgs e)
        {
            errorTextBlock.Visibility = Visibility.Collapsed;
            AuthButton.IsEnabled = true;
        }


        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(tbName.Text) || string.IsNullOrEmpty(tbPassword.Text) || tbNumber.Text.Length == 1)
            {
                ShowError("Заполните данными все поля");
                return;
            }


            if (!Regex.IsMatch(tbNumber.Text, @"^(\+)(\d){10,14}", RegexOptions.IgnoreCase))
            {
                ShowError("Некорректный номер телефона.");
                tbNumber.Text="";
                return;

            }


            var optionsBuilder = new DbContextOptionsBuilder<AutoCaffeeBDContext>();
            var options = optionsBuilder.UseSqlServer(ConfigurationHelper.getInstance().conString).Options;

            using (AutoCaffeeBDContext bd = new AutoCaffeeBDContext(options))
            {

                //var a = bd.Personals.ToList().Find(item => item.Phonenumber == tbNumber.Text);

                //if (a == null) ShowError("не найдён");


                foreach( var a in bd.Personals)
                {
                    MessageBox.Show(a.Firstname);
                }

            }


            /*

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
