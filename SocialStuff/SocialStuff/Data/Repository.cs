using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using SocialStuff.Data.Database;
using Windows.System;
using Windows.UI.Notifications;
using User = SocialStuff.Model.User;
using SocialStuff.Model;
using SocialStuff.Model.MessageClasses;
namespace SocialStuff.Data
{
    internal class Repository
    {
        private DatabaseConnection dbConnection;
        private static int loggedInUserID=1;

        public Repository()
        {
            dbConnection = new DatabaseConnection();
            Console.WriteLine("Repo created");
            //AddUser("Razvan", "0751198737");
            //AddUser("Carmen", "0720511858");
            //AddUser("Maria", "0712345678");
            var users = GetUsersList();

            foreach(var user in users)
            { System.Diagnostics.Debug.WriteLine(user.ToString()); }
        }


        public DatabaseConnection GetDatabaseConnection()
        {
            return dbConnection;
        }

        public static int GetLoggedInUserID()
        {
            return loggedInUserID;
        }

        // Get all users
        public List<User> GetUsersList()
        {
            DataTable dataTable = dbConnection.ExecuteReader("select * from Users", null, false);
            List<User> users = new List<User>();

            foreach (DataRow row in dataTable.Rows)
            {
               int userID = Convert.ToInt32(row["UserID"]);
                string username = row["Username"].ToString();
                string phoneNumber = row["PhoneNumber"].ToString();
                int reportedCount = Convert.ToInt32(row["ReportedCount"]);
                users.Add(new User(userID, username, phoneNumber, reportedCount));
            }
            return users;
        }

        //Get all notifications
        public List<Notification> GetNotificationsList()
        {
            DataTable dataTable = dbConnection.ExecuteReader("select * from Notifications", null, false);
            List<Notification> notifications = new List<Notification>();

            foreach (DataRow row in dataTable.Rows)
            {
                notifications.Add(new Notification());
            }
            return notifications;
        }
        //Get the Friends of a USERID, friends returned as User Class Type
        public List<User> GetUserFriendsList(int userId)
        {
            DataTable dataTable = dbConnection.ExecuteReader("select * from Users", null, false);
            DataTable dataTable1 = dbConnection.ExecuteReader("select * from Friends", null, false);
            List<int> FriendIds = new List<int>();
            foreach (DataRow row in dataTable1.Rows)
            {
                if (Convert.ToInt32(row["UserId"]) == userId)
                {
                    FriendIds.Add(Convert.ToInt32(row["FriendID"]));
                }
            }
            List<User> users = new List<User>();
            foreach (DataRow row in dataTable.Rows)
            { int userID = Convert.ToInt32(row["UserID"]);
               
                if (FriendIds.Contains(userID))
                {
                    string username = row["Username"].ToString();
                    string phoneNumber = row["PhoneNumber"].ToString();
                    int reportedCount = Convert.ToInt32(row["ReportedCount"]);
                    users.Add(new User(userID, username, phoneNumber, reportedCount));
                }
            }
            return users;
        }
        // Get all the Chats for a USERID, chats returned as Chat Class Type
        public List<Chat> GetUserChatsList(int userId)
        {
            DataTable dataTable = dbConnection.ExecuteReader("select * from Chats", null, false);
            DataTable dataTable1 = dbConnection.ExecuteReader("select * from ChatParticipants", null, false);
            List<int> ChatIds = new List<int>();
            foreach (DataRow row in dataTable1.Rows)
            {
                if (Convert.ToInt32(row["UserID"]) == userId)
                {
                    ChatIds.Add(Convert.ToInt32(row["ChatID"]));
                }
            }
            List<Chat> Chats = new List<Chat>();
            foreach (DataRow row in dataTable.Rows)
            {
                int chat = Convert.ToInt32(row["ChatID"]);
                if (ChatIds.Contains(chat))
                {
                    string chatName = row["ChatName"].ToString();
                    Chats.Add(new Chat(chat, chatName));
                
                }
            }
            return Chats;
        }

