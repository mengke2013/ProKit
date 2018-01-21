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

namespace Demo.ui
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
        }

        private void btnTube1_Click(object sender, RoutedEventArgs e)
        {
            TubeWindow tubeWindow = new TubeWindow(1);
            tubeWindow.Show();
        }

        private void btnTube2_Click(object sender, RoutedEventArgs e)
        {
            TubeWindow tubeWindow = new TubeWindow(2);
            tubeWindow.Show();
        }

        private void btnTube3_Click(object sender, RoutedEventArgs e)
        {
            TubeWindow tubeWindow = new TubeWindow(3);
            tubeWindow.Show();
        }
    }
}
