using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        get { return (_unitPrice*_quantity); }

    }

    public Beer Beer
    {
        get { return _beer; }

    }
    public int Quantity
    {
        get { return _quantity; }

    }

    public double UnitPrice
    {
        get { return _unitPrice;}
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
    #endregion 

    public override string ToString()
    {
        return _beer+" "+_quantity+"-qty "+_unitPrice.ToString("C")+" Ammount: "+Ammount.ToString("C");
    }
}

