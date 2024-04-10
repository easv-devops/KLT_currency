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
        var sql = @"SELECT * FROM History;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<CurrencyModel>(sql);
        }
    }

    //Post a new entry in the databases history table.
    public CurrencyModel PostCurrency(CurrencyModel currencyModel)
    {
        var sql =
            @"INSERT INTO History (Date, Source, Target, Value, Result) VALUES (@date, @source, @target, @value, @result) RETURNING *;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<CurrencyModel>(sql,
                new
                {
                    data = currencyModel.date, source = currencyModel.source, target = currencyModel.target,
                    value = currencyModel.value, result = currencyModel.result
                });
        }
    }
}