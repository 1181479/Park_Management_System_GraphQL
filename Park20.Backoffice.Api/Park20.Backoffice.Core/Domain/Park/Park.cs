namespace Park20.Backoffice.Core.Domain.Park
{
    public class Park
    {
        public int Id { get; set; }
        public int NumberFloors { get; set; }
        public string ParkName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Location { get; set; }
        public DateTime OpeningTime { get; set; }
        public DateTime ClosingTime { get; set; }
        public bool IsActive { get; set; }
        public double NightFee { get; set; }
        public PriceTable PriceTable { get; set; }
        public List<ParkingSpot> ParkingSpots { get; set; }
    }
}
