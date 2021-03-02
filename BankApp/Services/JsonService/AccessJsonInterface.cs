using System;
using Newtonsoft.Json.Linq;

namespace BankApp.Services.JsonService
{
    public interface AccessJsonInterface
    {

        JObject GetDataFromJsonFile(string path);

        void writeToJsonFille(JObject rss, string path);
    }
}
