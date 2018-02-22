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
using System.Windows.Shapes;
using Demo.ui.model;

namespace Demo.ui.view
{
    /// <summary>
    /// Interaction logic for ProgressDlg.xaml
    /// </summary>
    public partial class ProgressDlg : Window
    {

        private ProgressDlgModel mProgressModel;

        public ProgressDlg()
        {
            InitializeComponent();
            ProgressModel = new ProgressDlgModel();
            this.DataContext = ProgressModel;
        }

        public ProgressDlgModel ProgressModel
        {
            get { return mProgressModel; }
            set { mProgressModel = value; }
        }
    }
}
