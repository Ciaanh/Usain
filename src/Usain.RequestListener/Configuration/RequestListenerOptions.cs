namespace Usain.RequestListener.Configuration
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;

    public class RequestListenerOptions : IConfigureOptions<RequestListenerOptions>
    {
        private const string OptionsSectionKeyName = "UsainRequestListener";
        private readonly IConfiguration? _configuration;

        public bool IsRequestAuthenticationEnabled { get; set; } = true;
        public string SigningKey { get; set; } = string.Empty;

        [Range(
            1,
            int.MaxValue,
            ErrorMessageResourceName = nameof(Resources
                .OptionsValidation_DeltaTimeTolerance_Range_NotValid),
            ErrorMessageResourceType = typeof(Resources))]
        public int DeltaTimeToleranceSeconds { get; set; } = 300;

        public RequestListenerOptions()
        {
        }

        public RequestListenerOptions(
            IConfiguration configuration)
            => _configuration = configuration;

        public void Configure(
            RequestListenerOptions options)
        {
            _configuration?.GetSection(OptionsSectionKeyName)
                .Bind(options);
        }
    }
}
