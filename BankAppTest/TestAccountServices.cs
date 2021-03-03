using System;
using BankApp.Models;
using Xunit;
using Xunit.Abstractions;

namespace BankAppTest
{
    public class TestAccountServices
    {

        BankApp.Services.UserServices.UserServices userServices = new BankApp.Services.UserServices.UserServices();
        BankApp.Services.JsonService.JsonService jsonService = new BankApp.Services.JsonService.JsonService();

        private readonly ITestOutputHelper output;


        public TestAccountServices(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void testCreateNewAccount()
        {
            User user = userServices.FindUserByUsernameAndPassword("johndoe", "johnDoeLovesPets2020");
            output.WriteLine(user.ToString());

            Account account = new Account("savings", 500.50, user.Username);
            bool isAccountCreated = jsonService.CreateNewAccount(account, user);
            output.WriteLine(user.ToString());
            Assert.True(isAccountCreated);
        }


        [Fact]
        public void testGetAccountByName()
        {
            string accountName = "test-account";

            User user = userServices.FindUserByUsernameAndPassword("johndoe", "johnDoeLovesPets2020!");

            Account account = jsonService.GetAccountByAccountName(user.Username, accountName);
            Assert.Null(account);
        }

        [Fact]
        public void testUpdateAccount()
        {
            string accountName = "test-account";
            User user = userServices.FindUserByUsernameAndPassword("test", "test");


            Account account = jsonService.GetAccountByAccountName(user.Username, accountName);

            account.deposit(500);

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
