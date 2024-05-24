using congestion.calculator.Model;

namespace congestion.calculator.Models
{
    class CityConfig
    {
        public List<TaxRule> TaxRules { get; set; }
        public List<string> ExemptVehicles { get; set; }
    }
}
