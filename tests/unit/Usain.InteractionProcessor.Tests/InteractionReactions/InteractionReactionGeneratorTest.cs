namespace Usain.InteractionProcessor.Tests.InteractionReactions
{
    using System;
    using InteractionProcessor.InteractionReactions;
    using Moq;
    using Slack.Models.Interactions;
    using Xunit;

    public class InteractionReactionGeneratorTest
    {
        private readonly Mock<IInteractionReactionFactory<Interaction>>
            _noopInteractionReactionFactoryMock =
                new Mock<IInteractionReactionFactory<Interaction>>();

        private readonly Mock<IInteractionReaction<Interaction>>
            _noopInteractionReactionMock = new Mock<IInteractionReaction<Interaction>>();

        private readonly Mock<IInteractionReactionFactory<GlobalShortcut>>
            _shortcutInteractionReactionFactoryMock =
                new Mock<IInteractionReactionFactory<GlobalShortcut>>();

        private readonly Mock<IInteractionReaction<GlobalShortcut>>
            _shortcutInteractionReactionMock =
                new Mock<IInteractionReaction<GlobalShortcut>>();

        public InteractionReactionGeneratorTest()
        {
            _noopInteractionReactionFactoryMock
                .Setup(x => x.Create(It.IsAny<Interaction>()))
                .Returns(_noopInteractionReactionMock.Object);
            _shortcutInteractionReactionFactoryMock
                .Setup(x => x.Create(It.IsAny<GlobalShortcut>()))
                .Returns(_shortcutInteractionReactionMock.Object);
        }

        [Fact]
        public void
            Generate_Throws_InvalidOperationException_When_Interaction_Is_Null()
        {
            var generator = CreateGenerator();

            Assert.Throws<InvalidOperationException>(
                () => generator.Generate(null));
        }

        [Fact]
        public void Generate_Returns_InteractionReaction_For_GlobalShortcut()
        {
            var shortcut = new GlobalShortcut { };
            var generator = CreateGenerator();

            var actual = generator.Generate(shortcut);

            Assert.Equal(
                _shortcutInteractionReactionMock.Object,
                actual);
            _shortcutInteractionReactionFactoryMock.Verify(
                x => x.Create(shortcut),
                Times.Once);
        }

        [Fact]
        public void Generate_Returns_NoopInteractionReaction_For_Unsupported_Interactions()
        {
            var interaction = new UnsupportedInteraction { };
            var generator = CreateGenerator();

            var actual = generator.Generate(interaction);

            Assert.IsAssignableFrom<IInteractionReaction>(actual);
            Assert.IsAssignableFrom<IInteractionReaction<Interaction>>(actual);
        }

        private InteractionReactionGenerator CreateGenerator()
            => new InteractionReactionGenerator(
                _noopInteractionReactionFactoryMock.Object,
                _shortcutInteractionReactionFactoryMock.Object);

        private class UnsupportedInteraction : Interaction { }
    }
}
