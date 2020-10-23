using SharedTrip.Services;
using SharedTrip.ViewModels.Trips;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripsService tripsService;

        public TripsController(ITripsService tripsService)
        {
            this.tripsService = tripsService;
        }

        // GET /cards/add
        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddTripInputModel model)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(model.StartPoint))
            {
                return this.Error("StartPoint should be valid!");
            }

            if (string.IsNullOrEmpty(model.EndPoint))
            {
                return this.Error("EndPoint should be valid!");
            }

            if (!DateTime.TryParseExact(model.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                return this.Error("Invalid DepartureTime. Please use dd.MM.yyyy HH:mm format!");
            }

            if (model.Seats < 2 || model.Seats > 6)
            {
                return this.Error("Seats shout be between 2 and 6");
            }

            if (string.IsNullOrWhiteSpace(model.Description) || model.Description.Length > 80)
            {
                return this.Error("Description is required and its length should be at most 80 characters.");
            }

            this.tripsService.AddTrip(model);
            return this.Redirect("/Trips/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var trips = this.tripsService.GetAll();
            return this.View(trips);
        }

        public HttpResponse Details(string tripId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var trip = this.tripsService.GetTripDetails(tripId);
            return this.View(trip);
        }

        public HttpResponse AddUserToTrip(string tripId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (!this.tripsService.HasAvailableSeats(tripId))
            {
                return this.Error("No Seats Available!");
            }

            var userId = this.GetUserId();
            var isUserAddedAlready = this.tripsService.AddUserToTrip(tripId, userId);
            if (!isUserAddedAlready)
            {
                return this.Redirect("/Trips/Details?tripId=" + tripId);
            }
            return this.Redirect("/Trips/All");
        }
    }
}
