using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using SocialStuff.Model;
using SocialStuff.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SocialStuff.ViewModels
{
    public class ChatListViewModel : ObservableObject
    {
        private readonly ChatService _chatService;

        public ObservableCollection<Chat> Chats { get; set; } = new ObservableCollection<Chat>();
        public string NoChatsMessage { get; set; } = "You have no chats!";

        public ChatListViewModel()
        {
            _chatService = new ChatService();
            LoadChats();
        }

        public void LoadChats()
        {
            // Access the logged-in user ID from the central service
            int userId = UserService.Instance.LoggedInUserId;

            // Fetch chats for the logged-in user
            var chats = _chatService.getChats(userId);

            // Clear existing chats and add the new ones
            Chats.Clear();

            if (chats.Count == 0)
            {
                // If no chats, show the message
                NoChatsMessage = "You have no chats!";
            }
            else
            {
                // Otherwise, update the Chats collection
                foreach (var chat in chats)
                {
                    Chats.Add(chat);
                }
                NoChatsMessage = ""; // Clear the message if chats exist
            }
        }
    }
}
