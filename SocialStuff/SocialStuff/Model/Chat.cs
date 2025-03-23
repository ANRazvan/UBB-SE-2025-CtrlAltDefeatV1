using Microsoft.Data.SqlClient;
using SocialStuff.Data.Database;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialStuff.Model.MessageClasses;

namespace SocialStuff.Model
{
    public class Chat
    {
        private List<int> UserIDsList;
        private int ChatID;
        private string ChatName;

        public Chat(int ChatID, string ChatName, List<int> userIds)
        {
            this.ChatID = ChatID;
            this.UserIDsList = userIds;
            this.ChatName = ChatName;
        }

        public int getChatID() { return this.ChatID; }

        public List<int> getUserIDsList() { return this.UserIDsList; }

        public string getChatName() { return this.ChatName; }
        public int getUserCount() { return this.UserIDsList.Count; }

        public void AddUser(int UserID)
        {
            this.UserIDsList.Add(UserID);
        }
        public void RemoveUser(int UserID)
        {
            this.UserIDsList.Remove(UserID);
        }


        public bool IsUserInChat(int UserID)
        {
            return this.UserIDsList.Contains(UserID);
        }

        public override string ToString()
        {
            return this.ChatName;
        }
    }
}