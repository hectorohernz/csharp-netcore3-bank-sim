using System;
using BankApp.Helpers.View;
using BankApp.Models;
namespace BankApp.Services.ApplicationServices
{
    public class ApplicationServices : ApplicationServiceInterface
    {
        ViewHelper viewHelper = new ViewHelper();
        Services.UserServices.UserServices userServices = new UserServices.UserServices();
        public void Greetings()
        {
            string logo = @"


 _____   _____                  
|  ___| /  __ \                 
| |__   | /  \/ ___  _ __ _ __  
|  __|  | |    / _ \| '__| '_ \ 
| |___  | \__/\ (_) | |  | |_) |
\____/   \____/\___/|_|  | .__/ 
                         | |    
                         |_|    

";
            viewHelper.Message(logo);
            string title = "\n E-Corp Banking System V1.0.0 \n";
            viewHelper.Message(title);
        }

        public int IsUserNew()
        {
            // Ask To Sign In || Create New Account
            viewHelper.Message("Select Any of the following options:");
            viewHelper.Message("1: Log In ");
            viewHelper.Message("2: Sign Up ");
            viewHelper.AskQuestion("Enter:");
            int num;
            bool success;
            do
            {
                string inputNumber = Console.ReadLine();
                success = Int32.TryParse(inputNumber, out num);

                if (!success)
                {
                    viewHelper.ErrorMessage("Please Enter 1 Or 2");
                    continue;
                }

                if (num == 1 || num == 2)
                {
                    break;
                }
                else
                {
                    viewHelper.ErrorMessage("Enter 1 Or 2");
                    success = false;
                }


            } while (!success);
            return num;
        }

        public User login()
        {
            User user;
            do
            {
                viewHelper.AskQuestion("Enter Username: ");
                string username = Console.ReadLine();

                viewHelper.AskQuestion("Enter Password: ");
                string passsword = Console.ReadLine();

                user = userServices.FindUserByUsernameAndPassword(username, passsword);

                if (user != null)
                {
                    return user;
                }

                viewHelper.ErrorMessage("\n Credentials are invalid! \n");
                viewHelper.AskQuestion(" \n Would you like create a new account: \n Yes:y or No:n \n Enter:");
                string option = Console.ReadLine().ToLower();

                if(option == "yes" || option == "y")
                {
                    return null;
                }

 

            } while (true);
        }

        public User newUser()
        {
            do
            {

                string username, password, email, name;

                do
                {
                    viewHelper.AskQuestion("Enter Username :");
                    username = Console.ReadLine();
                    if (!User.IsVaildName(username))
                    {
                        viewHelper.ErrorMessage("Please Enter a Username that isn't empty!");
                        continue;
                    }

                    bool isUsernameTaken = userServices.FindUserByUsername(username) == null ? false : true;

                    if (isUsernameTaken)
                    {
                        viewHelper.ErrorMessage("Username already taken:");
                        continue;
                    }


                    if (User.IsVaildName(username) && !isUsernameTaken)
                    {
                        break;
                    }
                } while (true);

                do
                {
                    viewHelper.AskQuestion("Enter Email: ");
                    email = Console.ReadLine();

                    if (User.IsVaildEmail(email))
                    {
                        break;
                    }

                    viewHelper.ErrorMessage("Email Is Invalid!");
                } while (true);

                do
                {
                    viewHelper.AskQuestion("Enter Password: ");
                    password = Console.ReadLine();

                    if (User.IsVaildPassword(password))
                    {
                        break;
                    }
                    viewHelper.ErrorMessage("Password Is Invalid!");
                    viewHelper.ErrorMessage("Password Must Be 8 Characters or greater.");

                } while (true);

                do
                {
                    viewHelper.AskQuestion("Enter Name: ");
                    name = Console.ReadLine();

                    if (User.IsVaildName(name))
                    {
                        break;
                    }

                    viewHelper.ErrorMessage("Name Is Invalid!");
                    viewHelper.ErrorMessage("Name is empty.");

                } while (true);

                User user = new User(name, email, password, username);

                viewHelper.Message("\n" + user.ToString() + "\n");
                
                viewHelper.Message("\n" +  "Does This Information Look Correct ?" + "\n");
                viewHelper.Message("Yes, No, or Exit");
                viewHelper.AskQuestion("Enter :");
                
                string isRightInfo = Console.ReadLine();

                if(isRightInfo.ToLower() == "y" || isRightInfo.ToLower() == "yes")
                {
                    return user;
                }
                else if(isRightInfo.ToLower() == "exit" || isRightInfo.ToLower() == "e")
                {
                    viewHelper.Message("\n Thanks For Using E-Corp. \n");
                    System.Environment.Exit(1);
                }

            } while (true);
        }
    }
}
