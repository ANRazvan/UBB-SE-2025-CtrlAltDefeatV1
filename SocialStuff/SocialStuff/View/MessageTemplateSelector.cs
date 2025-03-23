using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using SocialStuff.Model.MessageClasses;
using SocialStuff.Services;
using System.Runtime.CompilerServices;
using SocialStuff.Data;
using System.Windows.Input;

namespace SocialStuff.View
{
    public class MessageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TextMessageTemplateLeft { get; set; }
        public DataTemplate TextMessageTemplateRight { get; set; }
        public DataTemplate ImageMessageTemplateLeft { get; set; }
        public DataTemplate ImageMessageTemplateRight { get; set; }
        public DataTemplate TransferMessageTemplateLeft { get; set; }
        public DataTemplate TransferMessageTemplateRight { get; set; }
        public DataTemplate RequestMessageTemplateLeft { get; set; }
        public DataTemplate RequestMessageTemplateRight { get; set; }

        public int CurrentUserID { get; set; }

        public MessageTemplateSelector()
        {
            Repository repo = new Repository();      // THIS MIGHT EXPLODE IF REPOSITORY USERID IS NOT STATIC, ILL FIGURE IT OUT
            this.CurrentUserID = repo.GetLoggedInUserID();
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is Message message)
            {
                switch (message)
                {
                    case TextMessage _:
                        return message.getSenderID() == this.CurrentUserID ? TextMessageTemplateRight : TextMessageTemplateLeft;

                    case ImageMessage _:
                        return message.getSenderID() == this.CurrentUserID ? ImageMessageTemplateRight : ImageMessageTemplateLeft;

                    case TransferMessage _:
                        return message.getSenderID() == this.CurrentUserID ? TransferMessageTemplateRight : TransferMessageTemplateLeft;

                    case RequestMessage _:
                        return message.getSenderID() == this.CurrentUserID ? RequestMessageTemplateRight : RequestMessageTemplateLeft;
                }
            }

            return TextMessageTemplateLeft;
        }
    }
}
