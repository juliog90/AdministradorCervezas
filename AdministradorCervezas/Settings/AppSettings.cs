using System.Configuration;


public class AppSettings : ISettings
{
    public string getSettings()
    {
        return ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
    }
}

