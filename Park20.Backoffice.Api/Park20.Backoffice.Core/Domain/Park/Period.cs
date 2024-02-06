using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.Core.Domain.Park
{
    public class Period
    {
        public int PeriodId { get; set; }
        public TimeSpan InitialTime { get; set; }
        public TimeSpan FinalTime { get; set; }
        public List<Fraction> Fractions { get; set; }
    }
}
