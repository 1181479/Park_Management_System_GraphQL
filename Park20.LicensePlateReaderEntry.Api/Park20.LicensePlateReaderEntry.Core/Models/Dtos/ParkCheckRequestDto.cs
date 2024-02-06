namespace Park20.LicensePlateReader.Api.Models.Dtos
{
    public class ParkCheckRequestDto
    {
        public string LicensePlate { get; set; }
        public string ParkName { get; set; }
        public bool IsEntrance { get; set; }
    }
}
