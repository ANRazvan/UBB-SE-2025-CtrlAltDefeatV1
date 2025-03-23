using Microsoft.Data.SqlClient;
using SocialStuff.Data.Database;
using System;
using System.Collections.Generic;
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

        // The Chat constructor receives a chat ID in the constructor

        public Chat(int ChatID, string ChatName, List<int> userIds)
        {
            this.ChatID = ChatID;
            this.UserIDsList = userIds;
            this.ChatName = ChatName;
        }

        public int getChatID() { return this.ChatID; }

        public List<int> getUserIDsList() { return this.UserIDsList; }

        public string getChatName() { return this.ChatName; }

        //public List<Message> getChatHistory()
        //{
        //    List<Message> chatHistory = new List<Message>();
        //    DatabaseConnection dbConnection = new DatabaseConnection();

        //    try
        //    {
        //        dbConnection.OpenConnection();
        //        string query = "SELECT TypeID, UserID, Timestamp, Content, Status, Amount, Currency FROM MESSAGES WHERE ChatID = @ChatID ORDER BY Timestamp";

        //        SqlCommand cmd = new SqlCommand(query, dbConnection.getConnection());
        //        cmd.Parameters.AddWithValue("@ChatID", this.ChatID);

        //        using (SqlDataReader reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                int typeID = reader.GetInt32(0);
        //                int userID = reader.GetInt32(1);
        //                DateTime timestamp = reader.GetDateTime(2);
        //                string content = reader.GetString(3);
        //                string status = reader.GetString(4);
        //                float amount = reader.GetFloat(5);
        //                string currency = reader.GetString(6);

        //                Message message;

        //                //switch (typeID)
        //                //{
        //                //    case 1: // Text message
        //                //        message = new TextMessage(userID, this.ChatID, timestamp, content);
        //                //        break;
        //                //    case 2: // Image message
        //                //        message = new ImageMessage(userID, this.ChatID, timestamp, content);
        //                //        break;
        //                //    case 3: // Transfer message
        //                //        message = new TransferMessage(userID, this.ChatID, timestamp, status, amount, content, currency);
        //                //        break;
        //                //    case 4: // Request message
        //                //        message = new RequestMessage(userID, this.ChatID, timestamp, status, amount, content, currency);
        //                //        break;
        //                //    default:
        //                //        throw new Exception("Unknown message type");
        //                //}

        //                //chatHistory.Add(message);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Model - Chat Error getChatHistory(): " + ex.Message);
        //    }
        //    finally
        //    {
        //        dbConnection.CloseConnection();
        //    }

        //    return chatHistory;
        //}

        public int getUserCount() { return this.UserIDsList.Count; }

        //public void AddUser(int UserID)
        //{
        //    DatabaseConnection dbConnection = new DatabaseConnection();
        //    dbConnection.OpenConnection();
        //    try
        //    {
        //        string query = "INSERT INTO CHAT_PARTICIPANTS (ChatID, UserID) VALUES (@ChatID, @UserID)";
        //        SqlCommand cmd = new SqlCommand(query, dbConnection.getConnection());
        //        cmd.Parameters.AddWithValue("@ChatID", this.ChatID);
        //        cmd.Parameters.AddWithValue("@UserID", UserID);
        //        cmd.ExecuteNonQuery();

        //        this.UserIDsList.Add(UserID);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Model - Chat Error AddUser(): " + ex.Message);
        //    }
        //    finally
        //    {
        //        dbConnection.CloseConnection();
        //    }
        //}

        //public void RemoveUser(int UserID)
        //{
        //    DatabaseConnection dbConnection = new DatabaseConnection();
        //    dbConnection.OpenConnection();
        //    try
        //    {
        //        string query = "DELETE FROM CHAT_PARTICIPANTS WHERE UserID = @UserID AND ChatID = @ChatID";
        //        SqlCommand cmd = new SqlCommand(query, dbConnection.getConnection());
        //        cmd.Parameters.AddWithValue("@ChatID", this.ChatID);
        //        cmd.Parameters.AddWithValue("@UserID", UserID);
        //        cmd.ExecuteNonQuery();

        //        this.UserIDsList.Remove(UserID);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Model - Chat Error RemoveUser(): " + ex.Message);
        //    }
        //    finally
        //    {
        //        dbConnection.CloseConnection();
        //    }
        //}

        public bool IsUserInChat(int UserID)
        {
            return this.UserIDsList.Contains(UserID);
        }
    }
}