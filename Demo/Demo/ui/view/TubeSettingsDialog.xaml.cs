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
using log4net;
using Demo.ui.model;
using Demo.utilities;

namespace Demo.ui.view
{
    /// <summary>
    /// Interaction logic for TubeSettingsDialog.xaml
    /// </summary>
    public partial class TubeSettingsDialog : Window
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private byte mSelectedTube;
        private TubeSettingsViewModel mSettingsModel;

        public TubeSettingsDialog(byte selectedTube)
        {
            InitializeComponent();

            mSelectedTube = selectedTube;
            mSettingsModel = new TubeSettingsViewModel();
        }

        private void Read_Click(object sender, RoutedEventArgs e)
        {
            log.Debug("read configuration");
            Demo.utilities.Properties config = new Demo.utilities.Properties("configuration.properties");
            mSettingsModel.Gas1MaxValue = config.get("Gas1MaxValue");
            mSettingsModel.Gas2MaxValue = config.get("Gas2MaxValue");
            mSettingsModel.Gas5MaxValue = config.get("Gas5MaxValue");
            mSettingsModel.Gas6MaxValue = config.get("Gas6MaxValue");
            mSettingsModel.Gas8MaxValue = config.get("Gas8MaxValue");
            mSettingsModel.Ana1MaxValue = config.get("Ana1MaxValue");
            mSettingsModel.Ana3MaxValue = config.get("Ana3MaxValue");
            mSettingsModel.Ana4MaxValue = config.get("Ana4MaxValue");
            mSettingsModel.Ana5MaxValue = config.get("Ana5MaxValue");
            mSettingsModel.Ana6MaxValue = config.get("Ana6MaxValue");
            mSettingsModel.VacuumKp = config.get("VacuumKp");
            mSettingsModel.VacuumTn = config.get("VacuumTn");
            mSettingsModel.VacuumTd = config.get("VacuumTd");
            mSettingsModel.Gas1Ev = config.get("Gas1Ev");
            mSettingsModel.Gas2Ev = config.get("Gas2Ev");
            mSettingsModel.Gas5Ev = config.get("Gas5Ev");
            mSettingsModel.Gas6Ev = config.get("Gas6Ev");
            mSettingsModel.Gas8Ev = config.get("Gas8Ev");
            mSettingsModel.TemperIntKp1 = config.get("TemperIntKp1");
            mSettingsModel.TemperIntKp2 = config.get("TemperIntKp2");
            mSettingsModel.TemperIntKp3 = config.get("TemperIntKp3");
            mSettingsModel.TemperIntKp4 = config.get("TemperIntKp4");
            mSettingsModel.TemperIntKp5 = config.get("TemperIntKp5");
            mSettingsModel.TemperIntKp6 = config.get("TemperIntKp6");
            mSettingsModel.TemperIntTn1 = config.get("TemperIntTn1");
            mSettingsModel.TemperIntTn2 = config.get("TemperIntTn2");
            mSettingsModel.TemperIntTn3 = config.get("TemperIntTn3");
            mSettingsModel.TemperIntTn4 = config.get("TemperIntTn4");
            mSettingsModel.TemperIntTn5 = config.get("TemperIntTn5");
            mSettingsModel.TemperIntTn6 = config.get("TemperIntTn6");
            mSettingsModel.TemperIntTd1 = config.get("TemperIntTd1");
            mSettingsModel.TemperIntTd2 = config.get("TemperIntTd2");
            mSettingsModel.TemperIntTd3 = config.get("TemperIntTd3");
            mSettingsModel.TemperIntTd4 = config.get("TemperIntTd4");
            mSettingsModel.TemperIntTd5 = config.get("TemperIntTd5");
            mSettingsModel.TemperIntTd6 = config.get("TemperIntTd6");
            mSettingsModel.TemperExtKp1 = config.get("TemperExtKp1");
            mSettingsModel.TemperExtKp2 = config.get("TemperExtKp2");
            mSettingsModel.TemperExtKp3 = config.get("TemperExtKp3");
            mSettingsModel.TemperExtKp4 = config.get("TemperExtKp4");
            mSettingsModel.TemperExtKp5 = config.get("TemperExtKp5");
            mSettingsModel.TemperExtKp6 = config.get("TemperExtKp6");
            mSettingsModel.TemperExtTn1 = config.get("TemperExtTn1");
            mSettingsModel.TemperExtTn2 = config.get("TemperExtTn2");
            mSettingsModel.TemperExtTn3 = config.get("TemperExtTn3");
            mSettingsModel.TemperExtTn4 = config.get("TemperExtTn4");
            mSettingsModel.TemperExtTn5 = config.get("TemperExtTn5");
            mSettingsModel.TemperExtTn6 = config.get("TemperExtTn6");
            mSettingsModel.TemperExtTd1 = config.get("TemperExtTd1");
            mSettingsModel.TemperExtTd2 = config.get("TemperExtTd2");
            mSettingsModel.TemperExtTd3 = config.get("TemperExtTd3");
            mSettingsModel.TemperExtTd4 = config.get("TemperExtTd4");
            mSettingsModel.TemperExtTd5 = config.get("TemperExtTd5");
            mSettingsModel.TemperExtTd6 = config.get("TemperExtTd6");
            mSettingsModel.MaxPressure = config.get("MaxPressure");
            mSettingsModel.MinPressure = config.get("MinPressure");
            mSettingsModel.MaxTemper = config.get("MaxTemper");
            mSettingsModel.MaxTemper5 = config.get("MaxTemper5");
            mSettingsModel.MinTemper5 = config.get("MinTemper5");
            mSettingsModel.MaxPump = config.get("MaxPump");
            mSettingsModel.DI1 = config.get("DI1");
            mSettingsModel.DI2 = config.get("DI2");
            mSettingsModel.DI3 = config.get("DI3");
            mSettingsModel.DI4 = config.get("DI4");
            mSettingsModel.DI5 = config.get("DI5");
            mSettingsModel.DI6 = config.get("DI6");
            mSettingsModel.DI7 = config.get("DI7");
            mSettingsModel.DI8 = config.get("DI8");
            mSettingsModel.DI9 = config.get("DI9");
            mSettingsModel.DI10 = config.get("DI10");
            mSettingsModel.DI11 = config.get("DI11");
            mSettingsModel.DI12 = config.get("DI12");
            mSettingsModel.DI13 = config.get("DI13");
            mSettingsModel.DI14 = config.get("DI14");
            mSettingsModel.DI15 = config.get("DI15");
            mSettingsModel.Offset = config.get("Offset");
            mSettingsModel.PositionDev = config.get("PositionDev");
            mSettingsModel.ClosePosition = config.get("ClosePosition");
            this.DataContext = mSettingsModel;
        }

