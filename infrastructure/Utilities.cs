namespace infrastructure;

public class Utilities
{
   private static readonly string DBPassword = new(Environment.GetEnvironmentVariable("PGPASSWORD"));
   public static readonly string connectionString = "Server=localhost:5432;Database=postgres;User Id=postgres;Password=" + "secret" + ";";
}