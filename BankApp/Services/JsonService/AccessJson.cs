using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BankApp.Services.JsonService
{
    public class AccessJson : AccessJsonInterface
    {
   
        public JObject GetDataFromJsonFile(string path)
        {
            string pathUTF = @$"{path}";
            string json;

            using (var jsonFileReader = File.OpenText(pathUTF))
            {
                json = jsonFileReader.ReadToEnd();
            }

            JObject rss = JObject.Parse(json);
            return rss;
        }

        public void writeToJsonFille(JObject rss, string path)
        {
            string pathUTF = @$"{path}";
            using (StreamWriter file = File.CreateText(pathUTF))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                writer.Formatting = Formatting.Indented;
                rss.WriteTo(writer);
            }
        }
    }
}
