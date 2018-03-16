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
using Demo.ui.model;
using Demo.ui.view;
using Demo.controller;

namespace Demo.ui
{
    /// <summary>
    /// Interaction logic for TubeEventsPage.xaml
    /// </summary>
    public partial class TubeAlarmPage : UserControl
    {

        public event TubeControlBar.ClickHandler CloseClick;

        public TubeAlarmPage()
        {
            InitializeComponent();

    
            
        }

        public void LoadTubePage(byte selectedTube)
        {
            
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.CloseClick(sender, e);
            e.Handled = false;
        }
    }
}
