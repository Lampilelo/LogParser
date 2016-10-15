using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LogSplit.Config
{
    //Class responsible for reading config.json file.
    //Its structure needs to be as follows:
    //  {
    //      "Category1_name": {
    //          "Key1" : "Value1",
    //          "Key2" : "Value2",
    //          ...
    //          "KeyN" : "ValueN"
    //      },
    //      "Category2_name": {
    //          "Key1" : "Value1",
    //          "Key2" : "Value2",
    //          ...
    //          "KeyK" : "ValueK"
    //      },
    //      ...
    //      "CategoryL_name": {
    //          "Key1" : "Value1",
    //          "Key2" : "Value2",
    //          ...
    //          "KeyM" : "ValueM"
    //      }
    //  }
    public class ConfigManager
    {
        public string ConfigPath { get; set; }

        public ConfigManager(string path)
        {
            if ( ! File.Exists(path)) throw new Exception("Config File not found!");
            ConfigPath = path;
        }

        //Returns string Value corresponding to Key within given Category from conf.json
        public string GetConfigValue(string Category, string Key)
        {
            return GetConfigCategory(Category)[Key];
        }

        //Returns Dictionary<string, string> wchich resides under given Category in conf.json 
        public Dictionary<string, string> GetConfigCategory(string Category)
        {
            try
            {
                var config = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(File.ReadAllText(ConfigPath));
                return config[Category];
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
    }
}
