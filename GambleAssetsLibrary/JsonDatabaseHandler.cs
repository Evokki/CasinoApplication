using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;

namespace GambleAssetsLibrary
{
    public class JsonDatabaseHandler
    {
        public static void SaveJsonData(User user)
        {
            string newJsonString = JsonSerializer.Serialize(user);
            string JsonString = LoadJsonData() + newJsonString;
            File.WriteAllText("SaveData/savedUserdata.json", JsonString);
        }

        public static List<User> ConvertJsonToObject(string json)
        {
           return JsonSerializer.Deserialize<List<User>>(json);
        }

        public static string LoadJsonData(){
            if (File.Exists("SaveData/savedUserdata.json"))
            {
                string JsonString = File.ReadAllText("SaveData/savedUserdata.json");
                return JsonString;
            }
            else
            {
                return "";
            }
        }
    }
}
