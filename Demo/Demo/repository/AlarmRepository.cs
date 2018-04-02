using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Demo.model;
using MySql.Data.MySqlClient;

namespace Demo.repository
{
    public class AlarmRepository : IAlarmRepository
    {
        private String myConnectionString = "Server=localhost;Database=RPB66;Uid=root;Pwd=root;";
        

        public AlarmRepository()
        {

            
        }

        public List<Alarm> ListAlarms(byte selectedTube)
        {
            List<Alarm> alarmLst;
            MySqlConnection connection;
            connection = new MySqlConnection(myConnectionString);
            try
            {

                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = string.Format("select ID, EVENT_TYPE, EVENT_DESC, EVENT_TIME from event where TUBE_NUM = {0} order by EVENT_TIME desc limit 20", selectedTube);
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adap.Fill(ds);
                //this.clothesListView.DataContext = ds.Tables[0].DefaultView;
                alarmLst = new List<Alarm>();
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    string t = r["ID"].ToString();
                    string d = r["EVENT_DESC"].ToString();
                    Alarm alarm = new Alarm();

                    alarmLst.Add(alarm);
                }
           
 
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();

                }
            }
            return alarmLst;
        }

        public bool CreateAlarm(Alarm alarmToCreate)
        {
            try
            {

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateAlarm(Alarm alarmToUpdate)
        {
            try
            {

                return true;
            }
            catch
            {
                return false;
            }
        }

    }

    public interface IAlarmRepository
    {
        bool CreateAlarm(Alarm alarmToCreate);

        bool UpdateAlarm(Alarm alarmToUpdate);

        List<Alarm> ListAlarms(byte selectedTube);
    }
}
