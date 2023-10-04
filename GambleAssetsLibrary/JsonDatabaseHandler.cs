using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using System.Security.Cryptography.Xml;

namespace GambleAssetsLibrary
{
    public class JsonDatabaseHandler
    {
        public static void SaveJsonData(List<User> user)
        {
            string JsonString = JsonSerializer.Serialize(user);
            File.WriteAllText("savedUserdata.json", JsonString);
        }

        public static List<User> ConvertJsonToObject(string json)
        {
            var J = JsonSerializer.Deserialize<List<User>>(json);
            foreach(var k in J)
            {
                Console.WriteLine(k);
            }
            return J;
        }

        public static string LoadJsonData(){
            if (File.Exists("savedUserdata.json"))
            {
                string JsonString = File.ReadAllText("savedUserdata.json");
                return JsonString;
            }
            else
            {
                return "";
            }
        }
    }
}
