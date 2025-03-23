using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialStuff.Model.MessageClasses
{
    public class ImageMessage : Message
    {
        private int MessageID { get; set; }
        public int getMessageID() => MessageID;
        private int SenderID { get; set; }
        public int getSenderID() => SenderID;
        private int ChatID { get; set; }
        public int getChatID() => ChatID;
        private DateTime Timestamp { get; set; }
        public DateTime getTimestamp() => Timestamp;
        public string ImageURL { get; set; }
        public string getImageURL() => ImageURL;
        private List<int> UsersReport { get; set; }
        public List<int> getUsersReport() => UsersReport;

        public ImageMessage(int messageID, int senderID, int chatID, string imageUrl, List<int> usersReport)
            : base(messageID, senderID, chatID)
        {
            MessageID = messageID;
            SenderID = senderID;
            ChatID = chatID;
            Timestamp = DateTime.Now;
            ImageURL = imageUrl;
            UsersReport = usersReport;
        }

        public ImageMessage(int messageID, int senderID, int chatID, DateTime timestamp, string imageUrl, List<int> usersReport)
            : base(messageID, senderID, chatID, timestamp)
        {
            MessageID = messageID;
            SenderID = senderID;
            ChatID = chatID;
            Timestamp = timestamp;
            ImageURL = imageUrl;
            UsersReport = usersReport;
        }

        public override string ToString()
        {
            return ImageURL;
        }
    }
}