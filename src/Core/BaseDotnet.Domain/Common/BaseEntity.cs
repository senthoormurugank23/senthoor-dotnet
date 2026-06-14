using System;

namespace BaseDotnet.Domain.Common
{
    public abstract class BaseEntity<TId> : IAuditableEntity
    {
        public TId Id { get; protected set; } = default!;
        public DateTime CreatedAtUtc { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? LastModifiedAtUtc { get; set; }
        public string? LastModifiedBy { get; set; }

        /// <summary>
        /// Populates audit metadata for the entity. If the entity is being created (CreatedAtUtc is default),
        /// it sets the creation metadata. Otherwise, it sets the last modification metadata.
        /// </summary>
        /// <param name="userId">The ID or username of the user making the change.</param>
        /// <param name="timestamp">Optional custom timestamp. Defaults to UTC now.</param>
        public virtual void PopulateAuditMetadata(string userId, DateTime? timestamp = null)
        {
            var time = timestamp ?? DateTime.UtcNow;

            if (CreatedAtUtc == default)
            {
                CreatedAtUtc = time;
                CreatedBy = userId;
            }
            else
            {
                LastModifiedAtUtc = time;
                LastModifiedBy = userId;
            }
        }
    }
}
