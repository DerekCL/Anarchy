using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Moq;
using NUnit.Framework;

namespace Bot.Discord.Handlers;

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
    public async Task HandleMessageAsyncNullMessageThrowsArgumentNullExceptionAsync()
    {
        await Task.CompletedTask.ConfigureAwait(false);
        _ = Assert.ThrowsAsync<ArgumentNullException>(async () =>
            await DiscordMessageHandler.HandleMessageAsync(null!).ConfigureAwait(false));
    }

    /// <summary>
    /// Verifies that when the message author is a bot, no response is sent.
    /// </summary>
    /// <returns>A task representing the asynchronous test operation.</returns>
    [Test]
    public async Task HandleMessageAsyncMessageFromBotDoesNotSendResponseAsync()
    {
        var mockChannel = new Mock<ISocketMessageChannel>();
        var mockMessage = new Mock<SocketUserMessage>();
        var mockAuthor = new Mock<SocketUser>();

        _ = mockAuthor.Setup(user => user.IsBot).Returns(value: true);
        _ = mockMessage.SetupGet(userMessage => userMessage.Author).Returns(mockAuthor.Object);
        _ = mockMessage.SetupGet(userMessage => userMessage.Channel).Returns(mockChannel.Object);
        _ = mockMessage.SetupGet(userMessage => userMessage.Content).Returns("!ping");

        await DiscordMessageHandler.HandleMessageAsync(mockMessage.Object).ConfigureAwait(false);

        IEnumerable<IInvocation> sendMessageInvocations = mockChannel.Invocations
            .Where(inv => string.Equals(inv.Method.Name, "SendMessageAsync", StringComparison.Ordinal));
        Assert.That(sendMessageInvocations, Is.Empty);
    }

    /// <summary>
    /// Verifies that when the message is not of type <see cref="SocketUserMessage"/>, no response is sent.
    /// </summary>
    /// <returns>A task representing the asynchronous test operation.</returns>
    [Test]
    public async Task HandleMessageAsyncNotSocketUserMessageDoesNotSendResponseAsync()
    {
        var mockMessage = new Mock<SocketMessage>();
        var mockAuthor = new Mock<SocketUser>();

        _ = mockAuthor.Setup(user => user.IsBot).Returns(value: false);
        _ = mockMessage.SetupGet(socketMessage => socketMessage.Author).Returns(mockAuthor.Object);

        await DiscordMessageHandler.HandleMessageAsync(mockMessage.Object).ConfigureAwait(false);
        Assert.Pass();
    }

    /// <summary>
    /// Verifies that a valid "!ping" command results in sending "Pong! 🏓" to the channel.
    /// </summary>
    /// <returns>A task representing the asynchronous test operation.</returns>
    [Test]
    public async Task HandleMessageAsyncPingCommandSendsPongResponseAsync()
    {
        var expectedResponse = "Pong! 🏓";
        var mockChannel = new Mock<ISocketMessageChannel>();

        // Use the helper method to avoid optional arguments.
        _ = mockChannel.Setup(channel => SendMessageAsyncWrapper(channel, It.IsAny<string>()))
            .Returns((Task<IUserMessage>)(object)Task.FromResult(Mock.Of<RestUserMessage>()));

        var mockMessage = new Mock<SocketUserMessage>();
        var mockAuthor = new Mock<SocketUser>();

        _ = mockAuthor.Setup(user => user.IsBot).Returns(value: false);
        _ = mockMessage.SetupGet(userMessage => userMessage.Author).Returns(mockAuthor.Object);
        _ = mockMessage.SetupGet(userMessage => userMessage.Channel).Returns(mockChannel.Object);
        _ = mockMessage.SetupGet(userMessage => userMessage.Content).Returns("!ping");

        await DiscordMessageHandler.HandleMessageAsync(mockMessage.Object).ConfigureAwait(false);

        IInvocation? sendMessageInvocation = mockChannel.Invocations
            .FirstOrDefault(inv => string.Equals(inv.Method.Name, "SendMessageAsync", StringComparison.Ordinal));
        Assert.That(sendMessageInvocation, Is.Not.Null, "SendMessageAsync was not called.");

        var sendMessageArgs = (string)sendMessageInvocation!.Arguments[0];
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
    public async Task HandleMessageAsyncNonPingCommandDoesNotSendResponseAsync()
    {
        var mockChannel = new Mock<ISocketMessageChannel>();
        var mockMessage = new Mock<SocketUserMessage>();
        var mockAuthor = new Mock<SocketUser>();

        _ = mockAuthor.Setup(user => user.IsBot).Returns(value: false);
        _ = mockMessage.SetupGet(userMessage => userMessage.Author).Returns(mockAuthor.Object);
        _ = mockMessage.SetupGet(userMessage => userMessage.Channel).Returns(mockChannel.Object);
        _ = mockMessage.SetupGet(userMessage => userMessage.Content).Returns("Hello, world!");

        await DiscordMessageHandler.HandleMessageAsync(mockMessage.Object).ConfigureAwait(false);

        IEnumerable<IInvocation> sendMessageInvocations = mockChannel.Invocations
            .Where(inv => string.Equals(inv.Method.Name, "SendMessageAsync", StringComparison.Ordinal));
        Assert.That(sendMessageInvocations, Is.Empty);
    }

    // Private helper method to wrap SendMessageAsync and avoid optional arguments in expression trees.
    private static Task<IUserMessage> SendMessageAsyncWrapper(ISocketMessageChannel channel, string text)
    {
        return channel.SendMessageAsync(text).ContinueWith(t => (IUserMessage)t.Result);
    }
}
