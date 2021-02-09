namespace Usain.InteractionProcessor.Tests.DependencyInjection
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Configuration;
    using Core.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Options;
    using Moq;
    using Slack.Models.Events;
    using Usain.Slack.Models.Interactions;
    using Xunit;

    public class InteractionProcessorBuilderExtensionsTest
    {
        private readonly Mock<IInteractionProcessorBuilder> _builderMock =
            new Mock<IInteractionProcessorBuilder>();
        private readonly ServiceCollection _serviceCollection =
            new ServiceCollection();

        public InteractionProcessorBuilderExtensionsTest()
        {
            _builderMock
                .SetupGet(x => x.Services)
                .Returns(_serviceCollection);
        }

        [Fact]
        public void
            AddEventQueue_Register_Singleton_If_Not_Already_Registered()
        {
            _serviceCollection.Add(
                new ServiceDescriptor(
                    typeof(IEventQueue<GlobalShortcut>),
                    typeof(InteractionQueueFirst),
                    ServiceLifetime.Singleton));
            var builder = _builderMock.Object;
            builder.AddEventQueue<InteractionQueueSecond>();

            Assert.Equal(
                1,
                _serviceCollection.Count(
                    x => x.Lifetime == ServiceLifetime.Singleton
                        && x.ServiceType == typeof(IEventQueue<GlobalShortcut>)
                        && x.ImplementationType == typeof(InteractionQueueFirst)));
        }

        [Fact]
        public void AddEventQueue_Registers_Singleton()
        {
            var builder = _builderMock.Object;
            builder.AddEventQueue<InteractionQueueFirst>();

            Assert.Equal(
                1,
                _serviceCollection.Count(
                    x => x.Lifetime == ServiceLifetime.Singleton
                        && x.ServiceType == typeof(IEventQueue<GlobalShortcut>)
                        && x.ImplementationType == typeof(InteractionQueueFirst)));
        }

        [Fact]
        public void
            AddEventQueue_Register_Singleton_With_ImplementationFactory()
        {
            var builder = _builderMock.Object;
            Func<IServiceProvider, InteractionQueueFirst> factory =
                sp => new InteractionQueueFirst();
            builder.AddEventQueue(factory);

            Assert.Equal(
                1,
                _serviceCollection.Count(
                    x => x.Lifetime == ServiceLifetime.Singleton
                        && x.ServiceType == typeof(IEventQueue<GlobalShortcut>)
                        && x.ImplementationFactory == factory));
        }

        [Fact]
        public void
            AddEventQueue_Register_Singleton_With_ImplementationFactory_If_Not_Already_Registered()
        {
            var builder = _builderMock.Object;
            Func<IServiceProvider, InteractionQueueFirst> factoryFirst =
                sp => new InteractionQueueFirst();
            _serviceCollection.Add(
                new ServiceDescriptor(
                    typeof(IEventQueue<GlobalShortcut>),
                    factoryFirst,
                    ServiceLifetime.Singleton));

            Func<IServiceProvider, InteractionQueueSecond> factorySecond =
                sp => new InteractionQueueSecond();
            builder.AddEventQueue(factorySecond);

            Assert.Equal(
                1,
                _serviceCollection.Count(
                    x => x.Lifetime == ServiceLifetime.Singleton
                        && x.ServiceType == typeof(IEventQueue<GlobalShortcut>)
                        && x.ImplementationFactory == factoryFirst));
        }

        [Fact]
        public void AddPlatformServices_Add_Configuration()
        {
            var builder = _builderMock.Object;
            builder.AddPlatformServices();

            Assert.Equal(
                1,
                _serviceCollection.Count(
                    x => x.Lifetime == ServiceLifetime.Singleton
                        && x.ServiceType
                        == typeof(
                            IConfigureOptions<InteractionProcessorOptions>)
                        && x.ImplementationType
                        == typeof(InteractionProcessorOptions)));
        }


        private class InteractionQueueFirst : IEventQueue<GlobalShortcut>
        {
            public Task EnqueueAsync(
                GlobalShortcut item,
                CancellationToken cancellationToken)
                => throw new NotImplementedException();

            public Task<GlobalShortcut> DequeueAsync(
                CancellationToken cancellationToken)
                => throw new NotImplementedException();
        }

        private class InteractionQueueSecond : IEventQueue<GlobalShortcut>
        {
            public Task EnqueueAsync(
                GlobalShortcut item,
                CancellationToken cancellationToken)
                => throw new NotImplementedException();

            public Task<GlobalShortcut> DequeueAsync(
                CancellationToken cancellationToken)
                => throw new NotImplementedException();
        }
    }
}
