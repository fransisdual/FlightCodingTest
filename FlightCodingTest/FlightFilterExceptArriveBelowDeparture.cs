using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gridnine.FlightCodingTest
{
    /// <summary>
    /// The flight filter class.
    /// Contains logic to filter flight IEnumerable except flights where arrive below departure.
    /// </summary>
    public class FlightFilterExceptArriveBelowDeparture : IFlightFilter
    {
        public IEnumerable<Flight> GetFilteredEnumerable(IEnumerable<Flight> flights)
        {
            var filterQuery = from flight in flights
                              from s in flight.Segments
                              where s.ArrivalDate > s.DepartureDate
                              select flight;

            return filterQuery.Distinct();
        }
    }
}
