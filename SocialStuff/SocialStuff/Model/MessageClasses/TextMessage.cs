using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace SocialStuff.Model.MessageClasses
{
    public class TextMessage : Message
    {
        private int MessageID { get; set; }
        public int getMessageID() => MessageID;
        private int SenderID { get; set; }
        public int getSenderID() => SenderID;
        private int ChatID { get; set; }
        public int getChatID() => ChatID;
        private DateTime Timestamp { get; set; }
        public DateTime getTimestamp() => Timestamp;
        public string Content { get; set; }
        public string getContent() => Content;
        private List<int> UsersReport { get; set; }
        public List<int> getUsersReport() => UsersReport;

        public TextMessage(int messageID, int senderID, int chatID, string content)
            : base(messageID, senderID, chatID)
        {
            this.MessageID = messageID;
            this.SenderID = senderID;
            this.ChatID = chatID;
            this.Timestamp = DateTime.Now;
            this.Content = content;
            this.UsersReport = new List<int>();
        }

        public TextMessage(int messageID, int senderID, int chatID, DateTime timestamp, string content, List<int> usersReport)
            : base(messageID, senderID, chatID, timestamp)
        {
            this.MessageID = messageID;
            this.SenderID = senderID;
            this.ChatID = chatID;
            this.Timestamp = timestamp;
            this.Content = content;
            this.UsersReport = usersReport;
        }

        public override string ToString() { return Content; }
    }
}