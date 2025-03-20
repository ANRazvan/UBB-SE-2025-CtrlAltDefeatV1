using SocialStuff.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using SocialStuff.Model;
using Windows.System.UserProfile;

namespace SocialStuff.ViewModel
{
    public class FriendsListViewModel : INotifyPropertyChanged
    {
        private UserService userService;
        private ObservableCollection<User> friends;
        private User selectedFriend;
        private string searchQuery;

        public FriendsListViewModel(UserService userService)
        {
            this.userService = userService;
            friends = new ObservableCollection<User>(userService.GetRepo().GetUserFriendsList(userService.GetCurrentUser()));
            searchQuery = "";
        }
        public ObservableCollection<User> Friends
        {
            get { return friends; }
            set
            {
                friends = value;
                OnPropertyChanged();
            }
        }
        public User SelectedFriend
        {
            get { return selectedFriend; }
            set
            {
                selectedFriend = value;
                OnPropertyChanged();
            }
        }
        public string SearchQuery
        {
            get { return searchQuery; }
            set
            {
                searchQuery = value;
                OnPropertyChanged();
                Friends = new ObservableCollection<User>(userService.GetRepo().GetUserFriendsList(userService.GetCurrentUser()).Where(f => f.GetUsername().Contains(searchQuery)));
            }
        }
        //public ICommand AddFriendCommand
        //{
        //    get
        //    {
        //        return new RelayCommand(param =>
        //        {
        //            if (SelectedFriend != null)
        //            {
        //                userService.AddFriend(userService.GetCurrentUser(), SelectedFriend.ID);
        //                Friends = new ObservableCollection<User>(userService.GetRepo().GetFriends(userService.GetCurrentUser()));
        //            }
        //        });
        //    }
        //}
        public ICommand RemoveFriendCommand
        {
            get
            {
                return new RelayCommand(param =>
                {
                    if (SelectedFriend != null)
                    {
                        userService.RemoveFriend(userService.GetCurrentUser(), SelectedFriend.GetUserId());
                        Friends = new ObservableCollection<User>(userService.GetRepo().GetUserFriendsList(userService.GetCurrentUser()));
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
