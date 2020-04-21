using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RevDogs.DataAccess.Model
{
    public partial class RevatureDogsP2Context : DbContext
    {
        public RevatureDogsP2Context()
        {
        }

        public RevatureDogsP2Context(DbContextOptions<RevatureDogsP2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<DogTypes> DogTypes { get; set; }
        public virtual DbSet<Dogs> Dogs { get; set; }
        public virtual DbSet<Tricks> Tricks { get; set; }
        public virtual DbSet<TricksProgress> TricksProgress { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DogTypes>(entity =>
            {
                entity.ToTable("DogTypes", "RD");

                entity.Property(e => e.Breed)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Dogs>(entity =>
            {
                entity.ToTable("Dogs", "RD");

                entity.Property(e => e.AdoptionDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Age).HasDefaultValueSql("((2))");

                entity.Property(e => e.Energy).HasDefaultValueSql("((3))");

                entity.Property(e => e.Hunger).HasDefaultValueSql("((100))");

                entity.Property(e => e.IsAlive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Mood).HasDefaultValueSql("((50))");

                entity.Property(e => e.PetName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.DogType)
                    .WithMany(p => p.Dogs)
                    .HasForeignKey(d => d.DogTypeId)
                    .HasConstraintName("FK__Dogs__DogTypeId__2A164134");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Dogs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Dogs__UserId__2B0A656D");
            });

            modelBuilder.Entity<Tricks>(entity =>
            {
                entity.ToTable("Tricks", "RD");

                entity.Property(e => e.TrickName)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<TricksProgress>(entity =>
            {
                entity.ToTable("TricksProgress", "RD");

                entity.Property(e => e.Progress).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Pet)
                    .WithMany(p => p.TricksProgress)
                    .HasForeignKey(d => d.PetId)
                    .HasConstraintName("FK__TricksPro__PetId__3587F3E0");

                entity.HasOne(d => d.Trick)
                    .WithMany(p => p.TricksProgress)
                    .HasForeignKey(d => d.TrickId)
                    .HasConstraintName("FK__TricksPro__Trick__367C1819");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("Users", "RD");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Score).HasDefaultValueSql("((0))");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
