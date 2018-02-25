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
    /// Interaction logic for TubeRecipeView.xaml
    /// </summary>
    public partial class TubeRecipeView : UserControl
    {
        public delegate void ClickHandler(object sender, RoutedEventArgs e, int stepIndex);
        public event ClickHandler CommitClick;

        private TubeRecipeViewModel mRecipeViewMode;

        public TubeRecipeView()
        {
            InitializeComponent();
        }

        private void Commit_Click(object sender, RoutedEventArgs e)
        {
            this.CommitClick(sender, e, mRecipeViewMode.StepIndex);
            e.Handled = false;
        }

        public TubeRecipeViewModel RecipeViewMode
        {
            get { return mRecipeViewMode; }
            set { mRecipeViewMode = value; }
        }
    }
}
