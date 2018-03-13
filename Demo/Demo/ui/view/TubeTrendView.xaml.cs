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
using System.Threading;
using System.Windows.Threading;

namespace Demo.ui.view
{
    /// <summary>
    /// Interaction logic for TubeTrendView.xaml
    /// </summary>
    public partial class TubeTrendView : UserControl
    {
        private TubeTrendViewModel mModel;
        

        public TubeTrendView()
        {
            InitializeComponent();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var pen = new Pen(Brushes.Black, 1);
            
            if (mModel != null && mModel.DataPoints != null)
            {
                for (int i = 0; i < mModel.DataPoints.Count - 1; ++i)
                {
                    Point point0 = new Point(ActualWidth/ mModel.DataPoints.Count * i, mModel.DataPoints[i]);
                    Point point1 = new Point(ActualWidth / mModel.DataPoints.Count * (i + 1), mModel.DataPoints[i + 1]);

                    drawingContext.DrawLine(pen, point0, point1);
                }
            }

        }

        public void UpdatePlot()
        {
            this.InvalidateVisual();
        }

        public TubeTrendViewModel ViewModel
        {
            get { return mModel; }
            set { mModel = value; }
        }
    }
}
