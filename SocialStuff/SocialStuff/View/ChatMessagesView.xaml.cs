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

        public ChatMessagesView(Window mainWindow, int ChatID, UserService userService, ChatService chatService, MessageService messageService)
        {
            this.InitializeComponent();
            SelectedChat = ChatID;
            chatMessagesViewModel = new ChatMessagesViewModel(mainWindow, ChatID, messageService, chatService, userService);

            chatMessagesViewModel.ChatListView = ChatListView;
            chatMessagesViewModel.SetupMessageTracking();

            MainGrid.DataContext = chatMessagesViewModel;
        }

        private void OnContextMenuButtonClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            FlyoutBase.ShowAttachedFlyout(button);
        }
    }
}
