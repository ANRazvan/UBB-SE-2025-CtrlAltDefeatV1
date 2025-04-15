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
using SocialStuff.Model;
using SocialStuff.Services.Implementations;
using SocialStuff.Services.Interfaces;

namespace SocialStuff.View
{
    public sealed partial class ChatMessagesView : Page
    {
        public int SelectedChat { get; set; }
        private ChatMessagesViewModel chatMessagesViewModel;
        private Frame RightFrame;
        private IUserService userService;
        private IChatService chatService;
        private IReportService reportService;
        private ChatListViewModel chatListViewModel;
        private GenerateTransferViewModel GenerateTransferViewModel;

        public ChatMessagesView(ChatListViewModel chatListViewModel, Window mainWindow, Frame RightFrame, int ChatID, IUserService userService, IChatService chatService, IMessageService messageService, IReportService reportService)
        {
            this.InitializeComponent();
            SelectedChat = ChatID;
            this.chatListViewModel = chatListViewModel;
            this.userService = userService;
            this.chatService = chatService;
            this.reportService = reportService;
            this.RightFrame = RightFrame;
            chatMessagesViewModel = new ChatMessagesViewModel(mainWindow,RightFrame, ChatID, messageService, chatService, userService, reportService);
            GenerateTransferViewModel = new GenerateTransferViewModel(chatService, ChatID);
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

        public void SendTransfer_Click(object sender, RoutedEventArgs e)
        {
            this.RightFrame.Content = new GenerateTransferView(GenerateTransferViewModel, this, this.RightFrame, SelectedChat, chatService);
        }

    }
}