//ChatService.cs
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Microsoft.UI.Xaml.Controls;
using SocialStuff.Data;
using SocialStuff.Model;

namespace SocialStuff.Services
{
    public class ChatService
    {
        private Repository repository;

        public ObservableCollection<Chat> Chats { get; set; } = new ObservableCollection<Chat>();

        public ObservableCollection<int> FilteredFriends { get; set; } = new ObservableCollection<int>();
        public Dictionary<int, string> friendsDict = new Dictionary<int, string>();
        public event Action ChatsUpdated;

        public ChatService()
        {
            this.repository = new Repository();
        }


        public void NotifyChatsUpdated()
        {
            ChatsUpdated?.Invoke();
        }

        public Repository GetRepo()
        {
            return repository;
        }

        public bool addChat(List<int> usersIDs, string chatName)
        {
            try
            {
                int chatID = repository.AddChat(chatName);


                foreach (var userID in usersIDs)
                {
                    repository.AddUserToChat(userID, chatID);
                }
                var addedUsers = repository.GetChatParticipants(chatID);


                if (addedUsers.Count == usersIDs.Count)
                {
                    ChatsUpdated?.Invoke();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ChatService - addChat Error: " + ex.Message);
                return false;
            }
        }

        public int numberOfSelectedFriends(List<int> selectedFriends)
        {
            return selectedFriends.Count;
        }

        public async Task RemoveUserFromChatAsync(int userID, int chatID)
        {
            var dialog = new ContentDialog
            {
                Title = "Remove User",
                Content = "Are you sure you want to remove this user from the chat?",
                PrimaryButtonText = "Yes",
                CloseButtonText = "No",
                //XamlRoot = App.MainWindow.Content.XamlRoot // Required for WinUI 3
            };

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                try
                {
                    repository.RemoveUserFromChat(userID, chatID);
                    if (repository.GetChatParticipants(chatID).Count == 0)
                    {
                        repository.DeleteChat(chatID);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ChatService - removeUserFromChat Error: " + ex.Message);
                }
            }
        }

        public void AddUserToChat(int UserID, int ChatID)
        {
            this.repository.AddUserToChat(UserID, ChatID);
        }

        public void RemoveUserFromChat(int UserID, int ChatID)
        {
            this.repository.RemoveUserFromChat(UserID, ChatID);
        }

        public string getChatNameByID(int ChatID)
        {
            List<Chat> chatList = this.repository.GetChatsList();
            string chatName = chatList.Where(c => c.getChatID() == ChatID).FirstOrDefault().getChatName();

            return chatName;
        }

        public List<string> getChatParticipantsList(int ChatID)
        {
            List<User> participants = this.repository.GetChatParticipants(ChatID);
            List<string> participantsList = participants.Select(p => p.GetUsername()).ToList();
            return participantsList;
        }


        public void removeChat(int chatID)
        {
            try
            {
                repository.DeleteChat(chatID);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ChatService - removeChat Error: " + ex.Message);
            }
            return;
        }

        public void loadFriends(int userID)
        {
            friendsDict = repository.GetUserFriendsList(userID).ToDictionary(friend => friend.GetUserId(), friend => friend.GetUsername());
            FilteredFriends.Clear();
            foreach (var id in friendsDict.Keys)
            {
                FilteredFriends.Add(id);
            }
        }

        public void filterFriends(string seq, int userID)
        {
            FilteredFriends.Clear();
            foreach (var id in friendsDict.Keys)
            {
                if (friendsDict[id].Contains(seq))
                {
                    FilteredFriends.Add(id);
                }
            }

        }

        public List<User> getChatParticipants(int chatID)
        {
            List<User> users = new List<User>();
            try
            {
                users = repository.GetChatParticipants(chatID);
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ChatService - getChatParticipants Error: " + ex.Message);
            }
            return users;
        }

        public List<User> getFriends(int userID)
        {
            try
            {
                Console.WriteLine(repository.GetUserFriendsList(userID));
                var friends = repository.GetUserFriendsList(userID);
                return friends.OrderBy(friend => friend.GetUsername()).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ChatService - getFriends Error: " + ex.Message);
            }
            return null;
        }

        // get friends not in chat
        public List<User> getFriendsNotInChat(int userID, int chatID)
        {
            List<User> friends = getFriends(userID);
            List<User> participants = repository.GetChatParticipants(chatID);
            List<User> friendsNotInChat = new List<User>();
            foreach (User friend in friends)
            {
                if (!participants.Contains(friend))
                {
                    friendsNotInChat.Add(friend);
                }
            }
            return friendsNotInChat;
        }

        public Chat getChat(int chatID)
        {
            List<Chat> chatList = repository.GetChatsList();
            foreach (Chat chat in chatList)
            {
                if (chat.getChatID() == chatID)
                    return chat;
            }
            return null;
        }

        public List<Chat> getChats(int userID)
        {
            var chats = repository.GetChatsList();
            var validChats = new List<Chat>();

            foreach (var chat in chats)
            {
                int id = chat.getChatID();
                var participants = repository.GetChatParticipants(id);


                if (participants.Exists(user => user.GetUserId() == userID))
                {
                    validChats.Add(chat);
                }
            }

            return validChats.OrderByDescending(chat => chat.getChatID()).ToList();
        }

        public void loadChats(int userID)
        {
            var chats = repository.GetChatsList()
                .Where(chat => repository.GetChatParticipants(chat.getChatID())
                .Exists(user => user.GetUserId() == userID))
                .OrderByDescending(chat => chat.getChatID())
                .ToList();

            Chats.Clear(); // ✅ Clear before reloading
            foreach (var chat in chats)
            {
                Chats.Add(chat);
            }

            NotifyChatsUpdated();
        }

    }
}