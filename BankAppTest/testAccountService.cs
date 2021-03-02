using System;
using System.Collections.Generic;
using BankApp.Models;
using Xunit;

namespace BankAppTest
{
    public class testAccountService
    {
        BankApp.Services.UserServices.UserServices userSer = new BankApp.Services.UserServices.UserServices();
        BankApp.Services.AccountServices.AccountServices accountServices = new BankApp.Services.AccountServices.AccountServices();
        BankApp.Services.JsonService.JsonService jsonService = new BankApp.Services.JsonService.JsonService();

        [Fact]
        public void ListOfAccountTest()
        {
            User user = userSer.FindUserByUsernameAndPassword("test", "test");
            IList<Account> listOfAccounts = accountServices.ListOfAccount(user);

            int acutal = listOfAccounts.Count;

            int expected = 1;

            Assert.Equal(expected, acutal);
        }

        [Fact]
        public void CreateAccountTest()
        {
            User user = userSer.FindUserByUsernameAndPassword("test", "test");
            Account account = new Account("test-account", 1.00, user.Username);
            Account acc = accountServices.CreateAccount(account, user);

            IList<Account> listOfAccounts = accountServices.ListOfAccount(user);

            int acutal = listOfAccounts.Count;
            int expected = 2;
            Assert.Equal(expected, acutal);
        }

        [Fact]
        public void WithdrawAccountTest()
        {
            User user = userSer.FindUserByUsernameAndPassword("test", "test");
            Account account = new Account("account", 500.50, user.Username);
            bool acc = accountServices.WithdrawAccount(account,500.00);
            Assert.True(true);
        }

        [Fact]
        public void DepositAccountTest()
        {
            User user = userSer.FindUserByUsernameAndPassword("test", "test");
            Account account = new Account("account", 500.50, user.Username);
            bool acc = accountServices.WithdrawAccount(account, -100.20);
            Assert.False(acc);
        }

        [Fact]
        public void DeleteAccountTest()
        {
                         
            // Implement 
        }


    }
}
