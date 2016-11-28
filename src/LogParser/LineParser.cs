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
        Whisper, Raid, Party, Say, Emote, Yell, Officer, Guild, Loot, System, Achievement,
        Instance, GeneralChat, CustomChat, CommandOutput, Roll, NotDefined, GenericChat
    };

    /// <summary>
    /// This class is responsible for converting raw input line into Line object
    /// </summary>
    public static class LineParser
    {
        //contains regex instructions for every MessageType. Have to be loaded from config.json
        private static Dictionary<string, string> regexConfDictionary;
        private static Dictionary<string, string> regexNameConfDictionary;
        private static string regexEmoteChecking;
        private static ConfigManager configManager;
        
        //TODO: Maybe dynamically rearrange set based on rarity of names for faster iteration
        // Stores names acquired while parsing messages for MessageType.Emote identification
        private static HashSet<string> nameSet;
        private static string namesAsString = "";

        static LineParser()
        {
            configManager = new ConfigManager(@"Config\config.json");
            regexConfDictionary = configManager.GetConfigCategory("Regex");
            regexNameConfDictionary = configManager.GetConfigCategory("Regex_name");
            regexEmoteChecking = configManager.GetConfigCategory("Regex_emote")["NotFollowedBy"];
            nameSet = new HashSet<string>();
        }

        public static Line ParseLine(string unparsedLine)
        {
            if (String.IsNullOrWhiteSpace(unparsedLine) || unparsedLine == String.Empty) return null;

            string[] _splittedLine;
            try
            {
                _splittedLine = unparsedLine.Split(new string[] { "  " }, 2, StringSplitOptions.None);
                //TODO: Implement after logging system
                //if (_splittedLine.Length < 2) Logger.Log("Line has unexpected structure.", unparsedLine);
                //FIXME: I need to use GetMessage(string message, MessageType type) method!
                try
                {
                    if (_splittedLine[1] == "") return null;
                    MessageType type;
                    type = GetType(_splittedLine[1]);
                    string name;
                    if (regexNameConfDictionary.ContainsKey(type.ToString()))
                        name = GetName(_splittedLine[1], type);
                    else name = null;
                    nameSet.Add(name);
                    return new Line(GetTimeFromString(_splittedLine[0]), type, _splittedLine[1], name);
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
            foreach (string messageType in regexConfDictionary.Keys)
            {
                // Cast name of enum value to real enum value
                var typeEnum = (MessageType)Enum.Parse(typeof(MessageType), messageType, true);
                if (Regex.IsMatch(message, regexConfDictionary[messageType]))
                    return typeEnum;
            }
            // If not returned value yet, return NotDefined message type. (This is else statement)
            return MessageType.NotDefined;
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

        /// <summary>
        /// Method for extracting Name from message part of an unparsed line
        /// </summary>
        /// <param name="message">Message part of an unparsed line</param>
        /// <param name="type">Type of message</param>
        /// <returns></returns>
        private static string GetName(string message, MessageType type)
        {
            string name = Regex.Match(message, regexNameConfDictionary[type.ToString()]).ToString();
            return (name != "" ? name : null);
        }

        /// <summary>
        /// Checks if Line of type NotDefined is an Emote. If it is, it changes its Type
        /// and Name accordingly. It needs to be used after ParseLine() on every line 
        /// in collection for best effect. Warning: Before using that method make sure you
        /// called PrepareEmoteParsing() method!
        /// </summary>
        /// <param name="line">Line of type MessageType.NotDefined</param>
        /// <returns>Parsed emote line if line is an Emote, input line otherwise</returns>
        /// <exception cref="Exception">If name set is empty or didn't initialize emote parsing
        /// by PrepareEmoteParsing()</exception>
        //FIXME: Needs to return null if not parsed. Have to refactor tests and Program.cs for that.
        public static Line ParseEmote(Line line)
        {
            if (namesAsString == "")
                throw new Exception("Can't find name set for emote parsing. Did you forget to run PrepareEmoteParsing()?");
                if (line.Type != MessageType.NotDefined) return line;
            Line ret = new Line(line);

            if (Regex.IsMatch(line.Text, regexEmoteChecking + namesAsString))
            {
                ret.Type = MessageType.Emote;
                //TODO: Parse name?
                ret.Name = Regex.Match(ret.Text, "^[^-: ]*").ToString();
                return ret;
            }
            return line;
        }

        /// <summary>
        /// For testing purposes. Clears name set collected while parsing lines.
        /// </summary>
        public static void ClearNameSet()
        {
            nameSet.Clear();
            namesAsString = "";
        }

        /// <summary>
        /// Prepares LineParser for parsing NotDefined lines to check if
        /// they're not Emote types
        /// </summary>
        public static void PrepareEmoteParsing()
        {
            nameSet.Remove("You");
            nameSet.Remove(null);
            namesAsString = "(";
            foreach (string name in nameSet)
                namesAsString += $"{name}|";
            namesAsString = namesAsString.Remove(namesAsString.Length - 1);
            namesAsString += ')';
            Console.WriteLine(namesAsString);
        }
    }
}