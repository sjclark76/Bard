using System;
using Newtonsoft.Json.Linq;

namespace Bard
{
    public class BardSnapshotException : Exception
    {
        public BardSnapshotException(JObject diff) : base($"Snapshot Mismatch: {Environment.NewLine} {diff}")
        {
        }
    }
}