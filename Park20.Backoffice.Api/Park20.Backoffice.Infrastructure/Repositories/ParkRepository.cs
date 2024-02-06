using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using Park20.Backoffice.DatabaseManagement.Sql.Queries;
using Park20.Backoffice.Core.Domain.Park;
using Park20.Backoffice.Core.IRepositories;
using System.Data;
using Park20.Backoffice.Core.Domain;

namespace Park20.Backoffice.Infrastructure.Repositories
{
    public class ParkRepository(IConfiguration configuration) : IParkRepository
    {
        public async Task<List<ParkingSpot>> GetParkingSpotsByParkName(string parkName)
        {
            using IDbConnection dbConnection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            {

                var parkingSpots = await dbConnection.QueryAsync<ParkingSpot>(ParkQueries.ParkingSpotsByParkName, new { ParkName = parkName });

                return parkingSpots.ToList();
            }
        }

        public async Task<List<ParkingSpot>> GetParkingSpotsAvailableByParkName(string parkName)
        {
            using IDbConnection dbConnection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            {
                var parkingSpots = await dbConnection.QueryAsync<ParkingSpot>(ParkQueries.ParkingSpotsAvailableByParkName, new { ParkName = parkName });

                return parkingSpots.ToList();
            }
        }

        public async Task<Park> GetParkByName(string name)
        {
            using IDbConnection dbConnection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            {

                var lookup = new Dictionary<int, Park>();
                await dbConnection.QueryAsync<Park, PriceTable, LinePriceTable, Period, ParkingSpot, Park>(
                    ParkQueries.ParkByName,
                    (park, priceTable, linePriceTable, period, parkingSpot) =>
                    {
                        Park parkEntry;

                        if (!lookup.TryGetValue(park.Id, out parkEntry))
                        {
                            lookup.Add(park.Id, parkEntry = park);
                            parkEntry.PriceTable = priceTable;

                            if (parkEntry.PriceTable != null)
                                parkEntry.PriceTable.LinePrices = new List<LinePriceTable>();
                        }

                        if (linePriceTable != null && parkEntry.PriceTable != null)
                        {
                            linePriceTable.Period = period;

                            if (period != null && !parkEntry.PriceTable.LinePrices.Any(lp => lp.Period?.PeriodId == period.PeriodId))
                            {
                                parkEntry.PriceTable.LinePrices.Add(linePriceTable);

                                // Get Fractions of the current Period and add them
                                var fractions = GetFractionsOfPeriod(period.PeriodId);
                                period.Fractions = fractions.ToList();
                            }
                        }

                        // Check if the ParkingSpot already exists in the Park
                        if (parkingSpot != null && parkEntry.ParkingSpots?.All(ps => ps.ParkingSpotId != parkingSpot.ParkingSpotId) != false)
                        {
                            if (parkEntry.ParkingSpots == null)
                                parkEntry.ParkingSpots = new List<ParkingSpot>();

                            parkEntry.ParkingSpots.Add(parkingSpot);
                        }

                        return parkEntry;
                    },
                    new { ParkName = name }, // Parameters for the query
                    splitOn: "PriceTableId,LinePriceTableId,PeriodId,ParkingSpotId"
                );

                return lookup.Values.FirstOrDefault(); // Return the first matching park
            }
        }
        public async Task<IEnumerable<Park>> GetAllParks()
        {
            using IDbConnection dbConnection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            {
                var lookup = new Dictionary<int, Park>();
                await dbConnection.QueryAsync<Park, PriceTable, LinePriceTable, Period, ParkingSpot, Park>(
                    ParkQueries.AllParks,
                    (park, priceTable, linePriceTable, period, parkingSpot) =>
                    {
                        Park parkEntry;

                        if (!lookup.TryGetValue(park.Id, out parkEntry))
                        {
                            lookup.Add(park.Id, parkEntry = park);
                            parkEntry.PriceTable = priceTable;

                            if (parkEntry.PriceTable != null)
                                parkEntry.PriceTable.LinePrices = new List<LinePriceTable>();
                        }

                        if (linePriceTable != null && parkEntry.PriceTable != null)
                        {
                            linePriceTable.Period = period;

                            if (period != null && !parkEntry.PriceTable.LinePrices.Any(lp => lp.Period?.PeriodId == period.PeriodId))
                            {
                                parkEntry.PriceTable.LinePrices.Add(linePriceTable);

                                // Get Fractions of the current Period and add them
                                var fractions = GetFractionsOfPeriod(period.PeriodId);
                                period.Fractions = fractions.ToList();
                            }
                        }

                        // Check if the ParkingSpot already exists in the Park
                        if (parkingSpot != null && parkEntry.ParkingSpots?.All(ps => ps.ParkingSpotId != parkingSpot.ParkingSpotId) != false)
                        {
                            if (parkEntry.ParkingSpots == null)
                                parkEntry.ParkingSpots = new List<ParkingSpot>();

                            parkEntry.ParkingSpots.Add(parkingSpot);
                        }

                        return parkEntry;
                    },
                    splitOn: "PriceTableId,LinePriceTableId,PeriodId,ParkingSpotId"
                );

                return lookup.Values.ToList();
            }
        }
        private IEnumerable<Fraction> GetFractionsOfPeriod(int periodId)
        {
            using IDbConnection dbConnection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            {
                var fractions = dbConnection.Query<Fraction>(ParkQueries.Fractions, new { PeriodId = periodId });

                return fractions;
            }
        }

