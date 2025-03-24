using Microsoft.UI.Xaml.Controls;
using SocialStuff.ViewModel;
using SocialStuff.Services;
using SocialStuff.Data;
using Microsoft.UI.Xaml;

namespace SocialStuff.View
{
    public sealed partial class NotificationView : Page
    {
        private NotificationViewModel viewModel;
        
        public NotificationView()
        { 
            this.InitializeComponent();
            var repo = new Repository();
            var notificationService = new NotificationService(repo);
            var userService = new UserService(repo, notificationService);
            int currentUserID= repo.GetLoggedInUserID();
            this.DataContext = new NotificationViewModel(notificationService, currentUserID);
        }
    }
}