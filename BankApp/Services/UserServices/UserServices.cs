using System;
using System.Collections.Generic;
using BankApp.Models;
using System.Linq;

namespace BankApp.Services.UserServices
{
    public class UserServices : UserServicesInterface
    {
        JsonService.JsonService jsonService = new JsonService.JsonService();

        public User CreateNewUser(User user)
        {
           
            User isUser = FindUserByUsername(user.Username);

            if (isUser != null)
            {
                return null;
            }
            
            jsonService.addNewUser(user);
            return user;
        }

        public User FindUserByUsername(string username)
        {

            IList<User> listOfUser = jsonService.listOfUser();

            User user = listOfUser.Where(us => us.Username == username).FirstOrDefault();

            return user;
        }

        public User FindUserByUsernameAndPassword(string username, string password)
        {
            IList<User> listOfUser = jsonService.listOfUser();

            User user = listOfUser.Where(us => us.Username == username && us.Password == password).FirstOrDefault();

            

            return user;
        }
    }
}
