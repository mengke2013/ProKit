using Demo.ui.view;
using Demo.ui.model;
using Demo.model;
using Demo.service;


namespace Demo.controller
{
    public class SettingsController
    {
        TubeSettingsPage mPage;

        public SettingsController(TubeSettingsPage page)
        {
            mPage = page;
        }

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


        public void ConvertSettingsPageModel(byte tubeIndex)
        {
            Settings settings = SettingsService.Instance.GetSettings();
            TubeSettingsViewModel settingsPage = mPage.SettingsModel;
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

        public void ConvertSettingsModel(byte tubeIndex)
        {
            Settings settings = SettingsService.Instance.GetSettings();
            TubeSettingsViewModel settingsPage = mPage.SettingsModel;
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

            settings.Gas1Name = settingsPage.Gas1Name;
            settings.Gas2Name = settingsPage.Gas2Name;
            settings.Gas5Name = settingsPage.Gas5Name;
            settings.Gas6Name = settingsPage.Gas6Name;
            settings.Gas8Name = settingsPage.Gas8Name;
            settings.Ana1Name = settingsPage.Ana1Name;
            settings.Ana3Name = settingsPage.Ana3Name;
            settings.Ana4Name = settingsPage.Ana4Name;
            settings.Ana5Name = settingsPage.Ana5Name;
            settings.Ana6Name = settingsPage.Ana6Name;

            settings.EvNames[0] = settingsPage.EvName1;
            settings.EvNames[1] = settingsPage.EvName2;
            settings.EvNames[2] = settingsPage.EvName3;
            settings.EvNames[3] = settingsPage.EvName4;
            settings.EvNames[4] = settingsPage.EvName5;
            settings.EvNames[5] = settingsPage.EvName6;
            settings.EvNames[6] = settingsPage.EvName7;
            settings.EvNames[7] = settingsPage.EvName8;
            settings.EvNames[8] = settingsPage.EvName9;
            settings.EvNames[9] = settingsPage.EvName10;
            settings.EvNames[10] = settingsPage.EvName11;
            settings.EvNames[11] = settingsPage.EvName12;
            settings.EvNames[12] = settingsPage.EvName13;
            settings.EvNames[13] = settingsPage.EvName14;
            settings.EvNames[14] = settingsPage.EvName15;
            settings.EvNames[15] = settingsPage.EvName16;
            settings.EvNames[16] = settingsPage.EvName17;
            settings.EvNames[17] = settingsPage.EvName18;
            settings.EvNames[18] = settingsPage.EvName19;
            settings.EvNames[19] = settingsPage.EvName20;
            settings.EvNames[20] = settingsPage.EvName21;
            settings.EvNames[21] = settingsPage.EvName22;
            settings.EvNames[22] = settingsPage.EvName23;
            settings.EvNames[23] = settingsPage.EvName24;
            settings.EvNames[24] = settingsPage.EvName25;
            settings.EvNames[25] = settingsPage.EvName26;
            settings.EvNames[26] = settingsPage.EvName27;
            settings.EvNames[27] = settingsPage.EvName28;
            settings.EvNames[28] = settingsPage.EvName29;
            settings.EvNames[29] = settingsPage.EvName30;
            settings.EvNames[30] = settingsPage.EvName31;
            settings.EvNames[31] = settingsPage.EvName32;

            settings.DiNames[0] = settingsPage.DiName1;
            settings.DiNames[1] = settingsPage.DiName2;
            settings.DiNames[2] = settingsPage.DiName3;
            settings.DiNames[3] = settingsPage.DiName4;
            settings.DiNames[4] = settingsPage.DiName5;
            settings.DiNames[5] = settingsPage.DiName6;
            settings.DiNames[6] = settingsPage.DiName7;
            settings.DiNames[7] = settingsPage.DiName8;
            settings.DiNames[8] = settingsPage.DiName9;
            settings.DiNames[9] = settingsPage.DiName10;
            settings.DiNames[10] = settingsPage.DiName11;
            settings.DiNames[11] = settingsPage.DiName12;
            settings.DiNames[12] = settingsPage.DiName13;
            settings.DiNames[13] = settingsPage.DiName14;
            settings.DiNames[14] = settingsPage.DiName15;
            settings.DiNames[15] = settingsPage.DiName16;
            settings.DiNames[16] = settingsPage.DiName17;
            settings.DiNames[17] = settingsPage.DiName18;
            settings.DiNames[18] = settingsPage.DiName19;
            settings.DiNames[19] = settingsPage.DiName20;
            settings.DiNames[20] = settingsPage.DiName21;
            settings.DiNames[21] = settingsPage.DiName22;
            settings.DiNames[22] = settingsPage.DiName23;
            settings.DiNames[23] = settingsPage.DiName24;
            settings.DiNames[24] = settingsPage.DiName25;
            settings.DiNames[25] = settingsPage.DiName26;
            settings.DiNames[26] = settingsPage.DiName27;
            settings.DiNames[27] = settingsPage.DiName28;
            settings.DiNames[28] = settingsPage.DiName29;
            settings.DiNames[29] = settingsPage.DiName30;
            settings.DiNames[30] = settingsPage.DiName31;
            settings.DiNames[31] = settingsPage.DiName32;

            settings.DoNames[0] = settingsPage.DoName1;
            settings.DoNames[1] = settingsPage.DoName2;
            settings.DoNames[2] = settingsPage.DoName3;
            settings.DoNames[3] = settingsPage.DoName4;
            settings.DoNames[4] = settingsPage.DoName5;
            settings.DoNames[5] = settingsPage.DoName6;
            settings.DoNames[6] = settingsPage.DoName7;
            settings.DoNames[7] = settingsPage.DoName8;
            settings.DoNames[8] = settingsPage.DoName9;
            settings.DoNames[9] = settingsPage.DoName10;
            settings.DoNames[10] = settingsPage.DoName11;
            settings.DoNames[11] = settingsPage.DoName12;
            settings.DoNames[12] = settingsPage.DoName13;
            settings.DoNames[13] = settingsPage.DoName14;
            settings.DoNames[14] = settingsPage.DoName15;
            settings.DoNames[15] = settingsPage.DoName16;
        }
    }
}
