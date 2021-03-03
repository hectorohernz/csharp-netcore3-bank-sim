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
            string name = "John Doe";
            string email = "JohnDoe@gmail.com";
            string password = "johnDoeLovesPets2020!";
            string username = "johndoe";
            User tempUser = new User(name, email, password, username);
           
            User user = userSer.CreateNewUser(tempUser);

            Assert.NotNull(user);
        }


        [Fact]
        public void TestCreateNewUserIsNull()
        {
            // User is already in the mockdata base, should return null because username is already used.
            string name = "John Doe";
            string email = "JohnDoe@gmail.com";
            string password = "johnDoeLovesPets2020!";
            string username = "johndoe";
            User tempUser = new User(name, email, password, username);
            User user = userSer.CreateNewUser(tempUser);
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
            string username = "johndoe";

            User user = userSer.FindUserByUsername(username);

            Assert.Equal(username, user.Username);
        }


        [Fact] // Should return null : Because password is wrong 
        public void TestLoginBasedOnWrongPassword()
        {
            string username = "johndoe";

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
            string username = "johndoe";

            string password = "johnDoeLovesPets2020!";

            User user = userSer.FindUserByUsernameAndPassword(username, password);
            output.WriteLine(user.ToString());
            Assert.NotNull(user);
        }


    }
}
