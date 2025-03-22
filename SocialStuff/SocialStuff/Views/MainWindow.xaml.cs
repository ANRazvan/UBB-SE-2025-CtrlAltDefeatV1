using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SocialStuff.ViewModels;
using SocialStuff.Views;
using SocialStuff.Services; // ? Added for ChatService

namespace SocialStuff.Views
{
    public sealed partial class MainWindow : Window
    {
        private readonly ChatService _chatService = new ChatService(); 

        public MainWindow()
        {
            this.InitializeComponent();

            var ViewModel = new ChatListViewModel(_chatService); 
            ChatList.DataContext = ViewModel; 
        }

        private void Chat_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = ChatList.DataContext as ChatListViewModel;
            viewModel?.LoadChats();

            ChatList.Focus(FocusState.Programmatic);
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
            // ? Pass ChatService to CreateChatView so it can update the chat list
            MainContent.Navigate(typeof(CreateChatView), _chatService);
        }
    }
}
