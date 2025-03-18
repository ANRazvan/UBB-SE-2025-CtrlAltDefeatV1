using Microsoft.Data.SqlClient;
using SocialStuff.Database;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialStuff.Model
{
    class Chat
    {
        private List<int> UserIDsList;
        private int ChatID;
        private string ChatName;

        // am presupus ca chat-ul va primi in constructor 
        public Chat(int ChatID)
        {
            this.ChatID = ChatID;
            this.UserIDsList = new List<int>(); 
            this.ChatName = "";
            loadChatDataFromDB();
        }
        private void loadChatDataFromDB()
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            dbConnection.OpenConnection();

            try
            {
                string chatQuery = "SELECT ChatName FROM CHATS WHERE ChatID = @ChatID";
                SqlCommand chatCommand = new SqlCommand(chatQuery, dbConnection.getConnection());
                chatCommand.Parameters.AddWithValue("@ChatID", this.ChatID);
                this.ChatName = (string)chatCommand.ExecuteScalar();

                string query = "SELECT UserID FROM CHAT_PARTICIPANTS WHERE ChatID = @ChatID";
                using (SqlCommand cmd = new SqlCommand(query, dbConnection.getConnection()))
                {
                    cmd.Parameters.AddWithValue("@ChatID", this.ChatID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserIDsList.Add(reader.GetInt32(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            finally { dbConnection.CloseConnection(); }
            
        }

        public int getChatID() { return this.ChatID; }

        public List<int> getUserIDsList() { return this.UserIDsList; }

        public string getChatName() { return this.ChatName; }

        //public DateTime getLastMessageTimeStamp()
        //{

        //}
    }
}
