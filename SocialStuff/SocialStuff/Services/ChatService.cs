using SocialStuff.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialStuff.Model.MessageClasses;
using SocialStuff.Model;

namespace SocialStuff.Services
{
    public class ChatService
    {
        private Repository repository;

        public ChatService(Repository repo)
        {
            this.repository = repo;
        }

        public Repository getRepo()
        {
            return this.repository;
        }

        public void requestMoneyViaChat(List<int> ListOfRequestedUsersID, int RequesterID, float Amount, string Currency, int ChatID, string Description)
        {
            throw new NotImplementedException();
        }

        public void sendMoneyViaChat(List<int> ListOfReceiversID, int SenderID, float Amount, string Currency, string Description, int ChatID)
                {
            throw new NotImplementedException();
                }
                var addedUsers = repository.GetChatParticipants(chatID);

        public void createChat(List<int> ParticipantsID, string ChatName)
                {
            throw new NotImplementedException();
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ChatService - addChat Error: " + ex.Message);
                return false;
            }
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
            catch (Exception ex)
            {
                Console.WriteLine("ChatService - removeChat Error: " + ex.Message);
            }
            return;
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

    }
}
