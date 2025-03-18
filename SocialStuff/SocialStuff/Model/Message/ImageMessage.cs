using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialStuff.Model.Message
{
    class ImageMessage : Message
    {
        private int SenderID;
        private int ChatID;
        private DateTime Timestamp;
        private string ImageURL;
        private List<int> UsersReport;

        public ImageMessage(int senderID, int chatID, string imageUrl)
        {
            SenderID = senderID;
            ChatID = chatID;
            Timestamp = DateTime.Now;
            ImageURL = imageUrl;
            UsersReport = new List<int>();
        }

        public string getImageURL() { return ImageURL; }
        public int getSenderID() { return SenderID; }

        public int getChatID() { return ChatID; }
        public DateTime getTimestamp() { return Timestamp; }
        public string toString() { return ImageURL; }
    }
}
