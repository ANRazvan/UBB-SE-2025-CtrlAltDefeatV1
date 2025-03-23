using Microsoft.UI.Xaml.Controls;
using SocialStuff.Model;
using SocialStuff.Services;
using SocialStuff.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SocialStuff.ViewModel
{
    public class AddNewMemberViewModel : INotifyPropertyChanged
    {
        private List<User> allUnaddedFriends;
        public ObservableCollection<User> UnaddedFriends { get; set; }
        public ObservableCollection<User> CurrentChatMembers { get; set; }
        public ObservableCollection<User> NewlyAddedFriends { get; set; }
        private UserService userService;
        private ChatService chatService;
        private Page lastChat;
        private string searchQuery;
        private int ChatID;
        private ChatMessagesViewModel chatMessagesViewModel;
        public string ChatName { get; set; }
        public ICommand AddToSelectedCommand { get; set; }
        public ICommand RemoveFromSelectedCommand { get; set; }
        public ICommand AddUsersToChatCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AddNewMemberViewModel(ChatMessagesViewModel chatMessagesViewModel, Page lastChat, int ChatID, ChatService chat, UserService user)
        {
            this.chatMessagesViewModel = chatMessagesViewModel;
            this.ChatID = ChatID;
            this.lastChat = lastChat;
            this.userService = user;
            this.chatService = chat;
            ChatName = this.chatService.getChatNameByID(ChatID);

            this.UnaddedFriends = new ObservableCollection<User>();
            this.CurrentChatMembers = new ObservableCollection<User>();
            this.NewlyAddedFriends = new ObservableCollection<User>();

            AddToSelectedCommand = new RelayCommand<User>(AddToSelected);
            RemoveFromSelectedCommand = new RelayCommand<User>(RemoveFromSelected);
            AddUsersToChatCommand = new RelayCommand(AddUsersToChat);

            UpdateObservableLists();
        }

        public void AddUsersToChat()
        {
            foreach(User user in this.NewlyAddedFriends)
            {
                this.chatService.AddUserToChat(user.GetUserId(), this.ChatID);
            }
            this.NewlyAddedFriends.Clear();
            this.UpdateObservableLists();

            chatMessagesViewModel.CurrentChatParticipants = chatService.getChatParticipantsStringList(ChatID);
        }

        public void AddToSelected(User user)
        {
            this.NewlyAddedFriends.Add(user);
            this.UnaddedFriends.Remove(user);
        }

        public void RemoveFromSelected(User user)
        {
            this.NewlyAddedFriends.Remove(user);
            this.UnaddedFriends.Add(user);
        }

        public string SearchQuery
        {
            get { return searchQuery; }
            set
            {
                if (searchQuery != value)
                {
                    searchQuery = value;
                    OnPropertyChanged(nameof(SearchQuery));
                    UpdateFilteredFriends();
                }
            }
        }

        public void LoadAllUnaddedFriendsList()
        {
            var allFriends = this.userService.GetFriendsByUser(this.userService.GetCurrentUser());
            var currentChatParticipants = this.chatService.getChatParticipantsList(ChatID);
            this.allUnaddedFriends = allFriends.Where(friend => !currentChatParticipants.Any(participant => participant.GetUserId() == friend.GetUserId())).ToList();
        }

        public void UpdateObservableLists()
        {
            LoadAllUnaddedFriendsList();

            this.CurrentChatMembers.Clear();
            foreach (var participant in this.chatService.getChatParticipantsList(ChatID))
            {
                this.CurrentChatMembers.Add(participant);
            }

            UpdateFilteredFriends();
        }

        public void UpdateFilteredFriends()
        {
            UnaddedFriends.Clear();
            foreach (var friend in allUnaddedFriends.Where(f =>
                 string.IsNullOrEmpty(SearchQuery) ||
                 f.GetUsername()?.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) == true ||
                 f.GetPhoneNumber()?.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) == true))
            {
                UnaddedFriends.Add(friend);
            }
        }
    }
}
