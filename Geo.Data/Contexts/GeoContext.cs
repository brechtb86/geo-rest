using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Geo.Rest.Data.Entities.Geo;

#nullable disable

namespace Geo.Rest.Data.Contexts
{
    public partial class GeoContext : DbContext
    {
        public GeoContext()
        {
        }

        public GeoContext(DbContextOptions<GeoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<CountryNameTranslation> CountryNameTranslations { get; set; }
        public virtual DbSet<CountryTimeZone> CountryTimeZones { get; set; }
        public virtual DbSet<State> States { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=Server-Brecht;Database=Geo;Persist Security Info=False;User ID=Geo;Password=welcome123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_100_CI_AS_SC_UTF8");

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(N'2014-01-01 01:01:01')");

                entity.Property(e => e.Flag).HasDefaultValueSql("((1))");

                entity.Property(e => e.Latitude).HasColumnType("decimal(11, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(11, 8)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.StateCode)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(N'2014-01-01 01:01:01')");

                entity.Property(e => e.WikiDataId).HasMaxLength(255);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cities_Countries");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cities_States");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Capital).HasMaxLength(255);

                entity.Property(e => e.CreatedAt).HasColumnType("date");

                entity.Property(e => e.Currency).HasMaxLength(255);

                entity.Property(e => e.CurrencySymbol).HasMaxLength(255);

                entity.Property(e => e.Emoji).HasMaxLength(191);

                entity.Property(e => e.EmojiUnicode).HasMaxLength(191);

                entity.Property(e => e.Flag).HasDefaultValueSql("((1))");

                entity.Property(e => e.Latitude).HasColumnType("decimal(11, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(11, 8)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Native).HasMaxLength(255);

                entity.Property(e => e.NumericCode).HasMaxLength(3);

                entity.Property(e => e.PhoneCode).HasMaxLength(255);

                entity.Property(e => e.Region).HasMaxLength(255);

                entity.Property(e => e.SubRegion).HasMaxLength(255);

                entity.Property(e => e.ThreeLetterIsoCode).HasMaxLength(3);

                entity.Property(e => e.Tld)
                    .HasMaxLength(255)
                    .HasColumnName("TLD");

                entity.Property(e => e.TwoLetterIsoCode).HasMaxLength(2);

                entity.Property(e => e.UpdatedAt).HasColumnType("date");

                entity.Property(e => e.WikiDataId).HasMaxLength(255);
            });

            modelBuilder.Entity<CountryNameTranslation>(entity =>
            {
                entity.Property(e => e.Language).HasMaxLength(2);

                entity.Property(e => e.Value).HasMaxLength(100);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryNameTranslations)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CountryNameTranslations_Countries");
            });

            modelBuilder.Entity<CountryTimeZone>(entity =>
            {
                entity.Property(e => e.Abbreviation).HasMaxLength(10);

                entity.Property(e => e.GmtOffsetName).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.TimeZoneName).HasMaxLength(100);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryTimeZones)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CountryTimeZones_Countries");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CreatedAt).HasColumnType("date");

                entity.Property(e => e.FipsCode).HasMaxLength(255);

                entity.Property(e => e.Flag).HasDefaultValueSql("((1))");

                entity.Property(e => e.Latitude).HasColumnType("decimal(11, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(11, 8)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.IsoCode).HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("date");

                entity.Property(e => e.WikiDataId).HasMaxLength(255);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.States)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_States_Countries");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
