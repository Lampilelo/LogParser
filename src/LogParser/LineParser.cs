using LogSplit.Config;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace LogParser
{
    public enum MessageType
    {
        Whisper, Raid, Party, Say, Yell, Officer, Guild, Loot, System, Achievement,
        Instance, GeneralChat, CustomChat, GenericChat, CommandOutput, Roll, NotDefined
    };

    static class LineParser
    {
        //contains regex instructions for every MessageType. Have to be loaded from config.json
        static private Dictionary<string, string> regexConfDictionary;
        static private ConfigManager configManager;

        static LineParser()
        {
            configManager = new ConfigManager(@"Config\config.json");
            regexConfDictionary = configManager.GetConfigCategory("Regex");
        }

        static public Line ParseLine(string unparsedLine)
        {
            var result = new Line();
            string[] _splittedLine;
            try
            {
                _splittedLine = unparsedLine.Split(new string[] { "  " }, 2, StringSplitOptions.None);
                //FIXME: I need to use GetMessage(string message, MessageType type) method!
                return new Line(GetTimeFromString(_splittedLine[0]), GetType(_splittedLine[1]), _splittedLine[1]);
            }
            catch (Exception e) { result.AdditionalInfo = e.Message; }
            return result;
        }

        static private MessageType GetType(string message)
        {
            if (Regex.IsMatch(message, regexConfDictionary[MessageType.Whisper.ToString()]))
                return MessageType.Whisper;
            else if (Regex.IsMatch(message, regexConfDictionary[MessageType.Raid.ToString()]))
                return MessageType.Raid;
            else if (Regex.IsMatch(message, regexConfDictionary[MessageType.Party.ToString()]))
                return MessageType.Party;
            else if (Regex.IsMatch(message, regexConfDictionary[MessageType.Say.ToString()]))
                return MessageType.Say;
            else if (Regex.IsMatch(message, regexConfDictionary[MessageType.Yell.ToString()]))
                return MessageType.Yell;
            else if (Regex.IsMatch(message, regexConfDictionary[MessageType.Officer.ToString()]))
                return MessageType.Officer;
            else if (Regex.IsMatch(message, regexConfDictionary[MessageType.Guild.ToString()]))
                return MessageType.Guild;
            else if (Regex.IsMatch(message, regexConfDictionary[MessageType.Loot.ToString()]))
                return MessageType.Loot;
            else if (Regex.IsMatch(message, regexConfDictionary[MessageType.Achievement.ToString()]))
                return MessageType.Achievement;
            else if (Regex.IsMatch(message, regexConfDictionary[MessageType.System.ToString()]))
                return MessageType.System;
            else if (Regex.IsMatch(message, regexConfDictionary[MessageType.Instance.ToString()]))
                return MessageType.Instance;
            else if (Regex.IsMatch(message, regexConfDictionary[MessageType.CommandOutput.ToString()]))
                return MessageType.CommandOutput;
            else if (Regex.IsMatch(message, regexConfDictionary[MessageType.Roll.ToString()]))
                return MessageType.Roll;
            else if (Regex.IsMatch(message, regexConfDictionary[MessageType.GenericChat.ToString()]))
                return MessageType.GenericChat;
            else return MessageType.NotDefined;
        }

        //Not used for now
        static private string GetMessage(string message, MessageType type)
        {
            switch (type)
            {
                case MessageType.Whisper:
                    return message;
                case MessageType.Raid:
                    break;
                case MessageType.Party:
                    break;
                case MessageType.Say:
                    return message;
                case MessageType.Yell:
                    return message;
                case MessageType.Officer:
                    break;
                case MessageType.Guild:
                    break;
                case MessageType.Loot:
                    break;
                case MessageType.System:
                    break;
                case MessageType.Achievement:
                    return message;
                case MessageType.Instance:
                    break;
                case MessageType.GeneralChat:
                    break;
                case MessageType.CustomChat:
                    break;
                case MessageType.GenericChat:
                    break;
                case MessageType.NotDefined:
                    break;
                default:
                    break;
            }

            return null;
        }

        static public DateTime GetTimeFromString(string timeString)
        {
            try
            {
                return DateTime.ParseExact(timeString, "M/d HH:mm:ss.fff", CultureInfo.InvariantCulture);
            }
            catch (FormatException e)
            {
                e.Data.Add("timeString", timeString);
                throw e;
            }
        }
    }
}