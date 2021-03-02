using System;
using System.Collections.Generic;
using BankApp.Models;
using Newtonsoft.Json.Linq;

namespace BankApp.Services.JsonService
{
    public interface JsonServiceInterface
    {
        IList<User> listOfUser();

        // Write A New User
        bool addNewUser(User user);

        // Update a new users
        bool updateExistingUser(User user);

        // Get User by Username
        User getUserByUsername(string username);

        IList<Account> GetAllAccountsByUsername(string username);

        bool CreateNewAccount(Account account, User user);

        bool DeleteAccount(Account account, User user);

        

        void SetNewUserAccounts(User user);

        Account GetAccountByAccountName(string username, string name);

        bool UpdateAccount(Account account);

    }
}
