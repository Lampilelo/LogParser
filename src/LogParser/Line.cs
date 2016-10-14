using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

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
            SetTimeFromString(timeString);
            Type = type;
            Text = text;
        }

        public Line(string unparsedLine)
        {
            LineParser parser = new LineParser(unparsedLine);
            Time = parser.Result.Time;
            Type = parser.Result.Type;
            Text = parser.Result.Text;
        }

        public Line()
        {
            Time = null;
            Type = MessageType.NotDefined;
            Text = null;
            AdditionalInfo = null;
        }

        public void SetTimeFromString(string timeString)
        {
            try
            { 
                Time = DateTime.ParseExact(timeString, "M/d HH:mm:ss.fff", CultureInfo.InvariantCulture);
            }
            catch (FormatException e) 
            {
                e.Data.Add("timeString", timeString);
                throw e;
            }
        }
    }
}
