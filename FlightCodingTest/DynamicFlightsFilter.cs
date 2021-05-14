using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gridnine.FlightCodingTest
{
    /// <summary>
    /// The flight filter class.
    /// Contains dynamic logic to filter flight IEnumerable, you can set FilterQuery and exec it.
    /// </summary>
    public class DynamicFlightsFilter : IFlightFilter
    {
        public DynamicFlightsFilter(IEnumerable<Flight> filterQuery)
        {
            FilterQuery = filterQuery;
        }

        public IEnumerable<Flight> FilterQuery { get; set; }

        public IEnumerable<Flight> GetFilteredEnumerable(IEnumerable<Flight> flights)
        {

            return FilterQuery;
        }
    }
}
