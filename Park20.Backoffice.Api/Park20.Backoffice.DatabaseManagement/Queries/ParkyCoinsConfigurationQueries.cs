namespace Park20.Backoffice.DatabaseManagement.Sql.Queries
{
    public class ParkyCoinsConfigurationQueries
    {
        public static string GetBulkValue => @"Select Amount From [ParkyCoinsConfiguration] WHERE ConfigName='BulkValue'";
        public static string UpdateBulkValue => @"UPDATE [ParkyCoinsConfiguration] SET Amount = @Value WHERE ConfigName='BulkValue'";
        public static string GetParkingValue => @"Select Amount From [ParkyCoinsConfiguration] WHERE ConfigName='ParkingValue'";
        public static string UpdateParkingValue => @"UPDATE [ParkyCoinsConfiguration] SET Amount = @Value WHERE ConfigName='ParkingValue'";
        public static string GetRegestryValue => @"Select Amount From [ParkyCoinsConfiguration] WHERE ConfigName='RegestryValue'";
        public static string UpdateRegestryValue => @"UPDATE [ParkyCoinsConfiguration] SET Amount = @Value WHERE ConfigName='RegestryValue'";
        public static string GetCurrencyValue => @"Select Amount  from [ParkyCoinsConfiguration] WHERE ConfigName='ParkyCoinsValue'";
        public static string UpdateCurrencyValue => @"UPDATE [ParkyCoinsConfiguration] SET Amount = @Value WHERE ConfigName='ParkyCoinsValue'";

    }
}
