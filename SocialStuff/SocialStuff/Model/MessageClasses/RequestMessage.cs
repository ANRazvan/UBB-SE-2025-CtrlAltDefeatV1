using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialStuff.Model.MessageClasses
{
    public class RequestMessage : Message
    {
        private int MessageID { get; set; }
        public int getMessageID() => MessageID;
        private int SenderID { get; set; }
        public int getSenderID() => SenderID;
        private int ChatID { get; set; }
        public int getChatID() => ChatID;
        private DateTime Timestamp { get; set; }
        public DateTime getTimestamp() => Timestamp;
        private string Status { get; set; }
        public string getStatus() => Status;
        public float Amount { get; set; }
        public float getAmount() => Amount;
        public string Description { get; set; }
        public string getDescription() => Description;
        public string Currency { get; set; }
        public string getCurrency() => Currency;
        public string FormattedAmount => $"{Amount} {Currency}";

        public RequestMessage(int messageID, int requesterID, int chatID, string status, float amount, string desc, string currency)
            : base(messageID, requesterID, chatID)
        {
            MessageID = messageID;
            SenderID = requesterID;
            ChatID = chatID;
            Timestamp = DateTime.Now;
            Status = status;
            Amount = amount;
            Description = desc;
            Currency = currency;
        }

        public RequestMessage(int messageID, int requesterID, int chatID, DateTime timestamp, string status, float amount, string desc, string currency)
            : base(messageID, requesterID, chatID, timestamp)
        {
            MessageID = messageID;
            SenderID = requesterID;
            ChatID = chatID;
            Timestamp = timestamp;
            Status = status;
            Amount = amount;
            Description = desc;
            Currency = currency;
        }

        public override string ToString()
        {
            return $"Request Message: {Amount}{Currency} - {Description}";
        }
    }
}