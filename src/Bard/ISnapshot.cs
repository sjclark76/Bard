using System;
using Snapshooter;

namespace Bard
{
    /// <summary>
    /// Fluent interface for working with snapshots.
    /// </summary>
    public interface ISnapshot
    {
        /// <summary>
        /// Creates a json snapshot of the given object and compares it with the
        /// already existing snapshot of the test.
        /// If no snapshot exists, a new snapshot will be created from the current result
        /// and saved under a certain file path, which will shown within the test message.
        /// </summary>
        /// <typeparam name="T">The type of the result/object to match.</typeparam>
        /// <param name="matchOptions">
        /// Additional compare actions, which can be applied during the snapshot comparison
        /// </param>
        void Match<T>(Func<MatchOptions, MatchOptions>? matchOptions = null);
    }
}