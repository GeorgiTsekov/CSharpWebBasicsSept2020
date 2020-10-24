using IRunes.Services;
using IRunes.ViewModels.Home;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsersService usersService;

        public HomeController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserSignedIn())
            {
                var viewModel = new IndexViewModel();
                viewModel.Username = this.usersService.GetUsername(this.GetUserId());
                return this.View(viewModel, "Home");
            }
            else
            {
                return this.View();
            }
        }
    }
}
