using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Demo.model;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace Demo.repository
{
    public class TemperRepository : ITemperRepository
    {
        private String myConnectionString = "Server=localhost;Database=Demo;Uid=root;Pwd=root;";
        

        public TemperRepository()
        {

            
        }

        public bool CreateTemperature(Temperature temperToCreate)
        {
            bool result = false;
            MySqlConnection connection;
            connection = new MySqlConnection(myConnectionString);
            try
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO temperature(TubeIndex) VALUES(?)";
                //cmd.Prepare();

                cmd.Parameters.Add("TubeIndex", MySqlDbType.Int32).Value = temperToCreate.TubeIndex;
                
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

    public interface ITemperRepository
    {
        bool CreateTemperature(Temperature temperToCreate);
    }


}
