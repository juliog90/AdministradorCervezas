using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class Place
{
    #region attributes

    protected string _id;
    protected string _name;

    #endregion

    #region properties

    /// <summary>
    /// Id
    /// </summary>
    public string Id
    {
        get { return _id; }
        set { _id = value; }
    }
    /// <summary>
    /// Name
    /// </summary>
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    #endregion

    #region constructors

    /// <summary>
    /// Creates an empty object
    /// </summary>
    public Place()
    {
        _id = "";
        _name = "";
    }
    /// <summary>
    /// Creates an object with data from the arguments
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="name">Name</param>
    public Place(string id, string name)
    {
        _id = id;
        _name = name;
    }

    #endregion

    #region methods

    /// <summary>
    /// Represents the object as a string
    /// </summary>
    /// <returns></returns>
    public abstract override string ToString();

    #endregion
}