using System.ComponentModel.DataAnnotations;
using infrastructure.repositories;
using infrastructure.datamodels;

namespace service.services;

public class CurrencyService
{
    private readonly CurrencyRepository _currencyRepository;
    
    public CurrencyService(CurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }
    
    //Get method which calls the method in the infrastructure.
    public IEnumerable<CurrencyModel> GetCurrencyHistory(bool testing)
    {
            return _currencyRepository.GetCurrencyHistory(testing);
    } 
    //Create method which uses the converter method before sending the it to the infrastructure.
    public CurrencyModel PostCurrency(CurrencyModel currencyModel)
    {
            currencyModel.Result = ConvertCurrency(currencyModel.Value, currencyModel.Source, currencyModel.Target);
            
            return _currencyRepository.PostCurrency(currencyModel);
    }
    
    static decimal ConvertCurrency(decimal amount, string fromCurrency, string toCurrency)
    {
        //Library of usable currencies for conversion. 
        Dictionary<string, decimal> rates = new Dictionary<string, decimal>
        {
            {"USD", 1m},
            {"EUR", 0.93m},
            {"GBP", 0.76m},
            {"JPY", 130.53m},
            {"AUD", 1.31m}
        };
        
        //The logic behind how the conversion of Value is calculated
        decimal rateToUSD = rates[fromCurrency];
        decimal amountInUSD = amount / rateToUSD;
        decimal targetRate = rates[toCurrency];
        return amountInUSD * targetRate;
    }
}