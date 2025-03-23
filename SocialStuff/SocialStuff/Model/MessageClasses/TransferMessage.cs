using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialStuff.Model.MessageClasses
{
    public class TransferMessage : Message
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
        private List<int> ListOfReceiversID { get; set; }
        public List<int> getListOfReceiversID() => ListOfReceiversID;
        public string FormattedAmount => $"{Amount} {Currency}";

        public TransferMessage(int messageID, int senderID, int chatID, string status, float amount, string desc, string currency)
            : base(messageID, senderID, chatID)
        {
            MessageID = messageID;
            SenderID = senderID;
            ChatID = chatID;
            Timestamp = DateTime.Now;
            Status = status;
            Amount = amount;
            Description = desc;
            Currency = currency;
            ListOfReceiversID = new List<int>();
        }

        public TransferMessage(int messageID, int senderID, int chatID, DateTime timestamp, string status, float amount, string desc, string currency)
            : base(messageID, senderID, chatID, timestamp)
        {
            MessageID = messageID;
            SenderID = senderID;
            ChatID = chatID;
            Timestamp = timestamp;
            Status = status;
            Amount = amount;
            Description = desc;
            Currency = currency;
            ListOfReceiversID = new List<int>();
        }

        public override string ToString()
        {
            return $"Transfer Message: {Amount}{Currency} - {Description}";
        }
    }
}