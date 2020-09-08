using System.Diagnostics;
using System.IO;
using System.Linq;
using Snapshooter;
using Snapshooter.Core;
using Snapshooter.Exceptions;
using Snapshooter.Extensions;

namespace Bard.Internal.Then
{
    /// <summary>
    ///     A xunit snapshot full name reader is responsible to get the information
    ///     for the snapshot file from a xunit test.
    /// </summary>
    internal class BardSnapshotFullNameReader : ISnapshotFullNameReader
    {
        /// <summary>
        ///     Evaluates the snapshot full name information.
        /// </summary>
        /// <returns>The full name of the snapshot.</returns>
        public SnapshotFullName ReadSnapshotFullName()
        {
            SnapshotFullName? snapshotFullName = null;
            var stackFrames = new StackTrace(true).GetFrames();

            if (stackFrames != null)
            {
                var response = stackFrames.Select((frame, index) =>
                        new {frame.GetMethod().DeclaringType, Index = index})
                    .First(arg => arg.DeclaringType == typeof(Response));

                var testMethodFrame = stackFrames[response.Index + 1];
                var testMethod = testMethodFrame.GetMethod();

                snapshotFullName = new SnapshotFullName(
                    testMethod.ToName(),
                    Path.GetDirectoryName(testMethodFrame.GetFileName()));
            }

            if (snapshotFullName == null)
                throw new SnapshotTestException(
                    "The snapshot full name could not be evaluated. " +
                    "This error can occur, if you use the snapshot match " +
                    "within a async test helper child method. To solve this issue, " +
                    "use the Snapshot.FullName directly in the unit test to " +
                    "get the snapshot name, then reach this name to your " +
                    "Snapshot.Match method.");

            return snapshotFullName;
        }
    }
}