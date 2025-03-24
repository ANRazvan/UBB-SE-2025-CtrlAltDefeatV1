using System;
using System.Collections.Generic;
using System.Timers;

namespace SocialStuff.Model
{
    public class User
    {
        private int UserId { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        private int ReportedCount { get; set; }
        private List<int> Friends { get; set; }
        private List<int> Chats { get; set; }
        private DateTime? TimeoutEnd { get; set; }

        public User() { }
        public User(int userId, string username, string phoneNumber, int reportedCount)
        {
            UserId = userId;
            Username = username;
            PhoneNumber = phoneNumber;
            ReportedCount = reportedCount;
            Friends = new List<int>();
            Chats = new List<int>();
        }
        public User(int userId, string username, string phoneNumber, List<int> friends, List<int> chats)
        {
            UserId = userId;
            Username = username;
            PhoneNumber = phoneNumber;
            Friends = friends;
            Chats = chats;
            ReportedCount = 0;
        }

        public int GetUserId()
        {
            return UserId;
        }
        public string GetUsername()
        {
            return Username;
        }
        public string GetPhoneNumber()
        {
            return PhoneNumber;
        }
        public int GetReportedCount()
        {
            return ReportedCount;
        }
        public List<int> GetFriends()
        {
            return Friends;
        }
        public List<int> GetChats()
        {
            return Chats;
        }

        public DateTime? GetTimeoutEnd() => TimeoutEnd;
        public void SetTimeoutEnd(DateTime? timeoutEnd) => TimeoutEnd = timeoutEnd;

        public override string ToString()
        {
            return $"User ID: {UserId}, " +
                $"Username: {Username}, " +
                $"Phone Number: {PhoneNumber}, " +
                $"Reported Count: {ReportedCount}" +
                $"Friends: {Friends}, " +
                $"Chats: {Chats}";
        }

        public string IncreaseReportCount()
        {
            ReportedCount++;
            System.Diagnostics.Debug.WriteLine($"User {Username} report count increased to {ReportedCount}");

            string message = "";
            if (ReportedCount >= 1)
            {
                SetTimeoutEnd(DateTime.Now.AddMinutes(3));
                System.Diagnostics.Debug.WriteLine($"User {Username} set in timeout until {TimeoutEnd}");
                ResetReportCountAfterDelay();

                message = $"User {Username} has been reported {ReportedCount} times and is now in timeout until {TimeoutEnd.Value:HH:mm:ss}";
            }

            return message;
        }



        public void ResetReportCount()
        {
            ReportedCount = 0;
        }

        private void ResetReportCountAfterDelay()
        {
            Timer timer = new Timer(3 * 60 * 1000); // 3 minutes in milliseconds
            timer.Elapsed += (sender, e) =>
            {
                ResetReportCount();
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }

        public void AddFriend(int friendID)
        {
            Friends.Add(friendID);
        }
        public void RemoveFriend(int friendID)
        {
            Friends.Remove(friendID);
        }
        public void JoinChat(int chatID)
        {
            Chats.Add(chatID);
        }
        public void LeaveChat(int chatID)
        {
            Chats.Remove(chatID);
        }
    }
}
