using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using Demo.ui.model;

namespace Demo.ui.view
{
    /// <summary>
    /// Interaction logic for StepListItem.xaml
    /// </summary>
    public partial class StepListItem : UserControl
    {
        public delegate void ClickHandler(object sender, RoutedEventArgs e, int stepIndex);
        public event ClickHandler ItemClick;


        private StepItemModel mItemMode;

        public StepListItem()
        {
            InitializeComponent();
        }

        public void Item_Click(object sender, MouseButtonEventArgs e)
        {
            this.Background = new SolidColorBrush(Colors.LightBlue);
            this.ItemClick(sender, e, mItemMode.StepIndex);
            if (e != null)
            {
                e.Handled = false;
            }
        }

        public StepItemModel ItemMode
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
