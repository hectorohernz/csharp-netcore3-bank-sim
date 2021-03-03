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
            User user = userSer.FindUserByUsernameAndPassword("johndoe", "johnDoeLovesPets2020");
            IList<Account> listOfAccounts = accountServices.ListOfAccount(user);
            int acutal = listOfAccounts.Count;
            int expected = 0;
            Assert.Equal(expected, acutal);
        }

        [Fact]
        public void CreateAccountTest()
        {
            User user = userSer.FindUserByUsernameAndPassword("johndoe", "johnDoeLovesPets2020!");
            Account account = new Account("test-account", 25.00, user.Username);
            accountServices.CreateAccount(account, user);

            IList<Account> listOfAccounts = accountServices.ListOfAccount(user);

            int acutal = listOfAccounts.Count;
            int expected = 1;
            Assert.Equal(expected, acutal);
        }

        [Fact]
        public void WithdrawAccountTest()
        {
            User user = userSer.FindUserByUsernameAndPassword("johndoe", "johnDoeLovesPets2020!");
            Account account = jsonService.GetAccountByAccountName(user.Username, "test-account");
            bool acc = accountServices.WithdrawAccount(account,10.00);
            Assert.True(acc);
        }

        [Fact]
        public void DepositAccountTest()
        {
            User user = userSer.FindUserByUsernameAndPassword("johndoe", "johnDoeLovesPets2020!");
            Account account = jsonService.GetAccountByAccountName(user.Username, "test-account");
            bool acc = accountServices.DepositAccount(account, 100.20);
            Assert.True(acc);
        }

        [Fact]
        public void DeleteAccountTest()
        {
            User user = userSer.FindUserByUsernameAndPassword("johndoe", "johnDoeLovesPets2020!");
            Account account = jsonService.GetAccountByAccountName(user.Username, "test-account");
            bool acc = accountServices.DeleteAccount(account, user);
            Assert.True(acc);
        }


    }
}
