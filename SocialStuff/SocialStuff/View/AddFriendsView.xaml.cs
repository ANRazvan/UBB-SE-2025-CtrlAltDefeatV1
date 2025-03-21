using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SocialStuff.Services;
using SocialStuff.ViewModel;
using SocialStuff.Data;
using SocialStuff.Model;
using System.Collections.Specialized;

namespace SocialStuff.View
{
    public sealed partial class AddFriendsView : Window
    {
        public AddFriendsViewModel ViewModel { get; private set; }

        public AddFriendsView()
        {
            this.InitializeComponent();

            // Initialize the ViewModel with dependencies
            Repository repo = new Repository();
            UserService userService = new UserService(repo);
            ViewModel = new AddFriendsViewModel(userService);

            // Load initial data
            ViewModel.LoadUsers();

            // Subscribe to the CollectionChanged event
            ViewModel.Users.CollectionChanged += Users_CollectionChanged;

            // Initial check
            UpdateNoUsersTextVisibility();
        }


        private void AddFriendButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.CommandParameter is User user)
            {
                ViewModel.SelectedFriend = user;
                if (ViewModel.AddFriendCommand.CanExecute(null))
                {
                    ViewModel.AddFriendCommand.Execute(null);
                    UpdateNoUsersTextVisibility(); // Update visibility after removing a friend
                }
            }
        }

        private void Users_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateNoUsersTextVisibility();
        }

        private void UpdateNoUsersTextVisibility()
        {
            NoUsersTextBlock.Visibility = ViewModel.Users.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
