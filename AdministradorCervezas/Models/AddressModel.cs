using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

public class Address
{
    #region Attributes

    private int _id;
    private string _street;
    private string _suburb;
    private string _number;
    private string _zipCode;
    private City _city;

    #endregion

    #region properties

    public int Id
    {
        get { return _id; }
    }

    public string Street
    {
        get { return _street; }
        set { _street = value; }
    }

    public string Suburb
    {
        get { return _suburb; }
        set { _suburb = value; }
    }

    public string Number
    {
        get { return _number; }
        set { _number = value; }
    }

    public string ZipCode
    {
        get { return _zipCode; }
        set { _zipCode = value; }
    }

    public City City
    {
        get { return _city; }
        set { _city = value; }
    }
    #endregion

    #region Constructors
    public Address()
    {
        _id = 0;
        _street = "";
        _suburb = "";
        _number = "";
        _zipCode = "";
        _city = new City();
    }

    public Address(int id)
    {
        //query
        string query = "SELECT add_id,add_street,add_suburb,add_number,add_zipCode,cit_code FROM Address WHERE add_id = @ID";
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
            _id = (int)row["add_id"];
            _street = (string)row["add_street"];
            _suburb = (string)row["add_suburb"];
            _number = (string)row["add_number"];
            _zipCode = (string)row["add_zipCode"];
            _city = new City((string)row["cit_code"]);
        }
    }

    public Address(int id, string street, string suburb, string number, string zipCode, City city)
    {
        _id = id;
        _street = street;
        _suburb = suburb;
        _number = number;
        _zipCode = zipCode;
        _city = city;
    }
    #endregion

    #region Instance methods

    public bool Add()
    {
        //statement
        string statement = "INSERT into Address (add_id,add_street,add_suburb,add_number,add_zipCode,cit_code) values (@ID, @STREET,@SUBURB,@NUMBER,@ZIPCODE,@CODE)";
        //command
        MySqlCommand command = new MySqlCommand(statement);
        //parameters
        command.Parameters.AddWithValue("@ID", _id);
        command.Parameters.AddWithValue("@STREET", _street);
        command.Parameters.AddWithValue("@SUBURB", _suburb);
        command.Parameters.AddWithValue("@NUMBER", _number);
        command.Parameters.AddWithValue("@ZIPCODE", _zipCode);
        command.Parameters.AddWithValue("@CODE", _city);
        //execute command
        MySqlConnection connection = new MySqlConnection();
        connection.ConnectionSource = new AppSettings();
        return connection.ExecuteNonQuery(command);
    }

    public bool Delete()
    {
        //statement
        string statement = "DELETE FROM Address WHERE add_id = @ID";
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
        string statement = "UPDATE Address set add_street = @STREET, add_suburb = @SUBURB, add_number = @NUMBER, add_zipCode = @ZIPCODE WHERE add_id = @ID";
        //command
        MySqlCommand command = new MySqlCommand(statement);
        //parameters
        command.Parameters.AddWithValue("@ID", _id);
        command.Parameters.AddWithValue("@STREET", _street);
        command.Parameters.AddWithValue("@SUBURB", _suburb);
        command.Parameters.AddWithValue("@NUMBER", _number);
        command.Parameters.AddWithValue("@ZIPCODE", _zipCode);
        //execute command
        MySqlConnection connection = new MySqlConnection();
        connection.ConnectionSource = new AppSettings();
        return connection.ExecuteNonQuery(command);
    }

    #endregion

    #region Methods

    public static List<Address> GetAll()
    {
        //list
        List<Address> list = new List<Address>();
        //query
        string query = "SELECT add_id,add_street,add_suburb,add_number,add_zipCode,cit_code FROM Address";
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
            int id = (int)row["add_id"];
            string street = (string)row["add_street"];
            string suburb = (string)row["add_suburb"];
            string number = (string)row["add_number"];
            string zipCode = (string)row["add_zipCode"];
            City city = new City((string)row["cit_code"]);
            //add country to list
            list.Add(new Address(id, street, suburb, number, zipCode, city));
        }
        //return list
        return list;
    }

    public override string ToString()
    {
        return "street " + _street + "  " + _suburb + " No." + _number + " " + _zipCode + " " + _city;
    }
    #endregion
}

