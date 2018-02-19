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
    /// Interaction logic for DioSwitcher.xaml
    /// </summary>
    public partial class DioSwitcher : UserControl
    {
        public DioSwitcher()
        {
            InitializeComponent();

            BtnOn.Visibility = Value ? Visibility.Visible : Visibility.Hidden;
            BtnOff.Visibility = !Value ? Visibility.Visible : Visibility.Hidden;
        }

        private void Btn_Switch_Click(object sender, RoutedEventArgs e)
        {
            Value = !Value;
            BtnOn.Visibility = Value ? Visibility.Visible : Visibility.Hidden;
            BtnOff.Visibility = !Value ? Visibility.Visible : Visibility.Hidden;
        }

        #region Label DP

        private void OnValuePropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            //string SamplePropertyNewValue = (string)e.NewValue;

            //txtName.Text = SamplePropertyNewValue;
            BtnOn.Content = e.NewValue;
            BtnOff.Content = e.NewValue;
            BtnOn.Visibility = Value ? Visibility.Visible : Visibility.Hidden;
            BtnOff.Visibility = !Value ? Visibility.Visible : Visibility.Hidden;
        }

        /// <summary>
        /// Gets or sets the Value which is being displayed
        /// </summary>
        public bool Value
        {
            get { return (bool)GetValue(ValueProperty); }
            set
            {
                SetValue(ValueProperty, value);
            }
        }

        /// <summary>
        /// Identified the Label dependency property
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(bool),
                typeof(DioSwitcher), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSamplePropertyChanged));

        static void OnSamplePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            (obj as DioSwitcher).OnValuePropertyChanged(e);
        }

        #endregion
    }
}
