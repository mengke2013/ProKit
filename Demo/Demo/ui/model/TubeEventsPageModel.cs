using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql.Data.Entity;
using MySql.Data.MySqlClient;

namespace Demo.ui.model
{
    class TubeEventsPageModel
    {
        private String myConnectionString = "Server=localhost;Database=RPB66;Uid=root;Pwd=root;";
        public TubeEventsPageModel()
        {
 
        }

        public void LoadData(byte selectedTube)
        {
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = string.Format("select ID, EVENT_TYPE, EVENT_DESC, EVENT_TIME from event where TUBE_NUM = {0} order by EVENT_TIME desc limit 100", selectedTube);
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adap.Fill(ds);
                //this.clothesListView.DataContext = ds.Tables[0].DefaultView;
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    string t = r["ID"].ToString();
                    string d = r["EVENT_TYPE"].ToString();
                    string i = r["EVENT_DESC"].ToString();


                }
                this.Model = ds.Tables[0];
                //dataGrid.DataContext = ds.Tables[0];
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
        }

        public DataTable Model { get; private set; }
    }
}