        // Get all chats
        public List<Chat> GetChatsList()
        {
            DataTable dataTable = dbConnection.ExecuteReader("select * from Chats",null, false);
            List<Chat> chats = new List<Chat>();
            foreach (DataRow row in dataTable.Rows)
            {
                int chatID = Convert.ToInt32(row["ChatID"]);
                string chatName = row["ChatName"].ToString();
                chats.Add(new Chat(chatID, chatName));
            }
            return chats;
        }

        // Get all messages
        public List<Message> GetMessagesList()
        {
            // messagetypes : 1-text , 2-image, 3-request, 4-transfer
            DataTable dataTable = dbConnection.ExecuteReader("select * from Messages", null, false);
            List<Message> messages = new List<Message>();
            foreach (DataRow row in dataTable.Rows)
            {
                int messageID = Convert.ToInt32(row["MessageID"]);
                int typeID = Convert.ToInt32(row["TypeID"]);
                int userID = Convert.ToInt32(row["UserID"]);
                int chatID = Convert.ToInt32(row["ChatID"]);
                DateTime timestamp = Convert.ToDateTime(row["Timestamp"]);
                string content = row["Content"].ToString();
                string status = row["Status"].ToString();
                float amount = Convert.ToSingle(row["Amount"]);
                string currency = row["Currency"].ToString();
                switch (typeID)
                {
                    case 1: // Text message
                        messages.Add(new TextMessage(messageID, userID, chatID, timestamp, content));
                        break;
                    case 2: // Image message
                        messages.Add(new ImageMessage(messageID, userID, chatID, timestamp, content));
                        break;
                    case 3: // Request message
                        messages.Add(new RequestMessage(messageID, userID, chatID, timestamp, status, amount, content, currency));
                        break;
                    case 4: // Transfer message
                        messages.Add(new TransferMessage(messageID, userID, chatID, timestamp, status, amount, content, currency));
                        break;
                    default:
                        throw new Exception("Unknown message type");
                }
            }
            return messages;
        }

        // Get all reports
        public List<Report> GetReportsList()

        {
            DataTable dataTable = dbConnection.ExecuteReader("select * from Reports",null, false);
            List<Report> reports = new List<Report>();
            foreach (DataRow row in dataTable.Rows)
            {
                int reportID = Convert.ToInt32(row["ReportID"]);
                int messageID = Convert.ToInt32(row["MessageID"]);
                string reason = row["Reason"].ToString();
                string description = row["Description"].ToString();
                string status = row["Status"].ToString();
               // reports.Add(new Report(reportID, messageID, reason, description, status));
            }
            return reports;
        }
        //// Get all feed posts
        //public List<FeedPost> GetFeedPostsList()
        //{
        //    DataTable dataTable = dbConnection.ExecuteReader("select * from FeedPosts");
        //    List<FeedPost> feedPosts = new List<FeedPost>();
        //    foreach (DataRow row in dataTable.Rows)
        //    {
        //        int postID = Convert.ToInt32(row["postid"]);
        //        string title = row["title"].ToString();
        //        string category = row["category"].ToString();
        //        string content = row["content"].ToString();
        //        //feedPosts.Add(new FeedPost(postID, title, category, content));
        //    }
        //    return feedPosts;
        //}

        // Add a chat to the database
        public void AddChat(string chatName, out int chatID)
        { 
            SqlParameter[] parameters =
            {
                new SqlParameter("@ChatName", chatName),
                new SqlParameter("@ChatID", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            dbConnection.ExecuteNonQuery("AddChat", parameters);
            chatID = (int)parameters[1].Value; // Get the generated ChatID from the output parameter
        }

        // Update a chat in the database
        public void UpdateChat(int chatID, string chatName)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ChatID", chatID),
                new SqlParameter("@ChatName", chatName)
            };

            dbConnection.ExecuteNonQuery("UpdateChat", parameters);
        }

        // Delete a chat from the database
        public void DeleteChat(int chatID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ChatID", chatID)
            };

            dbConnection.ExecuteNonQuery("DeleteChat", parameters);
        }

