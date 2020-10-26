using Git.Services;
using Git.ViewModels.Commits;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly ICommitsService commitsService;

        public CommitsController(ICommitsService commintsService)
        {
            this.commitsService = commintsService;
        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var repoName = this.commitsService.GetNameById(id);

            var viewModel = new CreateViewModel
            {
                Id = id,
                Name = repoName,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(string id, string repositoryId, string description)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(description) || description.Length < 5)
            {
                return this.Error("Description should be minimum 5 characters!");
            }

            var userId = GetUserId();
            this.commitsService.Create(id,  userId, repositoryId, description);
            return this.Redirect("/Repositories/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var commitsViewModel = this.commitsService.GetAll();
            return this.View(commitsViewModel);
        }
    }
}
