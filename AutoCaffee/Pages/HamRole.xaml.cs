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

namespace AutoCaffee.Pages
{
    /// <summary>
    /// Логика взаимодействия для HamRole.xaml
    /// </summary>
    public partial class HamRole : Page
    {
        public HamRole()
        {
            InitializeComponent();

            using(AutoCaffeeBDContext bd = new AutoCaffeeBDContext())
        }
    }
}
