using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BaseDotnet.Domain.Entities;

namespace BaseDotnet.Infrastructure.Persistence
{
    public class ApplicationDbContextInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ApplicationDbContextInitializer> _logger;

        public ApplicationDbContextInitializer(
            ApplicationDbContext context,
            ILogger<ApplicationDbContextInitializer> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            try
            {
                _logger.LogInformation("Checking database migrations...");
                if (_context.Database.GetPendingMigrations().Any())
                {
                    _logger.LogInformation("Applying migrations...");
                    await _context.Database.MigrateAsync();
                }
                
                _logger.LogInformation("Syncing custom migration history...");
                await SyncMigrationHistoryAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initializing the database.");
                throw;
            }
        }

        private async Task SyncMigrationHistoryAsync()
        {
            var appliedMigrations = await _context.Database.GetAppliedMigrationsAsync();
            var loggedMigrations = await _context.MigrationHistories.AsNoTracking().Select(x => x.Id).ToListAsync();

            var newMigrations = appliedMigrations.Except(loggedMigrations).ToList();

            if (newMigrations.Any())
            {
                var productVersion = typeof(DbContext).Assembly.GetName().Version?.ToString() ?? "9.0";
                foreach (var migrationId in newMigrations)
                {
                    var history = new DbMigrationHistory(migrationId, productVersion, "DbInitializer");
                    _context.MigrationHistories.Add(history);
                }

                await _context.SaveChangesAsync();
                _logger.LogInformation("Synced {Count} new migrations to the custom history tracker.", newMigrations.Count);
            }
        }
    }
}
