using Dapper;
using infrastructure.datamodels;
using Npgsql;

namespace infrastructure.repositories;

public class CurrencyRepository
{
    private readonly NpgsqlDataSource _dataSource;
    private readonly bool _testing;
    private List<CurrencyModel> list;

    public CurrencyRepository(NpgsqlDataSource dataSource, bool testing)
    {
        _dataSource = dataSource;
        _testing = testing;
        list = new List<CurrencyModel>();
    }
    
    //Gets all the entries from the databases history table.
    public IEnumerable<CurrencyModel> GetCurrencyHistory()
    {
        if (_testing)
        {
            CurrencyModel model1 = new CurrencyModel
                { Date = DateTime.Today, Source = "EUR", Target = "USD", Value = 30, Result = 25, Testing = true};
            CurrencyModel model2 = new CurrencyModel { Date = DateTime.Today, Source = "USD", Target = "EUR", Value = 40, Result = 20, Testing = true};
            list.Add(model1);
            list.Add(model2);
            foreach (var item in list)
            {
                yield return item;
            }
        }
        else
        {
            var sql = @"SELECT * FROM history;";

            using (var conn = _dataSource.OpenConnection())
            { 
                foreach (var item in conn.Query<CurrencyModel>(sql))
                {
                    yield return item;
                }
            }
        }
    }

    //Post a new entry in the databases history table.
    public CurrencyModel PostCurrency(CurrencyModel currencyModel)
    {
        if (currencyModel.Testing)
        {
            return currencyModel;
        }
        else
        {
            var sql =
                @"INSERT INTO history (""Date"", ""Source"", ""Target"", ""Value"", ""Result"") VALUES (@Date, @Source, @Target, @Value, @Result) RETURNING *;";
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