using Git.Services;
using Git.ViewModels.Repositories;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly IRepositoriesService repositoriesService;

        public RepositoriesController(IRepositoriesService repositoriesService)
        {
            this.repositoriesService = repositoriesService;
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
        public HttpResponse Create(string name, string repositoryType)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(name) || name.Length > 10 || name.Length < 3)
            {
                return this.Error("Name should be between 3 and 10 characters!");
            }

            var userId = GetUserId();
            this.repositoriesService.Create(name, repositoryType, userId);
            return this.Redirect("/Repositories/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var repositories = this.repositoriesService.All();
            return this.View(repositories);
        }
    }
}
