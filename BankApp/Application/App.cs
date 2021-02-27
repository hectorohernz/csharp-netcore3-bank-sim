using System;
using BankApp.Helpers.View;
using BankApp.Models;
using BankApp.Services.ApplicationServices;
using BankApp.Services.JsonService;
using BankApp.Services.UserServices;

namespace BankApp.Application
{
    public class App
    {
        public static void Run()
        {
            UserServices userServices = new UserServices();
            ApplicationServices appSer = new ApplicationServices();
            ViewHelper viewHelper = new ViewHelper();

            // ToDo: Greeting the user 
            appSer.Greetings();

            //
            int option = appSer.IsUserNew();
            bool isUserNew = option == 2 ? true : false;

            if (isUserNew)
            {
                // ToDo: Create an account
                User user = appSer.newUser();
                userServices.CreateNewUser(user);
                viewHelper.Successmessage("Thanks For Signing up");
            }
            else
            {
                // ToDo: Login Verfication 
                viewHelper.Successmessage("Welcome Back !");
            }

            // ToDo: Prompt following questions to
            // Exit, Create New Account, Widthdraw, Deposit,

        }

    }
}
