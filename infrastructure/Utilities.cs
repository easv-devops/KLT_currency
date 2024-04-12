namespace infrastructure;

public class Utilities
{
   private static readonly string DBPassword = new(Environment.GetEnvironmentVariable("PGPASSWORD"));
   public static readonly string connectionStringDev = "Server=localhost:5432;Database=postgres;User Id=postgres;Password=" + "secret" + ";";
   public static readonly string connectionStringProd = "Server=app-database:5432;Database=postgres;User Id=postgres;Password=" + "secret" + ";";
}