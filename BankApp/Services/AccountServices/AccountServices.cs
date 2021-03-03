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

        public bool DeleteAccount(Account account, User user)
        {
            bool isAccountDeleted = jsonService.DeleteAccount(account, user);
            return isAccountDeleted;
        }

        public bool DepositAccount(Account account, double amount)
        {
            if(amount <= 0)
            {
                return false;
            }

            account.deposit(amount);
            return jsonService.UpdateAccount(account);
        }

        public IList<Account> ListOfAccount(User user)
        {
            return jsonService.GetAllAccountsByUsername(user.Username);
        }

        public bool WithdrawAccount(Account account, double amount)
        {
            if (0 > account.amount - amount)
            {
                return false;
            }

            account.withdraw(amount);
            bool updatedAccount = jsonService.UpdateAccount(account);
            return updatedAccount;
        }
    }
}
