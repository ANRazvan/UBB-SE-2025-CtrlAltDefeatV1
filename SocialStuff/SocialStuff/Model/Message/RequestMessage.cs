using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialStuff.Model.Message
{
    class RequestMessage : Message
    {
        private int ChatID;
        private int RequesterID;
        private DateTime Timestamp;
        private int Status;
        private float Amount;
        private string Description;
        private string Currency;

        public RequestMessage(int requesterID, int chatID, int status, float amount, string desc, string currency)
        {
            this.RequesterID = requesterID;
            this.ChatID = chatID;
            this.Timestamp = DateTime.Now;

            this.Status = status;
            this.Amount = amount;
            this.Description = desc;
            this.Currency = currency;
        }

        public int getChatID() { return this.ChatID; }

        public string getDescription() { return this.Description; }

        public int getSenderID() { return this.RequesterID; }

        public DateTime getTimestamp() { return this.Timestamp; }

        public float getAmount() { return this.Amount; }

        public int getMessageStatus() { return this.Status; }

        public string getCurrency() { return this.Currency; }

        public string toString()
        {
            String message = "Request Message: " + this.Amount + this.Currency + this.Description;
            return message;
        }
    }
}
