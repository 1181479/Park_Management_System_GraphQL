using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.Core.Domain.Park;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.Core.Dtos.Requests
{
    public class PriceTableDto
    {
        public string ParkName { get; set; }
        public double NightFee { get; set; }
        public DateTime InitialDate { get; set; }
        public List<LinePriceTableDto> PriceLines { get; set; }

        public PriceTableDto(string parkName, double nightFee, DateTime initialDate, List<LinePriceTableDto> priceLines)
        {
            ParkName = parkName;
            NightFee = nightFee;
            InitialDate = initialDate;
            PriceLines = priceLines;
        }
    }

    public class LinePriceTableDto
    {
        public PeriodDto Period { get; set; }

        public LinePriceTableDto(PeriodDto period)
        {
            Period = period;
        }
    }

    public class PeriodDto
    {
        public string InitialTime { get; set; }
        public string FinalTime { get; set; }
        public List<FractionsDto> FractionList { get; set; }

        public PeriodDto(string initialTime, string finalTime, List<FractionsDto> fractionList)
        {
            InitialTime = initialTime;
            FinalTime = finalTime;
            FractionList = fractionList;
        }
    }

    public class FractionsDto
    {
        public int Order { get; set; }
        public int Minutes { get; set; }
        public VehicleType VehicleType { get; set; }
        public decimal Price { get; set; }

        public FractionsDto(int order, int minutes, VehicleType vehicleType, decimal price)
        {
            Order = order;
            Minutes = minutes;
            VehicleType = vehicleType;
            Price = price;
        }
    }


}
