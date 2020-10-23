using SharedTrip.Data;
using SharedTrip.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SharedTrip.Services
{
    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext db;

        public TripsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddTrip(AddTripInputModel input)
        {
            var trip = new Trip
            {
                StartPoint = input.StartPoint,
                EndPoint = input.EndPoint,
                DepartureTime = DateTime.ParseExact(input.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                ImagePath = input.ImagePath,
                Seats = input.Seats,
                Description = input.Description,
            };
            this.db.Trips.Add(trip);
            this.db.SaveChanges();
        }

        public bool AddUserToTrip(string tripId, string userId)
        {
            var userInTrip = this.db.UserTrips.Any(x => x.UserId == userId && x.TripId == tripId);
            if (userInTrip)
            {
                return false;
            }

            var userTrip = new UserTrip
            {
                TripId = tripId,
                UserId = userId,
            };
            this.db.UserTrips.Add(userTrip);
            this.db.SaveChanges();
            return true;
        }

        public bool HasAvailableSeats(string tripId)
        {
            var trip = this.db.Trips.Where(x => x.Id == tripId)
                .Select(x => new { x.Seats, TakenSeats = x.UserTrips.Count() })
                .FirstOrDefault();
            var availableSeats = trip.Seats - trip.TakenSeats;
            return availableSeats > 0;
        }

        public IEnumerable<TripViewModel> GetAll()
        {
            return this.db.Trips.Select(x => new TripViewModel
            {
                StartPoint = x.StartPoint,
                EndPoint = x.EndPoint,
                DepartureTime = x.DepartureTime,
                Id = x.Id,
                Seats = x.Seats,
                UsedSeats = x.UserTrips.Count(),
            }).ToList();
        }

        public TripDetailsViewModel GetTripDetails(string tripId)
        {
            return this.db.Trips
                .Where(x => x.Id == tripId)
                .Select(t => new TripDetailsViewModel
                {
                    ImagePath = t.ImagePath,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime,
                    Id = t.Id,
                    Seats = t.Seats,
                    Description = t.Description,
                    UsedSeats = t.UserTrips.Count(),
                })
                .FirstOrDefault();
        }
    }
}
