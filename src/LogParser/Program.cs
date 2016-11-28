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
            var notDefinedLines = new List<Line>(1000);

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
                                Line parsedLine = LineParser.ParseLine(line);
                                if (parsedLine == null) continue;
                                if (parsedLine.Type == MessageType.NotDefined)
                                {
                                    notDefinedLines.Add(parsedLine);
                                    continue;
                                }
                                string outputLine = $"Time: {parsedLine.Time.ToString()}, Type: {parsedLine.Type}, Text: {parsedLine.Text}";
                                writers[parsedLine.Type].WriteLine(parsedLine.Text);
                                //Console.WriteLine(outputLine);
                            }
                        }
                        LineParser.PrepareEmoteParsing();
                        for (int i = 0; i < notDefinedLines.Count; i++)
                        {
                            Line emoteLine = LineParser.ParseEmote(notDefinedLines[i]);
                            if (emoteLine.Type == MessageType.Emote)
                                writers[MessageType.Emote].WriteLine(emoteLine.Text);
                            else writers[MessageType.NotDefined].WriteLine(notDefinedLines[i].Text);
                            notDefinedLines.RemoveAt(i);
                        }
                    }
                    //TODO: Log
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