        // Add a friend to the database
        public void AddFriend(int userID, int friendID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", userID),
                new SqlParameter("@FriendID", friendID)
            };

            dbConnection.ExecuteNonQuery("AddFriend", parameters);
        }

        // Delete a friend from the database
        public void DeleteFriend(int userID, int friendID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", userID),
                new SqlParameter("@FriendID", friendID)
            };

            dbConnection.ExecuteNonQuery("DeleteFriend", parameters);
        }

        // Add a user to the database
        //added property such that u cannot add an already existing phone number
        public void AddUser(string username, string phoneNumber)
        {
            List<User> users = GetUsersList();
            if(users.Exists(user => user.GetPhoneNumber() == phoneNumber))
            {
                throw new Exception("User already exists");
            }
            SqlParameter[] parameters =
            {
                new SqlParameter("@Username", username),
                new SqlParameter("@PhoneNumber", phoneNumber)
            };
            dbConnection.ExecuteNonQuery("AddUser", parameters);
        }

        // Update a user in the database
        public void UpdateUser(int userID, string username, string phoneNumber)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", userID),
                new SqlParameter("@Username", username),
                new SqlParameter("@PhoneNumber", phoneNumber)
            };

            dbConnection.ExecuteNonQuery("UpdateUser", parameters);
        }

        // Delete a user from the database
        public void DeleteUser(int userID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", userID)
            };

            dbConnection.ExecuteNonQuery("DeleteUser", parameters);
        }

        // Add a message to the database (handles all types)
        public void AddTextMessage(int userID, int chatID, string content)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@TypeID", 1),
                new SqlParameter("@UserID", userID),
                new SqlParameter("@ChatID", chatID),
                new SqlParameter("@Content", content)
            };

            dbConnection.ExecuteNonQuery("AddMessage", parameters);
        }

        public void AddImageMessage(int userID, int chatID, string ImageURL)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@TypeID", 2),
                new SqlParameter("@UserID", userID),
                new SqlParameter("@ChatID", chatID),
                new SqlParameter("@Content", ImageURL)
            };

            dbConnection.ExecuteNonQuery("AddMessage", parameters);
        }


        public void AddRequestMessage(int userID, int chatID, string content, string status = null, decimal? amount = null, string currency = null)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@TypeID", 3),
                new SqlParameter("@UserID", userID),
                new SqlParameter("@ChatID", chatID),
                new SqlParameter("@Content", content),
                new SqlParameter("@Status", status ?? (object)DBNull.Value),
                new SqlParameter("@Amount", amount ?? (object)DBNull.Value),
                new SqlParameter("@Currency", currency ?? (object)DBNull.Value)
            };

            dbConnection.ExecuteNonQuery("AddMessage", parameters);
        }

        public void AddTransferMessage(int userID, int chatID, string content, string status = null, decimal? amount = null, string currency = null)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@TypeID", 4),
                new SqlParameter("@UserID", userID),
                new SqlParameter("@ChatID", chatID),
                new SqlParameter("@Content", content),
                new SqlParameter("@Status", status ?? (object)DBNull.Value),
                new SqlParameter("@Amount", amount ?? (object)DBNull.Value),
                new SqlParameter("@Currency", currency ?? (object)DBNull.Value)
            };

            dbConnection.ExecuteNonQuery("AddMessage", parameters);
        }


        // Update a message in the database
        public void UpdateMessage(int messageID, int typeID, string content, string status = null, decimal? amount = null, string currency = null)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@MessageID", messageID),
                new SqlParameter("@TypeID", typeID),
                new SqlParameter("@Content", content),
                new SqlParameter("@Status", status ?? (object)DBNull.Value),
                new SqlParameter("@Amount", amount ?? (object)DBNull.Value),
                new SqlParameter("@Currency", currency ?? (object)DBNull.Value)
            };

            dbConnection.ExecuteNonQuery("UpdateMessage", parameters);
        }

        // Delete a message from the database
        public void DeleteMessage(int messageID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@MessageID", messageID)
            };

            dbConnection.ExecuteNonQuery("DeleteMessage", parameters);
        }

        // Add a notification
        public void AddNotification(string content, int userID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Content", content),
                new SqlParameter("@UserID", userID)
            };

            dbConnection.ExecuteNonQuery("AddNotification", parameters);
        }

        // Delete a notification
        public void DeleteNotification(int notifID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@NotifID", notifID)
            };

            dbConnection.ExecuteNonQuery("DeleteNotification", parameters);
        }

        // Clear all notifications
        public void ClearAllNotifications()
        {
            dbConnection.ExecuteNonQuery("DeleteAllNotifications");
        }

        // Add a report
        public void AddReport(int messageID, string reason, string description, string status)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@MessageID", messageID),
                new SqlParameter("@Reason", reason),
                new SqlParameter("@Description", description ?? (object)DBNull.Value),
                new SqlParameter("@Status", status)
            };

            dbConnection.ExecuteNonQuery("AddReport", parameters);
        }

        // Update a report
        public void UpdateReport(int reportID, string reason, string description, string status)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ReportID", reportID),
                new SqlParameter("@Reason", reason),
                new SqlParameter("@Description", description ?? (object)DBNull.Value),
                new SqlParameter("@Status", status)
            };

            dbConnection.ExecuteNonQuery("UpdateReport", parameters);
        }

        // Delete a report
        public void DeleteReport(int reportID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ReportID", reportID)
            };

            dbConnection.ExecuteNonQuery("DeleteReport", parameters);
        }

        //// Add a feed post
        //public void AddFeedPost(string title, string category, string content)
        //{
        //    SqlParameter[] parameters =
        //    {
        //        new SqlParameter("@Title", title),
        //        new SqlParameter("@Category", category),
        //        new SqlParameter("@Content", content)
        //    };

        //    dbConnection.ExecuteNonQuery("AddFeedPost", parameters);
        //}

        //// Update a feed post
        //public void UpdateFeedPost(int postID, string title, string category, string content)
        //{
        //    SqlParameter[] parameters =
        //    {
        //        new SqlParameter("@PostID", postID),
        //        new SqlParameter("@Title", title),
        //        new SqlParameter("@Category", category),
        //        new SqlParameter("@Content", content)
        //    };

        //    dbConnection.ExecuteNonQuery("UpdateFeedPost", parameters);
        //}

        //// Delete a feed post
        //public void DeleteFeedPost(int postID)
        //{
        //    SqlParameter[] parameters =
        //    {
        //        new SqlParameter("@PostID", postID)
        //    };

        //    dbConnection.ExecuteNonQuery("DeleteFeedPost", parameters);
        //}

        public void AddUserToChat(int userID, int chatID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", userID),
                new SqlParameter("@ChatID", chatID)
            };
            dbConnection.ExecuteNonQuery("AddUserToChat", parameters);
        }

        public void RemoveUserFromChat(int userID, int chatID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", userID),
                new SqlParameter("@ChatID", chatID)
            };
            dbConnection.ExecuteNonQuery("RemoveUserFromChat", parameters);
        }

        //get the pparticipants of a chat
        public List<User> GetChatParticipants(int chatID)
        {
            DataTable dataTable = dbConnection.ExecuteReader("select * from Users",null, false);
            DataTable dataTable1 = dbConnection.ExecuteReader("select * from Chat_Participants",null, false);
            List<int> UserIds = new List<int>();
            foreach (DataRow row in dataTable1.Rows)
            {
                if (Convert.ToInt32(row["ChatID"]) == chatID)
                {
                    UserIds.Add(Convert.ToInt32(row["UserID"]));
                }
            }

            List<User> users = new List<User>();
            foreach (DataRow row in dataTable.Rows)
            {
                int userID = Convert.ToInt32(row["ChatID"]);
                if (UserIds.Contains(userID))
                {
                    string username = row["Username"].ToString();
                    string phoneNumber = row["PhoneNumber"].ToString();
                    int reportedCount = Convert.ToInt32(row["ReportedCount"]);
                    users.Add(new User(userID, username, phoneNumber, reportedCount));
                }
            }
            return users;
        }
    }

}

