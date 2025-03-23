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
        private int UserID;

        public UserService(Repository repo)
        {
            this.repo = repo;
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


//        public List<int> FilterUsers(string keyword, int userID)
//        {
//            var users = repo.GetAllUsers();
//            return users.Where(u => (u.Username.Contains(keyword, StringComparison.OrdinalIgnoreCase)
//                                    || u.PhoneNumber.Contains(keyword)) && u.UserId != userID)
//                        .Select(u => u.UserId)
//                        .ToList();
//        }

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

        public int GetCurrentUser()
        {
            return repo.GetLoggedInUserID(); // This should be replaced with actual logic to get the logged-in user.
        }

//        public int GetCurrentUser()
//        {
//            return repo.getLoggedInUser(); // This should be replaced with actual logic to get the logged-in user.
//        }
//    }
//}
