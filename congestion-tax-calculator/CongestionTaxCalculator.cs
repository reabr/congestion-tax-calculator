using congestion.calculator.Model;
using congestion.calculator.Services;

public class CongestionTaxCalculator
{
    private readonly ITaxConfigService _configService;

    public CongestionTaxCalculator(ITaxConfigService configService)
    {
        _configService = configService;
    }

    public int CalculateTax(Vehicle vehicle, List<DateTime> passages, string city)
    {
        if (_configService.IsTaxExemptVehicle(vehicle.GetVehicleType(), city))
            return 0;

        int totalTax = 0;
        DateTime lastChargeTime = passages.First();
        int highestFeeInOneHour = 0;

        foreach (var passage in passages.OrderBy(p => p))
        {
            if (_configService.IsTaxExemptDate(passage, city))
                continue;

            int currentFee = CalculatePassageFee(passage, city);

            if ((passage - lastChargeTime).TotalMinutes <= 60)
            {
                highestFeeInOneHour = Math.Max(highestFeeInOneHour, currentFee);
            }
            else
            {
                totalTax += highestFeeInOneHour;
                lastChargeTime = passage;
                highestFeeInOneHour = currentFee;
            }
        }

        totalTax += highestFeeInOneHour;
        return Math.Min(totalTax, 60);
    }

    private int CalculatePassageFee(DateTime passage, string city)
    {
        var taxRules = _configService.GetTaxRules(city);
        foreach (var rule in taxRules)
        {
            if (passage.TimeOfDay >= rule.StartTime && passage.TimeOfDay <= rule.EndTime)
                return rule.TaxAmount;
        }
        return 0;
    }
}
