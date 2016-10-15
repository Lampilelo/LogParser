using System;

namespace LogParser
{
    public class Line
    {
        public DateTime? Time { get; set; }
        public MessageType Type { get; set; }
        public string Text { get; set; }
        public string AdditionalInfo { get; set; }

        public Line(DateTime time, MessageType type, string text)
        {
            Time = time;
            Type = type;
            Text = text;
        }

        public Line(string timeString, MessageType type, string text)
        {
            LineParser.GetTimeFromString(timeString);
            Type = type;
            Text = text;
        }

        public Line(string unparsedLine)
        {
            Line line = LineParser.ParseLine(unparsedLine);
            Time = line.Time;
            Type = line.Type;
            Text = line.Text;
        }

        public Line()
        {
            Time = null;
            Type = MessageType.NotDefined;
            Text = null;
            AdditionalInfo = null;
        }

        
    }
}
