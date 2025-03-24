using Microsoft.UI.Xaml.Controls;
using SocialStuff.ViewModel;
using SocialStuff.Services;
using SocialStuff.Data;

namespace SocialStuff.View
{
    public sealed partial class FeedView : Page
    {
        public FeedView()
        {
            this.InitializeComponent();
            var repository = new Repository();
            var notificationService = new NotificationService(repository);
            var userService = new UserService(repository, notificationService);
            var feedService = new FeedService(repository, userService);
            this.DataContext = new FeedViewModel(feedService);
        }
    }
}
