using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using SocialStuff.ViewModels;

namespace SocialStuff.Views
{
    public sealed partial class CreateChatView : Page
    {
        public CreateChatViewModel ViewModel { get; }

        public CreateChatView()
        {
            this.InitializeComponent();
            ViewModel = new CreateChatViewModel();
            DataContext = ViewModel;
        }
    }
}
