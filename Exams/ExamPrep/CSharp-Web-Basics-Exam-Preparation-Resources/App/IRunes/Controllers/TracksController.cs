using IRunes.Services;
using IRunes.ViewModels.Tracks;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.Controllers
{
    public class TracksController : Controller
    {
        private readonly ITracksService tracksService;

        public TracksController(ITracksService tracksService)
        {
            this.tracksService = tracksService;
        }

        public HttpResponse Create(string albumId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = new CreateViewModel
            {
                AlbumId = albumId,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(CreateInputModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(input.Name) || input.Name.Length > 20 || input.Name.Length < 4)
            {
                return this.Error("Name should be between 4 and 20 characters!");
            }

            if (string.IsNullOrEmpty(input.Link))
            {
                return this.Error("Link should be valid!");
            }

            if (input.Price < 0)
            {
                return this.Error("Price should be positive!");
            }

            this.tracksService.Create(input.AlbumId, input.Name, input.Link, input.Price);
            return this.Redirect("/Albums/Details?id=" + input.AlbumId);
        }

        public HttpResponse Details(string trackId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.tracksService.GetDetails(trackId);
            if (viewModel == null)
            {
                return this.Error("Track not found!");
            }

            return this.View(viewModel);
        }
    }
}
