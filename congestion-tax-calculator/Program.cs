using congestion.calculator.Model;
using congestion.calculator.Services;
class Program
{
    static void Main()
    {
        ITaxConfigService configService = new TaxConfigService();
        CongestionTaxCalculator calculator = new CongestionTaxCalculator(configService);

        Vehicle vehicle = new Car();

        List<DateTime> testDates = new List<DateTime>
        {
             new DateTime(2023, 5, 10, 6, 15, 0),
             new DateTime(2023, 5, 10, 6, 45, 0),
             new DateTime(2023, 5, 10, 7, 30, 0),
             new DateTime(2023, 5, 10, 8, 15, 0),
             new DateTime(2023, 5, 10, 9, 00, 0),
             new DateTime(2023, 5, 10, 14, 30, 0),
             new DateTime(2023, 5, 10, 15, 15, 0),
             //new DateTime(2023, 5, 10, 15, 45, 0),
             //new DateTime(2023, 5, 10, 16, 30, 0),
             //new DateTime(2023, 5, 10, 17, 15, 0),
             //new DateTime(2023, 5, 10, 18, 10, 0),
        };

        int totalTax = calculator.CalculateTax(vehicle, testDates, "Gothenburg");

        Console.WriteLine($"Total tax for the vehicle: {totalTax} SEK");
    }
}
