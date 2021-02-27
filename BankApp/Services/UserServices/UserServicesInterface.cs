using System;
using BankApp.Models;

namespace BankApp.Services.UserServices
{
    public interface UserServicesInterface
    {
        User CreateNewUser(User user);

        User FindUserByUsername(string username);

        User FindUserByUsernameAndPassword(string username, string password);
    }
}
