using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Demo.model;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

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
                MySqlDataReader myReader = null;
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = string.Format("select ID, ErrorCode from alarm where TubeIndex = {0} And Type = 0 order by StartTime desc limit 20", selectedTube);
                myReader = cmd.ExecuteReader();
                alarmLst = new List<Alarm>();
                while (myReader.Read())
                {
                    Alarm alarm = new Alarm();
                    alarm.ID = myReader.GetInt32("ID");
                    alarm.ErrorCode = (string)myReader["ErrorCode"];
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

        public Alarm FindAlarm(int tubeIndex, string errorCode)
        {
            Alarm alarm = null;
            MySqlConnection connection;
            connection = new MySqlConnection(myConnectionString);
            try
            {

                connection.Open();
                MySqlDataReader myReader = null;
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = string.Format("select ID, Type, StartTime, EndTime from alarm where TubeIndex = {0} And ErrorCode = {1} AND Type = 0", tubeIndex, errorCode);
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    alarm = new Alarm();
                    alarm.ID = myReader.GetInt32("ID");
                    alarm.Type = (int)myReader["Type"];                   
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
            return alarm;
        }

        public bool CreateAlarm(Alarm alarmToCreate)
        {
            bool result = false;
            MySqlConnection connection;
            connection = new MySqlConnection(myConnectionString);
            try
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO alarm(TubeIndex,ErrorCode) VALUES(?,?)";
                //cmd.Prepare();

                cmd.Parameters.Add("TubeIndex", MySqlDbType.Int32).Value = alarmToCreate.TubeIndex;
                cmd.Parameters.Add("ErrorCode", MySqlDbType.VarChar).Value = alarmToCreate.ErrorCode;
                cmd.ExecuteNonQuery(); // HERE I GOT AN EXCEPTION IN THIS LINE
                result = true;
            }
            catch(Exception e)
            {
                result = false;
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return result;
        }

        public bool UpdateAlarm(int tubeIndex)
        {
            bool result = false;
            MySqlConnection connection;
            connection = new MySqlConnection(myConnectionString);
            try
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "UPDATE alarm SET Type = @Type WHERE TubeIndex = @TubeIndex";
                //cmd.Prepare();

                cmd.Parameters.Add("@Type", MySqlDbType.Int32).Value = 1;
                cmd.Parameters.Add("@TubeIndex", MySqlDbType.Int32).Value = tubeIndex;
                cmd.ExecuteNonQuery(); // HERE I GOT AN EXCEPTION IN THIS LINE
                result = true;
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return result;
        }

    }

    public interface IAlarmRepository
    {
        bool CreateAlarm(Alarm alarmToCreate);

        bool UpdateAlarm(int tubeIndex);

        List<Alarm> ListAlarms(byte selectedTube);

        Alarm FindAlarm(int tubeIndex, string errorCode);
    }
}
