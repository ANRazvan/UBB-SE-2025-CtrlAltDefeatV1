using SocialStuff.Model;
using SocialStuff.Data;
using System.Collections.Generic;

using NotificationModel = SocialStuff.Model.Notification;
using WindowsNotification = Windows.UI.Notifications.Notification;

namespace SocialStuff.Services
{
    public class NotificationService
    {
        private readonly Repository repo;

        public NotificationService(Repository repository)
        {
            repo = repository;
        }

        public List<NotificationModel> GetNotifications(int userID)
        {
            return repo.GetNotifications(userID);
        }

        public void SendFriendNotification(int userID, int newFriendID)
        {
            var user = repo.GetUserById(userID);
            var newFriend = repo.GetUserById(newFriendID);

            if (user != null && newFriend != null)
            {
                string userContent = $"You added user {newFriend.GetUsername()} ({newFriend.GetPhoneNumber()}) to your friend list.";
                string friendContent = $"User {user.GetUsername()} ({user.GetPhoneNumber()}) added you as a friend.";
                repo.AddNotification(userContent, userID);
                repo.AddNotification(friendContent, newFriendID);
            }
        }

        public void SendRemoveFriendNotification(int userID, int oldFriendID)
        {
            var user = repo.GetUserById(userID);
            var oldFriend = repo.GetUserById(oldFriendID);

            if (user != null && oldFriend != null)
            {
                string userContent = $"You removed user {oldFriend.GetUsername()} ({oldFriend.GetPhoneNumber()}) from your friend list.";
                string friendContent = $"User {user.GetUsername()} ({user.GetPhoneNumber()}) deleted you from their friend list. So selfish!";
                repo.AddNotification(userContent, userID);
                repo.AddNotification(friendContent, oldFriendID);
            }
        }

        public void SendMessageNotification(int messageSenderID, int chatID)
        {
            var sender = repo.GetUserById(messageSenderID);
            var chat = repo.GetChatById(chatID);
            var participants = repo.GetChatParticipants(chatID);

            if (sender != null && chat != null)
            {
                string content = $"User {sender.GetUsername()} sent a message in chat {chat.getChatName()}.";
                foreach (var participant in participants)
                {
                    if (participant.GetUserId() != messageSenderID)
                    {
                        repo.AddNotification(content, participant.GetUserId());
                    }
                }
            }
        }

        public void SendTransactionNotification(int receiverID, int chatID, string type, float amount, string currency)
        {
            var moneyReceiver = repo.GetUserById(receiverID);
            var chat = repo.GetChatById(chatID);
            var participants = repo.GetChatParticipants(chatID);

            if (moneyReceiver != null && chat != null)
            {
                string senderContent = $"You requested {amount} {currency} in {chat.getChatName()} group.";
                repo.AddNotification(senderContent, receiverID);

                string participantContent = $"User {moneyReceiver.GetUsername()} requested {amount} {currency} in {chat.getChatName()} group.";
                foreach (var participant in participants)
                {
                    if (participant.GetUserId() != receiverID)
                    {
                        repo.AddNotification(participantContent, participant.GetUserId());
                    }
                }
            }
        }

        public void SendNewChatNotification(int chatID)
        {
            var participants = repo.GetChatParticipants(chatID);
            foreach (var participant in participants)
            {
                string content = $"You have been added to a new chat {chatID}.";
                repo.AddNotification(content, participant.GetUserId());
            }
        }

        public void ClearNotification(int notificationID)
        {
            repo.DeleteNotification(notificationID);
        }

        public void ClearAllNotifications(int userID)
        {
            repo.ClearAllNotifications(userID);
        }
    }
}


