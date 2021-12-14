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

namespace AutoCaffee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool hamPanelActive;
        public bool HamPanelActive
        {
            get => hamPanelActive;
            set
            {
                if (hamPanelActive == false)
                {
                    MenuColumn.Width = new GridLength(160);
                }
                else
                {
                    MenuColumn.Width = new GridLength(40);
                }
                hamPanelActive = value;
            }
        }

        public MainWindow(WindowState state)
        {
            WindowState = state;
            InitializeComponent();
        }

        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            (sender as Button).FontSize = 20;
        }

        private void HamButton_Click(object sender, RoutedEventArgs e)
        {
            HamPanelActive = !HamPanelActive;
        }
    }
}
