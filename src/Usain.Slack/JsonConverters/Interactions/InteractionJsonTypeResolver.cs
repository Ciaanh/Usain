namespace Usain.Slack.JsonConverters
{
    using System;
    using System.Text.Json;
    using Models.Interactions;

    internal class InteractionJsonTypeResolver
    {
        private readonly JsonElement _jsonElement;

        public InteractionJsonTypeResolver(
            JsonElement jsonElement)
            => _jsonElement = jsonElement;

        public Type ResolveType()
        {
            if (!_jsonElement.TryGetProperty(
                Interaction.InteractionTypeJsonName,
                out var property)) { throw new JsonException(); }

            var typeValue = property.GetString();
            return typeValue switch
            {
                GlobalShortcut.InteractionTypeValue =>
                    typeof(GlobalShortcut),
                // We should throw an exception here.
                // We don't do it until the complete Slack Event API surface is covered,
                // otherwise we wouldn't be able to support unknown (not yet implemented) events.
                // This will certainly change in a future version.
                _ => typeof(Interaction),
            };
        }
    }
}
