namespace Usain.InteractionProcessor.Tests.HostedServices
{
    using System.Threading;
    using System.Threading.Tasks;
    using Configuration;
    using InteractionProcessor.HostedServices;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Moq;
    using Xunit;

    public class InteractionProcessorServiceTest
    {
        private readonly Mock<ILogger<InteractionProcessorService>> _loggerMock =
            new Mock<ILogger<InteractionProcessorService>>();
        private readonly Mock<IInteractionQueueProcessor> _interactionQueueProcessorMock =
            new Mock<IInteractionQueueProcessor>();
        private readonly Mock<IOptions<InteractionProcessorOptions>> _optionsMock =
            new Mock<IOptions<InteractionProcessorOptions>>();

        public InteractionProcessorServiceTest()
        {
            _optionsMock.SetupGet(x => x.Value)
                .Returns(
                    new InteractionProcessorOptions
                    {
                        CheckUpdateTimeMs = 10,
                    });
        }

        [Fact]
        public async Task ExecuteAsync_Calls_InteractionQueueProcessor()
        {
            var service = new InteractionProcessorService(
                _loggerMock.Object,
                _interactionQueueProcessorMock.Object,
                _optionsMock.Object);

            await service.StartAsync(CancellationToken.None);
            // A little delay to ensure we don't request cancellation
            // before first loop has run once (test stability).
            await Task.Delay(
                250,
                CancellationToken.None);
            await service.StopAsync(CancellationToken.None);

            _interactionQueueProcessorMock.Verify(
                x => x.ProcessQueueAsync(It.IsAny<CancellationToken>()),
                Times.AtLeastOnce);
        }
    }
}
