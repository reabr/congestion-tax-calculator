using congestion.calculator.Model;
using congestion.calculator.Models;
using System.Text.Json;

namespace congestion.calculator.Services
{
    public interface ITaxConfigService
    {
        List<TaxRule> GetTaxRules(string city);
        bool IsTaxExemptVehicle(string vehicleType, string city);
        bool IsTaxExemptDate(DateTime date, string city);
    }

    public class TaxConfigService : ITaxConfigService
    {
        public List<TaxRule> GetTaxRules(string city)
        {
            string jsonFilePath = @"C:\Users\reyha\OneDrive\Desktop\Backend Technical Test\congestion-tax-calculator\congestion-tax-calculator\config.json";
            string jsonString = File.ReadAllText(jsonFilePath);

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new TimeSpanConverter() }
            };
            Dictionary<string, CityConfig> cityConfig = JsonSerializer.Deserialize<Dictionary<string, CityConfig>>(jsonString, options);

            if (cityConfig != null && cityConfig.ContainsKey(city))
            {
                return cityConfig[city].TaxRules;
            }
            return new List<TaxRule>();
        }
        public bool IsTaxExemptVehicle(string vehicleType, string city)
        {
            return vehicleType.Equals("Motorbike");
        }

        public bool IsTaxExemptDate(DateTime date, string city)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }
    }

}
