using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SocialStuff.Model;
using SocialStuff.Services;
using SocialStuff.ViewModels;

namespace SocialStuff.ViewModels
{
    public partial class CreateChatViewModel : ObservableObject
    {
        private string _groupName;
        private readonly ChatService _chatService;
        private readonly int _userId = App.LoggedInUserID; // Use actual logged-in user ID

        [ObservableProperty]
        private string searchQuery;

        public ObservableCollection<User> FilteredFriends { get; set; } = new();
        public ObservableCollection<User> SelectedFriends { get; set; } = new();

        public ICommand AddFriendCommand { get; }
        public ICommand RemoveFriendCommand { get; }
        public ICommand CreateChatCommand { get; }

        public string GroupName
        {
            get => _groupName;
            set => SetProperty(ref _groupName, value);
        }

        public CreateChatViewModel()
        {
            _chatService = new ChatService();
            LoadFriends();

            AddFriendCommand = new RelayCommand<User>(AddFriend);
            RemoveFriendCommand = new RelayCommand<User>(RemoveFriend);
            CreateChatCommand = new RelayCommand(CreateChat);
        }

        private void LoadFriends()
        {
            var friends = _chatService.getFriends(_userId);
            if (friends != null)
            {
                FilteredFriends.Clear();
                foreach (var friend in friends)
                {
                    FilteredFriends.Add(friend);
                }
            }
        }

        private void AddFriend(User friend)
        {
            if (friend != null && !SelectedFriends.Contains(friend))
            {
                SelectedFriends.Add(friend);
                FilteredFriends.Remove(friend);
            }
        }

        private void RemoveFriend(User friend)
        {
            if (friend != null && SelectedFriends.Contains(friend))
            {
                SelectedFriends.Remove(friend);
                FilteredFriends.Add(friend);
            }
        }

        private async void CreateChat()
        {
            if (SelectedFriends.Count == 0)
            {
                var dialog = new ContentDialog
                {
                    Title = "Error",
                    Content = "Group name and at least one friend are required.",
                    CloseButtonText = "OK",
                    XamlRoot = App.MainWindow.Content.XamlRoot // Required for WinUI 3
                };
                await dialog.ShowAsync();
                return;
            }

            if (string.IsNullOrWhiteSpace(GroupName) && SelectedFriends.Count == 1)
            {
                var friend = SelectedFriends[0];
                GroupName = friend.GetUsername();
            }

            if (string.IsNullOrWhiteSpace(GroupName))
            {
                var dialog = new ContentDialog
                {
                    Title = "Error",
                    Content = "Group name is required.",
                    CloseButtonText = "OK",
                    XamlRoot = App.MainWindow.Content.XamlRoot 
                };
                await dialog.ShowAsync();
                return;
            }

            var userIds = SelectedFriends.Select(user => user.GetUserId()).ToList();
            userIds.Add(_userId); // Include the creator in the chat

            bool success = _chatService.addChat(userIds, GroupName);
           
            var resultDialog = new ContentDialog
            {
                Title = success ? "Success" : "Error",
                Content = success ? "Chat created successfully!" : "Failed to create chat.",
                CloseButtonText = "OK",
                XamlRoot = App.MainWindow.Content.XamlRoot
            };

            await resultDialog.ShowAsync();
        }

        
        partial void OnSearchQueryChanged(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                LoadFriends();
            }
            else
            {
                var filtered = _chatService.getFriends(_userId)
                    .Where(f => f.GetUsername().Contains(value, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                FilteredFriends.Clear();
                foreach (var friend in filtered)
                {
                    FilteredFriends.Add(friend);
                }
            }
        }
    }
}
