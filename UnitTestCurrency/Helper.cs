using Npgsql;

namespace Test;

public static class Helper
{
    public static readonly NpgsqlDataSource DataSource;

    public static readonly string apiBaseUrl = "http://localhost:5100/";

    static Helper() 
    { 
        var  DBPassword = Environment.GetEnvironmentVariable("PGPASSWORD"); 
        var connectionString = "Server=localhost:5432;Database=postgres;User Id=postgres;Password=secret;"; 
        DataSource = new NpgsqlDataSourceBuilder(connectionString).Build();
        DataSource.OpenConnection().Close();
    }

}