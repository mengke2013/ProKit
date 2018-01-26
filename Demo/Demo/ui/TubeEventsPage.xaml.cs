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

namespace Demo.ui
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

        public void LoadTubePage(byte selectedTube)
        {
            mTubeEventspageModel.LoadData(selectedTube);
            this.DataContext = mTubeEventspageModel.Model;
        }
    }
}
