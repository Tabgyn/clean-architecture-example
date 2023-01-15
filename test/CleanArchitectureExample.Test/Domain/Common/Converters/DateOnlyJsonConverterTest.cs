using System.Text;
using System.Text.Json;
using CleanArchitectureExample.Domain.Common.Converters;
using Xunit;

namespace CleanArchitectureExample.UnitTest.Domain.Common.Converters;

public class DateOnlyJsonConverterTest
{
    private readonly DateOnlyJsonConverter _dateOnlyJsonConverter;

    public DateOnlyJsonConverterTest()
    {
        _dateOnlyJsonConverter = new DateOnlyJsonConverter();
    }

    [Theory]
    [InlineData("\"1990-12-30\"")]
    [InlineData("\"19901230\"")]
    [InlineData("\"30/12/1990\"")]
    [InlineData("\"30121990\"")]
    public void Read_ShouldReturnValidDateOnly_WhenPassingValue(string dateOnlyString)
    {
        //Arrange
        byte[] bytes = Encoding.UTF8.GetBytes(dateOnlyString);
        var reader = new Utf8JsonReader(bytes.AsSpan());
        Type typeToConvert = typeof(DateOnly);
        var options = new JsonSerializerOptions();

        //Act
        reader.Read();
        DateOnly deserializedDateOnly = _dateOnlyJsonConverter.Read(
            ref reader,
            typeToConvert,
            options);

        //Assert
        Assert.Equal(1990, deserializedDateOnly.Year);
        Assert.Equal(12, deserializedDateOnly.Month);
        Assert.Equal(30, deserializedDateOnly.Day);
    }

    [Theory]
    [InlineData("\"\"")]
    [InlineData("\"   \"")]
    public void Read_ShouldReturnDefaultDateOnly_WhenPassingEmptyOrWhitespaces(string dateOnlyString)
    {
        //Arrange
        byte[] bytes = Encoding.UTF8.GetBytes(dateOnlyString);
        var reader = new Utf8JsonReader(bytes.AsSpan());
        Type typeToConvert = typeof(DateOnly);
        var options = new JsonSerializerOptions();

        //Act
        reader.Read();
        DateOnly deserializedDateOnly = _dateOnlyJsonConverter.Read(
            ref reader,
            typeToConvert,
            options);

        //Assert
        Assert.Equal(1, deserializedDateOnly.Year);
        Assert.Equal(1, deserializedDateOnly.Month);
        Assert.Equal(1, deserializedDateOnly.Day);
    }

    [Fact]
    public void Write_ShouldReturnStringDateOnly_WhenPassingValue()
    {
        //Arrange
        var dateOnly = new DateOnly(1990, 12, 30);
        var stream = new MemoryStream();
        var writer = new Utf8JsonWriter(stream);
        var options = new JsonSerializerOptions();
        const string expectedValue = "\"1990-12-30\"";

        //Act
        _dateOnlyJsonConverter.Write(writer, dateOnly, options);
        writer.Flush();
        string dateOnlyString = Encoding.UTF8.GetString(stream.ToArray());

        //Assert
        Assert.Equal(expectedValue, dateOnlyString);
    }
}
