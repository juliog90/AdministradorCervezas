using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

public class Clasification
{

    #region Atributte 

    private string _code;
    private string _name;
    private BeerType _beerType;

    #endregion

    #region properties

    public string Code
    {
        get { return _code; }
        set { _code = value; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public BeerType BeerType
    {
        get { return _beerType; }
        set { _beerType = value; }
    }

    #endregion

    #region constructors

    public Clasification()
    {
        _code = "";
        _name = "";
        _beerType = new BeerType();
    }

    public Clasification(string id)
    {
        //query
        string query = "select cla_code,cla_name,type_code from clasification where cla_code = @ID";
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
            _code = (string)row["cla_code"];
            _name = (string)row["cla_name"];
            _beerType = new BeerType((string)row["type_code"]);
        }
    }



    public Clasification(string code, string name)
    {
        _code = code;
        _name = name;
    }


    public Clasification(string code, string name, BeerType beertype)
    {
        _code = code;
        _name = name;
        _beerType = beertype;
    }
    #endregion
    #region Instance methods


    public bool Add()
    {
        //statement
        string statement = "insert into Clasification (cla_code, cla_name,type_code) values (@ID, @NAME,@BT)";
        //command
        MySqlCommand command = new MySqlCommand(statement);
        //parameters
        command.Parameters.AddWithValue("@ID", _code);
        command.Parameters.AddWithValue("@NAME", _name);
        command.Parameters.AddWithValue("@BT", _beerType.Id);
        //execute command
        MySqlConnection connection = new MySqlConnection();
        connection.ConnectionSource = new AppSettings();
        return connection.ExecuteNonQuery(command);
    }

    public bool Delete()
    {
        //statement
        string statement = "delete from clasification where cla_code = @ID";
        //command
        MySqlCommand command = new MySqlCommand(statement);
        //parameters
        command.Parameters.AddWithValue("@ID", _code);
        //execute command
        MySqlConnection connection = new MySqlConnection();
        connection.ConnectionSource = new AppSettings();
        return connection.ExecuteNonQuery(command);
    }

    public bool Edit()
    {
        //statement
        string statement = "update clasification set cla_name = @NAME,type_code=@BT where cla_code = @ID";
        //command
        MySqlCommand command = new MySqlCommand(statement);
        //parameters
        command.Parameters.AddWithValue("@ID", _code);
        command.Parameters.AddWithValue("@NAME", _name);
        command.Parameters.AddWithValue("@BT", _beerType.Id);
        //execute command
        MySqlConnection connection = new MySqlConnection();
        connection.ConnectionSource = new AppSettings();
        return connection.ExecuteNonQuery(command);
    }

    public static List<Clasification> GetAll()
    {
        //list
        List<Clasification> list = new List<Clasification>();
        //query
        string query = "select cla_code,cla_name,type_code from clasification";
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
            string code = (string)row["cla_code"];
            string name = (string)row["cla_name"];
            BeerType beertype = new BeerType((string)row["type_code"]);
            //add clasification to list
            list.Add(new Clasification(code, name, beertype));
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
