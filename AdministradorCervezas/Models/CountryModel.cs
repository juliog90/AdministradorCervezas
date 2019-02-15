using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

public class Country : Place
{


    #region constructors

    /// <summary>
    /// Creates an empty object
    /// </summary>
    public Country() : base()
    {
    }

    /// <summary>
    /// Creates an object with data from the databas
    /// </summary>
    /// <param name="id">Country Id</param>
    public Country(String id)
    {
        //query
        string query = "select cn_code, cn_name from country where cn_code = @ID";
        //command
        MySqlCommand command = new MySqlCommand(query);
        //parameters
        command.Parameters.AddWithValue("@ID", id);
        //execute query
        MySqlConnection connection = new MySqlConnection();
        connection.ConnectionSource = new AppSettings();
        DataTable table = connection.ExecuteQuery(command);
        //check if rows were found
        if (table.Rows.Count > 0)
        {
            //read first and only row
            DataRow row = table.Rows[0];
            //read data
            _id = (string)row["cn_code"];
            _name = (string)row["cn_name"];
        }
    }

    /// <summary>
    /// Creates an object with data from the arguments
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    public Country(string id, string name)
    {
        _id = id;
        _name = name;
    }

    #endregion

    #region instance methods

    /// <summary>
    /// Adds the country to the database
    /// </summary>
    /// <returns></returns>
    public bool Add()
    {
        //statement
        string statement = "insert into country (cn_code, cn_name) values (@ID, @NAME)";
        //command
        MySqlCommand command = new MySqlCommand(statement);
        //parameters
        command.Parameters.AddWithValue("@ID", _id);
        command.Parameters.AddWithValue("@NAME", _name);
        //execute command
        MySqlConnection connection = new MySqlConnection();
        connection.ConnectionSource = new AppSettings();
        return connection.ExecuteNonQuery(command);
    }

    /// <summary>
    /// Edit the country
    /// </summary>
    /// <returns></returns>
    public bool Edit()
    {
        //statement
        string statement = "update country set cn_name = @NAME where cn_code = @ID";
        //command
        MySqlCommand command = new MySqlCommand(statement);
        //parameters
        command.Parameters.AddWithValue("@ID", _id);
        command.Parameters.AddWithValue("@NAME", _name);
        //execute command
        MySqlConnection connection = new MySqlConnection();
        connection.ConnectionSource = new AppSettings();
        return connection.ExecuteNonQuery(command);
    }

    /// <summary>
    /// Delete country
    /// </summary>
    /// <returns></returns>
    public bool Delete()
    {
        //statement
        string statement = "delete from country where cn_code = @ID";
        //command
        MySqlCommand command = new MySqlCommand(statement);
        //parameters
        command.Parameters.AddWithValue("@ID", _id);
        //execute command
        MySqlConnection connection = new MySqlConnection();
        connection.ConnectionSource = new AppSettings();
        return connection.ExecuteNonQuery(command);
    }

    /// <summary>
    /// Represents the object as a string
    /// </summary>
    /// <returns></returns>

    #endregion

    #region class methods

    public static List<Country> GetAll()
    {
        //list
        List<Country> list = new List<Country>();
        //query
        string query = "select cn_code,cn_name from country order by cn_name";
        //command
        MySqlCommand command = new MySqlCommand(query);
        //execute query
        MySqlConnection connection = new MySqlConnection();
        connection.ConnectionSource = new AppSettings();
        DataTable table = connection.ExecuteQuery(command);
        //iterate rows
        foreach (DataRow row in table.Rows)
        {
            //read fields
            string id = (string)row["cn_code"];
            string name = (string)row["cn_name"];
            //add country to list
            list.Add(new Country(id, name));
        }
        //return list
        return list;
    }
    public override string ToString()
    {
        return _name;
    }

    public static implicit operator Country(string v)
    {
        throw new NotImplementedException();
    }
    #endregion
}