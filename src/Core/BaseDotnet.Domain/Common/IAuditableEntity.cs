using System;

namespace BaseDotnet.Domain.Common
{
    public interface IAuditableEntity
    {
        DateTime CreatedAtUtc { get; set; }
        string CreatedBy { get; set; }
        DateTime? LastModifiedAtUtc { get; set; }
        string? LastModifiedBy { get; set; }
        void PopulateAuditMetadata(string userId, DateTime? timestamp = null);
    }
}
