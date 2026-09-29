using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Senparc.Weixin.TenPayV3.Apis.BasePay
{
    public class StringOrNumberJsonConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert,
            JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    return reader.GetString();
                case JsonTokenType.Number:
                    using (var document = JsonDocument.ParseValue(ref reader))
                    {
                        return document.RootElement.ToString();
                    }
                case JsonTokenType.Null:
                    return null;
                default:
                    throw new JsonException(
                        $"Cannot convert {reader.TokenType} to string.");
            }
        }

        public override void Write(Utf8JsonWriter writer, string value,
            JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }
}
