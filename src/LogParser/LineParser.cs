using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.IO;

namespace LogParser
{
    internal class LineParser
    {
        public Line Result { get; private set; }
        private string[] _splittedLine;
        private Dictionary<MessageType, string> regexConfDictionary; //contains regex instructions for every MessageType. Have to be loaded from config.json

        public LineParser(string unparsedLine)
        {
            //regexConfDictionary = //JsonConvert.DeserializeObject<Dictionary<MessageType, string>>(File.ReadAllText("./Resources/configure.json"));

            Result = new Line();
            try
            {
                _splittedLine = unparsedLine.Split(new string[] { "  " }, 2, StringSplitOptions.None);
                Result.SetTimeFromString(_splittedLine[0]);
                Result.Type = GetType(_splittedLine[1]);
                //TEMPORARY
                Result.Text = _splittedLine[1];
            }
            catch (Exception e) { Result.AdditionalInfo = e.Message; }
        }

        //TODO: read regex strings from config.json
        private MessageType GetType(string message)
        {
            if (Regex.IsMatch(message, @""))
                return MessageType.Whisper;
            else if (Regex.IsMatch(message, @""))
                return MessageType.Raid;
            else if (Regex.IsMatch(message, @""))
                return MessageType.Party;
            else if (Regex.IsMatch(message, @""))
                return MessageType.Say;
            else if (Regex.IsMatch(message, @""))
                return MessageType.Yell;
            else if (Regex.IsMatch(message, @""))
                return MessageType.Officer;
            else if (Regex.IsMatch(message, @""))
                return MessageType.Guild;
            else if (Regex.IsMatch(message, @""))
                return MessageType.Loot;
            else if (Regex.IsMatch(message, @""))
                return MessageType.Achievement;
            else if (Regex.IsMatch(message, @""))
                return MessageType.System;
            else if (Regex.IsMatch(message, @""))
                return MessageType.Instance;
            else if (Regex.IsMatch(message, @""))
                return MessageType.CommandOutput;
            else if (Regex.IsMatch(message, @""))
                return MessageType.Roll;
            //Needs to be last
            else if (Regex.IsMatch(message, @""))
                return MessageType.GenericChat;
            else return MessageType.NotDefined;
        }

        //FOR NOW NOT USED
        private string GetMessage(string message, MessageType type)
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
    }
}