using System;
using System.Collections.Generic;
using BankApp.Models;
using BankApp.Services;
namespace BankApp.Services.AccountServices
{
    public class AccountServices : AccountServicesInterface
    {
        JsonService.JsonService jsonService = new JsonService.JsonService();

        public Account CreateAccount(Account account, User user)
        {
            bool isAccountRegistered = jsonService.CreateNewAccount(account, user);

            if (!isAccountRegistered)
            {
                return null;
            }
            return account;
        }

        public bool DeleteAccount(Account account)
        {
            throw new NotImplementedException();
        }

        public bool DepositAccount(Account account, double amount)
        {
            throw new NotImplementedException();
        }

        public IList<Account> ListOfAccount(User user)
        {
            throw new NotImplementedException();
        }

        public bool WithdrawAccount(Account account, double amount)
        {
            throw new NotImplementedException();
        }
    }
}
