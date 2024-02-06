using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.DatabaseManagement.Sql.Queries
{
    public class ParkQueries
    {
        public static string AllParks = @"
            SELECT
            p.Id,
            p.NumberFloors,
            p.ParkName,
            p.Location,
            p.Latitude,
            p.Longitude,
            p.OpeningTime,
            p.ClosingTime,
            p.IsActive,
            p.NightFee,
            pt.Id AS PriceTableId,
            pt.InitialDate,
            lpt.Id AS LinePriceTableId,
            per.Id AS PeriodId,
            per.InitialTime,
            per.FinalTime,
            ps.Id AS ParkingSpotId,
            ps.VehicleType,
            ps.Status,
            ps.FloorNumber
        FROM Park p
        INNER JOIN PriceTable pt ON p.PriceTableId = pt.Id
        LEFT JOIN LinePriceTable lpt ON pt.Id = lpt.PriceTableId
        LEFT JOIN Period per ON lpt.PeriodId = per.Id
        LEFT JOIN ParkingSpot ps ON p.Id = ps.ParkId";
        public static string ParkByName = @"SELECT
                p.Id,
                p.NumberFloors,
                p.ParkName,
                p.Latitude,
                p.Longitude,
                p.Location,
                p.OpeningTime,
                p.ClosingTime,
                p.IsActive,
                p.NightFee,
                pt.Id AS PriceTableId,
                pt.InitialDate,
                lpt.Id AS LinePriceTableId,
                per.Id AS PeriodId,
                per.InitialTime,
                per.FinalTime,
                ps.Id AS ParkingSpotId,
                ps.VehicleType,
                ps.Status,
                ps.FloorNumber
            FROM Park p
            INNER JOIN PriceTable pt ON p.PriceTableId = pt.Id
            LEFT JOIN LinePriceTable lpt ON pt.Id = lpt.PriceTableId
            LEFT JOIN Period per ON lpt.PeriodId = per.Id
            LEFT JOIN ParkingSpot ps ON p.Id = ps.ParkId
            WHERE p.ParkName = @ParkName";
        public static string ParkingSpotsByParkName = @"
            SELECT
                ps.Id AS ParkingSpotId,
                ps.VehicleType,
                ps.Status,
                ps.FloorNumber
            FROM Park p
            INNER JOIN ParkingSpot ps ON p.Id = ps.ParkId
            WHERE p.ParkName = @ParkName";

        public static string ParkingSpotsAvailableByParkName = @"
            SELECT
                ps.Id AS ParkingSpotId,
                ps.VehicleType,
                ps.Status,
                ps.FloorNumber
            FROM Park p
            INNER JOIN ParkingSpot ps ON p.Id = ps.ParkId
            WHERE p.ParkName = @ParkName AND ps.[Status] = 0";

        public static string Fractions = @"
            SELECT
            f.Id AS FractionId,
            f.[Order],
            f.Minutes,
            f.VehicleType,
            f.Price
            FROM Fraction f
            WHERE f.PeriodId = @PeriodId";


        public static string UpdateAvailableParkingSpots =>
            @"UPDATE ParkingSpot
            SET Status = CASE WHEN @Entrance = 1 THEN 1 ELSE 0 END
            WHERE Id = (SELECT TOP 1 ps.Id
                FROM ParkingSpot ps
                INNER JOIN Park p ON ps.ParkId = p.Id
                WHERE p.ParkName = @ParkName and
	            ps.Status = CASE WHEN @Entrance = 1 THEN 0 ELSE 1 END
                AND ps.VehicleType = (
                    SELECT [Type]
                    FROM Vehicle
                    WHERE LicensePlate = @LicensePlate
                )
            );";

        public static string CreatePriceTable => @"INSERT INTO PriceTable (InitialDate) VALUES (@InitialDate);";
        public static string CreateLinePriceTable => @"INSERT INTO LinePriceTable (PriceTableId, PeriodId) VALUES (@PriceTableId, @PeriodId);";
        public static string CreatePeriod => @"INSERT INTO [dbo].[Period] ([InitialTime], [FinalTime]) VALUES (@InitialTime, @FinalTime)";
        public static string CreateFraction => @"INSERT INTO [dbo].[Fraction] ([Order], [Minutes], [VehicleType], [PeriodId], [Price]) VALUES (@Order, @Minutes, @VehicleType, @PeriodId, @Price)";
        public static string UpdateParkPriceTable => @"UPDATE [dbo].[Park] SET [NightFee] = @NightFee, [PriceTableId] = @PriceTableId WHERE [Id] = @ParkId";
        public static string GetParkNames => @"SELECT DISTINCT ParkName FROM [dbo].[Park]";
        public static string UpdateParkingSpotStatus => @"UPDATE ParkingSpot SET Status = @Status WHERE Id = @id;";

    }
}
