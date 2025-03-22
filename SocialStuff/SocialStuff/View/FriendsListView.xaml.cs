using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SocialStuff.Services;
using SocialStuff.ViewModel;
using SocialStuff.Data;
using SocialStuff.Model;
using System.Collections.Specialized;

namespace SocialStuff.View
{
    public sealed partial class FriendsListView : Window
    {
        public FriendsListViewModel ViewModel { get; private set; }

        public FriendsListView()
        {
            this.InitializeComponent();

            // Initialize the ViewModel with dependencies
            Repository repo = new Repository();
            UserService userService = new UserService(repo);
            ViewModel = new FriendsListViewModel(userService);

            // Load initial data
            ViewModel.LoadFriends();

            // Subscribe to the CollectionChanged event
            ViewModel.Friends.CollectionChanged += Friends_CollectionChanged;

            // Initial check
            UpdateNoFriendsTextVisibility();
        }

        private void AddFriendButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the AddFriend window
            //AddFriend addFriendView = new AddFriend();
            //addFriendView.Activate();
        }

        private void RemoveFriendButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.CommandParameter is User user)
            {
                ViewModel.SelectedFriend = user;
                if (ViewModel.RemoveFriendCommand.CanExecute(null))
                {
                    ViewModel.RemoveFriendCommand.Execute(null);
                    UpdateNoFriendsTextVisibility(); // Update visibility after removing a friend
                }
            }
        }

        private void Friends_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateNoFriendsTextVisibility();
        }

        private void UpdateNoFriendsTextVisibility()
        {
            NoFriendsTextBlock.Visibility = ViewModel.Friends.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}

