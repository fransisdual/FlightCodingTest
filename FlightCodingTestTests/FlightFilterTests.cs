using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gridnine.FlightCodingTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gridnine.FlightCodingTest.Tests
{
    [TestClass()]
    public class FlightFilterTests
    {
        [TestMethod()]
        public void FlightFilter_ExceptArriveBelowDeparture_GetFilteredEnumerableTest()
        {
            // Arrange
            IList<Flight> flights = new List<Flight>();

            Flight flight = new Flight();
            Segment segment = new Segment() { DepartureDate = DateTime.Now, ArrivalDate = DateTime.Now.AddHours(-1) };
            flight.Segments = new List<Segment>() { segment };
            flights.Add(flight);
            IFlightFilter flightFilter = new FlightFilterExceptArriveBelowDeparture();


            // Act
            IEnumerable<Flight> filteredFlights = flightFilter.GetFilteredEnumerable(flights);
            // Assert
            int count = filteredFlights.Count();
            Assert.AreEqual(0, count);

        }

        [TestMethod()]
        public void FlightFilter_ExceptDepartureBeforeNow_GetFilteredEnumerableTest()
        {
            // Arrange
            IList<Flight> flights = new List<Flight>();

            Flight flight = new Flight();
            Segment segment = new Segment() { DepartureDate = DateTime.Now.AddHours(-1), ArrivalDate = DateTime.Now };
            flight.Segments = new List<Segment>() { segment };
            flights.Add(flight);
            IFlightFilter flightFilter = new FlightFilterExceptDepartureBeforeNow();


            // Act
            IEnumerable<Flight> filteredFlights = flightFilter.GetFilteredEnumerable(flights);
            // Assert
            int count = filteredFlights.Count();
            Assert.AreEqual(0, count);

        }

        [TestMethod()]
        public void FlightFilter_Except2HoursOnGround_GetFilteredEnumerableTest()
        {
            // Arrange
            IList<Flight> flights = new List<Flight>();

            Flight flight = new Flight();
            Segment segment1 = new Segment() { DepartureDate = DateTime.Now, ArrivalDate = DateTime.Now.AddHours(1) };
            Segment segment2 = new Segment() { DepartureDate = DateTime.Now.AddHours(5), ArrivalDate = DateTime.Now.AddHours(6) };


            flight.Segments = new List<Segment>() { segment1, segment2 };
            flights.Add(flight);
            IFlightFilter flightFilter = new FlightFilterExceptHoursOnGround(2);


            // Act
            IEnumerable<Flight> filteredFlights = flightFilter.GetFilteredEnumerable(flights);
            // Assert
            int count = filteredFlights.Count();
            Assert.AreEqual(0, count);

        }

        [TestMethod()]
        public void FlightFilter_Dynamic_GetFilteredEnumerableTest()
        {
            // Arrange
            IList<Flight> flights = new List<Flight>();

            Flight flight = new Flight();
            Segment segment = new Segment() { DepartureDate = DateTime.Now.AddHours(-1), ArrivalDate = DateTime.Now.AddHours(1) };


            flight.Segments = new List<Segment>() { segment };
            flights.Add(flight);

            IEnumerable<Flight> query = from f in flights
                                        from b in flight.Segments
                                        where b.DepartureDate < DateTime.Now
                                        select f;
            DynamicFlightsFilter dynamicFilter = new DynamicFlightsFilter(query);


            // Act
            IEnumerable<Flight> filteredFlights = dynamicFilter.GetFilteredEnumerable(flights);
            // Assert
            int count = filteredFlights.Count();
            Assert.AreEqual(1, count);

        }
    }
}