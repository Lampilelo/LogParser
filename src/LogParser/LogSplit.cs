using System.Collections;
using System.IO;

namespace LogParser
{
    public enum MessageType
    {
        Whisper, Raid, Party, Say, Yell, Officer, Guild, Loot, System, Achievement,
        Instance, GeneralChat, CustomChat, GenericChat, CommandOutput, Roll, NotDefined
    };

    public class LogParser
    {
        private string _RawLogPath;
        private Hashtable _FileList;

        
        public LogParser(string path)
        {
            _RawLogPath = path;

            _FileList = new Hashtable();
            _FileList.Add("whisper", "Whisper.log");
            _FileList.Add("raid", "Raid.log");
            _FileList.Add("party", "Party.log");
            _FileList.Add("say", "Say.log");
            _FileList.Add("yell", "Yell.log");
            _FileList.Add("officer", "Officer.log");
            _FileList.Add("guild", "Guild.log");
            _FileList.Add("loot", "Loot.log");
            _FileList.Add("system", "System.log");
            _FileList.Add("achievement", "Achievement.log");
            _FileList.Add("instance", "Instance.log");
            //_FileList.Add(");
        }
    }
}
