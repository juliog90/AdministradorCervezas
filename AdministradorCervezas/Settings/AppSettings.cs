using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class AppSettings : ISettings
{ 
    public string getSettings()
    {
        return ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
    }
}

