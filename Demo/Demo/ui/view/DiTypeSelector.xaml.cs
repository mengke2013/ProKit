using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Demo.ui.view
{
    /// <summary>
    /// Interaction logic for DioSwitcher.xaml
    /// </summary>
    public partial class DiTypeSelector : UserControl
    {
        public DiTypeSelector()
        {
            InitializeComponent();

            BtnNone.Visibility = Value==0 ? Visibility.Visible : Visibility.Hidden;
            BtnHold.Visibility = Value==1 ? Visibility.Visible : Visibility.Hidden;
            BtnAbort.Visibility = Value == 2 ? Visibility.Visible : Visibility.Hidden;
            BtnAlarm.Visibility = Value == 3 ? Visibility.Visible : Visibility.Hidden;
            BtnNext.Visibility = Value == 4 ? Visibility.Visible : Visibility.Hidden;
        }

        private void Btn_Switch_Click(object sender, MouseButtonEventArgs e)
        {
            Value++;
            if (Value == 5)
            {
                Value = 0;
            }
            BtnNone.Visibility = Value == 0 ? Visibility.Visible : Visibility.Hidden;
            BtnHold.Visibility = Value == 1 ? Visibility.Visible : Visibility.Hidden;
            BtnAbort.Visibility = Value == 2 ? Visibility.Visible : Visibility.Hidden;
            BtnAlarm.Visibility = Value == 3 ? Visibility.Visible : Visibility.Hidden;
            BtnNext.Visibility = Value == 4 ? Visibility.Visible : Visibility.Hidden;
        }

        #region Label DP

        private void OnValuePropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            BtnNone.Visibility = Value == 0 ? Visibility.Visible : Visibility.Hidden;
            BtnHold.Visibility = Value == 1 ? Visibility.Visible : Visibility.Hidden;
            BtnAbort.Visibility = Value == 2 ? Visibility.Visible : Visibility.Hidden;
            BtnAlarm.Visibility = Value == 3 ? Visibility.Visible : Visibility.Hidden;
            BtnNext.Visibility = Value == 4 ? Visibility.Visible : Visibility.Hidden;
        }

        /// <summary>
        /// Gets or sets the Value which is being displayed
        /// </summary>
        public byte Value
        {
            get { return (byte)GetValue(ValueProperty); }
            set
            {
                SetValue(ValueProperty, value);
            }
        }

        /// <summary>
        /// Identified the Label dependency property
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(byte),
                typeof(DiTypeSelector), new FrameworkPropertyMetadata(default(byte), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSamplePropertyChanged));

        static void OnSamplePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            (obj as DiTypeSelector).OnValuePropertyChanged(e);
        }

        #endregion
    }
}
