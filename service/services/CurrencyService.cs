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
    
    public IEnumerable<CurrencyModel> GetCurrencyHistory()
    {
        try
        {
            return _currencyRepository.GetCurrencyHistory();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ValidationException("Error in getting currency history");
        }
    } 

    public CurrencyModel PostCurrency(CurrencyModel currencyModel)
    {
        try
        {
            currencyModel.result = ConvertCurrency(currencyModel.value, currencyModel.source, currencyModel.target);
            return _currencyRepository.PostCurrency(currencyModel);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ValidationException("Error in posting the converted currency to history");
        }
    }
    
    static decimal ConvertCurrency(decimal amount, string fromCurrency, string toCurrency)
    {
        Dictionary<string, decimal> rates = new Dictionary<string, decimal>
        {
            {"USD", 1m},
            {"EUR", 0.93m},
            {"GBP", 0.76m},
            {"JPY", 130.53m},
            {"AUD", 1.31m}
        };
        
        decimal rateToUSD = rates[fromCurrency];
        decimal amountInUSD = amount / rateToUSD;
        decimal targetRate = rates[toCurrency];
        return amountInUSD * targetRate;
    }
}