using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Gridnine.FlightCodingTest
{
    /// <summary>
    /// The flight filter class.
    /// Contains logic to filter flight IEnumerable except flights where time on ground is bigger than 'hours'.
    /// </summary>
    public class FlightFilterExceptHoursOnGround : IFlightFilter
    {

        private int hours;

        public FlightFilterExceptHoursOnGround(int hours)
        {
            this.hours = hours;
        }

        public IEnumerable<Flight> GetFilteredEnumerable(IEnumerable<Flight> flights)
        {
            // Если время на земле отсутствует, то возвращаем исходный список
            if (hours <= 0) return flights.ToList();

            IList<Flight> flightsResult = new List<Flight>();

            // Берем время всех сегментов полета от начала до конца 
            // Вычитаем время когда самолет был в воздухе
            // Таким образом получаем время на земле, если время на земле больше чем hours
            foreach (Flight v in flights)
            {

                DateTime minDepartureDate = (from segments in v.Segments
                                          select segments.DepartureDate).Min();

                DateTime maxArrivvalDate = (from segments in v.Segments
                                          select segments.ArrivalDate).Max();

                double flightInAirDuration = (from segments in v.Segments
                                          select (segments.ArrivalDate - segments.DepartureDate).TotalHours).Sum();

                double wholeFlightDuration = (maxArrivvalDate - minDepartureDate).TotalHours;

                if (wholeFlightDuration - flightInAirDuration <= hours)
                    flightsResult.Add(v);

            }


            return flightsResult;
        }
    }
}

