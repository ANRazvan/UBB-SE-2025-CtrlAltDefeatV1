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
                if (friends != value)
                {
                    friends = value;
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

        public void LoadFriends()
        {
            Friends = new ObservableCollection<User>(userService.GetRepo().GetUserFriendsList(userService.GetCurrentUser()));
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
            Friends = new ObservableCollection<User>(filteredFriends);
        }

        public ICommand RemoveFriendCommand
        {
            get
            {
                return new RelayCommand(param =>
                {
                    if (SelectedFriend != null)
                    {
                        userService.RemoveFriend(userService.GetCurrentUser(), SelectedFriend.GetUserId());
                        Friends.Remove(SelectedFriend); // Remove the friend from the collection
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




//using SocialStuff.Services;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using System.Windows.Input;
//using SocialStuff.Model;

//namespace SocialStuff.ViewModel
//{
//    public class FriendsListViewModel : INotifyPropertyChanged
//    {
//        private UserService userService;
//        private ObservableCollection<User> friends;
//        private User selectedFriend;
//        private string searchQuery;

//        public FriendsListViewModel(UserService userService)
//        {
//            this.userService = userService;
//            friends = new ObservableCollection<User>(userService.GetRepo().GetUserFriendsList(userService.GetCurrentUser()));
//            searchQuery = "";
//        }

//        public ObservableCollection<User> Friends
//        {
//            get { return friends; }
//            set
//            {
//                if (friends != value)
//                {
//                    friends = value;
//                    OnPropertyChanged();
//                }
//            }
//        }

//        public User SelectedFriend
//        {
//            get { return selectedFriend; }
//            set
//            {
//                if (selectedFriend != value)
//                {
//                    selectedFriend = value;
//                    OnPropertyChanged();
//                }
//            }
//        }

//        public void LoadFriends()
//        {
//            Friends = new ObservableCollection<User>(userService.GetRepo().GetUserFriendsList(userService.GetCurrentUser()));
//        }

//        public string SearchQuery
//        {
//            get { return searchQuery; }
//            set
//            {
//                if (searchQuery != value)
//                {
//                    searchQuery = value;
//                    OnPropertyChanged();
//                    UpdateFilteredFriends();
//                }
//            }
//        }

//        private void UpdateFilteredFriends()
//        {
//            var filteredFriendIds = userService.FilterFriends(searchQuery, userService.GetCurrentUser());
//            var filteredFriends = userService.GetRepo().GetUsersList().Where(user => filteredFriendIds.Contains(user.GetUserId()));
//            Friends = new ObservableCollection<User>(filteredFriends);
//        }

//        public ICommand RemoveFriendCommand
//        {
//            get
//            {
//                return new RelayCommand(param =>
//                {
//                    if (SelectedFriend != null)
//                    {
//                        userService.RemoveFriend(userService.GetCurrentUser(), SelectedFriend.GetUserId());
//                        Friends = new ObservableCollection<User>(userService.GetRepo().GetUserFriendsList(userService.GetCurrentUser()));
//                    }
//                });
//            }
//        }

//        public event PropertyChangedEventHandler PropertyChanged;
//        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }
//}
