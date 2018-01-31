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
    /// Interaction logic for RecipeStepDetailItem.xaml
    /// </summary>
    public partial class RecipeStepDetailItem4 : UserControl
    {
        private bool detailLayout = false;

        public RecipeStepDetailItem4()
        {
            InitializeComponent();
            DisplayLayout();
        }

        private void OnChangeContentLayout(object sender, RoutedEventArgs e)
        {
            detailLayout = !detailLayout;
            DisplayLayout();
        }

        private void DisplayLayout()
        {
            if (detailLayout)
            {
                //DetailPanel.Height = DetailPanel.ActualHeight;
                DetailPanel.Visibility = Visibility.Visible;
                //SummaryPanel.Height = 0;
                SummaryPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                //SummaryPanel.Height = DetailPanel.ActualHeight;
                SummaryPanel.Visibility = Visibility.Visible;
               // DetailPanel.Height = 0;
                DetailPanel.Visibility = Visibility.Collapsed;
            }
        }
    }
}
