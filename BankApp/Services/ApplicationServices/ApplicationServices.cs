using System;
using System.Collections.Generic;
using BankApp.Helpers.View;
using BankApp.Models;
namespace BankApp.Services.ApplicationServices
{
    public class ApplicationServices : ApplicationServiceInterface
    {
        ViewHelper viewHelper = new ViewHelper();
        Services.UserServices.UserServices userServices = new UserServices.UserServices();
        Services.AccountServices.AccountServices accountServices = new AccountServices.AccountServices();

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
                string username = Console.ReadLine().Trim();

                viewHelper.AskQuestion("Enter Password: ");
                string passsword = Console.ReadLine().Trim();

                user = userServices.FindUserByUsernameAndPassword(username, passsword);

                if (user != null)
                {
                    return user;
                }

                viewHelper.ErrorMessage("\n Credentials are invalid! \n");
                viewHelper.AskQuestion(" \n Would you like create a new account: \n Yes:y or No:n \n Enter:");
                string option = Console.ReadLine().ToLower().Trim();

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
                    username = Console.ReadLine().Trim();
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

        public void userGreeting(User user)
        {
            viewHelper.Successmessage($"\n Hello {user.Username},\n Welcome Back To E-Corp Banking\n");
        }

        public void accountPrompt(User user)
        {
            IList<Account> listOfAccounts = accountServices.ListOfAccount(user);
            if (listOfAccounts.Count == 0)
            {
                viewHelper.Message("You don't have any accounts. \n");
            }
            int i = 1;
            foreach(Account acc in listOfAccounts)
            {
                viewHelper.Message(" \n "+ "Account Number :" + i + "   " + acc.ToString() + "\n");
                i++;
            }

            createNewAccount(user);
            modifyAccounts(user, listOfAccounts);

        }

        public void createNewAccount(User user)
        {

            viewHelper.AskQuestion("\n Would you like to create a new account : ");
            string answer = Console.ReadLine().ToLower().Trim();

            if (answer == "yes" || answer == "y")
            {
                viewHelper.AskQuestion("\n Enter Account Name: ");
                string name = Console.ReadLine().Trim();
                Account account = new Account(name, 0, user.Username);
                accountServices.CreateAccount(account, user);
            }
            else if (answer == "exit" || answer == "exit")
            {
                viewHelper.Successmessage("\n Thank you for using E-Corp Banking.\n");
                System.Environment.Exit(1);
            }
        }

        public void modifyAccounts(User user, IList<Account> listOfAccounts)
        {
            if (listOfAccounts.Count > 0)
            {
                viewHelper.AskQuestion("\n Would you like to modify any exisiting accounts ?");
                string answer = Console.ReadLine().ToLower().Trim();
                if (answer == "yes" || answer == "y")
                {
                    Account acc = null;
                    while (true)
                    {
                        viewHelper.AskQuestion("\n Enter Account Name: ");
                        answer = Console.ReadLine().ToLower().Trim();

                        if (answer == "exit" || answer == "e")
                        {
                            break;
                        }

                        acc = accountServices.GetAccount(answer, user);

                        if (acc == null)
                        {
                            viewHelper.ErrorMessage("\n Account Doesn't Exist!");
                            continue;
                        }

                        break;

                    }

                    while (true)
                    {
                        viewHelper.AskQuestion($"\n Current Balance ${acc.amount}");
                        viewHelper.AskQuestion($"\n Would you like deposit, withdraw from {acc.name}: ");
                        answer = Console.ReadLine().ToLower().Trim();
                        if (answer == "deposit" || answer == "d")
                        {
                            viewHelper.AskQuestion($"\nAmount Deposit : ");
                            int num;
                            string inputNumber = Console.ReadLine().Trim();
                            bool success = Int32.TryParse(inputNumber, out num);
                            if (!success)
                            {
                                viewHelper.ErrorMessage("Enter a vaild amount !");
                                continue;
                            }
                            bool isDepositSuccessful = accountServices.DepositAccount(acc, num);

                            if (!isDepositSuccessful)
                            {
                                viewHelper.ErrorMessage("Deposit was unsuccessful !");
                                continue;
                            }
                        }
                        else if (answer == "withdraw" )
                        {
                            viewHelper.AskQuestion($"\nAmount Deposit: ");
                            int num;
                            string inputNumber = Console.ReadLine().Trim();
                            bool success = Int32.TryParse(inputNumber, out num);
                            if (!success)
                            {
                                viewHelper.ErrorMessage("Enter a vaild amount !");
                                continue;
                            }

                            bool isDepositSuccessful = accountServices.WithdrawAccount(acc, num);

                            if (!isDepositSuccessful)
                            {
                                viewHelper.ErrorMessage("Withdraw was unsuccessful !");
                                continue;
                            }
                        }

                         viewHelper.ErrorMessage("\n Option Doesn't Exist!");
                         break;
                    }
                }
                else if (answer == "exit" || answer == "exit")
                {
                    viewHelper.Successmessage("\n Thank you for using E-Corp Banking.\n");
                    System.Environment.Exit(1);
                }
            }

        }
    }
}