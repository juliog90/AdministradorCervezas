using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;


public class Beer
{
    #region attributes
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
        set { _id = value; }
    }
    public double GradoAlcohol
    {
        get { return _gradoAlcohol; }
        set { _gradoAlcohol = value; }
    }

    public PresentationType Presentation
    {
        get { return _presentation; }
        set { _presentation = value; }
    }

    public Fermentation Fermlevel
    {
        get { return _fermentation; }
        set { _fermentation = value; }
    }

    public MeasurementUnit MeasurementUnit
    {
        get { return _measurementUnit; }
        set { _measurementUnit = value; }
    }

    public double Content
    {
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

    public double Price
    {
        get { return _price; }
        set { _price = value; }
    }

    public string Image
    {
        get { return _image; }
        set { _image = value; }
    }
    #endregion

    #region constructors

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
    /// Crea un objeto cerveza a partir de sus registro de la base datos, filtrado
    /// por su ID.
    /// </summary>
    /// <param name="id">Beer Id</param>
    public Beer(int id)
    {
        // Cadena de Consulta
        string query = "select be_id, be_grd_alcoh,be_presentation, be_level_ferm,"
            + "be_unitMeas,be_content,br_code,cla_code,be_price, be_image, be_level_ferm"
            + " from beer where be_id = @ID";

        // Comando MySQL
        MySqlCommand command = new MySqlCommand(query);

        // Parametros Preparados
        command.Parameters.AddWithValue("@ID", id);

        // Ejecutamos Consulta
        MySqlConnection connection = new MySqlConnection();
        connection.ConnectionSource = new AppSettings();

        // Guardamos la consulta en una tabla
        DataTable table = connection.ExecuteQuery(command);

        // Mostramos si la tabla tiene filas (no esta vacia)
        if (table.Rows.Count > 0)
        {
            // Guardamos la primera fila que guarda la informacion
            // de este objeto
            DataRow row = table.Rows[0];

            // Asignamos los datos de la fila a propiedades del objeto
            // a crear
            _id = (int)row["be_id"];
            _gradoAlcohol = (double)row["be_grd_alcoh"];
            _presentation = (PresentationType)(int)row["be_presentation"];
            _fermentation = (Fermentation)(int)row["be_level_ferm"];
            _measurementUnit = (MeasurementUnit)row["be_unitMeas"];
            _content = (double)row["be_content"];
            _image = (string)row["be_image"];
            _brand = (new Brand((string)row["br_code"]));
            _clasification = (new Clasification((string)row["cla_code"]));
            _price = (double)row["be_price"];
        }
    }

    /// <summary>
    /// Creamos una cerveza con parametros
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="gradoalcohol">GradoAlcohol</param>
    /// <param name="presentation">Presentación</param>
    /// <param name="fermentation">Fermentación</param>
    /// <param name="measurementUnit">UnidadMedida</param>
    /// <param name="content">Contenido</param>
    /// <param name="brand">Marca</param>
    /// <param name="clasification">Clasificación</param>
    /// <param name="price">Precio</param>
    /// <param name="image">Imagen</param>
    public Beer(int id, double gradoalcohol, PresentationType presentation, Fermentation fermentation,
        MeasurementUnit measurementUnit, double content, Brand brand, Clasification clasification, 
        double price, string image)
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
    /// Agregar un registro de cerveza a la base de datos
    /// </summary>
    /// <returns>Booleano que define si se agrego o no el registro</returns>
    public bool Add()
    {
        // string de comando SQL para insertar un registro de cerveza
        string statement = "Insert into Beer (be_grd_alcoh,be_presentation,"
            + "be_level_ferm,be_unitMeas,be_content, br_code,cla_code,be_price,"
            + "be_image) values (@GRD,@PRE,@FER,@MEA,@CONT,@BRA,@CLA,@PRI,@IMG)";

        // comando de envio
        MySqlCommand command = new MySqlCommand(statement);

        // parametros preparados con la informacion de cada campo
        command.Parameters.AddWithValue("@GRD", _gradoAlcohol);
        command.Parameters.AddWithValue("@PRE", _presentation);
        command.Parameters.AddWithValue("@FER", _fermentation);
        command.Parameters.AddWithValue("@MEA", _measurementUnit);
        command.Parameters.AddWithValue("@CONT", _content);
        command.Parameters.AddWithValue("@BRA", _brand.Id);
        command.Parameters.AddWithValue("@CLA", _clasification.Code);
        command.Parameters.AddWithValue("@PRI", _price);
        command.Parameters.AddWithValue("@IMG", _image);

        // ejecutar comando
        MySqlConnection connection = new MySqlConnection();
        connection.ConnectionSource = new AppSettings();

        // regresa si se logro agregar el registro o no
        return connection.ExecuteNonQuery(command);
    }

    /// <summary>
    /// Editar Cerveza
    /// </summary>
    /// <returns>Booleano: Si se logro editar la cerveza o no</returns>
    public bool Edit()
    {
        // Declaración
        string statement = "update beer set be_grd_alcoh = @GRAD, be_presentation = @PRE, be_level_ferm = @FERM,"
            + "be_unitMeas = @UNIT, be_content = @CON, br_code = @BRA, cla_code = @CLA, be_price = @PRI," 
            + "be_image = @IMG where be_id = @ID";

        // Comando 
        MySqlCommand command = new MySqlCommand(statement);

        // Parametrizado de Variables
        command.Parameters.AddWithValue("@ID", _id);
        command.Parameters.AddWithValue("@GRAD", _gradoAlcohol);
        command.Parameters.AddWithValue("@PRE", _presentation);
        command.Parameters.AddWithValue("@FERM", _fermentation);
        command.Parameters.AddWithValue("@UNIT", _measurementUnit);
        command.Parameters.AddWithValue("@CON", _content);
        command.Parameters.AddWithValue("@BRA", _brand.Id);
        command.Parameters.AddWithValue("@CLA", _clasification.Code);
        command.Parameters.AddWithValue("@PRI", _price);
        command.Parameters.AddWithValue("@IMG", _image);

        // Ejecutamos Comando
        MySqlConnection connection = new MySqlConnection();
        connection.ConnectionSource = new AppSettings();

        // Regresa resultado de la operación
        return connection.ExecuteNonQuery(command);
    }

    /// <summary>
    /// Borrar Cerveza
    /// </summary>
    /// <returns>Boleano: Que nos dice si se borro o no el registro</returns>
    public bool Delete()
    {
        // Comando
        string statement = "delete from beer where be_id = @ID";

        // Comando Prepardo
        MySqlCommand command = new MySqlCommand(statement);

        // Preparamos parametros
        command.Parameters.AddWithValue("@ID", _id);

        // Ejecutamos comando
        MySqlConnection connection = new MySqlConnection();
        connection.ConnectionSource = new AppSettings();

        // Regresamos si logro la operacion anterior
        return connection.ExecuteNonQuery(command);
    }

    /// <summary>
    /// Representacion en forma de cadena
    /// </summary>
    /// <returns>Marca con Grado, Fermentación, Presentación, Contenido y Unidad Medida</returns>
    public override string ToString()
    {
        return _brand + " " + _gradoAlcohol.ToString() + "° " + _fermentation + " " + _presentation + " " + _content + _measurementUnit;
    }

    #endregion

    #region class methods

    /// <summary>
    /// Obtener Cervezas
    /// </summary>
    /// <returns>Lista de todos los objetos cervezas que hay en la base de datos</returns>
    public static List<Beer> GetAll()
    {
        // Lista para guardar las cervezas de la base de datos
        List<Beer> list = new List<Beer>();

        // Consulta
        string query = "select be_id, be_grd_alcoh,be_presentation,be_level_ferm,be_unitMeas,be_content,br_code,cla_code,be_price,be_image from beer";

        // Comando
        MySqlCommand command = new MySqlCommand(query);

        // Ejecutar Consulta
        MySqlConnection connection = new MySqlConnection();
        connection.ConnectionSource = new AppSettings();

        // Tabla 
        DataTable table = connection.ExecuteQuery(command);

        // Iteramos filas de la consulta para asignar valores
        foreach (DataRow row in table.Rows)
        {
            // Asignamos valores 
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
            // Agregar Cerveza a la lista
            list.Add(new Beer(id, gradoalcohol, presentation, fermentation, measurementUnit, content, brand, clasification, price, image));
        }

        // Regresamos lista de cerveza
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
        MySqlConnection connection = new MySqlConnection();
        connection.ConnectionSource = new AppSettings();
        DataTable table = connection.ExecuteQuery(command);
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
            //add beer to list
            list.Add(new Beer(id, gradoalcohol, presentation, fermentation, measurementUnit, content, brand, clasification, price, image));
        }
        //return list
        return list;
    }

    public static List<Beer> GetBeers(Country co)
    {
        //list
        List<Beer> list = new List<Beer>();
        //query
        string query = "select be_id, be_grd_alcoh,be_presentation,be_level_ferm,be_unitMeas,be_content,beer.br_code,cla_code," +
            "be_price,be_image from beer join brand on beer.br_code= brand.br_code join country on country.cn_code = brand.cn_code" +
            " where country.cn_code=@CON";
        //command
        MySqlCommand command = new MySqlCommand(query);
        command.Parameters.AddWithValue("@CON", co.Id);
        //execute query
        MySqlConnection connection = new MySqlConnection();
        connection.ConnectionSource = new AppSettings();
        DataTable table = connection.ExecuteQuery(command);
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
            //add beer to list
            list.Add(new Beer(id, gradoalcohol, presentation, fermentation, measurementUnit, content, brand, clasification, price, image));
        }
        //return list
        return list;
    }



    #endregion
}

