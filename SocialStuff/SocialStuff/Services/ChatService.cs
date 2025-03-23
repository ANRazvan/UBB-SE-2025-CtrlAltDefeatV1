using SocialStuff.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialStuff.Model.MessageClasses;
using SocialStuff.Model;
using Microsoft.Identity.Client;

namespace SocialStuff.Services
{
    public class ChatService
    {
        private Repository repository;
        public const int chatID = 1;

        //Get current chat ID
        public int getCurrentChatID()
        {
            return chatID;
        }
        //Get logged in user ID
        public int GetCurrentUserID()
        {
            return repository.GetLoggedInUserID();
        }

        //return the number of participants in a chat beside the user
        public int getNumberOfParticipants(int ChatID)
        {
            return repository.GetChatParticipantsIDs(getCurrentChatID()).Count;
        }

        public ChatService(Repository repo)
        {
            this.repository = repo;
        }

        public Repository getRepo()
        {
            return this.repository;
        }




            /// MODIFY ERROR REQUEST HAS 1 INSTANCE WITH REQUESTER ID AND CHAT 1 -: DONE (depends on implementation)



        //Creates a new requestmessage and adds it to the database
        public void requestMoneyViaChat(float Amount, string Currency, int ChatID, string Description)
        {
            if (Amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero.");
            }

            if (string.IsNullOrEmpty(Currency))
            {
                throw new ArgumentException("Currency cannot be null or empty.");
            }

            try
            {
                repository.AddRequestMessage(GetCurrentUserID(), ChatID, Description, "Pending", Amount, Currency);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error requesting money from participant {ChatID}: {ex.Message}");
            }
        }





      

        //creates a new transfermessage and adds it to the database
        public void sendMoneyViaChat(float Amount, string Currency, string Description, int ChatID)
        {
            if (Amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero.");
            }

            if (string.IsNullOrEmpty(Currency))
            {
                throw new ArgumentException("Currency cannot be null or empty.");
            }

            List<int> participantIDs = repository.GetChatParticipantsIDs(ChatID);
            repository.AddTransferMessage(GetCurrentUserID(), ChatID, Description, "Accepted", Amount * (participantIDs.Count - 1), Currency);

            try
            {
                if (this.enoughFunds(Amount*(participantIDs.Count-1), Currency, GetCurrentUserID()))
                {
                    int currentUserId = GetCurrentUserID();
                   
                    foreach (int reciverid in participantIDs)
                    {
                        if (currentUserId != reciverid)
                        {
                            initiateTransfer(currentUserId, reciverid, Amount, Currency);
                        }
                    }

                }
                else
                {
                    repository.AddTransferMessage(GetCurrentUserID(), ChatID, Description, "Rejected", Amount * (participantIDs.Count - 1), Currency);
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error sending money to participant {ChatID}: {ex.Message}");
            }


        }

        //when a reqest is initiated in chat if you press accept a new transfer message is initiated with sender id the one who pressed and 
        //chat id the chat 
        public void acceptRequestViaChat(float amount, string currency, int AccepterID, int RequesterID)
        {
            //from Adrada
            if (this.enoughFunds(amount,currency, AccepterID))
            {
                initiateTransfer(AccepterID, RequesterID, amount, currency);
                repository.AddTransferMessage(AccepterID, getCurrentChatID(), "YOU JUST SENT " + amount.ToString() + currency +
                    "TO here function that retrives username by ID", "Accepted", amount, currency);     
            }
            else
            {
                repository.AddTransferMessage(AccepterID, getCurrentChatID(), "YOU FAILED TO SEND " + amount.ToString() + currency +
                    "TO here function that retrives username by ID", "Rejected", amount, currency);
            }
            
        }
        //mock function for enough funds
        public bool enoughFunds(float amount, string currency, int SenderID)
        {

            Random random = new Random();
            bool randomBool = random.Next(2) == 0; 
            return randomBool;
            
        }
        //mock transfer function
        public void initiateTransfer(int senderID, int reciverID, float amount, string currency)
        {
            return;
        }

        


        public void createChat(List<int> ParticipantsID, string ChatName)
        {

            int chatID;
            repository.AddChat(ChatName, out chatID);

            foreach (var userID in ParticipantsID)
            {
                repository.AddUserToChat(userID, chatID);
            }
            var addedUsers = repository.GetChatParticipants(chatID);

        }

        public void deleteChat(int ChatID)
        {
            this.repository.DeleteChat(ChatID);
        }

        public DateTime getLastMessageTimeStamp(int ChatID)
        {
            List<Message> allMessages = this.repository.GetMessagesList();

            var chatMessages = allMessages.Where(m => m.getChatID() == ChatID).ToList();

            var lastMessage = chatMessages.OrderByDescending(m => m.getTimestamp()).FirstOrDefault();

            if (lastMessage == null)
            {
                throw new Exception("No messages to show in the chat with id: " + ChatID);
            }

            return lastMessage.getTimestamp();
        }

        public List<Message> getChatHistory(int ChatID)
        {
            List<Message> allMessages = this.repository.GetMessagesList();

            List<Message> chatHistory = allMessages.Where(m => m.getChatID() == ChatID).ToList();

            return chatHistory;
        }

        public void AddUserToChat(int UserID, int ChatID)
        {
            this.repository.AddUserToChat(UserID, ChatID);
        }

        public void RemoveUserFromChat(int UserID, int ChatID)
        {
            this.repository.RemoveUserFromChat(UserID, ChatID);
        }

        public string getChatNameByID(int ChatID)
        {
            List<Chat> chatList = this.repository.GetChatsList();
            string chatName = chatList.Where(c => c.getChatID() == ChatID).FirstOrDefault().getChatName();

            return chatName;
        }

        public List<string> getChatParticipantsStringList(int ChatID)
        {
            List<User> participants = this.repository.GetChatParticipants(ChatID);
            List<string> participantsList = participants.Select(p => p.GetUsername()).ToList();
            return participantsList;
        }

        public List<User> getChatParticipantsList(int ChatID)
        {
            List<User> participants = this.repository.GetChatParticipants(ChatID);
            return participants;
        }
    }
}
