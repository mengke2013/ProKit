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

namespace Demo.ui.view
{
    /// <summary>
    /// Interaction logic for TubeControlBar.xaml
    /// </summary>
    public partial class TubeControlBar : UserControl
    {
        public delegate void ClickHandler(object sender, RoutedEventArgs e);
        public event ClickHandler Click;

        public TubeControlBar()
        {
            InitializeComponent();
        }


        public void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Click(sender, e);
            e.Handled = false;
        }
    }
}
