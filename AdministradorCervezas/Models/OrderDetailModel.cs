using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

public class OrderDetail
{
    #region attributes
    private Beer _beer;
    private int _quantity;
    private double _unitPrice;
    #endregion

    #region properties
    public double Ammount
    {
        get { return (_unitPrice * _quantity); }

    }

    public Beer Beer
    {
        get { return _beer; }
        set { _beer = value; }

    }
    public int Quantity
    {
        get { return _quantity; }
        set { _quantity = value; }

    }

    public double UnitPrice
    {
        get { return _unitPrice; }
        set { _unitPrice = value; }
    }

    #endregion

    #region constructor


    public OrderDetail()
    {
        _beer = new Beer();
        _quantity = 0;
        _unitPrice = 0;
    }

    public OrderDetail(Beer beer, int quantity, double unitprice)
    {
        _beer = beer;
        _quantity = quantity;
        _unitPrice = unitprice;
    }

    public static List<OrderDetail> AllOrderDetails(int id)
    {

        List<OrderDetail> details = new List<OrderDetail>();
        //query
        string query = @"select ord_id, be_id, ordDet_quantity, ordDet_UnitPrice from orderdetail where ord_id =@ID";

        //command
        MySqlCommand command = new MySqlCommand(query);
        //parameter 
        command.Parameters.AddWithValue("@ID", id);
        //execute command
        MySqlConnection connection = new MySqlConnection();
        connection.ConnectionSource = new AppSettings();
        DataTable table = connection.ExecuteQuery(command);
        //check if rows found 
        if (table.Rows.Count > 0)
        {

            //get first(and only) found row
            DataRow row = table.Rows[0];

            OrderDetail currentDetail = new OrderDetail();
            //read the values of the the field
            currentDetail.Beer = (new Beer((int)row["be_id"]));
            currentDetail.Quantity = (int)row["ord_ordDet_quantity"];
            currentDetail.UnitPrice = (double)row["ordDet_UnitPrice"];
            details.Add(currentDetail);
        }

        return details;
    }
    #endregion 

    public override string ToString()
    {
        return _beer + " " + _quantity + "-qty " + _unitPrice.ToString("C") + " Ammount: " + Ammount.ToString("C");
    }
}

