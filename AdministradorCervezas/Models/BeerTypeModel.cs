using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;


public class BeerType
{
    #region
    private string _id;
    private string _name;
    private Color _color;
    CategoryType _category;
    #endregion

    #region properties
    public string Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public Color Color
    {
        get { return _color; }
        set { _color = value; }
    }

    public CategoryType Category
    {
        get { return _category; }
        set { _category = value; }
    }

    #endregion

    #region constructors
    public BeerType()
    {
        _id = "";
        _name = "";
        _color = Color.Brown;
        _category = CategoryType.Ale;
    }


    public BeerType(string id)
    {
        //query
        string query = "Select type_code,type_name,type_color,type_category from Beer_type where type_code = @ID";
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
            _id = (string)row["type_code"];
            _name = (string)row["type_name"];
            _color = (Color)(int)row["type_color"];
            _category = (CategoryType)(int)row["type_category"];
        }
    }



    public BeerType(string id, string name, Color color, CategoryType category)
    {
        _id = id;
        _name = name;
        _color = color;
        _category = category;

    }
    #endregion

    #region instance methods

    public static List<BeerType> GetAll()
    {
        //list
        List<BeerType> list = new List<BeerType>();
        //query
        string query = "select type_code, type_name,type_color,type_category from Beer_type";
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
            string id = (string)row["type_code"];
            string name = (string)row["type_name"];
            Color color = (Color)(int)row["type_color"];
            CategoryType category = (CategoryType)row["type_category"];


            //add country to list
            list.Add(new BeerType(id, name, color, category));
        }
        //return list
        return list;
    }

    public bool Add()
    {
        //statement
        string statement = "insert into beer_type (type_code,type_name,type_color,type_category) values (@ID, @NAME,@COLOR,@CAT)";
        //command
        MySqlCommand command = new MySqlCommand(statement);
        //parameters
        command.Parameters.AddWithValue("@ID", _id);
        command.Parameters.AddWithValue("@NAME", _name);
        command.Parameters.AddWithValue("@COLOR", _color);
        command.Parameters.AddWithValue("@CAT", _category);
        //execute command
        MySqlConnection connection = new MySqlConnection();
        connection.ConnectionSource = new AppSettings();
        return connection.ExecuteNonQuery(command);
    }

    public bool Delete()
    {
        //statement
        string statement = "delete from beer_type where type_code = @ID";
        //command
        MySqlCommand command = new MySqlCommand(statement);
        //parameters
        command.Parameters.AddWithValue("@ID", _id);
        //execute command
        MySqlConnection connection = new MySqlConnection();
        connection.ConnectionSource = new AppSettings();
        return connection.ExecuteNonQuery(command);
    }

    public bool Edit()
    {
        //statement
        string statement = "update beer_type set type_name = @NAME,type_color=@COLOR,type_category=@CAT where type)code = @ID";
        //command
        MySqlCommand command = new MySqlCommand(statement);
        //parameters
        command.Parameters.AddWithValue("@ID", _id);
        command.Parameters.AddWithValue("@NAME", _name);
        command.Parameters.AddWithValue("@COLOR", _color);
        command.Parameters.AddWithValue("@CAT", _category);
        //execute command
        MySqlConnection connection = new MySqlConnection();
        connection.ConnectionSource = new AppSettings();
        return connection.ExecuteNonQuery(command);
    }

    public override string ToString()
    {
        return _name + " " + _category;
    }

    #endregion

}
