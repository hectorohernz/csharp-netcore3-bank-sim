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
        public bool addNewUser(User user)
        {
            string path = @"/Users/highsgod/Projects/BankApplication/BankApp/Database/mock.json";
            JObject rss = GetAllData();

            JArray data = (JArray)rss["Data"];

            JObject updatedObject = new JObject(new JProperty("name", user.Name), new JProperty("email", user.Email), new JProperty("password", user.Password), new JProperty("username", user.Username));

            data.Add(updatedObject);
            
            using (StreamWriter file = File.CreateText(path))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                writer.Formatting = Formatting.Indented;
                rss.WriteTo(writer);
            }
            return true;
        }

        public User getUserByUsername(string username)
        {

            JObject rss = GetAllData();

            List<JToken> output = rss.SelectToken("Data").Where(user => (string)user["username"] == username).ToList();

            if (output.Count == 0)
            {
                return null;
            }
            else
            {
                JToken JUser = output[0];
                User user = JUser.ToObject<User>();
                return user;
            }
        }

        public IList<User> listOfUser()
        {
            string path = @"/Users/highsgod/Projects/BankApplication/BankApp/Database/mock.json";
            using (var jsonFileReader = File.OpenText(path))
            {
                string json = jsonFileReader.ReadToEnd();
                JObject jsonData = JObject.Parse(json);
                IList<JToken> results = jsonData["Data"].Children().ToList();
                IList<User> users = new List<User>();
                foreach (JToken user in results)
                {
                    // JToken.ToObject is a helper method that uses JsonSerializer internally
                    User currentUser = user.ToObject<User>();
                    users.Add(currentUser);
                }

                return users;
            }
        }

        public bool updateExistingUser(User user)
        {
            JObject rss = GetAllData();

            JArray data = (JArray)rss["Data"];

            foreach (JObject u in data)
            {
                if ((string)u["username"] == user.Username)
                {
                    u["username"] = user.Username;
                    u["name"] = user.Name;
                    u["password"] = user.Password;
                    u["email"] = user.Email;

                    string path = @"/Users/highsgod/Projects/BankApplication/BankApp/Database/mock.json";
                    File.WriteAllText(path, rss.ToString());
                    using (StreamWriter file = File.CreateText(path))
                    using (JsonTextWriter writer = new JsonTextWriter(file))
                    {
                        writer.Formatting = Formatting.Indented;
                        rss.WriteTo(writer);
                    }
                    return true;

                }

                continue;
            }

            return false;
        }


        public JObject GetAllData()
        {

            string path = @"/Users/highsgod/Projects/BankApplication/BankApp/Database/mock.json";
            string json;

            using (var jsonFileReader = File.OpenText(path))
            {
                json = jsonFileReader.ReadToEnd();
            }

            JObject rss = JObject.Parse(json);
            return rss;
        }
    }
}
