// ComplianceTracker.Infrastructure/Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using ComplianceTracker.Domain.Entities;

namespace ComplianceTracker.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contractor> Contractors { get; set; } = null!;
        public DbSet<DocumentType> DocumentTypes { get; set; } = null!;
        public DbSet<ContractorDocument> ContractorDocuments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Contractor
            modelBuilder.Entity<Contractor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Email).IsUnique();

                entity.HasMany(e => e.Documents)
                      .WithOne(e => e.Contractor)
                      .HasForeignKey(e => e.ContractorId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // DocumentType
            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Name).IsUnique();
            });

            // ContractorDocument
            modelBuilder.Entity<ContractorDocument>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UploadedOn).IsRequired();
                entity.Property(e => e.ExpiryDate).IsRequired();

                entity.HasOne(e => e.Contractor)
                      .WithMany(e => e.Documents)
                      .HasForeignKey(e => e.ContractorId);

                entity.HasOne(e => e.DocumentType)
                      .WithMany(e => e.ContractorDocuments)
                      .HasForeignKey(e => e.DocumentTypeId);
            });
        }
    }
}