using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.Core.Entities.Records;

namespace Server.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext // IdentityDbContext 대신 DbContext 상속
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        #region DbSet Section
        public DbSet<Case> Cases { get; set; }
        public DbSet<CaseAttachment> CaseAttachments { get; set; }
        public DbSet<CaseResolution> CaseResolutions { get; set; }
        public DbSet<CaseStatement> CaseStatements { get; set; }


        #endregion

        /// <summary>
        /// Configures the model that was discovered by convention from the entity types
        /// exposed in DbSet properties on the derived context.
        /// </summary>
        /// <param name="builder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Delegates entity configurations to a separate static class for better organization.
            ApplicationDbContextConfigurations.Configure(builder);
        }
    }
}
