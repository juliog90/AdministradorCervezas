using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;


    public class Beer
    {
    #region Aattributes
    private int _id;
    private double _gradoAlcohol;
    private PresentationType _presentation;
    private Fermentation _fermentation;
    private MeasurementUnit _measurementUnit;
    private double _content;
    private Brand _brand = new Brand();
    private Clasification _clasification = new Clasification();
    private double _price;
    private string _image;

    #endregion


    #region properties
    public int Id
    {
        get { return _id; }
    }
    public double GradoAlcohol
    {
        get { return _gradoAlcohol; }
        set { _gradoAlcohol = value; }
    }

    public PresentationType Presentation{
        get { return _presentation; }
        set { _presentation = value; }
    }

    public Fermentation Fermlevel
    {
        get { return _fermentation; }
        set { _fermentation = value; }
    }

    public MeasurementUnit MeasurementUnit {
        get { return _measurementUnit; }
        set { _measurementUnit = value; }
    }

    public double Content {
        get { return _content; }
        set { _content = value; }
    }

    public Brand Brand
    {
        get { return _brand; }
        set { _brand = value; }
    }

    public Clasification Clasification
    {
        get { return _clasification; }
        set { _clasification = value; }
    }

    public double Price {
        get { return _price; }
        set { _price = value; }
    }

    public string Image
    {
        get { return _image; }
        set { _image = value; }
    }
    #endregion


    #region Constructors

    public Beer()
    {
        _id = 0;
        _gradoAlcohol = 0;
        _presentation = new PresentationType();
        _fermentation = new Fermentation();
        _measurementUnit = new MeasurementUnit();
        _content = 0;
        _brand = new Brand();
        _clasification = new Clasification();
        _price = 0;
    }


    /// <summary>
    /// Creates an object with data from the databas
    /// </summary>
    /// <param name="id">Country Id</param>
    public Beer(int id)
    {
        //query
        string query = "select be_id, be_grd_alcoh,be_presentation, be_level_ferm,be_unitMeas,be_content,br_code,cla_code,be_price from beer where be_id = @ID";
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
            _id = (int)row["be_id"];
            _gradoAlcohol = (double)row["be_grd_alcoh"];
            _presentation = (PresentationType)(int)row["be_presentation"];
            _fermentation = (Fermentation)row["be_level_ferm"];
            _measurementUnit = (MeasurementUnit)row["be_unitMeas"];
            _content = (double)row["be_content"];
                
            _brand = (new Brand((string)row["br_code"]));
            _clasification = (new Clasification((string)row["cla_code"]));
            _price = (double)row["be_price"];

        }
    }


    /// <summary>
    /// Creates an object with data from the arguments
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    public Beer(int id, double gradoalcohol,PresentationType presentation, Fermentation fermentation,MeasurementUnit measurementUnit,double content, Brand brand, Clasification clasification,double price, string image)
    {
        _id = id;
        _gradoAlcohol = gradoalcohol;
        _presentation = presentation;
        _fermentation = fermentation;
        _measurementUnit = measurementUnit;
        _content = content;
        _brand = brand;
        _clasification = clasification;
        _price = price;
        _image = image;
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
        string statement = "Insert into Beer (be_grd_alcoh,be_presentation,be_level_ferm,be_unitMeas,be_content,br_code,cla_code,be_price,be_image) values (@GRD,@PRE,@FER,@MEA,@CONT,@BRA,@CLA,@PRI,@IMG)";
        //command
        MySqlCommand command = new MySqlCommand(statement);
        //parameters
        command.Parameters.AddWithValue("@GRD", _gradoAlcohol);
        command.Parameters.AddWithValue("@PRE", _presentation);
        command.Parameters.AddWithValue("@FER", _fermentation);
        command.Parameters.AddWithValue("@MEA", _measurementUnit);
        command.Parameters.AddWithValue("@CONT", _content);
        command.Parameters.AddWithValue("@BRA", _brand);
        command.Parameters.AddWithValue("@CLA", _clasification);
        command.Parameters.AddWithValue("@PRI", _price);
        command.Parameters.AddWithValue("@IMG", _image);

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
        string statement = "update beer set be_grd_alcoh = @GRAD where be_id = @ID";
        //command
        MySqlCommand command = new MySqlCommand(statement);
        //parameters
        command.Parameters.AddWithValue("@ID", _id);

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
        string statement = "delete from beer where be_id = @ID";
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
    public override string ToString()
    {
        return _brand + " " + _gradoAlcohol.ToString() + "° " + _fermentation+" "+_presentation+" "+_content+_measurementUnit;
    }

    #endregion

    #region class methods
    public static List<Beer> GetAll()
    {
        //list
        List<Beer> list = new List<Beer>();
        //query
        string query = "select be_id, be_grd_alcoh,be_presentation,be_level_ferm,be_unitMeas,be_content,br_code,cla_code,be_price,be_image from beer";
        //command
        MySqlCommand command = new MySqlCommand(query);
        //execute query
        DataTable table = MySqlConnection.ExecuteQuery(command);
        //iterate rows
        foreach (DataRow row in table.Rows)
        {
            //read fields
            int id = (int)row["be_id"];
            double gradoalcohol = (double)row["be_grd_alcoh"];
            PresentationType presentation = (PresentationType)(int)row["be_presentation"];
            Fermentation fermentation = (Fermentation)(int)row["be_level_ferm"];
            MeasurementUnit measurementUnit = (MeasurementUnit)row["be_unitMeas"];
            double content = (double)row["be_content"];
            Brand brand = new Brand((string)row["br_code"]);
            Clasification clasification = new Clasification((string)row["cla_code"]);
            double price = (double)row["be_price"];
            string image = (string)row["be_image"];
            //add country to list
            list.Add(new Beer(id, gradoalcohol, presentation, fermentation, measurementUnit, content, brand, clasification, price,image));
        }
        //return list
        return list;
    }



    public static List<Beer> GetBeers(Brand br)
    {
        //list
        List<Beer> list = new List<Beer>();
        //query
        string query = "select be_id, be_grd_alcoh,be_presentation,be_level_ferm,be_unitMeas,be_content,br_code,cla_code,be_price from beer where br_code=@BRD";
        //command
        MySqlCommand command = new MySqlCommand(query);
        command.Parameters.AddWithValue("@BRD", br.Id);
        //execute query
        DataTable table = MySqlConnection.ExecuteQuery(command);
        //iterate rows
        foreach (DataRow row in table.Rows)
        {
            //read fields
            int id = (int)row["be_id"];
            double gradoalcohol = (double)row["be_grd_alcoh"];
            PresentationType presentation = (PresentationType)(int)row["be_presentation"];
            Fermentation fermentation = (Fermentation)(int)row["be_level_ferm"];
            MeasurementUnit measurementUnit = (MeasurementUnit)row["be_unitMeas"];
            double content = (double)row["be_content"];
            Brand brand = new Brand((string)row["br_code"]);
            Clasification clasification = new Clasification((string)row["cla_code"]);
            double price = (double)row["be_price"];
            string image = (string)row["be_image"];
            //add country to list
            list.Add(new Beer(id, gradoalcohol, presentation, fermentation, measurementUnit, content, brand, clasification, price,image));
        }
        //return list
        return list;
    }

    public static List<Beer> GetBeers(Country co)
    {
        //list
        List<Beer> list = new List<Beer>();
        //query
        string query = "select be_id, be_grd_alcoh,be_presentation,be_level_ferm,be_unitMeas,be_content,beer.br_code,cla_code,be_price,be_image from beer join brand on beer.br_code= brand.br_code join country on country.cn_code = brand.cn_code where country.cn_code=@CON";
        //command
        MySqlCommand command = new MySqlCommand(query);
        command.Parameters.AddWithValue("@CON", co.Id);
        //execute query
        DataTable table = MySqlConnection.ExecuteQuery(command);
        //iterate rows
        foreach (DataRow row in table.Rows)
        {
            //read fields
            int id = (int)row["be_id"];
            double gradoalcohol = (double)row["be_grd_alcoh"];
            PresentationType presentation = (PresentationType)(int)row["be_presentation"];
            Fermentation fermentation = (Fermentation)(int)row["be_level_ferm"];
            MeasurementUnit measurementUnit = (MeasurementUnit)row["be_unitMeas"];
            double content = (double)row["be_content"];
            Brand brand = new Brand((string)row["br_code"]);
            Clasification clasification = new Clasification((string)row["cla_code"]);
            double price = (double)row["be_price"];
            string image = (string)row["be_image"];
            //add country to list
            list.Add(new Beer(id, gradoalcohol, presentation, fermentation, measurementUnit, content, brand, clasification, price,image));
        }
        //return list
        return list;
    }


   
    #endregion
}

