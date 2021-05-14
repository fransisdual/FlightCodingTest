using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gridnine.FlightCodingTest
{
    /// <summary>
    /// The flight filter class.
    /// Contains logic to filter flight IEnumerable except flights where DepartureTime is before current time.
    /// </summary>
    public class FlightFilterExceptDepartureBeforeNow : IFlightFilter
    {
        public IEnumerable<Flight> GetFilteredEnumerable(IEnumerable<Flight> flights)
        {
            var filterQuery = from flight in flights
                                        from s in flight.Segments
                                        where s.DepartureDate >= DateTime.Now
                                        select flight;

            return filterQuery.Distinct();
        }
    }
}
