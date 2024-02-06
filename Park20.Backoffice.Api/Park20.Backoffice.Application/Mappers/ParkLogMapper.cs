using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.Core.Dtos.Requests;

namespace Park20.Backoffice.Application.Mappers
{
    public static class ParkLogMapper
    {
        public static ParkLog ToParkLogDomain(string licensePlate, string parkName)
        {
            return new ParkLog(licensePlate, parkName);
        }
    }
}
