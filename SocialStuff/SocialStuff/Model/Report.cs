using System;
using System.Collections.Generic;
using SocialStuff.Model;

namespace SocialStuff.Model
{
    public class Report
    {
        public int MessageID { get; set; }
        public int ReporterUserID { get; set; } // The user who reported the message or the user who got reported
        public string Status { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }

        public Report(int MessageID, int ReporterUserID, string Status, string Reason, string Description)
        {
            this.MessageID = MessageID;
            this.ReporterUserID = ReporterUserID;
            this.Status = Status;
            this.Reason = Reason;
            this.Description = Description;
        }



        public int GetMessageID()
        {
            return MessageID;
        }

        public int GetReporterUserID()
        {
            return ReporterUserID;
        }

        public string GetStatus()
        {
            return Status;
        }

        public string GetReason()
        {
            return Reason;
        }

        public string GetDescription()
        {
            return Description;
        }
    }
}