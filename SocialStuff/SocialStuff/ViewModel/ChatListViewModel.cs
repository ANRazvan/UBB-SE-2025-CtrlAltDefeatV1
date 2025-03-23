using SocialStuff.View;
using SocialStuff.Model;
using SocialStuff.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Chat;

namespace SocialStuff.ViewModel
{
    class ChatListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Chat> ChatList { get; set; }
        public ChatService chatService;
        public UserService userService;
        public CountToVisibilityConverter CountToVisibilityConverter { get; set; }

        public ChatListViewModel(ChatService chatS, UserService userS)
        {
            ChatList = new ObservableCollection<Chat>();
            chatService = chatS;
            userService = userS;
            CountToVisibilityConverter = new CountToVisibilityConverter();

            LoadChats();
        }

        public void LoadChats()
        {
            ChatList.Clear();
            var chat = this.userService.GetCurrentUserChats();

            foreach(var c in chat)
            {
                ChatList.Add(c);
            }
        }
    }
    
}
