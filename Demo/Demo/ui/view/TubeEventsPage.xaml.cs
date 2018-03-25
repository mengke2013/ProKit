using System.Windows;
using System.Windows.Controls;

using Demo.ui.model;

namespace Demo.ui.view
{
    /// <summary>
    /// Interaction logic for TubeEventsPage.xaml
    /// </summary>
    public partial class TubeEventsPage : UserControl
    {

        TubeEventsPageModel mTubeEventspageModel;

        public TubeEventsPage()
        {
            InitializeComponent();

            mTubeEventspageModel = new TubeEventsPageModel();
            
        }

        public void LoadPage(byte selectedTube)
        {
            mTubeEventspageModel.LoadData(selectedTube);
            this.DataContext = mTubeEventspageModel.Model;

            Visibility = Visibility.Visible;
        }

        public void UnloadPage(byte selectedTube)
        {
            Visibility = Visibility.Hidden;
            //            ClearValue(EffectProperty);
        }
    }
}
