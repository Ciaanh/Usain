namespace Usain.Slack.Models.Interactions
{
    using System.Text.Json.Serialization;
    using JsonConverters;

    [JsonConverter(typeof(InteractionJsonConverter))]
    public class Interaction
    {
        internal const string InteractionTypeJsonName = "type";
        internal const string DefaultInteractionTypeValue = "unknown";

        /// <summary>
        /// Indicates which kind of interaction dispatch this is, usually `shortcut`
        /// </summary>
        /// <example>shortcut</example>
        [JsonPropertyName(InteractionTypeJsonName)]
        public string InteractionType { get; set; } = DefaultInteractionTypeValue;
    }
}
