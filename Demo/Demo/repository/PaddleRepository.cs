using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Demo.model;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace Demo.repository
{
    public class PaddleRepository : IPaddleRepository
    {
        private String myConnectionString = "Server=localhost;Database=demo;Uid=root;Pwd=root;";
        

        public PaddleRepository()
        {

            
        }

        public bool CreatePaddle(Paddle paddleToCreate)
        {
            bool result = false;
            MySqlConnection connection;
            connection = new MySqlConnection(myConnectionString);
            try
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO paddle(TubeIndex,PaddlePosAct,PaddlePosSp, PaddleSpeedSp) VALUES(?,?,?,?)";
                //cmd.Prepare();

                cmd.Parameters.Add("TubeIndex", MySqlDbType.Int32).Value = paddleToCreate.TubeIndex;
                cmd.Parameters.Add("PaddlePosAct", MySqlDbType.Int32).Value = paddleToCreate.PaddlePosAct;
                cmd.Parameters.Add("PaddlePosSp", MySqlDbType.Int32).Value = paddleToCreate.PaddlePosSp;
                cmd.Parameters.Add("PaddleSpeedSp", MySqlDbType.Int32).Value = paddleToCreate.PaddleSpeedSp;

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

    public interface IPaddleRepository
    {
        bool CreatePaddle(Paddle paddleToCreate);
    }
}
