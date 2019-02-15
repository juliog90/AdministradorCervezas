
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

public class Customer
{
    #region Attributes
    private int _id;
    private string _firstName;
    private string _lastName;
    private string _email;
    private string _phone;
    private Address _address;
    private string _password;
    #endregion

    #region properties
    public int Id
    {
        get { return _id; }
    }

    public string Firstname
    {
        get { return _firstName; }
        set { _firstName = value; }
    }
    public string Lastname
    {
        get { return _lastName; }
        set { _lastName = value; }
    }

    public string FullName
    {
        get { return _firstName + " " + _lastName; }
    }

    public string email
    {
        get { return _email; }
        set { _email = value; }
    }

    public string Phone
    {
        get { return _phone; }
        set { _phone = value; }
    }

    public Address Address
    {
        get { return _address; }
        set { _address = value; }
    }

    public String Password
    {
        get { return _password; }
        set { _password = value; }
    }
    #endregion

    #region constructors
    public Customer()
    {
        _id = 0;
        _firstName = "";
        _lastName = "";
        _email = "";
        _phone = "";
        _address = new Address();
        _password = "";
    }
    public Customer(int id)
    {
        //query
        string query = "select cus_id,cus_firstName,cus_lastName,cus_email,cus_phone,add_id,cus_pass  from Customer where cus_id = @ID";
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
            _id = (int)row["cus_id"];
            _firstName = (string)row["cus_firstName"];
            _lastName = (string)row["cus_lastName"];
            _email = (string)row["cus_email"];
            _phone = (string)row["cus_phone"];
            _address = new Address((int)row["add_id"]);
            _password = (string)row["cus_pass"];
        }
    }

    /// <summary>
    /// Creates an object with data from the arguments
    /// </summary>

    public Customer(int id, string firstName, string lastName, string email, string phone, Address address, string password)
    {
        _id = id;
        _firstName = firstName;
        _lastName = lastName;
        _email = email;
        _phone = phone;
        _address = address;
        _password = password;
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
        string statement = "insert into customer (cus_firstName,cus_lastName,cus_email,cus_phone,add_id) values (@ID, @FIRSTNAME,@LASTNAME,@EMAIL,@PHONE,@ADD)";
        //command
        MySqlCommand command = new MySqlCommand(statement);
        //parameters
        command.Parameters.AddWithValue("@ID", _id);
        command.Parameters.AddWithValue("@FIRSTNAME", _firstName);
        command.Parameters.AddWithValue("@LASTNAME", _lastName);
        command.Parameters.AddWithValue("@EMAIL", _email);
        command.Parameters.AddWithValue("@PHONE", _phone);
        command.Parameters.AddWithValue("@ADD", _address);
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
        string statement = "update Customer set Cus_firstName = @FIRSTNAME, Cus_lastName=@LASTNAME,cus_email=@EMAIL,cus_phone=@PHONE,add_id=@ADD where cus_id = @ID";
        //command
        MySqlCommand command = new MySqlCommand(statement);
        //parameters
        command.Parameters.AddWithValue("@ID", _id);
        command.Parameters.AddWithValue("@FIRSNAME", _firstName);
        command.Parameters.AddWithValue("@LASTNAME", _lastName);
        command.Parameters.AddWithValue("@EMAIL", _email);
        command.Parameters.AddWithValue("@PHONE", _phone);
        command.Parameters.AddWithValue("@ADD", _address);

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
        string statement = "delete from customer where cus_id = @ID";
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
    public override string ToString()
    {
        return _firstName + " " + _lastName + "\nEmail: " + _email + "\nPhone: " + _phone + "\nAddress: " + _address;
    }

    #endregion

    #region class methods

    public static List<Customer> GetAll()
    {
        //list
        List<Customer> list = new List<Customer>();
        //query
        string query = "select cus_id,cus_firstName,cus_lastName,cus_email,cus_phone,add_id,cus_pass from customer order by cus_firstName";
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
            int id = (int)row["cus_id"];
            string firstName = (string)row["cus_firstName"];
            string lastName = (string)row["cus_lastName"];
            string email = (string)row["cus_email"];
            string phone = (string)row["cus_phone"];
            Address address = new Address((int)row["add_id"]);
            string password = (string)row["cus_pass"];
            //add country to list
            list.Add(new Customer(id, firstName, lastName, email, phone, address, password));
        }
        //return list
        return list;
    }
    #endregion

    public static List<Customer> GetCustomer(City ci)
    {
        //list
        List<Customer> list = new List<Customer>();
        //query
        string query = "select cus_id,cus_firstName,cus_lastName,cus_email,cus_phone,address.add_id,address.cit_code,cus_pass from customer join address on customer.add_id = address.add_id join City on city.cit_code = address.cit_code where address.cit_code=@CIT";
        //command
        MySqlCommand command = new MySqlCommand(query);
        command.Parameters.AddWithValue("@CIT", ci.Id);
        //execute query
        MySqlConnection connection = new MySqlConnection();
        connection.ConnectionSource = new AppSettings();
        DataTable table = connection.ExecuteQuery(command);
        //iterate rows
        foreach (DataRow row in table.Rows)
        {
            //read fields
            int id = (int)row["cus_id"];
            string firstName = (string)row["cus_firstName"];
            string lastName = (string)row["cus_lastName"];
            string email = (string)row["cus_email"];
            string phone = (string)row["cus_phone"];
            Address address = new Address((int)row["add_id"]);
            string password = (string)row["cus_pass"];

            //add country to list
            list.Add(new Customer(id, firstName, lastName, email, phone, address, password));
        }
        //return list
        return list;
    }



}



