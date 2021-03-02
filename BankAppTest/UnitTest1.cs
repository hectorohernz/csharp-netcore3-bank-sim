using System;
using Xunit;
using BankApp.Services.JsonService;
using BankApp.Models;
using System.Collections.Generic;
using Xunit.Abstractions;

namespace BankAppTest
{
    

    public class UnitTest1
    {

        private readonly ITestOutputHelper output;


        public UnitTest1(ITestOutputHelper output)
        {
            this.output = output;
        }

        // Get's a list of 
        [Fact]
        public void GetListOfUser()
        {
            JsonService jsonService = new JsonService();

            IList<User> isUser = jsonService.listOfUser();

            int exepected = 4;

            int acc = isUser.Count;

            output.WriteLine("This is my output");

            Assert.Equal(exepected, acc);
        }



        // Adds New User To Mock Database
        [Fact]
        public void TestCreateNewUser()
        {
            JsonService jsonService = new JsonService();

            User user = new User("test2 test2", "test2@gmail.com", "test2", "test2");

            jsonService.addNewUser(user);

            IList<User> listOfUser = jsonService.listOfUser();

            User lastUser = listOfUser[listOfUser.Count - 1];

            Assert.Equal(user.Name,lastUser.Name);
        }


        [Fact]
        public void TestGetUserByUsername()
        {
            string username = "test2";

            JsonService jsonService = new JsonService();

            User user = jsonService.getUserByUsername(username);

            Assert.NotNull(user);
        }



    }
}
