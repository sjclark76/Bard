using System.IO;
using System.Text;
using System.Text.Json;

namespace Bard.Infrastructure;

internal static class JsonDocumentExtensions
{
    internal static string ToJsonString(this JsonDocument jDoc)
    {
        using var stream = new MemoryStream();
        var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true });
        jDoc.WriteTo(writer);
        writer.Flush();
        return Encoding.UTF8.GetString(stream.ToArray());
    }

}