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

        // The Chat constructor receives a chat ID in the constructor
        public Chat(int ChatID)
        {
            this.ChatID = ChatID;
            this.UserIDsList = new List<int>(); 
            this.ChatName = "";
            loadChatDataFromDB();
        }
        private void loadChatDataFromDB()
        {
            loadChatNameFromDB();
            loadChatParticipantsFromDB();
        }

        private void loadChatNameFromDB()
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            dbConnection.OpenConnection();

            try
            {
                string query = "SELECT ChatName FROM CHATS WHERE ChatID = @ChatID";
                SqlCommand chatCommand = new SqlCommand(query, dbConnection.getConnection());
                chatCommand.Parameters.AddWithValue("@ChatID", this.ChatID);
                this.ChatName = (string)chatCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Model - Chat Error loadChatNameFromDB(): " + ex.Message);
            }

            finally { dbConnection.CloseConnection(); }
        }

        private void loadChatParticipantsFromDB()
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            dbConnection.OpenConnection();

            try
            {
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
                Console.WriteLine("Model - Chat Error loadChatParticipantsFromDB(): " + ex.Message);
            }
            
            finally { dbConnection.CloseConnection(); }
            
        }

        public int getChatID() { return this.ChatID; }

        public List<int> getUserIDsList() { return this.UserIDsList; }

        public string getChatName() { return this.ChatName; }

        public DateTime getLastMessageTimestamp()
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            dbConnection.OpenConnection();

            DateTime lastTimestamp = DateTime.MinValue;

            try
            {
                string query = "SELECT Timestamp FROM MESSAGES WHERE ChatID = @ChatID ORDER BY Timestamp DESC";
                SqlCommand chatCommand = new SqlCommand(query, dbConnection.getConnection());
                chatCommand.Parameters.AddWithValue("@ChatID", this.ChatID);
                object result = chatCommand.ExecuteScalar();
                if(result != null && result != DBNull.Value) { lastTimestamp = (DateTime)result; }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Model - Chat Error getLastMessageTimestamp(): " + ex.Message);
            }
            finally { dbConnection.CloseConnection(); }

            return lastTimestamp;
        }

        public List<Message> getChatHistory()
        {
            throw new NotImplementedException();
        }

        public int getUserCount() { return this.UserIDsList.Count; }

        public void AddUser(int UserID) 
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            dbConnection.OpenConnection();
            try
            {
                string query = "INSERT INTO CHAT_PARTICIPANTS (ChatID, UserID) VALUES (@ChatID, @UserID)";
                SqlCommand cmd = new SqlCommand(query, dbConnection.getConnection());
                cmd.Parameters.AddWithValue("@ChatID", this.ChatID);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.ExecuteNonQuery();

                this.UserIDsList.Add(UserID); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Model - Chat Error AddUser(): " + ex.Message);
            }
            finally
            {
                dbConnection.CloseConnection();
            }
        }

        public void RemoveUser(int UserID)
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            dbConnection.OpenConnection();
            try
            {
                string query = "DELETE FROM CHAT_PARTICIPANTS WHERE UserID = @UserID AND ChatID = @ChatID";
                SqlCommand cmd = new SqlCommand(query, dbConnection.getConnection());
                cmd.Parameters.AddWithValue("@ChatID", this.ChatID);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.ExecuteNonQuery();

                this.UserIDsList.Remove(UserID);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Model - Chat Error RemoveUser(): " + ex.Message);
            }
            finally
            {
                dbConnection.CloseConnection();
            }
        }

        public bool IsUserInChat(int UserID)
        {
            return this.UserIDsList.Contains(UserID);
        }
    }
}
