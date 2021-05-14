using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gridnine.FlightCodingTest
{
    public interface IFlightFilter
    {
        // returns filtered list, it may be IEnumerable
        IEnumerable<Flight> GetFilteredEnumerable(IEnumerable<Flight> flights);
    }
}
