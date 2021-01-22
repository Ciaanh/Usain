namespace Usain.Slack.JsonConverters
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Models;

    public class TimestampConverter : JsonConverter<Timestamp>
    {
        public override Timestamp? Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }

            string? readerValue = reader.GetString();
            if (String.IsNullOrWhiteSpace(readerValue)) { return null; }

            Timestamp.TryParse(readerValue, out var eventTimestamp);
            return eventTimestamp;
        }

        public override void Write(
            Utf8JsonWriter writer,
            Timestamp value,
            JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
