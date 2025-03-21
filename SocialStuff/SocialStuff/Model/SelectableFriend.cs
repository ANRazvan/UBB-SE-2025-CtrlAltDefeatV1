using CommunityToolkit.Mvvm.ComponentModel;

namespace SocialStuff.Model
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
}
