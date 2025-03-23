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
using Microsoft.UI;
using Microsoft.UI.Windowing;
using SocialStuff.ViewModel;
using WinRT.Interop;
using SocialStuff.Data;
using SocialStuff.Services;
using SocialStuff.Model;

namespace SocialStuff.View
{
    public sealed partial class ChatMessagesView : Page
    {
        public int SelectedChat { get; set; }
        private ChatMessagesViewModel chatMessagesViewModel;
        private Frame RightFrame;
        private UserService userService;
        private ChatService chatService;
        private ChatListViewModel chatListViewModel;

        public ChatMessagesView(ChatListViewModel chatListViewModel, Window mainWindow, Frame RightFrame, int ChatID, UserService userService, ChatService chatService, MessageService messageService)
        {
            this.InitializeComponent();
            SelectedChat = ChatID;
            this.chatListViewModel = chatListViewModel;
            this.userService = userService;
            this.chatService = chatService;
            this.RightFrame = RightFrame;
            chatMessagesViewModel = new ChatMessagesViewModel(mainWindow, ChatID, messageService, chatService, userService);

            chatMessagesViewModel.ChatListView = ChatListView;
            chatMessagesViewModel.SetupMessageTracking();

            this.DataContext = chatMessagesViewModel;
        }

        public void AddNewMember_Click(object sender, RoutedEventArgs e)
        {
            this.RightFrame.Content = new AddNewMemberView(chatMessagesViewModel, this, this.RightFrame, SelectedChat, chatService, userService);
        }

        public void LeaveChat_Click(object sender, RoutedEventArgs e)
        {
            this.RightFrame.Content = new LeaveChatView(SelectedChat, this.chatListViewModel, this, RightFrame, chatService, userService);
        }

    }
}
