using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.ui.model;
using Demo.model;
using Demo.service;


namespace Demo.controller
{
    public class SettingsController
    {
        public Settings LoadSettings(byte tubeIndex)
        {
            //add validation
            Settings settings = SettingsService.Instance.LoadSettings(tubeIndex);
            return settings;
        }

        public bool SynSettings(byte tubeIndex, SettingsService.OnSynSettingsComplete callback)
        {
            //add validation
            bool startSyn = SettingsService.Instance.SynSettings(tubeIndex, callback);
            return startSyn;
        }

        public bool DownloadSettings(string fileName, byte tubeIndex, SettingsService.OnDownloadSettingsComplete callback)
        {
            //add validation
            bool startDownload = SettingsService.Instance.DownloadSettings(fileName, tubeIndex, callback);
            return startDownload;
        }

        public bool CommitSettings(byte tubeIndex, SettingsService.OnDownloadSettingsComplete callback)
        {
            //add validation
            bool startCommit = SettingsService.Instance.CommitSettings(tubeIndex, callback);
            return startCommit;
        }

        public bool BackupSettings(string fileName, byte tubeIndex, SettingsService.OnBackupSettingsComplete callback)
        {
            //add validation
            bool startBackup = SettingsService.Instance.BackupSettings(fileName, tubeIndex, callback);
            return startBackup;
        }

        public Settings GetSettings(byte tubeIndex)
        {
            //add validation
            Settings settings = SettingsService.Instance.GetSettings();
            return settings;
        }


        public void ConvertSettingsPageModel(TubeSettingsViewModel settingsPage, Settings settings)
        {
            settingsPage.Gas1MaxValue = settings.Gas1MaxValue;
            settingsPage.Gas2MaxValue = settings.Gas2MaxValue;
            settingsPage.Gas5MaxValue = settings.Gas5MaxValue;
            settingsPage.Gas6MaxValue = settings.Gas6MaxValue;
            settingsPage.Gas8MaxValue = settings.Gas8MaxValue;

            settingsPage.Gas1Ev = settings.Gas1Ev;
            settingsPage.Gas2Ev = settings.Gas2Ev;
            settingsPage.Gas5Ev = settings.Gas5Ev;
            settingsPage.Gas6Ev = settings.Gas6Ev;
            settingsPage.Gas8Ev = settings.Gas8Ev;

            settingsPage.Ana1MaxValue = settings.Ana1MaxValue;
            settingsPage.Ana3MaxValue = settings.Ana3MaxValue;
            settingsPage.Ana4MaxValue = settings.Ana4MaxValue;
            settingsPage.Ana5MaxValue = settings.Ana5MaxValue;
            settingsPage.Ana6MaxValue = settings.Ana6MaxValue;

            settingsPage.TemperIntKp1 = settings.TemperIntKp1;
            settingsPage.TemperIntKp2 = settings.TemperIntKp2;
            settingsPage.TemperIntKp3 = settings.TemperIntKp3;
            settingsPage.TemperIntKp4 = settings.TemperIntKp4;
            settingsPage.TemperIntKp5 = settings.TemperIntKp5;
            settingsPage.TemperIntKp6 = settings.TemperIntKp6;
            settingsPage.TemperIntTn1 = settings.TemperIntTn1;
            settingsPage.TemperIntTn2 = settings.TemperIntTn2;
            settingsPage.TemperIntTn3 = settings.TemperIntTn3;
            settingsPage.TemperIntTn4 = settings.TemperIntTn4;
            settingsPage.TemperIntTn5 = settings.TemperIntTn5;
            settingsPage.TemperIntTn6 = settings.TemperIntTn6;
            settingsPage.TemperIntTd1 = settings.TemperIntTd1;
            settingsPage.TemperIntTd2 = settings.TemperIntTd2;
            settingsPage.TemperIntTd3 = settings.TemperIntTd3;
            settingsPage.TemperIntTd4 = settings.TemperIntTd4;
            settingsPage.TemperIntTd5 = settings.TemperIntTd5;
            settingsPage.TemperIntTd6 = settings.TemperIntTd6;

            settingsPage.TemperExtKp1 = settings.TemperExtKp1;
            settingsPage.TemperExtKp2 = settings.TemperExtKp2;
            settingsPage.TemperExtKp3 = settings.TemperExtKp3;
            settingsPage.TemperExtKp4 = settings.TemperExtKp4;
            settingsPage.TemperExtKp5 = settings.TemperExtKp5;
            settingsPage.TemperExtKp6 = settings.TemperExtKp6;
            settingsPage.TemperExtTn1 = settings.TemperExtTn1;
            settingsPage.TemperExtTn2 = settings.TemperExtTn2;
            settingsPage.TemperExtTn3 = settings.TemperExtTn3;
            settingsPage.TemperExtTn4 = settings.TemperExtTn4;
            settingsPage.TemperExtTn5 = settings.TemperExtTn5;
            settingsPage.TemperExtTn6 = settings.TemperExtTn6;
            settingsPage.TemperExtTd1 = settings.TemperExtTd1;
            settingsPage.TemperExtTd2 = settings.TemperExtTd2;
            settingsPage.TemperExtTd3 = settings.TemperExtTd3;
            settingsPage.TemperExtTd4 = settings.TemperExtTd4;
            settingsPage.TemperExtTd5 = settings.TemperExtTd5;
            settingsPage.TemperExtTd6 = settings.TemperExtTd6;

            settingsPage.VacuumKp = settings.VacuumKp;
            settingsPage.VacuumTn = settings.VacuumTn;
            settingsPage.VacuumTd = settings.VacuumTd;

            settingsPage.MaxPressure = settings.MaxPressure;
            settingsPage.MinPressure = settings.MinPressure;
            settingsPage.MaxTemper = settings.MaxTemper;
            settingsPage.MaxTemper5 = settings.MaxTemper5;
            settingsPage.MinTemper5 = settings.MinTemper5;
            settingsPage.MaxPump = settings.MaxPump;

            settingsPage.Offset = settings.Offset;
            settingsPage.PositionDev = settings.PositionDev;
            settingsPage.ClosePosition = settings.ClosePosition;

            settingsPage.Di = settings.Di;
        }

