using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SocialStuff.Model.Message
{
    class TextMessage : Message
    {
        private int SenderID;
        private int ChatID;
        private DateTime Timestamp;
        private string Content;
        private List<int> UsersReport;

        public TextMessage(int senderID, int chatID, string content)
        {
            SenderID = senderID;
            ChatID = chatID;
            Timestamp = DateTime.Now;
            Content = content;
            UsersReport = new List<int>();
        }

        public int getChatID() { return ChatID; }

        public int getSenderID() { return SenderID; }

        public DateTime getTimestamp() { return Timestamp; }

        public string getTextContent() { return Content; }

        public string toString() { return Content; }
    }
}
