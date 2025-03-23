using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using SocialStuff.Data.Database;
using SocialStuff.Data;
using Windows.Foundation;
using Windows.Foundation.Collections;
using SocialStuff.View;
using SocialStuff.Services;
using SocialStuff.ViewModel;
using SocialStuff.Model;

namespace SocialStuff
{
    public sealed partial class MainWindow : Window
    {
        private Window mainWindow;
        private UserService userService;
        private ChatService chatService;
        private MessageService messageService;

        public MainWindow()
        {
            this.InitializeComponent();

            mainWindow = this;
            Repository repo = new Repository();
            userService = new UserService(repo);
            chatService = new ChatService(repo);
            messageService = new MessageService(repo);
        }

        private void Chat_Click(object sender, RoutedEventArgs e)
        {
            if (LeftFrame.Content == null || !(LeftFrame.Content is ChatListView))
            {
                var chatListView = new ChatListView(this, chatService, userService, messageService, this.RightFrame);
                LeftFrame.Content = chatListView;
            }
        }
    }

    public class NavigationParameters
    {
        public int ChatID { get; set; }
        public UserService UserService { get; set; }
        public ChatService ChatService { get; set; }
        public MessageService MessageService { get; set; }
    }
}
