using IRunes.Services;
using IRunes.ViewModels.Albums;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumsService albumsService;

        public AlbumsController(IAlbumsService albumsService)
        {
            this.albumsService = albumsService;
        }

        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(AddAllbumInputModel model)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(model.Name) || model.Name.Length > 20 || model.Name.Length < 4)
            {
                return this.Error("Name should be between 4 and 20 characters!");
            }

            if (string.IsNullOrEmpty(model.Cover))
            {
                return this.Error("Cover should be valid!");
            }

            this.albumsService.Create(model);
            return this.Redirect("/Albums/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var albums = this.albumsService.GetAll();
            return this.View(albums);
        }

        public HttpResponse Details(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var album = this.albumsService.GetDetails(id);
            return this.View(album);
        }
    }
}
