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
using System.Globalization;

namespace Demo.ui.view
{
    /// <summary>
    /// Interaction logic for TubeTrendView.xaml
    /// </summary>
    public partial class TubeTrendView : UserControl
    {
        private TubeTrendViewModel mModel;
        private double mPlotHeight;
        private double mPlotWidth;
        private double mMarginX1 = 50;
        private double mMarginX2 = 20;
        private double mMarginY = 20;
        private double mLegentHeight = 50;

        private string[] mTemperLegentLabel = { "Zone 1", "Zone 2", "Zone 3", "Zone 4", "Zone 5", "Zone 6" };
        private string[] mGasLegentLabel = { "N2", "H2", "O2", "BCl3", "PC8" };
        private string[] mVacuumLegentLabel = { "Vacuum1", "Gate", "Vacuum2" };
        private Brush[] brushes = { Brushes.Red, Brushes.Pink, Brushes.Yellow, Brushes.Green, Brushes.Blue, Brushes.Brown, Brushes.Purple };
        private Pen[] pens = new Pen[7];
        private int maxValue;
        private int unit;
        private string[] legentLabel;



        public TubeTrendView()
        {
            InitializeComponent();

            for (int i = 0; i < brushes.Length; ++i)
            {
                pens[i] = new Pen(brushes[i], 1);
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            

            var axisPen = new Pen(Brushes.Black, 1);
            //draw x axis
            Point xAxisP1 = new Point(mMarginX1, mMarginY + mPlotHeight);
            Point xAxisP2 = new Point(mMarginX1 + mPlotWidth, mMarginY + mPlotHeight);
            drawingContext.DrawLine(axisPen, xAxisP1, xAxisP2);

            //draw y axis
            Point yAxisP1 = new Point(mMarginX1, mMarginY);
            Point yAxisP2 = new Point(mMarginX1, mMarginY + mPlotHeight);
            drawingContext.DrawLine(axisPen, yAxisP1, yAxisP2);

            var gridPen = new Pen(Brushes.LightBlue, 1);
            //draw x grid
            if (mModel != null && mModel.DataPoints != null)
            {
                for (int i = 0; i < unit; ++i)
                {
                    Point point0 = new Point(mMarginX1, mMarginY + mPlotHeight / unit * i);
                    Point point1 = new Point(mMarginX1 + mPlotWidth, mMarginY + mPlotHeight / unit * i);
                    drawingContext.DrawLine(gridPen, point0, point1);
                }
            }
            //draw y grid
            if (mModel != null && mModel.DataPoints != null)
            {
                for (int i = mModel.DataPoints.Count - 1; i >= 0; --i)
                {
                    if (i!= 0 && i % 30 == 0 || i == mModel.DataPoints.Count - 1)
                    {
                        Point point0 = new Point(mMarginX1 + mPlotWidth / mModel.DataPoints.Count * i, mMarginY + mPlotHeight);
                        Point point1 = new Point(i == mModel.DataPoints.Count - 1? mMarginX1 + mPlotWidth:mMarginX1 + mPlotWidth / mModel.DataPoints.Count * i, mMarginY);
                        drawingContext.DrawLine(gridPen, point0, point1);
                    }
                }
            }

            //draw x label
            if (mModel != null && mModel.DataPoints != null)
            {
                DateTime dataTimeNow = DateTime.Now;
                for (int i = mModel.DataPoints.Count - 1; i >= 0; --i)
                {
                    if (i % 60 == 0 || i == mModel.DataPoints.Count - 1)
                    {
                        FormattedText formattedText = new FormattedText(
                            string.Format("{0:HH:mm:ss}", dataTimeNow),
                            CultureInfo.GetCultureInfo("en-us"),
                            FlowDirection.LeftToRight,
                            new Typeface("Verdana"),
                            14,
                            Brushes.Black);
                        Point point0 = new Point(mMarginX1 + mPlotWidth / mModel.DataPoints.Count * i - formattedText.Width / 2, mMarginY + mPlotHeight + 5);
                        drawingContext.DrawText(formattedText, point0);
                        dataTimeNow = dataTimeNow.AddMinutes(-1);
                    }

                }
            }
            //draw y label
            if (mModel != null && mModel.DataPoints != null)
            {
                for (int i = 0; i < unit; ++i)
                {

                    FormattedText formattedText = new FormattedText(
                        string.Format("{0}", maxValue - maxValue / unit * i),
                        CultureInfo.GetCultureInfo("en-us"),
                        FlowDirection.LeftToRight,
                        new Typeface("Verdana"),
                        14,
                        Brushes.Black);
                    Point point0 = new Point(mMarginX1 - formattedText.Width - 5, mMarginY + mPlotHeight / unit * i - formattedText.Height / 2);
                    drawingContext.DrawText(formattedText, point0);
                }
            }

            //draw legent
            if (mModel != null && mModel.DataPoints != null)
            {
                Pen blackPen = new Pen(Brushes.Black, 1);
                for (int i = 0; i < legentLabel.Length; ++i)
                {

                    FormattedText formattedText = new FormattedText(
                        legentLabel[i],
                        CultureInfo.GetCultureInfo("en-us"),
                        FlowDirection.LeftToRight,
                        new Typeface("Verdana"),
                        14,
                        Brushes.Black);
                    Point point0 = new Point(mMarginX1 + mPlotWidth / 20 * (i*2+1) - formattedText.Width / 2, mMarginY + mPlotHeight + mLegentHeight - formattedText.Height/2);
                    drawingContext.DrawText(formattedText, point0);
                    Rect rect = new Rect(mMarginX1 + mPlotWidth / 20 * (i * 2 + 1) - formattedText.Width / 2 - 20 - 5, mMarginY + mPlotHeight + mLegentHeight - 10, 20, 20);
                    drawingContext.DrawRectangle(brushes[i], blackPen, rect);
                }
            }

            //draw data
            

            if (mModel != null && mModel.DataPoints != null)
            {
                for (int i = 0; i < mModel.DataPoints.Count - 1; ++i)
                {
                    for (int j = 0; j < mModel.DataPoints[i].Length; ++j)
                    {
                        Point point0 = new Point(mMarginX1 + mPlotWidth / mModel.DataPoints.Count * i, mMarginY + mPlotHeight - mPlotHeight / maxValue * mModel.DataPoints[i][j]);
                        Point point1 = new Point(mMarginX1 + mPlotWidth / mModel.DataPoints.Count * (i + 1), mMarginY + mPlotHeight - mPlotHeight / maxValue * mModel.DataPoints[i + 1][j]);
                        drawingContext.DrawLine(pens[j], point0, point1);
                    }

                }
            }

        }

        public void LoadView()
        {
            mPlotWidth = ActualWidth - mMarginX1 - mMarginX2;
            mPlotHeight = ActualHeight - 2 * mMarginY - mLegentHeight;

            if (mModel.PlotType == TrendPlotType.Temperature)
            {
                maxValue = 1200;
                unit = 12;
                legentLabel = mTemperLegentLabel;
            }
            else if (mModel.PlotType == TrendPlotType.Gas)
            {
                maxValue = 11000;
                unit = 11;
                legentLabel = mGasLegentLabel;
            }
            else if (mModel.PlotType == TrendPlotType.Vacuum)
            {
                maxValue = 1300;
                unit = 13;
                legentLabel = mVacuumLegentLabel;
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
