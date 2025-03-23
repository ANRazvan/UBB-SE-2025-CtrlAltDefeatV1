using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialStuff.View
{
    public class EmoticonToEmojiConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string message)
            {
                return EmoticonConverter.ConvertEmoticonsToEmojis(message);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // Usually, you don't need to convert back from emoji to emoticon
            return value;
        }
    }
}

