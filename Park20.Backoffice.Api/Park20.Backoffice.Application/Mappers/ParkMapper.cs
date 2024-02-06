using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.Core.Domain.Park;
using Park20.Backoffice.Core.Dtos.Requests;
using Park20.Backoffice.Core.Dtos.Results;
using static Park20.Backoffice.Core.Dtos.Results.GetPriceTableDto;

namespace Park20.Backoffice.Application.Mappers
{
    public static class ParkMapper
    {

        #region To Domain
        public static PriceTable ToPriceTableDomain(PriceTableDto priceTableDto)
        {
            PriceTable table = new()
            {
                InitialDate = priceTableDto.InitialDate,
                LinePrices = ToLinePriceDomain(priceTableDto.PriceLines)
            };
            return table;
        }
        public static List<LinePriceTable> ToLinePriceDomain(List<LinePriceTableDto> LinePriceDto)
        {
            List<LinePriceTable> linePriceTables = [];
            foreach (var linePriceTableDto in LinePriceDto)
            {
                linePriceTables.Add(ToLinePriceDomain(linePriceTableDto));
            }
            return linePriceTables;
        }
        public static LinePriceTable ToLinePriceDomain(LinePriceTableDto LinePriceDto)
        {
            LinePriceTable table = new()
            {
                Period = ToPeriodDomain(LinePriceDto.Period)
            };
            return table;
        }
        public static Period ToPeriodDomain(PeriodDto periodDto)
        {
            string formato = "hh\\:mm\\:ss";

            Period period = new()
            {
                InitialTime = TimeSpan.ParseExact(periodDto.InitialTime, formato, null),
                FinalTime = TimeSpan.ParseExact(periodDto.FinalTime, formato, null),
                Fractions = ToFractionsDomain(periodDto.FractionList)
            };
            return period;
        }

        public static List<Fraction> ToFractionsDomain(List<FractionsDto> fractionsDtos)
        {
            List<Fraction> fractions = [];
            foreach (var fractionDto in fractionsDtos)
            {
                fractions.Add(ToFractionsDomain(fractionDto));
            }
            return fractions;
        }
        public static Fraction ToFractionsDomain(FractionsDto fractionsDto)
        {
            Fraction fraction = new()
            {
                Order = fractionsDto.Order,
                Price = fractionsDto.Price,
                VehicleType = fractionsDto.VehicleType,
                Minutes = TimeSpan.FromMinutes(fractionsDto.Minutes)
            };
            return fraction;
        }

        #endregion

        #region To Dto
        public static GetPriceTableDto ToPriceTableDto(Park park, PriceTable priceTable)
        {
            GetPriceTableDto dto = new GetPriceTableDto(park.ParkName, park.NightFee, priceTable.InitialDate, ToLinePriceDto(priceTable.LinePrices));
            return dto;
        }

        public static List<GetLinePriceTableDto> ToLinePriceDto(List<LinePriceTable> linePriceTables)
        {
            List<GetLinePriceTableDto> linePriceDtoList = new List<GetLinePriceTableDto>();
            foreach (var linePriceTable in linePriceTables)
            {
                linePriceDtoList.Add(ToLinePriceDto(linePriceTable));
            }
            return linePriceDtoList;
        }

        public static GetLinePriceTableDto ToLinePriceDto(LinePriceTable linePriceTable)
        {
            GetLinePriceTableDto dto = new GetLinePriceTableDto(ToPeriodDto(linePriceTable.Period));

            return dto;
        }

        public static GetPeriodDto ToPeriodDto(Period period)
        {
            GetPeriodDto dto = new GetPeriodDto(period.InitialTime.ToString("hh\\:mm\\:ss"),
                period.FinalTime.ToString("hh\\:mm\\:ss"),
                ToFractionsDto(period.Fractions));
            return dto;
        }

        public static List<GetFractionsDto> ToFractionsDto(List<Fraction> fractions)
        {

            List<GetFractionsDto> fractionsDtoList = new List<GetFractionsDto>();

            var groupedFractions = fractions.GroupBy(f => new { f.Order, f.Minutes });

            foreach(var group in groupedFractions)
            {
                decimal automobilePrice = -1;
                decimal motocyclePrice = -1;
                decimal gplPrice = -1;
                decimal electricPrice = -1;

                foreach(var fraction in group)
                {
                    switch (fraction.VehicleType)
                    {
                        case VehicleType.Motocycle:
                            motocyclePrice = fraction.Price; 
                            break;
                        case VehicleType.Electric:
                            electricPrice = fraction.Price;
                            break;
                        case VehicleType.Automobile:
                            automobilePrice = fraction.Price;
                            break;
                        case VehicleType.GPL:
                            gplPrice = fraction.Price;
                            break;
                    }
                }
                GetFractionsDto dto = new GetFractionsDto(group.Key.Order, group.Key.Minutes.TotalMinutes, automobilePrice, motocyclePrice, gplPrice, electricPrice);
                fractionsDtoList.Add(dto);
            }

            return fractionsDtoList;

        }
        #endregion
    }
}
