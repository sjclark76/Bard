using System;
using Bard.Infrastructure;
using Snapshooter;
using Snapshooter.Core;
using Snapshooter.Core.Serialization;

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
                        new SnapshotSerializer(new GlobalSnapshotSettingsResolver()),
                        new SnapshotFileHandler(),
                        new SnapshotEnvironmentCleaner(
                            new SnapshotFileHandler()),
                        new JsonSnapshotComparer(
                            new BardAssert(),
                            new SnapshotSerializer(new GlobalSnapshotSettingsResolver()))),
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
            _logWriter.LogMessage($"SNAPSHOT: {GetFullSnapshotPath(snapshotFullName)}");

            try
            {
                snapShooter.AssertSnapshot(content, snapshotFullName, matchOptions);
            }
            catch (BardSnapshotException)
            {
                _logWriter.LogMessage($"MISMATCH: {GetFullMismatchPath(snapshotFullName)}");
                throw;
            }
        }

        private static string GetFullSnapshotPath(SnapshotFullName snapshotFullName)
        {
            return $"{snapshotFullName.FolderPath}\\__snapshots__\\{snapshotFullName.Filename}";
        }
        
        private static string GetFullMismatchPath(SnapshotFullName snapshotFullName)
        {
            return $"{snapshotFullName.FolderPath}\\__snapshots__\\__mismatch__\\{snapshotFullName.Filename}";
        }
    }
}