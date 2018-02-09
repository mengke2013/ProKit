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
    class TubePageStyle
    {
        private Style mStyleTextBox;
        private Setter mTextBoxWidthSetter;
        private Setter mTextBoxHeightSetter;

        public TubePageStyle()
        {
            mStyleTextBox = new Style(typeof(TextBox));

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

        public Style TextBoxStyle
        {
            get { return mStyleTextBox; }
        }
    }


}
