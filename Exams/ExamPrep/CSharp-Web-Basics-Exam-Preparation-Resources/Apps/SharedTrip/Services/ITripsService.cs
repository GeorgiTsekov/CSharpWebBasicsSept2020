using SharedTrip.ViewModels.Trips;
using System.Collections;
using System.Collections.Generic;

namespace SharedTrip.Services
{
    public interface ITripsService
    {
        void AddTrip(AddTripInputModel input);

        IEnumerable<TripViewModel> GetAll();

        TripDetailsViewModel GetTripDetails(string tripId);

        bool HasAvailableSeats(string tripId);

        bool AddUserToTrip(string tripId, string userId);
    }
}
