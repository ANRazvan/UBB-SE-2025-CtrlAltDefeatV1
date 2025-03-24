using Microsoft.Data.SqlClient;
using SocialStuff.Model;
using SocialStuff.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SocialStuff.Services
{
    public class UserService
    {
        private Repository repo;
        private readonly NotificationService notificationService;
        private int UserID;

        public UserService(Repository repo, NotificationService notificationService)
        {
            this.repo = repo;
            this.notificationService = notificationService;
            this.UserID = GetCurrentUser();
        }

        public Repository GetRepo()
        {
            return repo;
        }

        public void AddFriend(int userID, int newFriendID)
        {
            var user = GetUserById(userID);
            var friend = GetUserById(newFriendID);
            var friends = repo.GetFriendsIDs(userID);

            if (user != null && friend != null && !friends.Contains(newFriendID))
            {
                repo.AddFriend(userID, newFriendID);
                user.AddFriend(newFriendID);
                notificationService.SendFriendNotification(userID, newFriendID);
            }
        }

        public void RemoveFriend(int userID, int oldFriendID)
        {
            var user = GetUserById(userID);
            var friend = GetUserById(oldFriendID);
            var friends = repo.GetFriendsIDs(userID);

            if (user != null && friend != null && friends.Contains(oldFriendID))
            {
                repo.DeleteFriend(userID, oldFriendID);
                user.RemoveFriend(oldFriendID);
                notificationService.SendRemoveFriendNotification(userID, oldFriendID);
            }
        }

        public void JoinChat(int userID, int chatID)
        {
            var user = GetUserById(userID);
            var chats = repo.GetChatsIDs(userID);

            if (user != null && !chats.Contains(chatID))
            {
                repo.AddUserToChat(chatID, userID);
                user.JoinChat(chatID);
            }
        }

        public void LeaveChat(int userID, int chatID)
        {
            var user = GetUserById(userID);
            var chats = repo.GetChatsIDs(userID);

            if (user != null && chats.Contains(chatID))
            {
                repo.RemoveUserFromChat(chatID, userID);
                user.LeaveChat(chatID);
            }
        }

        public List<int> FilterUsers(string keyword, int userID)
        {
            var users = repo.GetUsersList();
            return users.Where(u => (u.GetUsername().Contains(keyword, StringComparison.OrdinalIgnoreCase)
                                    || u.GetPhoneNumber().Contains(keyword)) && u.GetUserId() != userID)
                        .Select(u => u.GetUserId())
                        .ToList();
        }

        public List<int> FilterFriends(string keyword, int userID)
        {
            var user = GetUserById(userID);
            if (user == null) return new List<int>();
            var friends = repo.GetUserFriendsList(userID); 

            return friends
                       .Select(friendID => friendID)
                       .Where(friend => friend != null &&
                                        (friend.GetUsername().Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                         friend.GetPhoneNumber().Contains(keyword)))
                       .Select(friend => friend.GetUserId())
                       .ToList();
        }

        public List<int> GetFriendsIDsByUser(int userID)
        {
            var friends = repo.GetFriendsIDs(userID);

            return friends;
        }

        public List<User> GetFriendsByUser(int userID)
        {
            return repo.GetUserFriendsList(userID);
        }


        public List<int> GetChatsByUser(int userID)
        {
            var chats = repo.GetChatsIDs(userID);
            return chats;
        }

        public List<Chat> GetCurrentUserChats()
        {
            List<Chat> chats = new List<Chat>();
            List<Chat> currentUserChats = new List<Chat>();
            chats = this.repo.GetChatsList();

            foreach (Chat chat in chats)
            {
                if(chat.getUserIDsList().Contains(UserID))
                {
                    currentUserChats.Add(chat);
                }
            }
            return currentUserChats;
        }

        public User GetUserById(int userID)
        {
            List<User> users = repo.GetUsersList();
            foreach (User user in users)
            {
                if (user.GetUserId() == userID)
                {
                    return user;
                }
            }

            return null;
        }

        public List<User> GetNonFriendsUsers(int UserID)
        {
            List<User> users = new List<User>(repo.GetUsersList().Where(user => user.GetUserId() != UserID));
            List<int> friends = repo.GetFriendsIDs(UserID);
            List<User> nonFriends = new List<User>();
            foreach (User user in users)
            {
                if (!friends.Contains(user.GetUserId()))
                {
                    nonFriends.Add(user);
                }
            }
            return nonFriends;
        }

        public int GetCurrentUser()
        {
            return repo.GetLoggedInUserID(); // This should be replaced with actual logic to get the logged-in user.
        }
    }
}