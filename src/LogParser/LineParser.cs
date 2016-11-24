using LogSplit.Config;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace LogParser
{
    // Sequence in which MessageType values are defined is the sequence in which
    // LineParser will read regex instructions from config
    // TODO: Rearange types from most common to rarest
    public enum MessageType
    {
        Whisper, Raid, Party, Say, Yell, Officer, Guild, Loot, System, Achievement,
        Instance, GeneralChat, CustomChat, CommandOutput, Roll, NotDefined,
        GenericChat // Needs to be after Raid, Party, Officer, Guild, Instance
    };

    /// <summary>
    /// This class is responsible for converting raw input line into Line object
    /// This class should split line into DateTime, line content String and getting MessageType
    /// </summary>
    public static class LineParser
    {
        //contains regex instructions for every MessageType. Have to be loaded from config.json
        private static Dictionary<string, string> regexConfDictionary;
        private static ConfigManager configManager;

        static LineParser()
        {
            configManager = new ConfigManager(@"Config\config.json");
            regexConfDictionary = configManager.GetConfigCategory("Regex");
        }

        public static Line ParseLine(string unparsedLine)
        {
            if (String.IsNullOrWhiteSpace(unparsedLine) || unparsedLine == String.Empty) return null;

            string[] _splittedLine;
            try
            {
                //FIXME: What if line isn't starting with date?
                _splittedLine = unparsedLine.Split(new string[] { "  " }, 2, StringSplitOptions.None);
                //FIXME: I need to use GetMessage(string message, MessageType type) method!
                try
                {
                    if (_splittedLine[1] == "") return null;
                    //TODO: If type isn't NotDefined get CharacterName and save it to an array
                    //TODO: If type is NotDefined check an character names array and if it starts with name, change type to Emote
                    return new Line(GetTimeFromString(_splittedLine[0]), GetType(_splittedLine[1]), _splittedLine[1]);
                }
                catch(Exception e)
                {
                    //TEMPORARY
                    //TODO: Legit exception handling.
                    if (e.GetType() == typeof(System.FormatException)) Console.WriteLine("Exception line: " + unparsedLine);
                    else Console.WriteLine("Something else? : " + unparsedLine);
                    return null;
                }
            }
            catch (Exception e) { throw e; }
        }

        private static MessageType GetType(string message)
        {
            // Get names of enum values
            string[] messageTypes = Enum.GetNames(typeof(MessageType));

            // This does the same thing as commented if-else underneath
            // Order of defined MessageType values is the order in which this foreach resolves
            // TODO: Maybe dynamically change order of types in messageTypes based on rarity of a type
            foreach (string messageType in messageTypes)
            {
                // Cast name of enum value to real enum value
                var typeEnum = (MessageType)Enum.Parse(typeof(MessageType), messageType, true);
                if (Regex.IsMatch(message, regexConfDictionary[messageType]))
                    return typeEnum;
            }
            // If not returned value yet, return NotDefined message type. (This is else statement)
            return MessageType.NotDefined;

            // Left for reference

            //if (Regex.IsMatch(message, regexConfDictionary[MessageType.Whisper.ToString()]))
            //    return MessageType.Whisper;
            //else if (Regex.IsMatch(message, regexConfDictionary[MessageType.Raid.ToString()]))
            //    return MessageType.Raid;
            //else if (Regex.IsMatch(message, regexConfDictionary[MessageType.Party.ToString()]))
            //    return MessageType.Party;
            //else if (Regex.IsMatch(message, regexConfDictionary[MessageType.Say.ToString()]))
            //    return MessageType.Say;
            //else if (Regex.IsMatch(message, regexConfDictionary[MessageType.Yell.ToString()]))
            //    return MessageType.Yell;
            //else if (Regex.IsMatch(message, regexConfDictionary[MessageType.Officer.ToString()]))
            //    return MessageType.Officer;
            //else if (Regex.IsMatch(message, regexConfDictionary[MessageType.Guild.ToString()]))
            //    return MessageType.Guild;
            //else if (Regex.IsMatch(message, regexConfDictionary[MessageType.Loot.ToString()]))
            //    return MessageType.Loot;
            //else if (Regex.IsMatch(message, regexConfDictionary[MessageType.Achievement.ToString()]))
            //    return MessageType.Achievement;
            //else if (Regex.IsMatch(message, regexConfDictionary[MessageType.System.ToString()]))
            //    return MessageType.System;
            //else if (Regex.IsMatch(message, regexConfDictionary[MessageType.Instance.ToString()]))
            //    return MessageType.Instance;
            //else if (Regex.IsMatch(message, regexConfDictionary[MessageType.CommandOutput.ToString()]))
            //    return MessageType.CommandOutput;
            //else if (Regex.IsMatch(message, regexConfDictionary[MessageType.Roll.ToString()]))
            //    return MessageType.Roll;
            //else if (Regex.IsMatch(message, regexConfDictionary[MessageType.GenericChat.ToString()]))
            //    return MessageType.GenericChat;
            //else return MessageType.NotDefined;
        }

        //Not used for now, probably deprecated/abandoned
        private static string GetMessage(string message, MessageType type)
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

        /// <summary>
        /// This parses given String value to a DateTime.
        /// Method hardcoded for date format of "M/d HH:mm:ss.fff".
        /// </summary>
        /// <param name="timeString">String value to convert to DateTime. Needs to be "M/d HH:mm:ss.fff".</param>
        /// <returns>Valid DateTime parsed from timeString or empty DateTime if failed to parse.</returns>
        private static DateTime GetTimeFromString(string timeString)
        {
            try
            {
                return DateTime.ParseExact(timeString, "M/d HH:mm:ss.fff", CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(FormatException)) e.Data.Add("timeString", timeString);
                //TODO: Log it!
                return new DateTime();
            }
        }
    }
}