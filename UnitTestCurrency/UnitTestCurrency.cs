using System.Net.Http.Json;
using Dapper;
using Newtonsoft.Json;
using NUnit.Framework;
using FluentAssertions;

namespace Test;

[TestFixture]
public class UnitTestCurrency
{
    [Test]
    public async Task GetAllEntries()
    {
        //Helper.TriggerRebuild();

        var expected = new List<object>();
        for (var i = 1; i <= 2; i++)
        {
            var entry = new CurrencyModel()
            {
                id = 1,
                date = DateTime.Today,
                source = "USD",
                target = "EUR",
                value = 10,
                result = 0
            };
            expected.Add(entry);

            var sql =
                @"INSERT INTO History (Date, Source, Target, Value, Result) VALUES (@date, @source, @target, @value, @result);";

            using (var conn = Helper.DataSource.OpenConnection())
            {
                conn.Execute(sql, entry);
            }
        }
    }

    [TestCase( "USD", "GBP", 10)]
    [TestCase( "GBP", "USD", 20)]
    public async Task CurrencyEntryIsSuccessfullyPushedToDb(string source, string target, decimal value)
    {
        //Helper.TriggerRebuild();

        var testEntry = new CurrencyModel
        {
            id = 1,
            date = DateTime.Today,
            source = source,
            target = target,
            value = value,
            result = 0,
        };
        var httpReponse = await new HttpClient().PostAsJsonAsync(Helper.apiBaseUrl + "currency/post", testEntry);

        var responseBodyString = await httpReponse.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<ResponseDto<CurrencyModel>>(responseBodyString);

        await using (await Helper.DataSource.OpenConnectionAsync())
        {
            obj.ResponseData.result.Should().NotBe(testEntry.result);
        }
    }
}