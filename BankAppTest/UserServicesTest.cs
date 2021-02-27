using System;
using BankApp.Models;
using Xunit;
using Xunit.Abstractions;

namespace BankAppTest
{
    public class UserServicesTest
    {

        private readonly ITestOutputHelper output;


        public UserServicesTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        BankApp.Services.UserServices.UserServices userSer = new BankApp.Services.UserServices.UserServices();

        [Fact]
        public void TestCreateNewUser()
        {
            string name = "Hector Hernandez";
            string email = "hector@gmail.com";
            string password = "password1";
            string username = "testingusername";
            User user = userSer.CreateNewUser(name, email, password, username);

            Assert.NotNull(user);
        }


        [Fact]
        public void TestCreateNewUserIsNull()
        {
            // User is already in the mockdata base, should return null because username is already used.
            string name = "Hector Hernandez";
            string email = "hector@gmail.com";
            string password = "password1";
            string username = "testingusername";
            User user = userSer.CreateNewUser(name, email, password, username);

            Assert.Null(user);
        }

        [Fact] // Should return null meaning username doesn't exist 
        public void TestFindByIsNullUsername()
        {
            string username = "sdfh";

            User user = userSer.FindUserByUsername(username);

            Assert.Null(user);
        }

        [Fact] // Should return user infomation 
        public void TestFindByUsernameIsFound()
        {
            string username = "testingusername";

            User user = userSer.FindUserByUsername(username);

            Assert.NotNull(user);
        }


        [Fact] // Should return null : Because password is wrong 
        public void TestLoginBasedOnWrongPassword()
        {
            string username = "testingusername";

            string password = "";

            User user = userSer.FindUserByUsernameAndPassword(username, password);


            Assert.Null(user);
        }


        [Fact] // Should return null : Because username is false but password is true 
        public void TestingLoginBasedOnWrongUsername()
        {
            string username = "jnasdkjfsjk";

            string password = "password1";

            User user = userSer.FindUserByUsernameAndPassword(username, password);


            Assert.Null(user);
        }

        [Fact] // Should return user : Because username is true but password is true 
        public void TestingLoginBasedOnTrueInfomation()
        {
            string username = "testingusername";

            string password = "password1";

            User user = userSer.FindUserByUsernameAndPassword(username, password);

            Assert.NotNull(user);
        }


    }
}
