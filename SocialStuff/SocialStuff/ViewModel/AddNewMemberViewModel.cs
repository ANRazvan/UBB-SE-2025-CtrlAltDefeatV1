using SocialStuff.Model;
using SocialStuff.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.UI.Xaml.Controls;
using SocialStuff.ViewModel;

namespace SocialStuff.ViewModel
{
    public class AddNewMemberViewModel : INotifyPropertyChanged
    {
        private ChatService chatService;
        private int currentUserId;
        private int chatId;
        private ObservableCollection<User> filteredFriends;
        private ObservableCollection<User> selectedFriends;
        private ObservableCollection<User> existingMembers;
        private string searchQuery;
        private string chatName;
        private User selectedFriend;

        public AddNewMemberViewModel(ChatService chatService, int chatId, int userId)
        {
            this.chatService = chatService;
            this.currentUserId = chatService.GetRepo().GetLoggedInUserID();
            this.chatId = chatId;
            chatName = chatService.getChatNameByID(chatId);


            filteredFriends = new ObservableCollection<User>();
            selectedFriends = new ObservableCollection<User>();
            existingMembers = new ObservableCollection<User>();
            searchQuery = string.Empty;

            LoadExistingMembers();
            LoadFriends();

            AddFriendCommand = new RelayCommand(param => AddFriend((User)param));
            RemoveFriendCommand = new RelayCommand(param => RemoveFriend((User)param));
            AddMembersCommand = new RelayCommand(param => AddMembers(), _ => SelectedFriends.Count > 0);
        }

        public string ChatName
        {
            get
            {
                //if (string.IsNullOrEmpty(chatName))
                //{
                //    chatName = chatService.getChatNameByID(chatId);
                //}
                return chatName;
            }
            set
            {
                if (chatName != value)
                {
                    chatName = value;
                    OnPropertyChanged();
                }
            }
        }

        private void LoadExistingMembers()
        {
            var members = chatService.getChatParticipants(chatId);
            if (members != null)
            {
                ExistingMembers = new ObservableCollection<User>(members);
            }
        }

        private void LoadFriends()
        {
            var friends = chatService.getFriends(currentUserId);
            if (friends != null)
            {
                var existingMemberIds = ExistingMembers.Select(m => m.GetUserId()).ToList();
                var eligibleFriends = friends.Where(f => !existingMemberIds.Contains(f.GetUserId())).ToList();
                FilteredFriends = new ObservableCollection<User>(eligibleFriends);
            }
        }

        public ObservableCollection<User> FilteredFriends
        {
            get { return filteredFriends; }
            set
            {
                if (filteredFriends != value)
                {
                    filteredFriends = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<User> SelectedFriends
        {
            get { return selectedFriends; }
            set
            {
                if (selectedFriends != value)
                {
                    selectedFriends = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<User> ExistingMembers
        {
            get { return existingMembers; }
            set
            {
                if (existingMembers != value)
                {
                    existingMembers = value;
                    OnPropertyChanged();
                }
            }
        }

        public User SelectedFriend
        {
            get { return selectedFriend; }
            set
            {
                if (selectedFriend != value)
                {
                    selectedFriend = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SearchQuery
        {
            get { return searchQuery; }
            set
            {
                if (searchQuery != value)
                {
                    searchQuery = value;
                    OnPropertyChanged();
                    UpdateFilteredFriends();
                }
            }
        }

        private void UpdateFilteredFriends()
        {
            var friends = chatService.getFriends(currentUserId);
            if (friends != null)
            {
                var existingMemberIds = ExistingMembers.Select(m => m.GetUserId()).ToList();
                var selectedFriendIds = SelectedFriends.Select(f => f.GetUserId()).ToList();

                var filteredFriends = friends
                    .Where(f => !existingMemberIds.Contains(f.GetUserId()) &&
                               !selectedFriendIds.Contains(f.GetUserId()) &&
                               (string.IsNullOrEmpty(searchQuery) ||
                                f.GetUsername().ToLower().Contains(searchQuery.ToLower())))
                    .ToList();

                FilteredFriends = new ObservableCollection<User>(filteredFriends);
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
                UpdateFilteredFriends();
            }
        }

        private async void AddMembers()
        {
            if (SelectedFriends.Count > 0)
            {
                bool success = true;

                foreach (var friend in SelectedFriends)
                {
                    try
                    {
                        chatService.AddUserToChat(friend.GetUserId(), chatId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error adding user {friend.GetUserId()} to chat: {ex.Message}");
                        success = false;
                    }
                }

                // Show result dialog
                var resultDialog = new ContentDialog
                {
                    Title = success ? "Success" : "Error",
                    Content = success ? "Members added successfully!" : "Failed to add some members.",
                    CloseButtonText = "OK",
                    //XamlRoot = App.MainWindow.Content.XamlRoot
                };

                await resultDialog.ShowAsync();

                // Update the lists
                if (success)
                {
                    ExistingMembers = new ObservableCollection<User>(
                        ExistingMembers.Concat(SelectedFriends));
                    chatService.NotifyChatsUpdated();
                }

                SelectedFriends.Clear();
                UpdateFilteredFriends();
            }
        }

        public ICommand AddFriendCommand { get; private set; }
        public ICommand RemoveFriendCommand { get; private set; }
        public ICommand AddMembersCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
