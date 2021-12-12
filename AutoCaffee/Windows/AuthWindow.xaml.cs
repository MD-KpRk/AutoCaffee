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

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
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
        }
    }
}
