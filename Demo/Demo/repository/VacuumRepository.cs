using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Demo.model;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace Demo.repository
{
    public class VacuumRepository : IVacuumRepository
    {
        private String myConnectionString = "Server=localhost;Database=demo;Uid=root;Pwd=root;";
        

        public VacuumRepository()
        {

            
        }

        public bool CreateVacuum(Vacuum vacuumToCreate)
        {
            bool result = false;
            MySqlConnection connection;
            connection = new MySqlConnection(myConnectionString);
            try
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO vacuum(TubeIndex) VALUES(?)";
                //cmd.Prepare();

                cmd.Parameters.Add("TubeIndex", MySqlDbType.Int32).Value = vacuumToCreate.TubeIndex;

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

    public interface IVacuumRepository
    {
        bool CreateVacuum(Vacuum vacuumToCreate);
    }
}
