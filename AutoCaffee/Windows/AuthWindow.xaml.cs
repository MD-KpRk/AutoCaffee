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
using System.Security.Cryptography;

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

            if (string.IsNullOrEmpty(tbName.Text) || string.IsNullOrEmpty(tbPassword.Password) || tbNumber.Text.Length == 1)
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

            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<AutoCaffeeBDContext>();
                var options = optionsBuilder.UseSqlServer(ConfigurationHelper.getInstance().conString).Options;

                using (AutoCaffeeBDContext bd = new AutoCaffeeBDContext(options))
                {

                    Personal User = bd.Personals.Include(u => u.Dolg).Include(item => item.Rol).Where(us => us.Phonenumber == tbNumber.Text).FirstOrDefault();

                    if (User == null)
                    {
                        ShowError("Пользователь с таким телефоном \n не найден");
                        return;
                    }

                    if(User.Firstname != tbName.Text)
                    {
                        ShowError("На данный номер телефона не зарегистрирован пользователь с таким именем.");
                        return;
                    }

                    if(Classes.PasswordManager.stringToStringHash(tbPassword.Password) != User.Hashpass)
                    {
                        ShowError("Неверный пароль");
                        return;
                    }
                    else
                    {
                        //вход
                        new MainWindow(WindowState, User).Show();
                        Close();
                    }

                }
            }
            catch(Exception)
            {
                ShowError("Ошибка соединения с базой данных");
            }
        }

        private void tbNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbNumber.Text)) tbNumber.Text = "+";
            tbNumber.Text = "+" + tbNumber.Text.Replace("+","");
        }



        //////////////////////////////////////////////////////////////////////////////

        private void Button_Click(object sender, RoutedEventArgs e) ///// DEBUG BUTTON
        {
            var optionsBuilder = new DbContextOptionsBuilder<AutoCaffeeBDContext>();
            var options = optionsBuilder.UseSqlServer(ConfigurationHelper.getInstance().conString).Options;



            using (AutoCaffeeBDContext bd = new AutoCaffeeBDContext(options))
            {
                Personal User = bd.Personals.Include(u => u.Dolg).Include(item => item.Rol).Where(us => us.Id == 1).FirstOrDefault();

                new MainWindow(WindowState, User).Show();
                Close();
            }
            //////////////////////////////////////////////////////////////////////////////
        }

    }
}
