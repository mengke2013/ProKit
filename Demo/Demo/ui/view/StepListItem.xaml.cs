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

namespace Demo.ui.view
{
    /// <summary>
    /// Interaction logic for StepListItem.xaml
    /// </summary>
    public partial class StepListItem : UserControl
    {
        public delegate void ClickHandler(object sender, RoutedEventArgs e, byte stepIndex);
        public event ClickHandler ItemClick;


        private StepListItemModel mItemMode;

        public StepListItem()
        {
            InitializeComponent();
        }

        private void Item_Click(object sender, MouseButtonEventArgs e)
        {
            this.Background = new SolidColorBrush(Colors.Green);
            this.ItemClick(sender, e, mItemMode.StepIndex);
            e.Handled = false;
        }

        public StepListItemModel ItemMode
        {
            get { return mItemMode; }
            set
            {
                mItemMode = value;
                this.DataContext = mItemMode;
            }
        }
    }
}
