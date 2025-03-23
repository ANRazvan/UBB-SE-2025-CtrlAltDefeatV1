using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialStuff.Model
{
    public class User
    {

        private int UserId { get; set; }
        private string Username { get; set; }
        private string PhoneNumber { get; set; }
        private int ReportedCount { get; set; }
        private List<int> Friends { get; set; }
        private List<int> Chats { get; set; }

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

        public string username
        {
            get { return this.Username; }
        }


        public override string ToString()
        {
            return $"User ID: {UserId}, " +
                $"Username: {Username}, " +
                $"Phone Number: {PhoneNumber}, " +
                $"Reported Count: {ReportedCount}" +
                $"Friends: {Friends}, " +
                $"Chats: {Chats}";
        }


        public void IncreaseReportCount()
        {
            ReportedCount++;
        }

        public void resetReportCount()
        {
            ReportedCount = 0;
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