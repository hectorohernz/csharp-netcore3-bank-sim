using System;
using BankApp.Models;
using Xunit;

namespace BankAppTest
{
    public class TestAccountServices
    {

        BankApp.Services.UserServices.UserServices userServices = new BankApp.Services.UserServices.UserServices();
        BankApp.Services.JsonService.JsonService jsonService = new BankApp.Services.JsonService.JsonService();
       

        [Fact]
        public void testCreateNewAccount()
        {
            User user = userServices.FindUserByUsernameAndPassword("test", "test");
            Account account = new Account("savings", 500.50, user.Username);
            bool isAccountCreated = jsonService.CreateNewAccount(account, user);
            Assert.True(isAccountCreated);
        }


        [Fact]
        public void testGetAccountByName()
        {
            string accountName = "savings";
            User user = userServices.FindUserByUsernameAndPassword("test", "test");

            Account account = jsonService.GetAccountByAccountName(user.Username, accountName);
            Account test = new Account("savings", 500.50, user.Username);

            Assert.Equal(account.name, test.name);
            Assert.Equal(account.ownerUsername, test.ownerUsername);
            Assert.Equal(account.amount, test.amount);
        }

        [Fact]
        public void testUpdateAccount()
        {
            string accountName = "savings";
            User user = userServices.FindUserByUsernameAndPassword("test", "test");


            Account account = jsonService.GetAccountByAccountName(user.Username, accountName);

            account.amount = 20.25;

            bool isUpdatedAccount = jsonService.UpdateAccount(account);

            Assert.True(isUpdatedAccount);
        }

        [Fact]
        public void testDeleteAccount()
        {
            string accountName = "savings";
            User user = userServices.FindUserByUsernameAndPassword("test", "test");
            Account account = jsonService.GetAccountByAccountName(user.Username, accountName);
            bool isAccountDelete = jsonService.DeleteAccount(account, user);
            Assert.True(isAccountDelete);

        }

    }
}
