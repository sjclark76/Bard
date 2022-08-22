using System;
using System.Text.Json;
using Bard.Infrastructure;

namespace Bard
{
    /// <summary>
    /// BardSnapshotException
    /// </summary>
    public class BardSnapshotException : Exception
    {
        /// <summary>
        /// BardSnapshotException constructor
        /// </summary>
        /// <param name="diff"></param>
        public BardSnapshotException(JsonDocument diff) : base($"Snapshot Mismatch: {Environment.NewLine} {diff.ToJsonString()}")
        {
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Message;
        }
    }
}