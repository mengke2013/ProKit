using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Demo.ui
{
    interface ITubePage
    {
        void LoadPage(byte selectedTube);

        UserControl UI();
    }
}
