using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

public class MySqlConnection
{

    #region attributes

    private static string _connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
    private static MySql.Data.MySqlClient.MySqlConnection _connection = new MySql.Data.MySqlClient.MySqlConnection(_connectionString);

    #endregion

    #region methods

    /// <summary>
    /// Opens a connection to the database
    /// </summary>
    /// <returns></returns>
    private bool Open()
    {
        //result
        bool connectionOpen = false;
        //check if connection is already open
        if (_connection.State != ConnectionState.Open)
        {
            try
            {
                _connection.Open(); //open connection
                connectionOpen = true; //connection opened successfully
            }
            catch (MySqlException ex)
            {
                //error in connection string
                Console.WriteLine(ex.Message);
            }
        }
        else
            connectionOpen = true; //connection was already open
        //return result5
        return connectionOpen;
    }

    /// <summary>
    /// Executes a query command and returns the resulting table
    /// </summary>
    /// <param name="command">SQL Command</param>
    /// <returns></returns>
    public DataTable ExecuteQuery(MySqlCommand command)
    {
        //result table
        DataTable table = new DataTable();
        //open connection
        if (Open())
        {
            command.Connection = _connection; //assign connection
            MySqlDataAdapter adapter = new MySqlDataAdapter(command); //data adapter
            try
            {
                adapter.Fill(table); //execute query and fill result table
            }
            catch (MySqlException ex)
            {
                //error in query string
                Console.WriteLine(ex.Message);
            }
            _connection.Close(); //close connection
        }
        //return result table
        return table;
    }

    /// <summary>
    /// Executes a non-query SQL command 
    /// </summary>
    /// <param name="command">SQL Command</param>
    /// <returns></returns>
    public bool ExecuteNonQuery(MySqlCommand command)
    {
        //result
        bool success = false;
        //open connection
        if (Open())
        {
            command.Connection = _connection; //assign connection
            try
            {
                if (command.ExecuteNonQuery() > 0) success = true;
            }
            catch (MySqlException ex)
            {
                //error in query string
                Console.WriteLine(ex.Message);
            }
            _connection.Close(); //close connection
        }
        //return result
        return success;
    }

    #endregion
}