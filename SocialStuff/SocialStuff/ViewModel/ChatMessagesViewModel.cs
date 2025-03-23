using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SocialStuff.Model.MessageClasses;
using MvvmHelpers;
using SocialStuff.Services;
using Windows.ApplicationModel.Chat;
using SocialStuff.Data;
using System.ComponentModel;
using Microsoft.UI.Xaml;
using SocialStuff.View;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Storage;
using WinRT.Interop;
using Windows.Storage.Pickers;

namespace SocialStuff.ViewModel
{
    public class ChatMessagesViewModel : INotifyPropertyChanged
    {
        private readonly Window _window;
        public ObservableCollection<Message> ChatMessages { get; set; }
        public ListView ChatListView { get; set; }
        public MessageService messageService;
        public ChatService chatService;
        public UserService userService;
        private MessageTemplateSelector templateSelector;
        public int CurrentChatID { get; set; }
        public int CurrentUserID { get; set; }
        public string CurrentChatName { get; set; }
        public List<string> CurrentChatParticipants { get; set; }
        public string CurrentChatParticipantsString => string.Join(", ", CurrentChatParticipants);



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string messageContent;
        public string MessageContent
        {
            get => messageContent;
            set
            {
                if (messageContent != value)
                {
                    messageContent = value;
                    OnPropertyChanged(nameof(MessageContent));
                    OnPropertyChanged(nameof(RemainingCharacterCount));
                }
            }
        }
        public int RemainingCharacterCount => 256 - (MessageContent?.Length ?? 0);

        public ICommand SendMessageCommand { get; }
        private void SendMessage()
        {
            this.messageService.sendMessage(CurrentUserID, CurrentChatID, MessageContent);
            this.LoadMessagesForChat();
            MessageContent = "";
        }

        public ICommand SendImageCommand { get; }
        private async void SendImage()
        {
            var picker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };

            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            var hwnd = WindowNative.GetWindowHandle(_window);
            InitializeWithWindow.Initialize(picker, hwnd);

            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                string imageUrl = await ImgurImageUploader.UploadImageAndGetUrl(file);
                this.messageService.sendImage(CurrentUserID, CurrentChatID, imageUrl);
                this.LoadMessagesForChat();
            }
        }

        public void ScrollToBottom()
        {
            if (ChatListView != null)
            {
                ChatListView.DispatcherQueue.TryEnqueue(() =>
                {
                    var scrollViewer = FindVisualChild<ScrollViewer>(ChatListView);
                    scrollViewer?.ChangeView(null, scrollViewer.ScrollableHeight, null);
                });
            }
        }
        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T typedChild)
                    return typedChild;

                T childOfChild = FindVisualChild<T>(child);
                if (childOfChild != null)
                    return childOfChild;
            }
            return null;
        }

        public ChatMessagesViewModel(Window window, int currentChatID, MessageService msgService, ChatService chtService, UserService usrService)
        {
            _window = window;
            ChatMessages = new ObservableCollection<Message>();
            messageService = msgService;
            chatService = chtService;
            userService = usrService;
            this.CurrentChatID = currentChatID;
            this.CurrentUserID = userService.GetCurrentUser();
            this.SendMessageCommand = new RelayCommand(SendMessage);
            this.SendImageCommand = new RelayCommand(SendImage);
            this.CurrentChatName = chatService.getChatNameByID(CurrentChatID);
            this.CurrentChatParticipants = chatService.getChatParticipantsList(CurrentChatID);

            templateSelector = new MessageTemplateSelector()
            {
                TextMessageTemplateLeft = (DataTemplate)App.Current.Resources["TextMessageTemplateLeft"],
                TextMessageTemplateRight = (DataTemplate)App.Current.Resources["TextMessageTemplateRight"],
                ImageMessageTemplateLeft = (DataTemplate)App.Current.Resources["ImageMessageTemplateLeft"],
                ImageMessageTemplateRight = (DataTemplate)App.Current.Resources["ImageMessageTemplateRight"],
                TransferMessageTemplateLeft = (DataTemplate)App.Current.Resources["TransferMessageTemplateLeft"],
                TransferMessageTemplateRight = (DataTemplate)App.Current.Resources["TransferMessageTemplateRight"],
                RequestMessageTemplateLeft = (DataTemplate)App.Current.Resources["RequestMessageTemplateLeft"],
                RequestMessageTemplateRight = (DataTemplate)App.Current.Resources["RequestMessageTemplateRight"]
            };

            this.LoadMessagesForChat();
        }

        private void LoadMessagesForChat()
        {
            ChatMessages.Clear();
            var messages = this.chatService.getChatHistory(this.CurrentChatID);

            foreach (var message in messages)
            {
                if (message is TextMessage textMessage)
                {
                    TextMessage newTextMessage = new TextMessage(textMessage.getMessageID(), textMessage.getSenderID(), textMessage.getChatID(), textMessage.getTimestamp(), textMessage.getContent(), textMessage.getUsersReport());
                    newTextMessage.SenderUsername = this.userService.GetUserById(textMessage.getSenderID()).GetUsername();
                    ChatMessages.Add(newTextMessage);
                }

                if (message is ImageMessage imageMessage)
                {
                    ImageMessage newImageMessage = new ImageMessage(imageMessage.getMessageID(), imageMessage.getSenderID(), imageMessage.getChatID(), imageMessage.getTimestamp(), imageMessage.getImageURL(), imageMessage.getUsersReport());
                    newImageMessage.SenderUsername = this.userService.GetUserById(imageMessage.getSenderID()).GetUsername();
                    ChatMessages.Add(newImageMessage);
                }

                if (message is TransferMessage transferMessage)
                {
                    TransferMessage newTransferMessage = new TransferMessage(transferMessage.getMessageID(), transferMessage.getSenderID(), transferMessage.getChatID(), transferMessage.getStatus(), transferMessage.getAmount(), transferMessage.getDescription(), transferMessage.getCurrency());
                    newTransferMessage.SenderUsername = this.userService.GetUserById(transferMessage.getSenderID()).GetUsername();
                    ChatMessages.Add(newTransferMessage);
                }

                if (message is RequestMessage requestMessage)
                {
                    RequestMessage newRequestMessage = new RequestMessage(requestMessage.getMessageID(), requestMessage.getSenderID(), requestMessage.getChatID(), requestMessage.getStatus(), requestMessage.getAmount(), requestMessage.getDescription(), requestMessage.getCurrency());
                    newRequestMessage.SenderUsername = this.userService.GetUserById(requestMessage.getSenderID()).GetUsername();
                    ChatMessages.Add(newRequestMessage);
                    
                }
            }

            ScrollToBottom();
        }

        public void SetupMessageTracking()
        {
            ChatMessages.CollectionChanged += (sender, e) =>
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add ||
                    e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
                {
                    ScrollToBottom();
                }
            };
        }

    }

}
