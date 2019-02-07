using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
public class Brand
    {
    #region attributes
    private  string _id;
    private  string _name;
    Country _country;
    #endregion

    #region properties
    public string Id {
        get { return _id;}
        set { _id = value;}
    }

    public string Name {
        get { return _name; }
        set { _name = value; }
    }

    public Country Country
    {
        get { return _country;}
        set { _country = value; }
    }
    #endregion

    #region constructors
    public Brand() {
        _id = "";
        _name = "";
        _country = new Country();
    }

    public Brand(string id)
    {
        //query
        string query = "select br_code, br_name,cn_code from brand where br_code = @ID";
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
            _id = (string)row["br_code"];
            _name = (string)row["br_name"];
            _country = new Country((string)row["cn_code"]);
        }
    }

    public Brand(string id, string name)
    {
        _id = id;
        _name = name;
    }

    public Brand(string id, string name, Country country){
        _id = id;
        _name = name;
        _country = country;
    }
    #endregion

    #region instance methods
    /// <summary>
    /// Add a new brand
    /// </summary>
    /// <returns></returns>
    public bool Add()
    {
        //statement
        string statement = "insert into brand (br_code, br_name,cn_code) values (@ID, @NAME,@CN)";
        //command
        MySqlCommand command = new MySqlCommand(statement);
        //parameters
        command.Parameters.AddWithValue("@ID", _id);
        command.Parameters.AddWithValue("@NAME", _name);
        command.Parameters.AddWithValue("@CN", _country);
        //execute command
        return MySqlConnection.ExecuteNonQuery(command);
    }



    public bool Delete()
    {
        //statement
        string statement = "delete from brand where br_code = @ID";
        //command
        MySqlCommand command = new MySqlCommand(statement);
        //parameters
        command.Parameters.AddWithValue("@ID", _id);
        //execute command
        return MySqlConnection.ExecuteNonQuery(command);
    }
    public bool Edit()
    {
        //statement
        string statement = "update brand set br_name = @NAME, cn_code = @CN where br_code = @ID" ;
        //command
        MySqlCommand command = new MySqlCommand(statement);
        //parameters
        command.Parameters.AddWithValue("@ID", _id);
        command.Parameters.AddWithValue("@NAME", _name);
        command.Parameters.AddWithValue("@CN", _country.Id);
        //execute command
        return MySqlConnection.ExecuteNonQuery(command);
    }
    public override string ToString()
    {
        return _name;
    }


    #endregion


    #region methods
    public static List<Brand> GetAll() {
        //list
        List<Brand> list = new List<Brand>();
        //query
        string query = "select br_code, br_name,cn_code from Brand";
        //command
        MySqlCommand command = new MySqlCommand(query);
        //execute query
        DataTable table = MySqlConnection.ExecuteQuery(command);
        //iterate rows
        foreach (DataRow row in table.Rows)
        {
            //read fields
            string id = (string)row["br_code"];
            string name = (string)row["br_name"];
            Country country = new Country((string)row["cn_code"]);
            //add country to list
            list.Add(new Brand(id, name,country));
        }


        //return list
        return list;
    }


    public static List<Brand> GetBrands(Country co)
    {
        //list
        List<Brand> list = new List<Brand>();
        //query
        string query = "select br_code, br_name,cn_code from Brand Where cn_code=@CON";
        //command
        MySqlCommand command = new MySqlCommand(query);
        command.Parameters.AddWithValue("@CON", co.Id);
        //execute query
        DataTable table = MySqlConnection.ExecuteQuery(command);
        //iterate rows
        foreach (DataRow row in table.Rows)
        {
            //read fields
            string id = (string)row["br_code"];
            string name = (string)row["br_name"];
            Country country = new Country((string)row["cn_code"]);
            //add country to list
            list.Add(new Brand(id, name, country));
        }


        //return list
        return list;
    }

    //creates a instance method for get the clasification of the a brand
    public  List<Clasification> GetClasifications()
    {
        //list
        List<Clasification> list = new List<Clasification>();
        //query
        string query = "select cla_code from clasification_brand where br_code=@ID";
        //command
        MySqlCommand command = new MySqlCommand(query);

        command.Parameters.AddWithValue("@ID", _id);
        //execute query
        DataTable table = MySqlConnection.ExecuteQuery(command);
        //iterate rows
        foreach (DataRow row in table.Rows)
        {
            //read fields
            Clasification id = new Clasification((string)row["cla_code"]);
            //add country to list
            list.Add(id);
        }


        //return list
        return list;
    }
    #endregion

    /// <summary>
    /// Generic method for querying Beers
    /// </summary>
    /// <param name="command">SQL Command with parameters</param>
    /// <returns></returns>


}
