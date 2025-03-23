using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using SocialStuff.Services;
using SocialStuff.ViewModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SocialStuff.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FriendsListView : Page
    {
        private FriendsListViewModel friendsListViewModel;
        private ChatService chatService;
        private UserService userService;
        private MessageService messageService;
        private Frame RightFrame;
        private Page AddFriendsPage;

        public FriendsListView(ChatService chatService, UserService userService, MessageService messageService, Frame RightFrame)
        {
            this.InitializeComponent();
            this.chatService = chatService;
            this.userService = userService;
            this.messageService = messageService;
            this.RightFrame = RightFrame;
            this.friendsListViewModel = new FriendsListViewModel(chatService,userService,messageService);

            this.DataContext = friendsListViewModel;
        }

        private void AddFriend_Click(object sender, RoutedEventArgs e)
        {
            this.RightFrame.Content = new AddFriendsView(friendsListViewModel, userService);
        }

    }
}
