using LogSplit.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LogParser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string HomeLocation = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            //try
            //{
            //    string tempPath = "Config/config.json";
            //    var configManager = new ConfigManager(tempPath);
            //    Console.WriteLine(configManager.GetConfigValue("regex", "System")); 
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            if (!(args == null || args.Length == 0))
            {
                if (!Directory.Exists(HomeLocation + $@"/Output")) Directory.CreateDirectory(HomeLocation + $@"/Output");
                if (File.Exists(args[0]))
                {
                    try
                    {
                        var writers = new Dictionary<MessageType, StreamWriter>(Enum.GetNames(typeof(MessageType)).Length);
                        foreach (MessageType type in Enum.GetValues(typeof(MessageType)).Cast<MessageType>())
                        {
                            writers.Add(type, File.CreateText(HomeLocation + $@"/Output/{type}.txt"));
                            writers[type].AutoFlush = true;
                        }

                        using (StreamReader reader = File.OpenText(args[0]))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                if (line == "" || line == "\n") continue; //Sometimes there are empty lines in log.
                                LineParser parser = new LineParser(line);
                                string outputLine = $"Time: {parser.Result.Time.ToString()}, Type: {parser.Result.Type}, Text: {parser.Result.Text}";
                                writers[parser.Result.Type].WriteLine(parser.Result.Text);
                                Console.WriteLine(outputLine);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Data);
                    }
                }
            }
            //Console.ReadKey();

        }
    }
}
