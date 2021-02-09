namespace Usain.Slack.JsonConverters
{
    using System.Text.Json;
    using Models.Interactions;

    internal class InteractionJsonWriter
    {
        private readonly Utf8JsonWriter _jsonWriter;
        private readonly JsonSerializerOptions _options;

        public InteractionJsonWriter(
            Utf8JsonWriter jsonWriter,
            JsonSerializerOptions options)
        {
            _jsonWriter = jsonWriter;
            _options = options;
        }

        public void Write(
            Interaction @interaction)
        {
            switch (@interaction)
            {
                case GlobalShortcut shortcut:
                    JsonSerializer.Serialize(
                        _jsonWriter,
                        shortcut,
                        _options);
                    return;
            }

            WriteDefault(@interaction);
        }

        private void WriteDefault(
            Interaction value)
        {
            _jsonWriter.WriteStartObject();
            _jsonWriter.WriteString(
                Interaction.InteractionTypeJsonName,
                value.InteractionType);
            _jsonWriter.WriteEndObject();
        }
    }
}
