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
using log4net;
using Demo.ui.model;

namespace Demo.ui
{
    /// <summary>
    /// Interaction logic for TubeRecipePage.xaml
    /// </summary>
    public partial class TubeRecipePage : UserControl
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        TubeRecipePageModel mTubeRecipePageModel;

        public TubeRecipePage()
        {
            InitializeComponent();

            mTubeRecipePageModel = new TubeRecipePageModel();
        }

        public void LoadTubePage(byte selectedTube)
        {
            log.Debug("TubeRecipePage:LoadTubePage");
            mTubeRecipePageModel.LoadData(selectedTube);
            this.DataContext = mTubeRecipePageModel;
        }
    }
}
