using System;

namespace LogParser
{
    /// <summary>
    /// Class for storing information of log line.
    /// It's not supposed to have real complex informations, 
    /// just time, type and string text value.
    /// </summary>
    public class Line
    {
        public DateTime Time { get; set; }
        public MessageType Type { get; set; }
        public string Text { get; set; }

        public Line(DateTime time, MessageType type, string text)
        {
            Time = time;
            Type = type;
            Text = text;
        }

        public Line()
        {
            Time = new DateTime();
            Type = MessageType.NotDefined;
            Text = null;
        }
    }
}
