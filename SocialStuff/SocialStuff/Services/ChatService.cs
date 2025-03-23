﻿using SocialStuff.Data;
using System;
using System.Collections.Generic;
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
