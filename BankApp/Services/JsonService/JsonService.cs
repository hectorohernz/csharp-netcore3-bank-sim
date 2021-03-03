using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BankApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BankApp.Services.JsonService
{
    public class JsonService : JsonServiceInterface
    {
        AccessJson accessJsonSer = new AccessJson();

        public bool addNewUser(User user)
        {
            string path = "/Users/highsgod/Projects/BankApplication/BankApp/Database/mock.json";
            JObject rss = accessJsonSer.GetDataFromJsonFile(path);
            JArray data = (JArray)rss["Data"];

            JObject updatedObject = new JObject(new JProperty("name", user.Name), new JProperty("email", user.Email), new JProperty("password", user.Password), new JProperty("username", user.Username));
            data.Add(updatedObject);
            SetNewUserAccounts(user);
            accessJsonSer.writeToJsonFille(rss, path);
            return true;
        }

        public User getUserByUsername(string username)
        {

            string path = "/Users/highsgod/Projects/BankApplication/BankApp/Database/mock.json";
            JObject rss = accessJsonSer.GetDataFromJsonFile(path);

            List<JToken> output = rss.SelectToken("Data").Where(user => (string)user["username"] == username).ToList();

            if (output.Count == 0)
            {
                return null;
            }
          
             JToken JUser = output[0];
             User user = JUser.ToObject<User>();
             return user;
        }

        public IList<User> listOfUser()
        {
    
            string path = "/Users/highsgod/Projects/BankApplication/BankApp/Database/mock.json";
            JObject rss = accessJsonSer.GetDataFromJsonFile(path);
            JArray listOfUsersJtoken = (JArray)rss["Data"];
            IList<User> listOfuserJson = listOfUsersJtoken.ToObject<IList<User>>();
            return listOfuserJson;
        }

        public bool updateExistingUser(User user)
        {

            string path = "/Users/highsgod/Projects/BankApplication/BankApp/Database/mock.json";
            JObject rss = accessJsonSer.GetDataFromJsonFile(path);
            JObject data = (JObject)rss["Data"].Where(us => (string)us["username"] == user.Username).FirstOrDefault();

            data["username"] = user.Username;
            data["name"] = user.Name;
            data["password"] = user.Password;
            data["email"] = user.Email;

            accessJsonSer.writeToJsonFille(rss, path);
            return true;

        }

        public IList<Account> GetAllAccountsByUsername(string username)
        {
            string path = "/Users/highsgod/Projects/BankApplication/BankApp/Database/Accounts.json";
            JObject rss = accessJsonSer.GetDataFromJsonFile(path);

            JArray output = (JArray)rss.SelectToken($"Data.{username}.accounts");
            IList<Account> listOfAccounts = output.ToObject<IList<Account>>();

            return listOfAccounts;
        }

        public bool CreateNewAccount(Account account, User user)
        {

            string path = "/Users/highsgod/Projects/BankApplication/BankApp/Database/Accounts.json";
      
            JObject rss = accessJsonSer.GetDataFromJsonFile(path);

            JToken output = rss.SelectToken($"Data.{user.Username}.accounts").Where(acc => (string)acc["name"] == account.name).FirstOrDefault();

            if (output != null)
            {
                return false;
            }

            JArray accountsArray = (JArray)rss["Data"][$"{user.Username}"]["accounts"];
           
            JToken convertAccount = JToken.FromObject(account);

            accountsArray.Add(convertAccount);

            accessJsonSer.writeToJsonFille(rss, path);

            return true;
        }

        public bool DeleteAccount(Account account, User user)
        {
            string path = @"/Users/highsgod/Projects/BankApplication/BankApp/Database/Accounts.json";
      
            JObject rss = accessJsonSer.GetDataFromJsonFile(path); 

            JToken accountInJson = rss["Data"][$"{user.Username}"]["accounts"].Where(acc => (string)acc["name"] == account.name && (string)acc["ownerUsername"] == account.ownerUsername).First();

            if(accountInJson == null)
            {
                return false;
            }

            accountInJson.Remove();
            accessJsonSer.writeToJsonFille(rss, path);
            return true;


        }

        public void SetNewUserAccounts(User user)
        {
            string path = @"/Users/highsgod/Projects/BankApplication/BankApp/Database/Accounts.json";
       
            JObject rss = accessJsonSer.GetDataFromJsonFile(path);

            JObject data = (JObject)rss["Data"];
            
            JObject userAccountInfo = new JObject(new JProperty("accounts", new JArray()));

            data.Add(new JProperty($"{user.Username}", userAccountInfo));

            accessJsonSer.writeToJsonFille(rss, path);
        }

        public Account GetAccountByAccountName(string username, string name)
        {
            string path = @"/Users/highsgod/Projects/BankApplication/BankApp/Database/Accounts.json";
           
            JObject rss = accessJsonSer.GetDataFromJsonFile(path); 

            JToken output = rss.SelectToken($"Data.{username}.accounts").Where(acc => (string)acc["name"] == name).First();

            if(output == null)
            {
                return null;
            }

            Account acc = output.ToObject<Account>();

            return acc;
        }

        public bool UpdateAccount(Account account)
        {
            string path = @"/Users/highsgod/Projects/BankApplication/BankApp/Database/Accounts.json";
            JObject rss = accessJsonSer.GetDataFromJsonFile(path); ;

            JArray data = (JArray)rss["Data"][account.ownerUsername]["accounts"];

            JObject accountFromData = (JObject)data.Where(acc => (string)acc["name"] == account.name).FirstOrDefault();

            if(accountFromData == null)
            {
                return false;
            }

            accountFromData["amount"] = account.amount;

            accountFromData["name"] = account.name;

            accessJsonSer.writeToJsonFille(rss,path);

            return true;
        }
    }
}
