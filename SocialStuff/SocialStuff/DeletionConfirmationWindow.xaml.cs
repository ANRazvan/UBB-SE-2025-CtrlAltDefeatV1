using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Threading.Tasks;

namespace SocialStuff.Views
{
    public sealed partial class DeletionConfirmationWindow : Window
    {
        private TaskCompletionSource<bool> _tcs;

        public DeletionConfirmationWindow()
        {
            this.InitializeComponent();
        }

        public Task<bool> ShowAsync()
        {
            _tcs = new TaskCompletionSource<bool>();
            this.Activate();
            return _tcs.Task;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            _tcs.SetResult(true);
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _tcs.SetResult(false);
            this.Close();
        }
    }
}
