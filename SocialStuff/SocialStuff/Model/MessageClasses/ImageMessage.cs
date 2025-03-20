﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialStuff.Model.MessageClasses
{
    public class ImageMessage : Message
    {
        private int MessageID;
        private int SenderID;
        private int ChatID;
        private DateTime Timestamp;
        private string ImageURL;
        private List<int> UsersReport;

        public ImageMessage(int messageID, int senderID, int chatID, string imageUrl, List<int> usersReport)
        {
            MessageID = messageID;
            SenderID = senderID;
            ChatID = chatID;
            Timestamp = DateTime.Now;
            ImageURL = imageUrl;
            UsersReport = usersReport;
        }

        public ImageMessage(int MessageID, int senderID, int chatID, DateTime timestamp, string imageUrl, List<int> usersReport)
        {
            this.MessageID = MessageID;
            this.SenderID = senderID;
            this.ChatID = chatID;
            this.Timestamp = timestamp;
            this.ImageURL = imageUrl;
            this.UsersReport = usersReport;
        }

        public string getImageURL() { return ImageURL; }
        public int getSenderID() { return SenderID; }

        public int getChatID() { return ChatID; }
        public DateTime getTimestamp() { return Timestamp; }
        public string toString() { return ImageURL; }
    }
}