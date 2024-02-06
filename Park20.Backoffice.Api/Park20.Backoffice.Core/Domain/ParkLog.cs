using Park20.Backoffice.Core.Domain.Payment;

namespace Park20.Backoffice.Core.Domain
{
    public class ParkLog
    {

        public string LicensePlate { get; private set; }
        public string ParkName { get; private set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Invoice Invoice { get; private set; }

        public ParkLog() { }

        public ParkLog(string vehicleId, string parkId)
        {
            LicensePlate = vehicleId;
            ParkName = parkId;
            StartTime = DateTime.Now;
        }

        public ParkLog(string vehicleId, string parkId, Invoice invoice)
        {
            LicensePlate = vehicleId;
            ParkName = parkId;
            StartTime = DateTime.Now;
            Invoice = invoice;
        }
    }
}
