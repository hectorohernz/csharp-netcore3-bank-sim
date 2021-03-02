using System;
using BankApp.Models;

namespace BankApp.Services.ApplicationServices
{
    public interface ApplicationServiceInterface
    {
        void Greetings();

        int IsUserNew();

        User newUser();

        User login();
    }
}
