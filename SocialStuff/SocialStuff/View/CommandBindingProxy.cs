using Microsoft.UI.Xaml;
using System.Windows.Input;

namespace SocialStuff.View
{
    public class CommandBindingProxy : DependencyObject
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(CommandBindingProxy), new PropertyMetadata(null));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
    }
}