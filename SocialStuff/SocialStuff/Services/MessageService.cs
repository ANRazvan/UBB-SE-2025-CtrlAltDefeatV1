using SocialStuff.Data;
using SocialStuff.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialStuff.Services
{
    class MessageService
    {
        private Repository repository;

        public MessageService(Repository repo)
        {
            this.repository = repo;
        }

        public Repository getRepo()
        {
            return this.repository;
        }

        public void sendMessage(int SenderID, int ChatID, string Content)
        {
            this.repository.AddTextMessage(SenderID, ChatID, Content);
        }

        public void sendImage(int SenderID, int ChatID, string ImageURL)
        {
            this.repository.AddImageMessage(SenderID, ChatID, ImageURL);
        }

        public void sendTransferMessage(int userID, int chatID, string content, string status, float amount, string currency)
        {
            this.repository.AddTransferMessage(userID, chatID, content, status, amount, currency);
        }

        public void sendRequestMessage(int userID, int chatID, string content, string status, float amount, string currency)
        {
            this.repository.AddRequestMessage(userID, chatID, content, status, amount, currency);
        }

        public async Task DeleteMessageAsync(int messageID)
        {
            var confirmationWindow = new DeletionConfirmationWindow();
            var result = await confirmationWindow.ShowAsync();

            if (result)
            {
                this.repository.DeleteMessage(messageID);
                Console.WriteLine("Message deleted successfully.");
            }
            else
            {
                Console.WriteLine("Message deletion canceled.");
            }
        }


    }
}