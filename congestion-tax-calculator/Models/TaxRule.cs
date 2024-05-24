namespace congestion.calculator.Model
{
    public class TaxRule
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int TaxAmount { get; set; }
    }

}
