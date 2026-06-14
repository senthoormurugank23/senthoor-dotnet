using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BaseDotnet.Domain.Entities;

namespace BaseDotnet.Infrastructure.Persistence.Configurations
{
    public class DbMigrationHistoryConfiguration : IEntityTypeConfiguration<DbMigrationHistory>
    {
        public void Configure(EntityTypeBuilder<DbMigrationHistory> builder)
        {
            builder.ToTable("DbMigrationHistories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.ProductVersion)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.CreatedBy)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.LastModifiedBy)
                .HasMaxLength(100);
        }
    }
}
