using System.Threading.Tasks;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Moq;
using NUnit.Framework;

namespace Bot.Discord.Handlers
{
    /// <summary>
    /// Contains unit tests for the <see cref="DiscordMessageHandler"/> class.
    /// </summary>
    public class DiscordMessageHandlerTests
    {
        private Mock<SocketMessage> messageMock;
        private Mock<ISocketMessageChannel> channelMock;
        private Mock<SocketUser> userMock;

        /// <summary>
        /// Sets up the test environment before each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.messageMock = new Mock<SocketMessage>();
            this.channelMock = new Mock<ISocketMessageChannel>();
            this.userMock = new Mock<SocketUser>();

            this.messageMock.Setup(m => m.Channel).Returns(this.channelMock.Object);
            this.messageMock.Setup(m => m.Author).Returns(this.userMock.Object);

            // Setup default message response using a wrapper to avoid optional parameter issues.
            var mockMessage = new Mock<IUserMessage>();
            this.channelMock
                .Setup(c => SendMessageAsyncWrapper(
                    c,
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<RequestOptions>()))
                .ReturnsAsync(mockMessage.Object);
        }

        /// <summary>
        /// Verifies that messages from bots are ignored.
        /// </summary>
        /// <returns>A task representing the asynchronous test operation.</returns>
        [Test]
        public async Task ShouldIgnoreBotMessages()
        {
            // Arrange
            this.userMock.Setup(u => u.IsBot).Returns(true);

            // Act
            await DiscordMessageHandler.HandleMessageAsync(this.messageMock.Object);

            // Assert – Verify that SendMessageAsync was not called.
            this.channelMock.Verify(
                c => SendMessageAsyncWrapper(
                    c,
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<RequestOptions>()),
                Times.Never);
        }

        /// <summary>
        /// Verifies that the ping command returns a pong response.
        /// </summary>
        /// <returns>A task representing the asynchronous test operation.</returns>
        [Test]
        public async Task ShouldRespondToPingWithPong()
        {
            // Arrange
            this.userMock.Setup(u => u.IsBot).Returns(false);
            this.messageMock.Setup(m => m.Content).Returns("!ping");

            // Act
            await DiscordMessageHandler.HandleMessageAsync(this.messageMock.Object);

            // Assert – Verify that SendMessageAsync was called with "Pong! 🏓".
            this.channelMock.Verify(
                c => SendMessageAsyncWrapper(
                    c,
                    "Pong! 🏓",
                    It.IsAny<bool>(),
                    It.IsAny<RequestOptions>()),
                Times.Once);
        }

        /// <summary>
        /// A wrapper for the SendMessageAsync method to avoid issues with optional parameters in expression trees.
        /// </summary>
        /// <param name="channel">The channel to send the message in.</param>
        /// <param name="text">The text of the message.</param>
        /// <param name="isTTS">Indicates whether the message is TTS.</param>
        /// <param name="options">Additional request options.</param>
        /// <returns>A task representing the asynchronous operation, with a user message as result.</returns>
        private static Task<IUserMessage> SendMessageAsyncWrapper(
            ISocketMessageChannel channel,
            string text,
            bool isTTS,
            RequestOptions options)
        {
            var result = channel.SendMessageAsync(text, isTTS, options: options);
            return result.ContinueWith(t => (IUserMessage)t.Result);
        }
    }
}
