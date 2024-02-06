namespace Park20.Backoffice.Core.Domain.Park
{
    public class Fraction
    {
        public int FractionId { get; set; }
        public int Order { get; set; }
        public TimeSpan Minutes { get; set; }
        public VehicleType VehicleType { get; set; }
        public decimal Price { get; set; }
    }
}
