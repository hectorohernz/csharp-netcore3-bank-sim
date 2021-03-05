using System;
using System.Collections.Generic;
using BankApp.Models;

namespace BankApp.Services.ApplicationServices
{
    public interface ApplicationServiceInterface
    {
        void Greetings();

        int IsUserNew();

        User newUser();

        User login();

        void userGreeting(User user);

        void accountPrompt(User user);

        void createNewAccount(User user);

        void modifyAccounts(User user, IList<Account> listOfAccounts);

        void exitProgram(string answer);
    }
}
