using SUS.HTTP;
using SUS.MvcFramework;
using System;

namespace MyFirstMvcApp.Controllers
{
    public class UsersController : Controller
    {
        public HttpResponse Login(HttpRequest request)
        {
            return this.View();
        }

        public HttpResponse Register(HttpRequest request)
        {
            return this.View();
        }

        public HttpResponse DoLogin(HttpRequest arg)
        {
            // TODO
            return this.Redirect("/");
        }
    }
}
