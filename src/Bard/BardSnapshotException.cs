using System;
using Newtonsoft.Json.Linq;

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
        public BardSnapshotException(JObject diff) : base($"Snapshot Mismatch: {Environment.NewLine} {diff}")
        {
        }
    }
}