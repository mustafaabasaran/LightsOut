using LightsOut.Domain.Models;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace LightsOut.Persistence.Context
{
    public partial class LightsOutContext : DbContext
    {
        public LightsOutContext()
        {
        }

        public LightsOutContext(DbContextOptions<LightsOutContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BoardSetting> BoardSettings { get; set; }
        public virtual DbSet<InitialState> InitialStates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<BoardSetting>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("BoardSetting_pk")
                    .IsClustered(false);

                entity.ToTable("BoardSetting");

                entity.Property(e => e.OffColor)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OnColor)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InitialState>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("InitialState_pk")
                    .IsClustered(false);

                entity.ToTable("InitialState");

                entity.HasIndex(e => new { e.Row, e.Column }, "InitialState_Row_Column_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
