using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialStuff.Model.MessageClasses
{
    public abstract class Message
    {
        private int MessageID { get; set; }
        public int getMessageID() => MessageID;
        private int SenderID { get; set; }
        public int getSenderID() => SenderID;
        private int ChatID { get; set; }
        public int getChatID() => ChatID;
        private DateTime Timestamp { get; set; }
        public DateTime getTimestamp() => Timestamp;
        public string SenderUsername { get; set; }

        public Message(int messageID, int senderID, int chatID)
        {
            MessageID = messageID;
            SenderID = senderID;
            ChatID = chatID;
            Timestamp = DateTime.Now;
        }

        public Message(int messageID, int senderID, int chatID, DateTime timestamp)
        {
            MessageID = messageID;
            SenderID = senderID;
            ChatID = chatID;
            Timestamp = timestamp;
        }

        public override string ToString()
        {
            return $"{GetType().Name} from {SenderID} in chat {ChatID} at {Timestamp}";
        }
    }
}