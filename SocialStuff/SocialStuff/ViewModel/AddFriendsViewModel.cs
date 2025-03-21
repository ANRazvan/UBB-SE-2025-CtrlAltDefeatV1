using SocialStuff.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SocialStuff.Model;

namespace SocialStuff.ViewModel
{
    public class AddFriendsViewModel : INotifyPropertyChanged
    {
        private UserService userService;
        private ObservableCollection<User> users;
        private User selectedFriend;
        private string searchQuery;

        public AddFriendsViewModel(UserService userService)
        {
            this.userService = userService;
            // get the users from getNonFriendsUsers from userService
            Users = new ObservableCollection<User>(userService.GetNonFriendsUsers(userService.GetCurrentUser()));

            searchQuery = "";
        }

        public ObservableCollection<User> Users
        {
            get { return users; }
            set
            {
                if (users != value)
                {
                    users = value;
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

        public void LoadUsers()
        {
            Users = new ObservableCollection<User>(userService.GetNonFriendsUsers(userService.GetCurrentUser()));
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
            var filteredFriendIds = userService.FilterFriends(searchQuery, userService.GetCurrentUser());
            var filteredFriends = userService.GetRepo().GetUsersList().Where(user => filteredFriendIds.Contains(user.GetUserId()));
            Users = new ObservableCollection<User>(filteredFriends);
        }

        public ICommand AddFriendCommand
        {
            get
            {
                return new RelayCommand(param =>
                {
                    if (SelectedFriend != null)
                    {
                        userService.AddFriend(userService.GetCurrentUser(), SelectedFriend.GetUserId());
                        Users.Remove(SelectedFriend); // Add the friend from the collection
                    }
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}


