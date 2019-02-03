using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

public class City : Place
{


    #region constructors

    /// <summary>
    /// Creates an empty object
    /// </summary>
    public City() : base()
    {
    }

    /// <summary>
    /// Creates an object with data from the databas
    /// </summary>
    /// <param name="id">Country Id</param>
    public City(String id)
    {
        //query
        string query = "select cit_code, cit_name from City where cit_code = @ID";
        //command
        MySqlCommand command = new MySqlCommand(query);
        //parameters
        command.Parameters.AddWithValue("@ID", id);
        //execute query
        DataTable table = MySqlConnection.ExecuteQuery(command);
        //check if rows were found
        if (table.Rows.Count > 0)
        {
            //read first and only row
            DataRow row = table.Rows[0];
            //read data
            _id = (string)row["cit_code"];
            _name = (string)row["cit_name"];
        }
    }

    /// <summary>
    /// Creates an object with data from the arguments
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    public City(string id, string name)
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
        string statement = "insert into City (cit_code, cit_name) values (@ID, @NAME)";
        //command
        MySqlCommand command = new MySqlCommand(statement);
        //parameters
        command.Parameters.AddWithValue("@ID", _id);
        command.Parameters.AddWithValue("@NAME", _name);
        //execute command
        return MySqlConnection.ExecuteNonQuery(command);
    }

    /// <summary>
    /// Edit the country
    /// </summary>
    /// <returns></returns>
    public bool Edit()
    {
        //statement
        string statement = "update City set cit_name = @NAME where cit_code = @ID";
        //command
        MySqlCommand command = new MySqlCommand(statement);
        //parameters
        command.Parameters.AddWithValue("@ID", _id);
        command.Parameters.AddWithValue("@NAME", _name);
        //execute command
        return MySqlConnection.ExecuteNonQuery(command);
    }

    /// <summary>
    /// Delete country
    /// </summary>
    /// <returns></returns>
    public bool Delete()
    {
        //statement
        string statement = "delete from City where cit_code = @ID";
        //command
        MySqlCommand command = new MySqlCommand(statement);
        //parameters
        command.Parameters.AddWithValue("@ID", _id);
        //execute command
        return MySqlConnection.ExecuteNonQuery(command);
    }

    /// <summary>
    /// Represents the object as a string
    /// </summary>
    /// <returns></returns>

    #endregion

    #region class methods

    public static List<City> GetAll()
    {
        //list
        List<City> list = new List<City>();
        //query
        string query = "select cit_code,cit_name from City order by cit_name";
        //command
        MySqlCommand command = new MySqlCommand(query);
        //execute query
        DataTable table = MySqlConnection.ExecuteQuery(command);
        //iterate rows
        foreach (DataRow row in table.Rows)
        {
            //read fields
            string id = (string)row["cit_code"];
            string name = (string)row["cit_name"];
            //add country to list
            list.Add(new City(id, name));
        }
        //return list
        return list;
    }
    public override string ToString()
    {
        return _name;
    }
    #endregion
}