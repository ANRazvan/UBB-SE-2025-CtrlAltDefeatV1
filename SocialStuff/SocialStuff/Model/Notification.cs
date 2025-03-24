using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialStuff.Model
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public DateTime Timestamp { get; set; }
        public string Content { get; set; }
        public int UserReceiverID { get; set; }

        public Notification(int notificationID, DateTime timestamp, string content, int userReceiverID)
        {
            NotificationID = notificationID;
            Timestamp = timestamp;
            Content = content;
            UserReceiverID = userReceiverID;
        }

        public string GetContent()
        {
            return Content;
        }

        public int GetReceiverID()
        {
            return UserReceiverID;
        }

        public DateTime GetTimestamp()
        {
            return Timestamp;
        }
    }
}
