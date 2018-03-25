using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Demo.ui
{
    public class TubePageStyle
    {
        private Style mStyleTextBox;
        private Style mStyleLabel;
        private Setter mTextBoxWidthSetter;
        private Setter mTextBoxHeightSetter;
        private Setter mLabelWidthSetter;
        private Setter mLabelHeightSetter;

        public TubePageStyle()
        {
            mStyleTextBox = new Style(typeof(TextBox));
            mStyleLabel = new Style(typeof(Label));

            /*
            mTextBoxWidthSetter = new Setter
            {
                Property = TextBox.WidthProperty,
                Value = 60
            };
            mTextBoxHeightSetter = new Setter
            {
                Property = TextBox.HeightProperty,
                Value = 20
            };
            mStyleTextBox.Setters.Add(mTextBoxWidthSetter);
            mStyleTextBox.Setters.Add(mTextBoxHeightSetter);
            */

            mStyleTextBox.Setters.Add(new Setter
            {
                Property = TextBox.VerticalContentAlignmentProperty,
                Value = VerticalAlignment.Center
            });
            mStyleTextBox.Setters.Add(new Setter
            {
                Property = TextBox.HorizontalContentAlignmentProperty,
                Value = HorizontalAlignment.Center
            });
            mStyleTextBox.Setters.Add(new Setter
            {
                Property = TextBox.FontSizeProperty,
                Value = 13.0
            });
            Random r = new Random();
            mStyleTextBox.Setters.Add(new Setter
            {
                Property = TextBox.ForegroundProperty,
               
                Value = new SolidColorBrush(Color.FromRgb((byte)100, (byte)99, (byte)102))
            });

            mStyleTextBox.Setters.Add(new Setter
            {
                Property = TextBox.FontWeightProperty,
                Value = FontWeights.Bold
            });

            mStyleLabel.Setters.Add(new Setter
            {
                Property = TextBox.VerticalContentAlignmentProperty,
                Value = VerticalAlignment.Center
            });
            mStyleLabel.Setters.Add(new Setter
            {
                Property = TextBox.HorizontalContentAlignmentProperty,
                Value = HorizontalAlignment.Center
            });
            mStyleLabel.Setters.Add(new Setter
            {
                Property = TextBox.FontSizeProperty,
                Value = 13.0
            });
            mStyleLabel.Setters.Add(new Setter
            {
                Property = Label.BackgroundProperty,

                Value = new SolidColorBrush(Color.FromRgb((byte)255, (byte)255, (byte)255))
            });
            mStyleLabel.Setters.Add(new Setter
            {
                Property = Label.BorderThicknessProperty,

                Value = new Thickness(1, 1, 1, 1)
            });

            mStyleLabel.Setters.Add(new Setter
            {
                Property = Label.BorderBrushProperty,

                Value = new SolidColorBrush(Color.FromRgb((byte)0, (byte)0, (byte)0))
            });

            mStyleLabel.Setters.Add(new Setter
            {
                Property = Label.PaddingProperty,

                Value = new Thickness(3,0,3,0)
            });
        }

        public object TextBoxWidth
        {
            set
            {
                if (mTextBoxWidthSetter == null || !mTextBoxWidthSetter.IsSealed)
                {
                    //mTextBoxWidthSetter.Value = value;
                    mStyleTextBox.Setters.Remove(mTextBoxWidthSetter);
                    mTextBoxWidthSetter = new Setter
                    {
                        Property = TextBox.WidthProperty,
                        Value = value
                    };
                    mStyleTextBox.Setters.Add(mTextBoxWidthSetter);
                }
            }
        }
        public object TextBoxHeight
        {
            get { return mTextBoxHeightSetter.Value; }
            set
            {
                if (mTextBoxHeightSetter == null || !mTextBoxHeightSetter.IsSealed)
                {
                    //mTextBoxWidthSetter.Value = value;
                    mStyleTextBox.Setters.Remove(mTextBoxHeightSetter);
                    mTextBoxHeightSetter = new Setter
                    {
                        Property = TextBox.HeightProperty,
                        Value = value
                    };
                    mStyleTextBox.Setters.Add(mTextBoxHeightSetter);
                }
            }
            //set { if (!mTextBoxHeightSetter.IsSealed) mTextBoxHeightSetter.Value = value; }
        }

        public object LabelWidth
        {
            set
            {
                if (mLabelWidthSetter == null || !mLabelWidthSetter.IsSealed)
                {
                    //mTextBoxWidthSetter.Value = value;
                    mStyleLabel.Setters.Remove(mLabelWidthSetter);
                    mLabelWidthSetter = new Setter
                    {
                        Property = TextBox.WidthProperty,
                        Value = value
                    };
                    mStyleLabel.Setters.Add(mLabelWidthSetter);
                }
            }
        }
        public object LabelHeight
        {
            get { return mLabelHeightSetter.Value; }
            set
            {
                if (mLabelHeightSetter == null || !mLabelHeightSetter.IsSealed)
                {
                    //mTextBoxWidthSetter.Value = value;
                    mStyleLabel.Setters.Remove(mLabelHeightSetter);
                    mLabelHeightSetter = new Setter
                    {
                        Property = TextBox.HeightProperty,
                        Value = value
                    };
                    mStyleLabel.Setters.Add(mLabelHeightSetter);
                }
            }
        }

        public Style TextBoxStyle
        {
            get { return mStyleTextBox; }
        }

        public Style LabelStyle
        {
            get { return mStyleLabel; }
        }
    }


}
