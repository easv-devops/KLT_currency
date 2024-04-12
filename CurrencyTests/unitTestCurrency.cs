using api.controller;
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
        _currencyController = new CurrencyController(new CurrencyService(new CurrencyRepository(null, true)));
    }

    [Test]
    public void GetCurrencyTest()
    {
        
        var result = _currencyController.GetCurrencyHistory();
        
        Assert.AreEqual("Successfully got all prior conversions of currency", result.MessageToClient);
    }
    
    [Test]
    public void PostCurrencyTest()
    {
        var currencyModel = new CurrencyModel
            { Date = DateTime.Today, Source = "USD", Target = "USD", Value = 20, Testing = true };
        
        var result = _currencyController.PostCurrency(currencyModel);
        
        Assert.AreEqual("Successfully created new entry of currency conversion", result.MessageToClient);
    }
    
    [TearDown]
    public void TearDown()
    {
        // Dispose of _currencyController after each test
        _currencyController.Dispose();
    }
}