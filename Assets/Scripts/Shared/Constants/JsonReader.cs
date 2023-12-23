using Newtonsoft.Json;
using static UnityEditor.Progress;
using System.Collections.Generic;
using System.IO;

namespace Assets.Scripts.Shared.Constants
{
    public static class JsonReader
    {
        public static string LoadJson(string filename)
        {
            using (StreamReader r = new StreamReader(filename))
            {
                string json = r.ReadToEnd();
                return json;
                //List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
            }
        }
    }
}
