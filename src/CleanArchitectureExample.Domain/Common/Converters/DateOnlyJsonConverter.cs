using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CleanArchitectureExample.Domain.Common.Converters;

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private readonly string[] _formats =
    {
        "yyyy-MM-dd",
        "yyyyMMdd",
        "dd/MM/yyyy",
        "ddMMyyyy"
    };

    public override DateOnly Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        return string.IsNullOrWhiteSpace(reader.GetString())
            ? new DateOnly()
            : DateOnly.ParseExact(
            reader.GetString()!,
            _formats,
            CultureInfo.InvariantCulture);
    }

    public override void Write(
        Utf8JsonWriter writer,
        DateOnly value,
        JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString(
            _formats[0],
            CultureInfo.InvariantCulture));
}
