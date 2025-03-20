using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using SocialStuff.Data;
using SocialStuff.Services;
using SocialStuff.Model;

namespace SocialStuff
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        public static Window MainWindow { get; private set; }

        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            ChatService chatService = new ChatService();

            // Retrieve friends
            List<User> users = chatService.getFriends(2);
            foreach (var user in users)
            {
                System.Diagnostics.Debug.WriteLine(user.GetUsername());
            }

            // Add chat
            List<Chat> chats = chatService.getChats();
            chatService.addChat(new List<int> { 1, 2 }, "New Name");

            System.Diagnostics.Debug.WriteLine(chats.Count == chatService.getChats().Count);

            MainWindow = new MainWindow();
            MainWindow.Activate();
        }
    }
}
