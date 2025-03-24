using SocialStuff.Data;
using SocialStuff.Model;
using System;

namespace SocialStuff.Services
{
    public class MessageService
    {
        private Repository repository;
        private UserService userService;

        public MessageService(Repository repo, UserService userService)
        {
            this.repository = repo;
            this.userService = userService;
        }

        public Repository getRepo()
        {
            return this.repository;
        }

        // Check if user is in timeout
        private bool IsUserInTimeout(int userID)
        {
            if (userService == null)
            {
                throw new InvalidOperationException("UserService is not initialized.");
            }

            User user = userService.GetUserById(userID);
            bool isInTimeout = user != null && userService.IsUserInTimeout(user);
            System.Diagnostics.Debug.WriteLine($"MessageService checking if user {user?.GetUsername()} is in timeout: {isInTimeout}");
            return isInTimeout;
        }


        //for all the functionalities below, we checked if the user is in timeout
        public void sendMessage(int SenderID, int ChatID, string Content)
        {
            if (this.IsUserInTimeout(SenderID)) return;
            this.repository.AddTextMessage(SenderID, ChatID, Content);
        }

        public void sendImage(int SenderID, int ChatID, string ImageURL)
        {
            if (this.IsUserInTimeout(SenderID)) return;
            this.repository.AddImageMessage(SenderID, ChatID, ImageURL);
        }

        public void deleteMessage(int MessageID)
        {
            this.repository.DeleteMessage(MessageID);
        }

        public void sendTransferMessage(int userID, int chatID, string content, string status, float amount, string currency)
        {
            if (IsUserInTimeout(userID)) return; // Check if user is in timeout
            this.repository.AddTransferMessage(userID, chatID, content, status, amount, currency);
        }

        public void sendRequestMessage(int userID, int chatID, string content, string status, float amount, string currency)
        {
            if (IsUserInTimeout(userID)) return; // Check if user is in timeout
            this.repository.AddRequestMessage(userID, chatID, content, status, amount, currency);
        }
    }
}
