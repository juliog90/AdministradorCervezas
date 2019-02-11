using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
public class Order
{

    #region Attributes

    private int _id;
    private DateTime _requestDate;
    private DateTime _deliveryDate;
    private Customer _customer;
    private double _total;

    #endregion

    #region Properties

    public int Id
    {
        get { return _id; }
    }

    public DateTime Requestdate
    {
        get { return _requestDate; }
        set { _requestDate = value; }
    }

    public DateTime Deliverydate
    {
        get { return _deliveryDate; }
    }

    public Customer Customer
    {
        get { return _customer; }
    }

    public List<OrderDetail> AllDetails
    {
        get
        {
            List<OrderDetail> details = new List<OrderDetail>();
            //query
            string query = @"select ord_id, be_id, ordDet_quantity, ordDet_UnitPrice from orderdetail where ord_id =@ID";

            //command
            MySqlCommand command = new MySqlCommand(query);
            //parameter 
            command.Parameters.AddWithValue("@ID", _id);
            //execute command
            DataTable table = MySqlConnection.ExecuteQuery(command);
            //check if rows found 
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    OrderDetail currentDetail = new OrderDetail();
                    //read the values of the the field
                    currentDetail.Beer = (new Beer((int)row["be_id"]));
                    currentDetail.Quantity = (int)row["ordDet_quantity"];
                    currentDetail.UnitPrice = (double)row["ordDet_UnitPrice"];
                    details.Add(currentDetail); 
                }
            }

            return details;
        }
    }

    public double Total
    {
        get
        {
            double total = 0;
            foreach(OrderDetail detail in AllDetails)
            {
                total += detail.Ammount;
            }

            return total;

        }

        set
        {
            _total = value;
        }
    }

    #endregion
    public Order(int id)
    {
        //query
        string query = @"SELECT ord_id,ord_request_date,ord_delivery_date,cus_id from Orders where ord_id=@ID";

        //command
        MySqlCommand command = new MySqlCommand(query);
        //parameter 
        command.Parameters.AddWithValue("@ID", id);
        //execute command
        DataTable table = MySqlConnection.ExecuteQuery(command);
        //check if rows found 
        if (table.Rows.Count > 0)
        {

            //get first(and only) found row
            DataRow row = table.Rows[0];
            //read the values of the the field
            _id = id;
            _requestDate = (DateTime)row["ord_request_date"];
            _deliveryDate = (DateTime)row["ord_delivery_date"];
            _customer = new Customer((int)row["cus_id"]);
        }

        query = @"select be.be_id,br.br_code,cl.cla_code,br.br_name,cl.cla_name, be.be_grd_alcoh, be.be_presentation,be.be_level_ferm,be.be_unitMeas,be.be_content,od.ordDet_quantity,be.be_price,od.ordDet_UnitPrice, be.be_image
                    from beer be join brand br on br.br_code=be.br_code join clasification cl on cl.cla_code = be.cla_code join orderdetail od on
                    od.be_id = be.be_id join orders ord on ord.ord_id = od.ord_id where od.ord_id=@ID";
        command = new MySqlCommand(query);
        command.Parameters.AddWithValue("@ID", id);
        //execute again command        
        table = MySqlConnection.ExecuteQuery(command);



        foreach (DataRow row in table.Rows)
        {
            Brand brand = new Brand((string)row["br_code"], (string)row["br_name"]);
            Clasification clasification = new Clasification((string)row["cla_code"], (string)row["cla_name"]);
            Beer beer = new Beer((int)row["be_id"], (double)row["be_grd_alcoh"], (PresentationType)(int)row["be_presentation"], (Fermentation)(int)row["be_level_ferm"], (MeasurementUnit)(int)row["be_unitMeas"], (double)row["be_content"], brand, clasification, (double)row["be_price"], (string)row["be_image"]);
            OrderDetail detail = new OrderDetail(beer, (int)row["ordDet_quantity"], (double)row["ordDet_UnitPrice"]);
        }
    }

    public Order(int id, DateTime requestDate, DateTime deliveryDate, Customer customer)
    {
        _id = id;
        _requestDate = requestDate;
        _deliveryDate = deliveryDate;
        _customer = customer;
    }

    #region Instance Methods
    public static List<Order> GetAll()
    {
        //list
        List<Order> list = new List<Order>();
        //query
        string query = @"SELECT ord_id,ord_request_date,ord_delivery_date,cus_id from Orders";
        //command
        MySqlCommand command = new MySqlCommand(query);
        //execute query
        DataTable table = MySqlConnection.ExecuteQuery(command);
        //iterate rows

        foreach (DataRow row in table.Rows)
        {
            //read fields
            int id = (int)row["ord_id"];
            DateTime requestdate = (DateTime)row["ord_request_date"];
            DateTime deliverydate = (DateTime)row["ord_delivery_date"];
            Customer customer = new Customer((int)row["cus_id"]);
            list.Add(new Order(id, requestdate, deliverydate, customer));
        }
        //return list
        return list;
    }

    public static List<Order> GetOrder(DateTime d)
    {
        //list
        List<Order> list = new List<Order>();
        //query
        string query = @"SELECT ord_id,ord_request_date,ord_delivery_date,cus_id from Orders Where ord_delivery_date=@DATE";
        //command
        MySqlCommand command = new MySqlCommand(query);
        command.Parameters.AddWithValue("@DATE", d.Year + "/" + d.Month + "/" + d.Day);
        //execute query
        DataTable table = MySqlConnection.ExecuteQuery(command);
        //iterate rows

        foreach (DataRow row in table.Rows)
        {
            //read fields
            int id = (int)row["ord_id"];
            DateTime requestdate = (DateTime)row["ord_request_date"];
            DateTime deliverydate = (DateTime)row["ord_delivery_date"];
            Customer customer = new Customer((int)row["cus_id"]);
            Order ord = new Order(id);
            list.Add(new Order(id, requestdate, deliverydate, customer));

        }
        //return list
        return list;
    }

    /// <summary>
    /// Represents the object as a string
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return "Request: " + _requestDate.ToShortDateString() + "\nDelivery: " + _deliveryDate.ToShortDateString() + "\nCustomer: " + _customer.FullName + "\nSub Total: ";
    }





    #endregion

}
