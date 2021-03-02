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
            
            int option = appSer.IsUserNew();

            bool isUserNew = option == 2 ? true : false;

            User user;

            if (isUserNew)
            {
                // ToDo: Create an account
                user = appSer.newUser();
                userServices.CreateNewUser(user);
                viewHelper.Successmessage("Thanks For Signing up");
            }
            else
            {
                user = appSer.login();
                if (user == null)
                {
                    user = appSer.newUser();
                    userServices.CreateNewUser(user);
                    viewHelper.Successmessage("\n Thanks For Signing up \n");
                }
            }


            do
            {



            } while (true);


     

        }

    }
}