        public void ConvertSettingsModel(Settings settings, TubeSettingsViewModel settingsPage)
        {
            settings.Gas1MaxValue = settingsPage.Gas1MaxValue;
            settings.Gas2MaxValue = settingsPage.Gas2MaxValue;
            settings.Gas5MaxValue = settingsPage.Gas5MaxValue;
            settings.Gas6MaxValue = settingsPage.Gas6MaxValue;
            settings.Gas8MaxValue = settingsPage.Gas8MaxValue;

            settings.Gas1Ev = settingsPage.Gas1Ev;
            settings.Gas2Ev = settingsPage.Gas2Ev;
            settings.Gas5Ev = settingsPage.Gas5Ev;
            settings.Gas6Ev = settingsPage.Gas6Ev;
            settings.Gas8Ev = settingsPage.Gas8Ev;

            settings.Ana1MaxValue = settingsPage.Ana1MaxValue;
            settings.Ana3MaxValue = settingsPage.Ana3MaxValue;
            settings.Ana4MaxValue = settingsPage.Ana4MaxValue;
            settings.Ana5MaxValue = settingsPage.Ana5MaxValue;
            settings.Ana6MaxValue = settingsPage.Ana6MaxValue;

            settings.TemperIntKp1 = settingsPage.TemperIntKp1;
            settings.TemperIntKp2 = settingsPage.TemperIntKp2;
            settings.TemperIntKp3 = settingsPage.TemperIntKp3;
            settings.TemperIntKp4 = settingsPage.TemperIntKp4;
            settings.TemperIntKp5 = settingsPage.TemperIntKp5;
            settings.TemperIntKp6 = settingsPage.TemperIntKp6;
            settings.TemperIntTn1 = settingsPage.TemperIntTn1;
            settings.TemperIntTn2 = settingsPage.TemperIntTn2;
            settings.TemperIntTn3 = settingsPage.TemperIntTn3;
            settings.TemperIntTn4 = settingsPage.TemperIntTn4;
            settings.TemperIntTn5 = settingsPage.TemperIntTn5;
            settings.TemperIntTn6 = settingsPage.TemperIntTn6;
            settings.TemperIntTd1 = settingsPage.TemperIntTd1;
            settings.TemperIntTd2 = settingsPage.TemperIntTd2;
            settings.TemperIntTd3 = settingsPage.TemperIntTd3;
            settings.TemperIntTd4 = settingsPage.TemperIntTd4;
            settings.TemperIntTd5 = settingsPage.TemperIntTd5;
            settings.TemperIntTd6 = settingsPage.TemperIntTd6;

            settings.TemperExtKp1 = settingsPage.TemperExtKp1;
            settings.TemperExtKp2 = settingsPage.TemperExtKp2;
            settings.TemperExtKp3 = settingsPage.TemperExtKp3;
            settings.TemperExtKp4 = settingsPage.TemperExtKp4;
            settings.TemperExtKp5 = settingsPage.TemperExtKp5;
            settings.TemperExtKp6 = settingsPage.TemperExtKp6;
            settings.TemperExtTn1 = settingsPage.TemperExtTn1;
            settings.TemperExtTn2 = settingsPage.TemperExtTn2;
            settings.TemperExtTn3 = settingsPage.TemperExtTn3;
            settings.TemperExtTn4 = settingsPage.TemperExtTn4;
            settings.TemperExtTn5 = settingsPage.TemperExtTn5;
            settings.TemperExtTn6 = settingsPage.TemperExtTn6;
            settings.TemperExtTd1 = settingsPage.TemperExtTd1;
            settings.TemperExtTd2 = settingsPage.TemperExtTd2;
            settings.TemperExtTd3 = settingsPage.TemperExtTd3;
            settings.TemperExtTd4 = settingsPage.TemperExtTd4;
            settings.TemperExtTd5 = settingsPage.TemperExtTd5;
            settings.TemperExtTd6 = settingsPage.TemperExtTd6;

            settings.VacuumKp = settingsPage.VacuumKp;
            settings.VacuumTn = settingsPage.VacuumTn;
            settings.VacuumTd = settingsPage.VacuumTd;

            settings.MaxPressure = settingsPage.MaxPressure;
            settings.MinPressure = settingsPage.MinPressure;
            settings.MaxTemper = settingsPage.MaxTemper;
            settings.MaxTemper5 = settingsPage.MaxTemper5;
            settings.MinTemper5 = settingsPage.MinTemper5;
            settings.MaxPump = settingsPage.MaxPump;

            settings.Offset = settingsPage.Offset;
            settings.PositionDev = settingsPage.PositionDev;
            settings.ClosePosition = settingsPage.ClosePosition;

            settings.Di = settingsPage.Di;
        }
    }
}
