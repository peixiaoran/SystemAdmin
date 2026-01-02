using System.Text.Json;
using System.Text.Json.Serialization;

namespace SystemAdmin.Model.ModelHelper.ModelConverter
{
    /// <summary>
    /// long类型转换为string类型的JsonConverter
    /// </summary>
    public class LongToStringConverter : JsonConverter<long>
    {
        public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var stringValue = reader.GetString();
            if (stringValue == null)
            {
                throw new JsonException("Expected a non-null string.");
            }
            return long.Parse(stringValue);
        }

        public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

    /// <summary>
    /// int类型转换为string类型的JsonConverter
    /// </summary>
    public class IntToStringConverter : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
                throw new JsonException("Expected string token type.");

            return reader.GetInt32();
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
