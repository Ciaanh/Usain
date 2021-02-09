namespace Usain.Slack.JsonConverters
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Models.Interactions;

    public class InteractionJsonConverter : JsonConverter<Interaction>
    {
        public override bool CanConvert(
            Type typeToConvert)
            => typeof(Interaction).IsAssignableFrom(typeToConvert);

        public override Interaction? Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            using JsonDocument document = JsonDocument.ParseValue(ref reader);
            var root = document.RootElement;
            var typeProvider = new InteractionJsonTypeResolver(root);
            var type = typeProvider.ResolveType();

            // Avoid infinite recursive behavior of the JsonSerializer
            // when returning Interaction type (default case of the type resolver).
            if (type == typeof(Interaction))
            {
                return new Interaction();
            }

            return (Interaction?) JsonSerializer.Deserialize(
                root.GetRawText(),
                type,
                options);
        }

        public override void Write(
            Utf8JsonWriter writer,
            Interaction value,
            JsonSerializerOptions options)
        {
            var jsonWriter = new InteractionJsonWriter(
                writer,
                options);
            jsonWriter.Write(value);
        }
    }
}
