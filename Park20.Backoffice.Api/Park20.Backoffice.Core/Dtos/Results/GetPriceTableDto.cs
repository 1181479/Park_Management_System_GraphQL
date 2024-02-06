using Park20.Backoffice.Core.Dtos.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.Core.Dtos.Results
{
    public class GetPriceTableDto
    {
        public string ParkName { get; set; }
        public double NightFee { get; set; }
        public DateTime InitialDate { get; set; }
        public List<GetLinePriceTableDto> PriceLines { get; set; }

        public GetPriceTableDto(string parkName, double nightFee, DateTime initialDate, List<GetLinePriceTableDto> priceLines)
        {
            ParkName = parkName;
            NightFee = nightFee;
            InitialDate = initialDate;
            PriceLines = priceLines;
        }


        public class GetLinePriceTableDto
        {
            public GetPeriodDto Period { get; set; }

            public GetLinePriceTableDto(GetPeriodDto period)
            {
                Period = period;
            }
        }

        public class GetPeriodDto
        {
            public string InitialTime { get; set; }
            public string FinalTime { get; set; }
            public List<GetFractionsDto> FractionList { get; set; }

            public GetPeriodDto(string initialTime, string finalTime, List<GetFractionsDto> fractionList)
            {
                InitialTime = initialTime;
                FinalTime = finalTime;
                FractionList = fractionList;
            }
        }

        public class GetFractionsDto
        {
            public int Order { get; set; }
            public double Minutes { get; set; }
            public decimal AutomobilePrice { get; set; }
            public decimal MotorcyclePrice { get; set; }
            public decimal GplPrice { get; set; }
            public decimal ElectricPrice { get; set; }


            public GetFractionsDto(int order, double minutes, decimal automobilePrice, decimal motorcyclePrice, decimal gplPrice, decimal electricPrice)
            {
                Order = order;
                Minutes = minutes;
                AutomobilePrice = automobilePrice;
                MotorcyclePrice = motorcyclePrice;
                GplPrice = gplPrice;
                ElectricPrice = electricPrice;
            }
        }
    }
    
}
