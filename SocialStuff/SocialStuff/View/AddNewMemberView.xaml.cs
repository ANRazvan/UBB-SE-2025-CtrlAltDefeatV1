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
using SocialStuff.Data;
using SocialStuff.Services;
using SocialStuff.ViewModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SocialStuff.View
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddNewMemberView : Window
    {
        public AddNewMemberViewModel viewModel { get; private set; }
        public AddNewMemberView()
        {
            this.InitializeComponent();

            ChatService chatService = new ChatService();
            viewModel = new AddNewMemberViewModel(chatService, 1, 8);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // UNCOMMENT THIS CODE THAT WORKS WHEN CHANGING FROM WINDOW TO PAGE

            // Navigate back to the previous page
            //if (this.Frame.CanGoBack)
            //{
            //    this.Frame.GoBack();
            //}

            this.Close();
        }
    }
}
