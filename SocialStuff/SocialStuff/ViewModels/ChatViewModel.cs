using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SocialStuff.Services;

namespace SocialStuff.ViewModels
{
    public partial class SelectableFriend : ObservableObject
{
    public int Id { get; set; }
    public string Name { get; set; }

    private bool _isSelected;
    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }
}


    public partial class CreateChatViewModel : ObservableObject
    {
        private readonly ChatService _chatService;

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (_searchQuery != value)
                {
                    _searchQuery = value;
                    OnPropertyChanged(nameof(SearchQuery));
                    FilterFriends();
                }
            }
        }

        public ObservableCollection<SelectableFriend> FilteredFriends { get; set; } = new();

        public ICommand CreateChatCommand { get; }

        public CreateChatViewModel(ChatService chatService)
        {
            _chatService = chatService;
            LoadFriends();
            CreateChatCommand = new RelayCommand(CreateChat);
        }

        private void LoadFriends()
        {
            FilteredFriends.Clear();
            foreach (var (id, name) in _chatService.friendsDict)
            {
                FilteredFriends.Add(new SelectableFriend { Id = id, Name = name, IsSelected = false });
            }
        }

        private void FilterFriends()
        {
            var filtered = _chatService.friendsDict
                .Where(f => f.Value.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase))
                .Select(f => new SelectableFriend { Id = f.Key, Name = f.Value, IsSelected = false });

            FilteredFriends.Clear();
            foreach (var friend in filtered)
            {
                FilteredFriends.Add(friend);
            }
        }

        private void CreateChat()
        {
            var selectedIds = FilteredFriends.Where(f => f.IsSelected).Select(f => f.Id).ToList();
            if (selectedIds.Count() == 0) return;

            bool success = _chatService.addChat(selectedIds, "New Chat");
            if (success)
            {
                // Notify UI that the chat was created
                _chatService.NotifyChatsUpdated();
            }
        }
    }
}
