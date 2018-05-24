using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Demo.model;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace Demo.repository
{
    public class DioevRepository : IDioevRepository
    {
        private String myConnectionString = "Server=localhost;Database=demo;Uid=root;Pwd=root;";
        

        public DioevRepository()
        {

            
        }

        public bool CreateDioev(Dioev dioevToCreate)
        {
            bool result = false;
            MySqlConnection connection;
            connection = new MySqlConnection(myConnectionString);
            try
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO dioev(TubeIndex) VALUES(?)";
                //cmd.Prepare();

                cmd.Parameters.Add("TubeIndex", MySqlDbType.Int32).Value = dioevToCreate.TubeIndex;

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

    public interface IDioevRepository
    {
        bool CreateDioev(Dioev DioevToCreate);
    }
}
