using System.Reflection;

namespace TicTacToe.ML
{
    public class MLModelConfig
    {
        private static Dictionary<string,string> configDictionary = new Dictionary<string, string>();

        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void AddOrReplace(string key, string value)
        {
            if (configDictionary.ContainsKey(key))
                configDictionary[key] = value;
            else
                configDictionary.Add(key, value);
        }

        public static void Remove(string key)
        {
            configDictionary.Remove(key);
        }

        public static string Get(string key)
        {
            if (configDictionary.ContainsKey(key))
                return configDictionary[key];

            return string.Empty;
        }
    }
}
