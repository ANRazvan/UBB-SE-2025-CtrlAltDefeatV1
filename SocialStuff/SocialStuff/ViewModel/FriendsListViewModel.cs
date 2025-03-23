using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SocialStuff.Model;
using SocialStuff.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SocialStuff.ViewModel
{
    public class FriendsListViewModel : INotifyPropertyChanged
    {
        public List<User> allFriends { get; set; }
        public ObservableCollection<User> FriendsList { get; set; }
        public UserService userService { get; set; }
        public ChatService chatService { get; set; }
        public MessageService messageService { get; set; }

        public ICommand RemoveFriend { get; }

        private Visibility _noFriendsVisibility = Visibility.Collapsed;
        public Visibility NoFriendsVisibility
        {
            get { return _noFriendsVisibility; }
            set
            {
                _noFriendsVisibility = value;
                OnPropertyChanged(nameof(NoFriendsVisibility));
            }
        }

        private string searchQuery;
        public string SearchQuery
        {
            get => searchQuery;
            set
            {
                if (searchQuery != value)
                {
                    searchQuery = value;
                    OnPropertyChanged(nameof(SearchQuery));
                    FilterFriends();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public FriendsListViewModel(ChatService chat, UserService user, MessageService message)
        {
            this.userService = user;
            this.chatService = chat;
            this.messageService = message;
            this.allFriends = this.userService.GetFriendsByUser(this.userService.GetCurrentUser());
            this.FriendsList = new ObservableCollection<User>();
            RemoveFriend = new RelayCommand<object>(RemoveFriendFromList);

            LoadFriends();
        }

        public void RemoveFriendFromList(object user)
        {
            var friend = user as User;
            if(friend != null)
            {
                this.userService.RemoveFriend(this.userService.GetCurrentUser(), friend.GetUserId());
            }
            this.allFriends = this.userService.GetFriendsByUser(this.userService.GetCurrentUser());

            this.LoadFriends();
        }

        public void LoadFriends()
        {
            this.allFriends = this.userService.GetFriendsByUser(this.userService.GetCurrentUser());
            FilterFriends();
        }

        public void FilterFriends()
        {
            FriendsList.Clear();

            foreach (var friend in allFriends.Where(f =>
                         string.IsNullOrEmpty(SearchQuery) ||
                         f.Username.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                         f.PhoneNumber.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)))
            {
                FriendsList.Add(friend);
            }

            UpdateNoFriendsVisibility();
        }

        private void UpdateNoFriendsVisibility()
        {
            NoFriendsVisibility = (FriendsList == null || FriendsList.Count == 0)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }
    }
}
