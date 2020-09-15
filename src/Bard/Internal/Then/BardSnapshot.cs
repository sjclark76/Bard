using System;
using Bard.Infrastructure;
using Snapshooter;
using Snapshooter.Core;

namespace Bard.Internal.Then
{
    internal class BardSnapshot : ISnapshot
    {
        private readonly object[] _extensions;
        private readonly LogWriter _logWriter;
        private readonly IShouldBe _shouldBe;

        public BardSnapshot(LogWriter logWriter, IShouldBe shouldBe, object[] extensions)
        {
            _logWriter = logWriter;
            _shouldBe = shouldBe;
            _extensions = extensions;
        }

        private static Snapshooter.Snapshooter SnapShooter
        {
            get
            {
                var snapShooter = new Snapshooter.Snapshooter(
                    new SnapshotAssert(
                        new SnapshotSerializer(),
                        new SnapshotFileHandler(),
                        new SnapshotEnvironmentCleaner(
                            new SnapshotFileHandler()),
                        new JsonSnapshotComparer(
                            new BardAssert(),
                            new SnapshotSerializer())),
                    new SnapshotFullNameResolver(
                        new BardSnapshotFullNameReader()));

                return snapShooter;
            }
        }

        public void Match<T>(Func<MatchOptions, MatchOptions>? matchOptions = null)
        {
            var content = _shouldBe.Content<T>();

            var snapshotExtension = SnapshotNameExtension.Create(_extensions);

            var snapShooter = SnapShooter;
            var snapshotFullName = snapShooter.ResolveSnapshotFullName(snapshotNameExtension: snapshotExtension);
            _logWriter.LogMessage("");
            _logWriter.LogMessage($"MATCHING AGAINST SNAPSHOT: {snapshotFullName.FolderPath}\\__snapshots__\\{snapshotFullName.Filename}");
            snapShooter.AssertSnapshot(content, snapshotFullName, matchOptions);
        }
    }
}