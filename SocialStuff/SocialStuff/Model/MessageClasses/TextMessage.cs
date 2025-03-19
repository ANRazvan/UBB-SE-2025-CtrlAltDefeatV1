using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
namespace SocialStuff.Model.MessageClasses
{
    class TextMessage : Message
    {
        private int MessageID;
        private int SenderID;
        private int ChatID;
        private DateTime Timestamp;
        private string Content;
        private List<int> UsersReport;

        // make the getting of the reports..
        public TextMessage(int MessageID, int senderID, int chatID, string content)
        {
            this.SenderID = senderID;
            this.ChatID = chatID;
            this.Timestamp = DateTime.Now;
            this.Content = content;
            this.UsersReport = new List<int>();
        }

        public TextMessage(int MessageID,int senderID, int chatID, DateTime timestamp, string content)
        {
            this.SenderID = senderID;
            this.ChatID = chatID;
            this.Timestamp = timestamp;
            this.Content = content;
            this.UsersReport = new List<int>();
        }

        public int getChatID() { return ChatID; }

        public int getSenderID() { return SenderID; }

        public DateTime getTimestamp() { return Timestamp; }

        public string getTextContent() { return Content; }

        public string toString() { return Content; }
    }
}