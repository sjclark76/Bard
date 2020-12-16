using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Snapshooter.Core;

namespace Bard.Internal.Then
{
    /// <summary>
    ///     The XunitAssert instance compares two strings with the XUnit assert utility.
    /// </summary>
    public class BardAssert : IAssert
    {
        private const string Expected = "expected";
        private const string Actual = "actual";

        /// <summary>
        ///     Asserts the expected snapshot and the actual snapshot
        ///     with the XUnit assert utility.
        /// </summary>
        /// <param name="expectedSnapshot">The expected snapshot.</param>
        /// <param name="actualSnapshot">The actual snapshot.</param>
        public void Assert(string expectedSnapshot, string actualSnapshot)
        {
            var expectedSnapshotJsonElement = JsonDocument.Parse(expectedSnapshot).RootElement;
            var actualSnapshotJsonElement = JsonDocument.Parse(actualSnapshot).RootElement;

            if (DeepEquals(expectedSnapshotJsonElement, actualSnapshotJsonElement))
                return;

            var snapShotDiff = FindDiff(expectedSnapshotJsonElement, actualSnapshotJsonElement);

            throw new BardSnapshotException(SerializeAndParse(snapShotDiff));
        }

        private static bool DeepEquals(JsonElement expected, JsonElement actual)
        {
            return new JsonElementComparer().Equals(expected, actual);
        }

        private static JsonDocument SerializeAndParse(object objct)
        {
            return JsonDocument.Parse(JsonSerializer.Serialize(objct, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }));
        }

        private static JsonElement GetProperty(JsonElement jsonElement, string propertyName)
        {
            var result = jsonElement.TryGetProperty(propertyName, out var newJsonElement);

            if (result == false)
                throw new BardException($"Could not find property name '{propertyName}' in snapshot.");

            return newJsonElement;
        }

        private static Dictionary<string, object> FindDiff(JsonElement expectedSnapshot, JsonElement actualSnapshot)
        {
            var diff = new Dictionary<string, object>();
            if (DeepEquals(expectedSnapshot, actualSnapshot))
                return diff;

            switch (expectedSnapshot.ValueKind)
            {
                case JsonValueKind.Object:
                {
                    var expectedKeys = expectedSnapshot.EnumerateObject().Select(c => c.Name).ToArray();
                    var actualKeys = actualSnapshot.EnumerateObject().Select(c => c.Name).ToArray();
                    
                    var addedKeys = expectedKeys
                        .Except(actualKeys).ToArray();

                    var removedKeys = actualKeys
                        .Except(expectedKeys).ToArray();

                    var unchangedKeys = expectedSnapshot.EnumerateObject()
                        .Where(expectedProperty => actualSnapshot.TryGetProperty(expectedProperty.Name, out _))
                        .Where(c => DeepEquals(c.Value, GetProperty(actualSnapshot, c.Name)))
                        .Select(c => c.Name);

                    foreach (var key in addedKeys)
                        diff[key] = new KeyValuePair<string, object>(Expected, GetProperty(expectedSnapshot, key));

                    foreach (var key in removedKeys)
                        diff[key] = new KeyValuePair<string, object>(Actual, GetProperty(actualSnapshot, key));


                    var potentiallyModifiedKeys =
                        expectedSnapshot.EnumerateObject().Select(c => c.Name).Except(addedKeys).Except(unchangedKeys);

                    foreach (var key in potentiallyModifiedKeys)
                    {
                        var expectedKey = expectedSnapshot.GetProperty(key);
                        var actualKey = actualSnapshot.GetProperty(key);

                        if (expectedKey.Equals(null) && actualKey.Equals(null))
                        {
                            var foundDiff = FindDiff(expectedKey, actualKey);
                            if (foundDiff.Count > 0)
                                diff[key] = foundDiff;
                        }
                    }
                }
                    break;
                case JsonValueKind.Array:
                {
                    //NOTE: We could make this better by sorting these + count these maybe?
                    var expected = expectedSnapshot;
                    var actual = actualSnapshot;

                    var plus = expected.EnumerateArray().Except(actual.EnumerateArray());
                    var minus = actual.EnumerateArray().Select(x => x)
                        .Except(expected.EnumerateArray().Select(x => x));

                    if (plus.Any()) diff[Expected] = plus;
                    if (minus.Any()) diff[Actual] = minus;
                }
                    break;
                default:
                    diff[Expected] = expectedSnapshot;
                    diff[Actual] = actualSnapshot;
                    break;
            }

            return diff;
        }
    }
}