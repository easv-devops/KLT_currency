using Dapper;
using infrastructure.datamodels;
using Npgsql;

namespace infrastructure.repositories;

public class CurrencyRepository
{
    private readonly NpgsqlDataSource? _dataSource;
    private readonly List<CurrencyModel> list;

    public CurrencyRepository(NpgsqlDataSource? dataSource)
    {
        if (dataSource != null)
        {
            _dataSource = dataSource;
        }
        list = new List<CurrencyModel>();
        CurrencyModel model1 = new CurrencyModel { Date = DateTime.Today, Source = "EUR", Target = "USD", Value = 30, Result = 25, Testing = true};
        CurrencyModel model2 = new CurrencyModel { Date = DateTime.Today, Source = "USD", Target = "EUR", Value = 40, Result = 20, Testing = true};
        list.Add(model1);
        list.Add(model2);
    }
    
    //Gets all the entries from the databases history table.
    public IEnumerable<CurrencyModel> GetCurrencyHistory(bool testing)
    {
        var sql = @"SELECT * FROM history;";
       if (testing)
       {
           return list;
       } else {
            using (var conn = _dataSource.OpenConnection())
            {
                return conn.Query<CurrencyModel>(sql);
            }
        }
    }

    //Post a new entry in the databases history table.
    public CurrencyModel PostCurrency(CurrencyModel currencyModel)
    {
        var sql =
                     @"INSERT INTO history (""Date"", ""Source"", ""Target"", ""Value"", ""Result"") VALUES (@Date, @Source, @Target, @Value, @Result) RETURNING *;";
        if (currencyModel.Testing)
        {
            return currencyModel;
        }
        else
        {
            using (var conn = _dataSource.OpenConnection())
            {
                return conn.QueryFirst<CurrencyModel>(sql,
                    new
                    {
                        Date = currencyModel.Date,
                        Source = currencyModel.Source,
                        Target = currencyModel.Target,
                        Value = currencyModel.Value,
                        Result = currencyModel.Result
                    });
            }
        }
    }
}