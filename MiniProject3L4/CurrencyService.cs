using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniProject3L4
{
    // Define a service class to handle currency conversion using the ExchangeRate-API
    public class CurrencyService
    {
        // keep the API key private and constant, as it should not be changed during runtime
        private const string ApiKey = "5d39c6182b245498aa91851a";

        // Create a dictionary to map countries to their respective currency codes, ignoring case for flexibility
        private static readonly Dictionary<string, string> CountryToCurrency = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Sweden", "SEK" },
            { "Peru", "PEN" },
            { "Spain", "EUR" },
            { "Germany", "EUR" },
            { "USA", "USD" },
            { "UK", "GBP" },
            { "Turkey", "TRY" },
            { "Japan", "JPY" },
            { "Canada", "CAD" },
            { "Australia", "AUD" },
            { "Mexico", "MXN" },
            { "Brazil", "BRL" },
            { "China", "CNY" },
            { "India", "INR" },
            { "Russia", "RUB" },
            { "South Africa", "ZAR" },
            { "Netherlands", "EUR" },
            { "Italy", "EUR" },
            { "France", "EUR" },
            { "Belgium", "EUR" },
            { "Switzerland", "CHF" },
            { "Norway", "NOK" },
            { "Denmark", "DKK" },
            { "Finland", "EUR" },
            { "Austria", "EUR" },
            { "Portugal", "EUR" },
            { "Greece", "EUR" },
            { "Ireland", "EUR" },
            { "Poland", "PLN" },
            { "Czech Republic", "CZK" },
            { "Hungary", "HUF" },
            { "Romania", "RON" }
        };

        //  Returns the converted price in local currency based on the provided USD price and country/office name
        public async Task<string> ConvertToLocalCurrencyAsync(decimal priceUSD, string countryOrOffice)
        {
            // Clean the input to ensure it is not null and trim any whitespace; default to "USD" if null
            string cleanKey = countryOrOffice?.Trim() ?? "USD";

            // Inicialice the target currency to USD by default; this will be updated if a valid country is found
            string targetCurrency = "USD";

            // Try to get the currency code from the dictionary; if not found, it will remain as USD
            if (CountryToCurrency.TryGetValue(cleanKey, out var code))
            {
                targetCurrency = code;
            }

            // Auxiliary variable to hold the final price after conversion; starts with the original USD price
            decimal finalPrice = priceUSD;

            // If the target currency is not USD, we need to fetch the conversion rate from the API
            if (targetCurrency != "USD")
            {
                // Block to handle potential exceptions during the API call and JSON parsing
                try
                {
                    string url = $"https://v6.exchangerate-api.com/v6/{ApiKey}/latest/USD";

                    using HttpClient client = new HttpClient();
                    string jsonResponse = await client.GetStringAsync(url);

                    using JsonDocument doc = JsonDocument.Parse(jsonResponse);

                    if (doc.RootElement.TryGetProperty("result", out JsonElement resultElement) &&
                        resultElement.GetString() == "success")
                    {
                        if (doc.RootElement.TryGetProperty("conversion_rates", out JsonElement rates))
                        {
                            if (rates.TryGetProperty(targetCurrency, out JsonElement rateElement))
                            {
                                if (rateElement.TryGetDecimal(out decimal rate))
                                {
                                    finalPrice = priceUSD * rate; 
                                    // Calculate the final price in the target currency using the conversion rate
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[CONTROLLED EXCEPTION WARNING]: Could not process currency conversion ({ex.Message}).");
                    Console.WriteLine("The program will continue using the original USD value.");
                    Console.ResetColor();
                    Console.WriteLine();

                    // If an exception occurs, we default to using the original USD price and set the target currency back to USD
                    targetCurrency = "USD";
                }
            }

            // Round the final price to the nearest whole number for display purposes and return it as a formatted string with the currency code
            int roundedPrice = (int)Math.Round(finalPrice);
            return $"{roundedPrice} {targetCurrency}";
        }
    }
}
