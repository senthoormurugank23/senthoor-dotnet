using System;
using BaseDotnet.Domain.Common;

namespace BaseDotnet.Domain.Entities
{
    public class DbMigrationHistory : BaseEntity<string>
    {
        public string ProductVersion { get; private set; } = string.Empty;

        // Required by EF Core
        private DbMigrationHistory() { }

        public DbMigrationHistory(string migrationId, string productVersion, string appliedBy, DateTime? appliedAtUtc = null)
        {
            Id = migrationId;
            ProductVersion = productVersion;
            PopulateAuditMetadata(appliedBy, appliedAtUtc ?? DateTime.UtcNow);
        }
    }
}