        public async Task<bool> UpdateParkPriceTable(string ParkName, double NightFee, PriceTable table)
        {
            Park park = await GetParkByName(ParkName);
            if (park == null)
            {
                return false;
            }
            using IDbConnection dbConnection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            {
                try
                {
                    int priceTableId = await dbConnection.ExecuteScalarAsync<int>(
                        ParkQueries.CreatePriceTable + " SELECT SCOPE_IDENTITY();",
                        new { InitialDate = table.InitialDate }
                    );

                    foreach (var lineTables in table.LinePrices)
                    {
                        int periodId = await dbConnection.ExecuteScalarAsync<int>(
                        ParkQueries.CreatePeriod + " SELECT SCOPE_IDENTITY();",
                        new { InitialTime = lineTables.Period.InitialTime, FinalTime = lineTables.Period.FinalTime }
                    );

                        int linePriceId = await dbConnection.ExecuteScalarAsync<int>(
                        ParkQueries.CreateLinePriceTable + " SELECT SCOPE_IDENTITY();",
                        new { PriceTableId = priceTableId, PeriodId = periodId }

                    );
                        foreach (var fraction in lineTables.Period.Fractions)
                        {
                            await dbConnection.ExecuteAsync(ParkQueries.CreateFraction,
                                new { Order = fraction.Order, Minutes = fraction.Minutes, VehicleType = fraction.VehicleType, Price = fraction.Price, PeriodId = periodId }
                            );
                        }
                    }
                    await dbConnection.ExecuteAsync(ParkQueries.UpdateParkPriceTable,
                                new { ParkId = park.Id, NightFee = NightFee, PriceTableId = priceTableId }
                            );
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }

        }
        public async Task<List<string>> GetParkNames()
        {
            using IDbConnection dbConnection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            {

                var parkNames = await dbConnection.QueryAsync<string>(ParkQueries.GetParkNames);
                return parkNames.ToList();

            }
        }

        public async Task<bool> UpdateParkingSpotStatus(bool status, int parkingSpotId)
        {
            using IDbConnection dbConnection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            {

                int rowsAffected = await dbConnection.ExecuteAsync(ParkQueries.UpdateParkingSpotStatus, new { Status = status, Id = parkingSpotId });

                return rowsAffected > 0;

            }
        }
    }
}

