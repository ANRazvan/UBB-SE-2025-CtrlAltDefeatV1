using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SocialStuff
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void myButtonCheck()
        {
            myButton.Content = "Connected to DB";

        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            Repository repo = new Repository();
            DatabaseConnection dc = repo.GetDatabaseConnection();
            dc.OpenConnection();
            int res = dc.CheckConnection();
            if (res == 1)
            {
                myButtonCheck();

                ChatListView chatWindow = new ChatListView();
                chatWindow.Activate(); // Show the window in WinUI 3
            }
            dc.CloseConnection();
        }
    }
}
