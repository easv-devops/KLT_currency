using System;
using System.Collections.Generic;

namespace CurrencyConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, decimal> rates = new Dictionary<string, decimal>
            {
                {"USD", 1m},
                {"EUR", 0.93m},
                {"GBP", 0.76m},
                {"JPY", 130.53m},
                {"AUD", 1.31m}
            };

            Console.WriteLine("Welcome to the Currency Converter!");
            Console.WriteLine("Supported currencies: USD, EUR, GBP, JPY, AUD");
            Console.Write("Enter the source currency: ");
            string fromCurrency = Console.ReadLine().ToUpper();

            Console.Write("Enter the target currency: ");
            string toCurrency = Console.ReadLine().ToUpper();

            Console.Write("Enter the amount to convert: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());

            if (!rates.ContainsKey(fromCurrency) || !rates.ContainsKey(toCurrency))
            {
                Console.WriteLine("Unsupported currency.");
                return;
            }

            decimal convertedAmount = ConvertCurrency(amount, fromCurrency, toCurrency, rates);
            Console.WriteLine($"{amount} {fromCurrency} is {convertedAmount} {toCurrency}.");
        }

        static decimal ConvertCurrency(decimal amount, string fromCurrency, string toCurrency, Dictionary<string, decimal> rates)
        {
            decimal rateToUSD = rates[fromCurrency];
            decimal amountInUSD = amount / rateToUSD;
            decimal targetRate = rates[toCurrency];
            return amountInUSD * targetRate;
        }
    }
}