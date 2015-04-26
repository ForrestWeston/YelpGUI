using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;//add mysql connector library
using System.Data;       

namespace YelpGUI
{
    class MySqlConnector
    {
        private MySqlConnection connection; //opens and closes connection to DB

        //constructor
        public MySqlConnector()
        {
            try
            {
                Initialize();

            }
            catch (MySqlException e)
            {
                /*handle here*/
            }
        }

        //Init Connection
        private void Initialize()
        {
            string server;
            string database;
            string uid;
            string paswword;

            server = "localhost";
            database = "yelp";
            uid = "root";
            paswword = "password";

            string connectionString = "SERVER=" + server + ";DATABASE=" + database + ";UID=" + uid + ";PASSWORD=" + paswword + ";";
            connection = new MySqlConnection(connectionString);
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch(MySqlException e)
            {
                if (e.Number == 0)
                    return false; //can't connect to server
                else if (e.Number == 1045)
                    return false; //invalid username/pasword
                //handle other exceptions as well
            }
            return false;

        }

        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException e)
            {
                /*handle exceptions*/
            }
            return false;

        }

        //Execute SELECT Query - return single attribute
        public List<String> SQLSELECTExec(string queryStr, string column_name)
        {
            List<String> qResult = new List<String>();
            if(this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(queryStr, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while(dataReader.Read())
                    qResult.Add(dataReader.GetString(column_name));

                dataReader.Close();
                this.CloseConnection();
            }
            return qResult;
        }

        public DataTable FillTable(string queryStr)
        {
           if(this.OpenConnection() == true)
           {
               using(MySqlDataAdapter a = new MySqlDataAdapter(queryStr, connection))
               {
                   DataTable t = new DataTable();
                   a.Fill(t);
                   this.CloseConnection();
                   return t;
               }
           }
           this.CloseConnection();
            //should never hit here unless we are disconnected from the db
           DataTable tp = new DataTable();
           return tp;
        }
    }
}
 