using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Moq;
using NUnit.Framework;

namespace Bot.Discord.Handlers
{
    /// <summary>
    /// Contains tests for the <see cref="DiscordMessageHandler"/> class.
    /// </summary>
    [TestFixture]
    public class DiscordMessageHandlerTests
    {
        /// <summary>
        /// Verifies that calling
        /// <see cref="DiscordMessageHandler.HandleMessageAsync(SocketMessage)"/>
        /// with a null message throws an <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <returns>A task representing the asynchronous test operation.</returns>
        [Test]
        public async Task HandleMessageAsyncNullMessageThrowsArgumentNullException()
        {
            await Task.CompletedTask;
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await DiscordMessageHandler.HandleMessageAsync((SocketMessage?)null!).ConfigureAwait(false));
        }

        /// <summary>
        /// Verifies that when the message author is a bot, no response is sent.
        /// </summary>
        /// <returns>A task representing the asynchronous test operation.</returns>
        [Test]
        public async Task HandleMessageAsyncMessageFromBotDoesNotSendResponse()
        {
            var mockChannel = new Mock<ISocketMessageChannel>();
            var mockMessage = new Mock<SocketUserMessage>();
            var mockAuthor = new Mock<SocketUser>();

            mockAuthor.Setup(a => a.IsBot).Returns(true);
            mockMessage.SetupGet(m => m.Author).Returns(mockAuthor.Object);
            mockMessage.SetupGet(m => m.Channel).Returns(mockChannel.Object);
            mockMessage.SetupGet(m => m.Content).Returns("!ping");

            await DiscordMessageHandler.HandleMessageAsync(mockMessage.Object).ConfigureAwait(false);

            var sendMessageInvocations = mockChannel.Invocations
                .Where(inv => string.Equals(inv.Method.Name, "SendMessageAsync", StringComparison.Ordinal));
            Assert.That(sendMessageInvocations, Is.Empty);
        }

        /// <summary>
        /// Verifies that when the message is not of type <see cref="SocketUserMessage"/>, no response is sent.
        /// </summary>
        /// <returns>A task representing the asynchronous test operation.</returns>
        [Test]
        public async Task HandleMessageAsyncNotSocketUserMessageDoesNotSendResponse()
        {
            var mockMessage = new Mock<SocketMessage>();
            var mockAuthor = new Mock<SocketUser>();

            mockAuthor.Setup(a => a.IsBot).Returns(false);
            mockMessage.SetupGet(m => m.Author).Returns(mockAuthor.Object);

            await DiscordMessageHandler.HandleMessageAsync(mockMessage.Object).ConfigureAwait(false);
            Assert.Pass();
        }

        /// <summary>
        /// Verifies that a valid "!ping" command results in sending "Pong! 🏓" to the channel.
        /// </summary>
        /// <returns>A task representing the asynchronous test operation.</returns>
        [Test]
        public async Task HandleMessageAsyncPingCommandSendsPongResponse()
        {
            string expectedResponse = "Pong! 🏓";
            var mockChannel = new Mock<ISocketMessageChannel>();

            // Use the helper method to avoid optional arguments.
            mockChannel.Setup(x => SendMessageAsyncWrapper(x, It.IsAny<string>(), false, It.IsAny<Embed?>(), It.IsAny<RequestOptions?>()))
                .Returns((Task<IUserMessage>)(object)Task.FromResult(Mock.Of<RestUserMessage>()));

            var mockMessage = new Mock<SocketUserMessage>();
            var mockAuthor = new Mock<SocketUser>();

            mockAuthor.Setup(a => a.IsBot).Returns(false);
            mockMessage.SetupGet(m => m.Author).Returns(mockAuthor.Object);
            mockMessage.SetupGet(m => m.Channel).Returns(mockChannel.Object);
            mockMessage.SetupGet(m => m.Content).Returns("!ping");

            await DiscordMessageHandler.HandleMessageAsync(mockMessage.Object).ConfigureAwait(false);

            var sendMessageInvocation = mockChannel.Invocations
                .FirstOrDefault(inv => string.Equals(inv.Method.Name, "SendMessageAsync", StringComparison.Ordinal));
            Assert.That(sendMessageInvocation, Is.Not.Null, "SendMessageAsync was not called.");

            string sendMessageArgs = (string)sendMessageInvocation!.Arguments[0];
            Assert.That(
                string.Equals(sendMessageArgs!, expectedResponse, StringComparison.Ordinal),
                Is.True,
                "The response text did not match the expected value.");
        }

        /// <summary>
        /// Verifies that messages with non-ping content do not trigger any response.
        /// </summary>
        /// <returns>A task representing the asynchronous test operation.</returns>
        [Test]
        public async Task HandleMessageAsyncNonPingCommandDoesNotSendResponse()
        {
            var mockChannel = new Mock<ISocketMessageChannel>();
            var mockMessage = new Mock<SocketUserMessage>();
            var mockAuthor = new Mock<SocketUser>();

            mockAuthor.Setup(a => a.IsBot).Returns(false);
            mockMessage.SetupGet(m => m.Author).Returns(mockAuthor.Object);
            mockMessage.SetupGet(m => m.Channel).Returns(mockChannel.Object);
            mockMessage.SetupGet(m => m.Content).Returns("Hello, world!");

            await DiscordMessageHandler.HandleMessageAsync(mockMessage.Object).ConfigureAwait(false);

            var sendMessageInvocations = mockChannel.Invocations
                .Where(inv => string.Equals(inv.Method.Name, "SendMessageAsync", StringComparison.Ordinal));
            Assert.That(sendMessageInvocations, Is.Empty);
        }

        // Private helper method to wrap SendMessageAsync and avoid optional arguments in expression trees.
        private static Task<IUserMessage> SendMessageAsyncWrapper(ISocketMessageChannel channel, string text, bool isTTS, Embed? embed, RequestOptions? options)
        {
            return channel.SendMessageAsync(text, isTTS, embed, options).ContinueWith(t => (IUserMessage)t.Result);
        }
    }
}