        private void Write_Click(object sender, RoutedEventArgs e)
        {
            log.Debug("write configuration");
            Demo.utilities.Properties config = new Demo.utilities.Properties("configuration.properties");
            config.set("Gas1MaxValue", mSettingsModel.Gas1MaxValue);
            config.set("Gas2MaxValue", mSettingsModel.Gas2MaxValue);
            config.set("Gas5MaxValue", mSettingsModel.Gas5MaxValue);
            config.set("Gas6MaxValue", mSettingsModel.Gas6MaxValue);
            config.set("Gas8MaxValue", mSettingsModel.Gas8MaxValue);
            config.set("Ana1MaxValue", mSettingsModel.Ana1MaxValue);
            config.set("Ana3MaxValue", mSettingsModel.Ana3MaxValue);
            config.set("Ana4MaxValue", mSettingsModel.Ana4MaxValue);
            config.set("Ana5MaxValue", mSettingsModel.Ana5MaxValue);
            config.set("Ana6MaxValue", mSettingsModel.Ana6MaxValue);
            config.set("VacuumKp", mSettingsModel.VacuumKp);
            config.set("VacuumTn", mSettingsModel.VacuumTn);
            config.set("VacuumTd", mSettingsModel.VacuumTd);
            config.set("Gas1Ev", mSettingsModel.Gas1Ev);
            config.set("Gas2Ev", mSettingsModel.Gas2Ev);
            config.set("Gas5Ev", mSettingsModel.Gas5Ev);
            config.set("Gas6Ev", mSettingsModel.Gas6Ev);
            config.set("Gas8Ev", mSettingsModel.Gas8Ev);
            config.set("TemperIntKp1", mSettingsModel.TemperIntKp1);
            config.set("TemperIntKp2", mSettingsModel.TemperIntKp2);
            config.set("TemperIntKp3", mSettingsModel.TemperIntKp3);
            config.set("TemperIntKp4", mSettingsModel.TemperIntKp4);
            config.set("TemperIntKp5", mSettingsModel.TemperIntKp5);
            config.set("TemperIntKp6", mSettingsModel.TemperIntKp6);
            config.set("TemperIntTn1", mSettingsModel.TemperIntTn1);
            config.set("TemperIntTn2", mSettingsModel.TemperIntTn2);
            config.set("TemperIntTn3", mSettingsModel.TemperIntTn3);
            config.set("TemperIntTn4", mSettingsModel.TemperIntTn4);
            config.set("TemperIntTn5", mSettingsModel.TemperIntTn5);
            config.set("TemperIntTn6", mSettingsModel.TemperIntTn6);
            config.set("TemperIntTd1", mSettingsModel.TemperIntTd1);
            config.set("TemperIntTd2", mSettingsModel.TemperIntTd2);
            config.set("TemperIntTd3", mSettingsModel.TemperIntTd3);
            config.set("TemperIntTd4", mSettingsModel.TemperIntTd4);
            config.set("TemperIntTd5", mSettingsModel.TemperIntTd5);
            config.set("TemperIntTd6", mSettingsModel.TemperIntTd6);
            config.set("TemperExtKp1", mSettingsModel.TemperExtKp1);
            config.set("TemperExtKp2", mSettingsModel.TemperExtKp2);
            config.set("TemperExtKp3", mSettingsModel.TemperExtKp3);
            config.set("TemperExtKp4", mSettingsModel.TemperExtKp4);
            config.set("TemperExtKp5", mSettingsModel.TemperExtKp5);
            config.set("TemperExtKp6", mSettingsModel.TemperExtKp6);
            config.set("TemperExtTn1", mSettingsModel.TemperExtTn1);
            config.set("TemperExtTn2", mSettingsModel.TemperExtTn2);
            config.set("TemperExtTn3", mSettingsModel.TemperExtTn3);
            config.set("TemperExtTn4", mSettingsModel.TemperExtTn4);
            config.set("TemperExtTn5", mSettingsModel.TemperExtTn5);
            config.set("TemperExtTn6", mSettingsModel.TemperExtTn6);
            config.set("TemperExtTd1", mSettingsModel.TemperExtTd1);
            config.set("TemperExtTd2", mSettingsModel.TemperExtTd2);
            config.set("TemperExtTd3", mSettingsModel.TemperExtTd3);
            config.set("TemperExtTd4", mSettingsModel.TemperExtTd4);
            config.set("TemperExtTd5", mSettingsModel.TemperExtTd5);
            config.set("TemperExtTd6", mSettingsModel.TemperExtTd6);
            config.set("MaxPressure", mSettingsModel.MaxPressure);
            config.set("MinPressure", mSettingsModel.MinPressure);
            config.set("MaxTemper", mSettingsModel.MaxTemper);
            config.set("MaxTemper5", mSettingsModel.MaxTemper5);
            config.set("MinTemper5", mSettingsModel.MinTemper5);
            config.set("MaxPump", mSettingsModel.MaxPump);
            config.set("DI1", mSettingsModel.DI1);
            config.set("DI2", mSettingsModel.DI2);
            config.set("DI3", mSettingsModel.DI3);
            config.set("DI4", mSettingsModel.DI4);
            config.set("DI5", mSettingsModel.DI5);
            config.set("DI6", mSettingsModel.DI6);
            config.set("DI7", mSettingsModel.DI7);
            config.set("DI8", mSettingsModel.DI8);
            config.set("DI9", mSettingsModel.DI9);
            config.set("DI10", mSettingsModel.DI10);
            config.set("DI11", mSettingsModel.DI11);
            config.set("DI12", mSettingsModel.DI12);
            config.set("DI13", mSettingsModel.DI13);
            config.set("DI14", mSettingsModel.DI14);
            config.set("DI15", mSettingsModel.DI15);
            config.set("Offset", mSettingsModel.Offset);
            config.set("PositionDev", mSettingsModel.PositionDev);
            config.set("ClosePosition", mSettingsModel.ClosePosition);
            config.Save();
        }
    }
}
