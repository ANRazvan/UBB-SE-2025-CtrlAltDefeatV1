using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialStuff.Model.MessageClasses
{
    class TransferMessage : Message
    {
        private int MessageID;
        private int ChatID;
        private int SenderID;
        private DateTime Timestamp;

        private string Status;
        private float Amount;
        private string Description;
        private string Currency;
        private List<int> ListOfReceiversID;

        public TransferMessage(int senderID, int chatID, string status, float amount, string desc, string currency)
        {
            this.SenderID = senderID;
            this.ChatID = chatID;
            this.Timestamp = DateTime.Now;

            this.Status = status;
            this.Amount = amount;
            this.Description = desc;
            this.Currency = currency;
            this.ListOfReceiversID = new List<int>();

        }

        public TransferMessage(int MessageID, int senderID, int chatID, DateTime timestamp, string status, float amount, string desc, string currency)
        {
            this.MessageID = MessageID;
            this.SenderID = senderID;
            this.ChatID = chatID;
            this.Timestamp = timestamp;

            this.Status = status;
            this.Amount = amount;
            this.Description = desc;
            this.Currency = currency;
            this.ListOfReceiversID = new List<int>();
        }

        public List<int> getReceiversIDs() { return this.ListOfReceiversID; }

        public int getSenderID() { return this.SenderID; }

        public int getChatID() { return this.ChatID; }

        public DateTime getTimestamp() { return this.Timestamp; }

        public string getMessageStatus() { return this.Status; }

        public float getAmount() { return this.Amount; }

        public string getDescription() { return this.Description; }

        public string getCurrency() { return this.Currency; }

        public string toString()
        {
            String message = "Transfer Message" + this.Amount + this.Currency + this.Description;
            return message;
        }
    }
}