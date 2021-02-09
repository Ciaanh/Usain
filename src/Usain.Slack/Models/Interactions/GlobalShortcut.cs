namespace Usain.Slack.Models.Interactions
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class GlobalShortcut : Interaction
    {
        internal const string TokenJsonName = "token";
        internal const string CallbackIdJsonName = "callback_id";
        internal const string TriggerIdJsonName = "trigger_id";

        /// <summary>
        /// Interaction type value for the <see cref="GlobalShortcut"/> interaction.
        /// </summary>
        public const string InteractionTypeValue = "shortcut";

        /// <summary>
        /// This deprecated verification token is proof that the request
        /// is coming from Slack on behalf of your application.
        /// </summary>
        /// <example>Jhj5dZrVaK7ZwHHjRyZWjbDl</example>
        [JsonPropertyName(TokenJsonName)]
        public string? Token { get; set; }

        /// <summary>
        /// An ID defined at the creation of the global shortcut
        /// </summary>
        /// <example>shortcut_create_task</example>
        [JsonPropertyName(CallbackIdJsonName)]
        public string? CallbackId { get; set; }

        /// <summary>
        /// A temporary ID generated for the interaction in your app
        /// </summary>
        /// <example>944799105734.773906753841.38b5894552bdd4a780554ee59d1f3638</example>
        [JsonPropertyName(TriggerIdJsonName)]
        public string? TriggerId { get; set; }

        /// <summary>
        /// Extra json properties not directly mapped to this type definition.
        /// </summary>
        [JsonExtensionData]
        public Dictionary<string, object> ExtraFields { get; set; } =
            new Dictionary<string, object>();

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalShortcut"/> class.
        /// </summary>
        public GlobalShortcut()
            => InteractionType = InteractionTypeValue;
    }
}
