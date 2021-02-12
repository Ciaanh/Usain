namespace Usain.InteractionProcessor.Tests.HostedServices
{
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Infrastructure;
    using InteractionProcessor.HostedServices;
    using InteractionProcessor.InteractionReactions;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Slack.Models.Interactions;
    using Xunit;

    public class InteractionQueueProcessorTest
    {
        private readonly Mock<ILogger<InteractionQueueProcessor>> _loggerMock =
            new Mock<ILogger<InteractionQueueProcessor>>();

        private readonly Mock<IEventQueue<Interaction>> _interactionQueueMock =
            new Mock<IEventQueue<Interaction>>();

        private readonly Mock<IInteractionReactionGenerator> _reactionGeneratorMock =
            new Mock<IInteractionReactionGenerator>();

        private readonly Mock<IInteractionReaction> _reactionMock =
            new Mock<IInteractionReaction>();

        private Interaction _interaction = new Interaction
        {
            //Event = new AppMentionEvent(),
        };

        public InteractionQueueProcessorTest()
        {
            _reactionGeneratorMock
                .Setup(x => x.Generate(_interaction))
                .Returns(_reactionMock.Object);
            _interactionQueueMock.Setup(
                    x => x.DequeueAsync(
                        It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(_interaction));
        }

        [Fact]
        public async Task ProcessQueueAsync_Dequeue_Interaction_And_React()
        {
            var queueProcessor = CreateInteractionQueueProcessor();
            await queueProcessor.ProcessQueueAsync(CancellationToken.None);

            _reactionMock.Verify(
                x => x.ReactAsync(),
                Times.Once);
        }

        [Fact]
        public async Task ProcessQueueAsync_Dequeue_Interaction_And_DoesNot_React_When_Interaction_Is_Null()
        {
            _interaction = new Interaction();
            _interactionQueueMock.Setup(
                    x => x.DequeueAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult((Interaction)null));

            var queueProcessor = CreateInteractionQueueProcessor();
            await queueProcessor.ProcessQueueAsync(CancellationToken.None);

            _reactionMock.Verify(
                x => x.ReactAsync(),
                Times.Never);
        }

        private InteractionQueueProcessor CreateInteractionQueueProcessor()
            => new InteractionQueueProcessor(
                _loggerMock.Object,
                _interactionQueueMock.Object,
                _reactionGeneratorMock.Object);
    }
}
