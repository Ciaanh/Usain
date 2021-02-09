namespace Usain.InteractionProcessor.Tests.InteractionReactions
{
    using System.Threading.Tasks;
    using InteractionProcessor.InteractionReactions;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Slack.Models.Interactions;
    using Xunit;

    public class NoopInteractionReactionTest
    {
        [Fact]
        public async Task ReactAsync_Complete_Successfully()
        {
            var loggerMock =
                new Mock<ILogger<NoopInteractionReaction<Interaction>>>();
            loggerMock
                .Setup((x => x.IsEnabled(It.IsAny<LogLevel>())))
                .Returns(true);
            var interaction = new Interaction { };
            var reaction = new NoopInteractionReaction<Interaction>(
                loggerMock.Object,
                interaction);

            await reaction.ReactAsync();

            loggerMock.VerifyAll();
        }
    }
}
