using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using SocialStuff.Data;
using SocialStuff.Services;
using SocialStuff.Model;
using SocialStuff.Views;

namespace SocialStuff
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        public static Window MainWindow { get; private set; }
        public static int LoggedInUserID = 1;

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
            LoggedInUserID = 1;

            MainWindow = new MainWindow();
            MainWindow.Activate();
        }
    }
}
