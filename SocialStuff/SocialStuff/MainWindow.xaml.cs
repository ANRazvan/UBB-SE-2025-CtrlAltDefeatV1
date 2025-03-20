using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace SocialStuff
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        // Handle Chat Selection
        private void ChatList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ChatList.SelectedItem != null)
            {
                MainContent.Content = new TextBlock { Text = "Chat View - Under Construction", FontSize = 24 };
            }
        }

        // Navigate to Different Views
        private void Chat_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new TextBlock { Text = "Chat List", FontSize = 24 };
        }

        private void Feed_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new TextBlock { Text = "Feed - Coming Soon!", FontSize = 24 };
        }

        private void Friends_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new TextBlock { Text = "Friends List - Select to Add Friends", FontSize = 24 };
        }

        private void Notifications_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new TextBlock { Text = "Notifications", FontSize = 24 };
        }

        // Open Group Creation Screen
        private void CreateChat_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new TextBlock { Text = "New Group Chat - Add Friends", FontSize = 24 };
        }
    }
}
