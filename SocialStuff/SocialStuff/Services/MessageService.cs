using SocialStuff.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialStuff.Services;
using SocialStuff.Model;

namespace SocialStuff.Services
{
    class MessageService
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
            var user = userService.GetUserById(userID);
            return user != null && userService.IsUserInTimeout(user);
        }

        //for all the functionalities below, we checked if the user is in timeout
        public void sendMessage(int SenderID, int ChatID, string Content)
        {
            if (IsUserInTimeout(SenderID)) return; 
            this.repository.AddTextMessage(SenderID, ChatID, Content);
        }

        public void sendImage(int SenderID, int ChatID, string ImageURL)
        {
            if (IsUserInTimeout(SenderID)) return; // Check if user is in timeout
            this.repository.AddImageMessage(SenderID, ChatID, ImageURL);
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