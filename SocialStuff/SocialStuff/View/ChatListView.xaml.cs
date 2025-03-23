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
using SocialStuff.Model;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SocialStuff.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChatListView : Page
    {
        UserService userService;
        ChatService chatService;
        MessageService messageService;
        Frame RightFrame;
        Window mainWindow;

        public ChatListView(Window mainWindow, ChatService chatService, UserService userService, MessageService messageService, Frame RightFrame)
        {
            this.InitializeComponent();

            this.mainWindow = mainWindow;
            this.userService = userService;
            this.chatService = chatService;
            this.messageService = messageService;
            this.RightFrame = RightFrame;
            ChatListViewModel chatListViewModel = new ChatListViewModel(chatService, userService);
            MainGrid.DataContext = chatListViewModel;
        }

        private void ChatList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ChatList.SelectedItem is Chat selectedChat)
            {
                this.RightFrame.Content = new ChatMessagesView(mainWindow, selectedChat.getChatID(), userService, chatService, messageService);
            }
        }
    }
}
