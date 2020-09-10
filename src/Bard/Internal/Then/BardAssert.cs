using System.Linq;
using Newtonsoft.Json.Linq;
using Snapshooter.Core;

namespace Bard.Internal.Then
{
    /// <summary>
    ///     The XunitAssert instance compares two strings with the XUnit assert utility.
    /// </summary>
    public class BardAssert : IAssert
    {
        /// <summary>
        ///     Asserts the expected snapshot and the actual snapshot
        ///     with the XUnit assert utility.
        /// </summary>
        /// <param name="expectedSnapshot">The expected snapshot.</param>
        /// <param name="actualSnapshot">The actual snapshot.</param>
        public void Assert(string expectedSnapshot, string actualSnapshot)
        {
            var expectedSnapshotJToken = JToken.Parse(expectedSnapshot);
            var actualSnapshotJToken = JToken.Parse(actualSnapshot);

            if (JToken.DeepEquals(expectedSnapshotJToken, actualSnapshotJToken))
                return;

            var snapShotDiff = FindDiff(JToken.Parse(expectedSnapshot), JToken.Parse(actualSnapshot));
            throw new BardSnapshotException(snapShotDiff);
        }

        private static JObject FindDiff(JToken expectedSnapshot, JToken actualSnapshot)
        {
            var diff = new JObject();
            if (JToken.DeepEquals(expectedSnapshot, actualSnapshot)) return diff;

            switch (expectedSnapshot.Type)
            {
                case JTokenType.Object:
                {
                    var expected = (JObject) expectedSnapshot;
                    var actual = (JObject) actualSnapshot;
                    
                    var addedKeys = expected.Properties().Select(c => c.Name)
                        .Except(actual.Properties().Select(c => c.Name)).ToArray();
                    
                    var removedKeys = actual.Properties().Select(c => c.Name)
                        .Except(expected.Properties().Select(c => c.Name));
                    
                    var unchangedKeys = expected.Properties().Where(c => JToken.DeepEquals(c.Value, actualSnapshot[c.Name]))
                        .Select(c => c.Name);
                   
                    foreach (var key in addedKeys)
                        diff[key] = new JObject
                        {
                            ["expected"] = expectedSnapshot[key]
                        };
                    
                    foreach (var key in removedKeys)
                        diff[key] = new JObject
                        {
                            ["actual"] = actualSnapshot[key]
                        };
                    
                    var potentiallyModifiedKeys =
                        expected.Properties().Select(c => c.Name).Except(addedKeys).Except(unchangedKeys);
                   
                    foreach (var key in potentiallyModifiedKeys)
                    {
                        var expectedKey = expected[key];
                        var actualKey = actual[key];

                        if (expectedKey != null && actualKey != null)
                        {
                            var foundDiff = FindDiff(expectedKey, actualKey);
                            if (foundDiff.HasValues) diff[key] = foundDiff;    
                        }
                    }
                }
                    break;
                case JTokenType.Array:
                {
                    var expected = (JArray) expectedSnapshot;
                    var actual = (JArray) actualSnapshot;
                    var plus = new JArray(expected.Except(actual, new JTokenEqualityComparer()));
                    var minus = new JArray(actual.Except(expected, new JTokenEqualityComparer()));
                    if (plus.HasValues) diff["+"] = plus;
                    if (minus.HasValues) diff["-"] = minus;
                }
                    break;
                default:
                    diff["expected"] = expectedSnapshot;
                    diff["actual"] = actualSnapshot;
                    break;
            }

            return diff;
        }
    }
}