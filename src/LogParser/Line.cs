using System;

namespace LogParser
{
    /// <summary>
    /// Class for storing information of log line.
    /// </summary>
    public class Line
    {
        //TODO: Maybe add Realm? In that case Name shouldn't contain realm name
        public DateTime Time { get; set; }
        public MessageType Type { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }

        public Line(DateTime time, MessageType type, string text, string name = null)
        {
            Time = time;
            Type = type;
            Text = text;
            Name = name;
        }

        public Line()
        {
            Time = new DateTime();
            Type = MessageType.NotDefined;
            Text = null;
            Name = null;
        }

        public Line(Line line) : this(line.Time, line.Type, line.Text, line.Name) { }
    }
}
