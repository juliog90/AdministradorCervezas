using System.Configuration;


class AppSettings : ISettings
{
    public string getSettings()
    {
        return ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
    }
}

