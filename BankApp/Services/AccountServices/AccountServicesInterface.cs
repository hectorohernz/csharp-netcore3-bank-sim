using System;
using System.Collections.Generic;
using BankApp.Models;

namespace BankApp.Services.AccountServices
{
    public interface AccountServicesInterface
    {
        IList<Account> ListOfAccount(User user);

        Account CreateAccount(Account account, User user);

        bool WithdrawAccount(Account account, double amount);

        bool DepositAccount(Account account, double amount);

        bool DeleteAccount(Account account, User user);

        Account GetAccount(string name, User user);
    }
}
