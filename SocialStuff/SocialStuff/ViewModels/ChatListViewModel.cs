using System.Collections.ObjectModel;
using SocialStuff.Model;
using SocialStuff.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SocialStuff.ViewModels
{
    public class ChatListViewModel : ObservableObject
    {
        private readonly ChatService _chatService;

        public ObservableCollection<Chat> Chats => _chatService.Chats;

        private string _noChatsMessage = "You have no chats!";
        public string NoChatsMessage
        {
            get => _noChatsMessage;
            set => SetProperty(ref _noChatsMessage, value);
        }

        public ChatListViewModel(ChatService chatService)
        {
            _chatService = chatService;
            _chatService.ChatsUpdated += UpdateNoChatsMessage; 
            LoadChats();
        }

        public void LoadChats()
        {
            int userId = UserService.Instance.LoggedInUserId;
            _chatService.loadChats(userId);
            UpdateNoChatsMessage();
        }

        private void UpdateChats()
        {
            OnPropertyChanged(nameof(Chats));
            NoChatsMessage = Chats.Count == 0 ? "You have no chats!" : "";
        }

        private void UpdateNoChatsMessage()
        {
            NoChatsMessage = Chats.Count == 0 ? "You have no chats!" : "";
        }

        ~ChatListViewModel()
        {
            _chatService.ChatsUpdated -= UpdateNoChatsMessage; 
        }
    }
}
