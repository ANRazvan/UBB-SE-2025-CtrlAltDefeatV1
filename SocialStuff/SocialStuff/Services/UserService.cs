//using Microsoft.Data.SqlClient;
//using SocialStuff.Model;
//using SocialStuff.Data;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;


//namespace SocialStuff.Services
//{
//    internal class UserService
//    {

//        private Repository repo; //readonly?
//        private int UserID;

//        public UserService(Repository repo)
//        {
//            repo = repo;
//            UserID = GetCurrentUser();
//        }

//        public Repository GetRepo()
//        {
//            return repo;
//        }

//        public void AddFriend(int userID, int newFriendID)
//        {
//            var user = repo.GetUserById(userID);
//            var friend = repo.GetUserById(newFriendID);

//            if (user != null && friend != null && !user.Friends.Contains(newFriendID))
//            {
//                // friends is one way process so we add the friend to the user's friend list
//                user.AddFriend(newFriendID);
//                repo.AddFriend(userID, newFriendID);  //function from repo

//                //friend.AddFriend(userID);
//                //repo.UpdateUser(friend);
//            }
//        }

//        public void RemoveFriend(int userID, int oldFriendID)
//        {
//            var user = repo.GetUserById(userID);
//            var friend = repo.GetUserById(oldFriendID);

//            if (user != null && friend != null && user.Friends.Contains(oldFriendID))
//            {
//                user.RemoveFriend(oldFriendID);
//                repo.RemoveFriend(userID, oldFriendID); //function from repo

//                //friend.RemoveFriend(userID);
//                //repo.UpdateUser(friend);
//            }
//        }

//        public void JoinChat(int userID, int chatID)
//        {
//            var user = repo.GetUserById(userID);
//            if (user != null && !user.Chats.Contains(chatID))
//            {
//                user.JoinChat(chatID);
//                repo.JoinChat(chatID, userID);
//            }
//        }

//        public void LeaveChat(int userID, int chatID)
//        {
//            var user = repo.GetUserById(userID);
//            if (user != null && user.Chats.Contains(chatID))
//            {
//                user.LeaveChat(chatID);
//                repo.LeaveChat(chatID, userID);
//            }
//        }

//        public List<int> FilterUsers(string keyword, int userID)
//        {
//            var users = repo.GetAllUsers();
//            return users.Where(u => (u.Username.Contains(keyword, StringComparison.OrdinalIgnoreCase)
//                                    //|| u.PhoneNumber.Contains(keyword)) && u.UserId != userID)
//                        .Select(u => u.UserId)
//                        .ToList();
//        }

//        public List<int> FilterFriends(string keyword, int userID)
//        {
//            var user = repo.GetUserById(userID);
//            if (user == null) return new List<int>();

//            return user.Friends
//                       .Select(friendID => repo.GetUserById(friendID))
//                       .Where(friend => friend != null &&
//                                        (friend.Username.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
//                                         friend.PhoneNumber.Contains(keyword)))
//                       .Select(friend => friend.UserId)
//                       .ToList();
//        }

//        public List<int> GetFriendsByUser(int userID)
//        {
//            var user = repo.GetUserById(userID);
//            return user?.Friends ?? new List<int>();
//        }

//        public int GetCurrentUser()
//        {
//            return repo.getLoggedInUser(); // This should be replaced with actual logic to get the logged-in user.
//        }
//    }
//}
