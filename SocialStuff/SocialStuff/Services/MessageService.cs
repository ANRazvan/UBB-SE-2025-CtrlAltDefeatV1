using SocialStuff.Data;
using SocialStuff.Model.MessageClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialStuff.Services
{
    public class MessageService
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

        public void deleteMessage(Message message)
        {
            this.repository.DeleteMessage(message.getMessageID());
        }

        public void sendImage(int SenderID, int ChatID, string ImageURL)
        {
            this.repository.AddImageMessage(repository.GetLoggedInUserID(), ChatID, ImageURL);
        }

        public void sendTransferMessage(int userID, int chatID, string content, string status, float amount, string currency)
        {
            this.repository.AddTransferMessage(userID, chatID, content, status, amount, currency);
        }

        public void sendRequestMessage(int userID, int chatID, string content, string status, float amount, string currency)
        {
            this.repository.AddRequestMessage(userID, chatID, content, status, amount, currency);
        }

    }
}
