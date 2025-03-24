using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using SocialStuff.Model;
using SocialStuff.Services;
using System.Collections.Generic; 
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SocialStuff.ViewModel
{
    public class NotificationViewModel : INotifyPropertyChanged
    {
        private readonly NotificationService notificationService;
        private readonly int currentUserID;
        private ObservableCollection<Notification> notifications;

        public ObservableCollection<Notification> Notifications
        {
            get { return notifications; }
            set
            {
                notifications = value;
                OnPropertyChanged(nameof(Notifications));
            }
        }

        public ICommand ClearNotificationCommand { get; }
        public ICommand ClearAllNotificationsCommand { get; }  

        public NotificationViewModel()
        {
            //  constructor 
        }

        public NotificationViewModel(NotificationService service, int userID)
        {
            notificationService = service;
            currentUserID = userID;
            Notifications = new ObservableCollection<Notification>();
            ClearNotificationCommand = new RelayCommand<int>(ClearNotification);
            ClearAllNotificationsCommand = new RelayCommand(ClearAllNotifications);  
            LoadNotifications();
        }

        public void LoadNotifications()
        {
            var notificationsList = notificationService.GetNotifications(currentUserID);
            Notifications = new ObservableCollection<Notification>(notificationsList);
        }

        public void ClearNotification(int notificationID)
        {
            notificationService.ClearNotification(notificationID);
            LoadNotifications();
        }

        public void ClearAllNotifications()
        {
            notificationService.ClearAllNotifications(currentUserID);
            LoadNotifications();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
