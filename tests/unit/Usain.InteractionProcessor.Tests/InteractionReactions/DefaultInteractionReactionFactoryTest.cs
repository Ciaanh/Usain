namespace Usain.InteractionProcessor.Tests.InteractionReactions
{
    using InteractionProcessor.InteractionReactions;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Slack.Models.Interactions;
    using Xunit;

    public class DefaultEventReactionFactoryTest
    {
        [Fact]
        public void Create_Creates_A_NoopEventReaction()
        {
            var factory = new DefaultInteractionReactionFactory<GlobalShortcut>(Mock.Of<ILoggerFactory>());
            var interaction = new GlobalShortcut { };

            var actual = factory.Create(interaction);

            Assert.Equal(interaction, actual.Interaction);
        }
    }
}
