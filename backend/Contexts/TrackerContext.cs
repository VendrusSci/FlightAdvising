using Contexts.Models;
using Microsoft.EntityFrameworkCore;

namespace Contexts
{
    public partial class TrackerContext : DbContext
    {
        public TrackerContext()
        {
        }

        public TrackerContext(DbContextOptions<TrackerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TrackedItem> TrackedItems { get; set; } = null!;
        public virtual DbSet<TrackedItemValue> TrackedItemValues { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrackedItem>(entity =>
            {
                entity.HasKey(e => e.TrackedItemId);

                entity.Property(e => e.Description);

                entity.Property(e => e.DateCreated);                
            });

            modelBuilder.Entity<TrackedItemValue>(entity =>
            {
                entity.HasKey(e => e.TrackedItemValueId);

                entity.Property(e => e.TrackedItemId);

                entity.Property(e => e.Date);
                
                entity.Property(e => e.GemValue);

                entity.Property(e => e.TreasureValue);

                entity.HasOne<TrackedItem>()
                    .WithMany()
                    .HasForeignKey(e => e.TrackedItemId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
