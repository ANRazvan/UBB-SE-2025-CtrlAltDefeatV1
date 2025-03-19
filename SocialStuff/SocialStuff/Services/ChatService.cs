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
        
        public ObservableCollection<int> FilteredFriends { get; set; } = new ObservableCollection<int>();
        public Dictionary<int, string> friendsDict = new Dictionary<int, string>();
        public event Action ChatsUpdated;

        public ChatService()
        {
            this.repository = new Repository    ();
        }

        public void NotifyChatsUpdated()
        {
            ChatsUpdated?.Invoke();
        }

        public bool addChat(List<int> usersIDs, string chatName)
        {
            try
            {
                int chatID;
                repository.AddChat(chatName, out chatID);
                ChatsUpdated?.Invoke();

                foreach (var userID in usersIDs)
                {
                    repository.AddUserToChat(userID, chatID);
                }

                var addedUsers = repository.GetChatParticipants(chatID);

                if (addedUsers.Count == usersIDs.Count)
                    return true;
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
                XamlRoot = App.main_window.Content.XamlRoot // Required for WinUI 3
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
            foreach(var id in friendsDict.Keys)
            {
                FilteredFriends.Add(id);
            }
        }

        public void filterFriends(string seq, int userID)
        {
           FilteredFriends.Clear();
            foreach(var id in friendsDict.Keys)
            {
                if (friendsDict[id].Contains(seq))
                {
                    FilteredFriends.Add(id);
                }
            }

        }

        public List<User> getChatParticipants(int chatID)
        {
            try
            {
                return repository.GetChatParticipants(chatID);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ChatService - getChatParticipants Error: " + ex.Message);
            }
            return null;
        }

        public List<User> getFriends(int userID)
        {
            try
            {
                return repository.GetUserFriendsList(userID);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ChatService - getFriends Error: " + ex.Message);
            }
            return null;
        }

    }
}
