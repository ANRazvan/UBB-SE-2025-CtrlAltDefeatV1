using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SocialStuff.ViewModels;
using SocialStuff.Views;

namespace SocialStuff.Views
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            var ViewModel = new ChatListViewModel();  
            ChatList.DataContext = ViewModel;        
            System.Diagnostics.Debug.WriteLine(ChatList.DataContext);
        }

        private void Chat_Click(object sender, RoutedEventArgs e)
        {
             // Navigate to ChatPage
        }

        private void Feed_Click(object sender, RoutedEventArgs e)
        {
            // You can add functionality for FeedPage later
        }

        private void Friends_Click(object sender, RoutedEventArgs e)
        {
            // You can add functionality for FriendsPage later
        }

        private void Notifications_Click(object sender, RoutedEventArgs e)
        {
            // You can add functionality for NotificationsPage later
        }

        private void CreateChat_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Navigate(typeof(CreateChatView));
        }
    }
}
