namespace Usain.InteractionProcessor.Configuration
{
    using System;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;

    public class InteractionProcessorOptions
        : IConfigureOptions<InteractionProcessorOptions>
    {
        private const string OptionsSectionKeyName = "UsainInteractionProcessor";
        private readonly IConfiguration? _configuration;

        public int CheckUpdateTimeMs { get; set; } = 1000;

        public InteractionProcessorOptions() { }

        public InteractionProcessorOptions(
            IConfiguration configuration)
            => _configuration = configuration
                ?? throw new ArgumentNullException(nameof(configuration));

        public void Configure(
            InteractionProcessorOptions options)
        {
            _configuration?.GetSection(OptionsSectionKeyName)
                .Bind(options);
        }
    }
}
