using api.controller;
using infrastructure;
using infrastructure.datamodels;
using infrastructure.repositories;
using service.services;

namespace CurrencyTests;

[TestFixture]
public class UnitTestCurrency
{
    private CurrencyController _currencyController;

    [SetUp]
    public void Setup()
    {
        _currencyController = new CurrencyController(new CurrencyService(new CurrencyRepository(null)));
    }

    [Test]
    public void GetCurrencyTest()
    {
        var result = _currencyController.GetCurrencyHistory(true);
        
        
        Assert.That(result.MessageToClient, Is.EqualTo("Successfully got all prior conversions of currency"));
    }
    
    [Test]
    public void PostCurrencyTest()
    {
        var currencyModel = new CurrencyModel
            { Date = DateTime.Today, Source = "USD", Target = "USD", Value = 20, Testing = true };
        
        var result = _currencyController.PostCurrency(currencyModel);
        
        Assert.That(result.MessageToClient, Is.EqualTo("Successfully created new entry of currency conversion"));
    }
    [TearDown]
    public void TearDown()
    {
        _currencyController.Dispose();
    }

    [Test]
    public void UtilityTest()
    {
        var expectedResult = "Server=localhost:5432";
        var result = Utilities.connectionStringDev.Substring(0, 21);
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}