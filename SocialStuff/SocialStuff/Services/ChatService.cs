using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Microsoft.UI.Xaml.Controls;
using SocialStuff.Database;
using SocialStuff.Repository;

namespace SocialStuff.Services
{
    public class ChatService
    {
        private Repository.Repository repository;
        
        public ObservableCollection<int> FilteredFriends { get; set; } = new ObservableCollection<int>();
        public Dictionary<int, string> friendsDict = new Dictionary<int, string>();
        public event Action ChatsUpdated;

        public ChatService()
        {
            this.repository = new Repository.Repository();
        }

        public void NotifyChatsUpdated()
        {
            ChatsUpdated?.Invoke();
        }

        public bool addChat(List<int> usersIDs, string chatName)
        {
            try
            {
                repository.AddChat(usersIDs, chatName);
                ChatsUpdated?.Invoke();

                int newChatID = repository.GetChatID(usersIDs, chatName);
                var addedUsers = repository.GetChatParticipants(newChatID);

                return usersIDs.All(id => addedUsers.Contains(id));
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
                        repository.RemoveChat(chatID);
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
                repository.RemoveChat(chatID);
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
            friendsDict = repository.GetFriends(userID).ToDictionary(friend => friend.id, friend => friend.name);
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

        public List<int> getChatParticipants(int chatID)
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

        public List<int> getFriends(int userID)
        {
            try
            {
                return repository.GetFriends(userID);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ChatService - getFriends Error: " + ex.Message);
            }
            return null;
        }

    }
}
