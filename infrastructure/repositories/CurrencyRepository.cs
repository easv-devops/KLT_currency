using Dapper;
using infrastructure.datamodels;
using Npgsql;

namespace infrastructure.repositories;

public class CurrencyRepository
{

    private readonly NpgsqlDataSource _dataSource;

    public CurrencyRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }
    
    //Gets all the entries from the databases history table.
    public IEnumerable<CurrencyModel> GetCurrencyHistory()
    {
        var sql = @"SELECT * FROM history;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<CurrencyModel>(sql);
        }
    }

    //Post a new entry in the databases history table.
    public CurrencyModel PostCurrency(CurrencyModel currencyModel)
    {
        Console.WriteLine(currencyModel.Date);
        Console.WriteLine(currencyModel.Result);
        Console.WriteLine(currencyModel.Source);
        Console.WriteLine(currencyModel.Value);
        Console.WriteLine(currencyModel.Target);
        
        var sql =
            @"INSERT INTO history (""Date"", ""Source"", ""Target"", ""Value"", ""Result"") VALUES (@Date, @Source, @Target, @Value, @Result) RETURNING *;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<CurrencyModel>(sql,
                new
                {
                    Date = currencyModel.Date, // Corrected from data to Date
                    Source = currencyModel.Source,
                    Target = currencyModel.Target,
                    Value = currencyModel.Value,
                    Result = currencyModel.Result
                });
        }

    }
}