using System.Windows;
using System.Windows.Controls;

namespace Demo.ui.view
{
    /// <summary>
    /// Interaction logic for DioSwitcher.xaml
    /// </summary>
    public partial class IntExtTemperSwitcher : UserControl
    {
        public IntExtTemperSwitcher()
        {
            InitializeComponent();

            BtnInt.Content = Value ? "Int" : "Ext";
            BtnExt.Content = Value ? "Int" : "Ext";
            BtnInt.Visibility = Value ? Visibility.Visible : Visibility.Hidden;
            BtnExt.Visibility = !Value ? Visibility.Visible : Visibility.Hidden;
        }

        private void Btn_Switch_Click(object sender, RoutedEventArgs e)
        {
            Value = !Value;
            BtnInt.Visibility = Value ? Visibility.Visible : Visibility.Hidden;
            BtnExt.Visibility = !Value ? Visibility.Visible : Visibility.Hidden;
        }

        #region Label DP

        private void OnValuePropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            //string SamplePropertyNewValue = (string)e.NewValue;

            //txtName.Text = SamplePropertyNewValue;
            BtnInt.Content = Value ? "Int" : "Ext";
            BtnExt.Content = Value ? "Int" : "Ext";
            BtnInt.Visibility = Value ? Visibility.Visible : Visibility.Hidden;
            BtnExt.Visibility = !Value ? Visibility.Visible : Visibility.Hidden;
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
                typeof(IntExtTemperSwitcher), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSamplePropertyChanged));

        static void OnSamplePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            (obj as IntExtTemperSwitcher).OnValuePropertyChanged(e);
        }

        #endregion
    }
}
