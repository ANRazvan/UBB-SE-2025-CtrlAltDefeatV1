using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using SocialStuff.Services;
using SocialStuff.Data;
using SocialStuff.Model;    

namespace SocialStuff.Tests
{
    public class ChatServiceTests
    {
        private readonly ChatService _chatService;
        private readonly Mock<Repository> _mockRepository;

        public ChatServiceTests()
        {
            _mockRepository = new Mock<Repository>();  // Mock Repository
            _chatService = new ChatService(); // Needs modification to accept mock repo
        }

        [Fact]
        public void AddChat_ShouldReturnTrue_whenChatIsCreatedSuccessfully()
        {
            // Arrange
            List<int> users = new List<int> { 1, 2, 3 };
            string chatName = "TestChat";
            // Define an output variable for the chatID
            int fakeChatId = 1;

            // Setup AddChat to assign a fake ID and return true
            _mockRepository.Setup(repo => repo.AddChat(It.IsAny<string>(), out fakeChatId));

            // Mock GetChatParticipants to return a list of fake users
            _mockRepository.Setup(repo => repo.GetChatParticipants(It.IsAny<int>()))
                .Returns(new List<User>
                {
                    new User(1, "User1", "123456789", new List<int>(), new List<int>()),
                    new User(2, "User2", "987654321", new List<int>(), new List<int>())
                });


            // Act
            bool result = _chatService.addChat(users, chatName);

            // Assert
            Assert.True(result, "Chat should be added successfully.");
        }
    }
}
