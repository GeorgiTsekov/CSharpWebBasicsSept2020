using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.Services
{
    public interface IUsersService
    {
        void CreateUser(string username, string email, string password);

        string GetUserId(string username, string password);

        bool IsUsernameAvailable(string username);

        bool IsEmailAvailable(string email);

        string GetUsername(string id);
    }
}
