using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;

namespace SocialStuff.View
{
    public class CountToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int count)
            {
                if (parameter?.ToString() == "HasItems")
                    return count > 0 ? Visibility.Visible : Visibility.Collapsed;

                if (parameter?.ToString() == "NoItems")
                    return count == 0 ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
