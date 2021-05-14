using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gridnine.FlightCodingTest
{
    public class Program
    {
        static void Main(string[] args)
        {  
            // Инициализируем базовый набор
            FlightBuilder flightBuilder = new FlightBuilder();
            IList<Flight> flights = flightBuilder.GetFlights();
            PrintFlights(flights, "Base flights collection");


            // Выбираемые правила, все правила наследуют IFlightFilter
            IFlightFilter simpleFilterBeforeNow = new FlightFilterExceptDepartureBeforeNow();
            IEnumerable<Flight> filteredFlights  = simpleFilterBeforeNow.GetFilteredEnumerable(flights);
            PrintFlights(filteredFlights, "\nExcept Departure before Now");

            IFlightFilter filterExceptArriveBelowDeparture = new FlightFilterExceptArriveBelowDeparture();
            filteredFlights = filterExceptArriveBelowDeparture.GetFilteredEnumerable(flights);
            PrintFlights(filteredFlights, "\nExcept Arrive below Departure");

            IFlightFilter filterExceptMore2HoursOnGround = new FlightFilterExceptHoursOnGround(2);
            filteredFlights = filterExceptMore2HoursOnGround.GetFilteredEnumerable(flights);
            PrintFlights(filteredFlights, "\nExcept 2 hours on ground");

            // Пример динамически задаваемых правил
            IEnumerable<Flight> query = from flight in flights
                                        from b in flight.Segments
                                        where b.DepartureDate < DateTime.Now
                                        select flight;
            DynamicFlightsFilter dynamicFilter = new DynamicFlightsFilter(query);
            filteredFlights = dynamicFilter.GetFilteredEnumerable(flights);
            PrintFlights(filteredFlights, "\nDynamic filter rules. Include Departure before Now");

            // Ждём ввод
            Console.ReadLine();

        }

        static void PrintFlights(IEnumerable<Flight> flights, string caption)
        {
            Console.WriteLine(caption);

            for (int i = 0; i < flights.Count(); i++)
            {
                Console.WriteLine("Flight #{0}", i);
                foreach (Segment segment in flights.ElementAt(i).Segments)
                    Console.WriteLine("Departure: " + segment.DepartureDate +"\t| Arrive: " + segment.ArrivalDate );
            }
           
        }
    }
}
