using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using SocialStuff.Data;
using SocialStuff.Model;


namespace SocialStuff.Services
{
    internal class ReportService
    {
      
        private Repository repository ;
        private readonly UserService userService;
        private  List<Report> reports ;    


        public ReportService(Repository repository, UserService userService )
        {
            this.repository = repository;
            this.reports = repository.GetReportsList();
            this.userService = userService;
        }

        public Report GetReportById(int id)
        {
            return reports.Find(report => report.MessageID == id);
        }

        public void AddReport(Report report)
        {
            reports.Add(report);
        }

        public bool CheckIfReportExists(int messageID, int reporterUserID)
        {
            return reports.Exists(report => report.MessageID == messageID && report.ReporterUserID == reporterUserID);
        }

        public void IncreaseReportCount(int reportedID)
        {
            // need the user to get the user id and increase the report count
            User user = userService.GetUserById(reportedID);
            user.IncreaseReportCount();
        }



        public void LogReportedMessages(List<Report> reports)
        {
            foreach (var report in reports)
            {
                repository.AddReport(report.MessageID, report.Reason, report.Description,report.Status);//need to include repo
            }
        }

        public void SendReport(Report report)
        {
            // todo - implement this method
            // same as the functioon above 

        }

    }
}