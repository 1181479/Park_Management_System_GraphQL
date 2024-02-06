using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.DatabaseManagement.Sql.Queries
{
    public class ParkingSpotQueries
    {
        public static string AllParkingSpotsAvailableByVehicleType =>
        "SELECT * FROM [ParkingSpot] (NOLOCK) WHERE [VehicleType] = @VehicleType AND [Status] IS NOT NULL AND [ParkId] = @ParkId";

        public static string ParkingSpotById => "SELECT * FROM [ParkingSpot] (NOLOCK) WHERE [Id] = @ParkId";

    }
}
