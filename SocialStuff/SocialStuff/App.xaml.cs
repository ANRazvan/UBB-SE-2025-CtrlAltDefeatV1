using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
<<<<<<< HEAD
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;

//using SocialStuff.Repository;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.
=======
using SocialStuff.Data;
using SocialStuff.Services;
using SocialStuff.Model;
using SocialStuff.Views;
>>>>>>> origin/Create-Chat;Leave-Chat

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
