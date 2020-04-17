using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RevDogsApi.Models
{
    public partial class Project2Context : DbContext
    {
        public Project2Context()
        {
        }

        public Project2Context(DbContextOptions<Project2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<DogTypes> DogTypes { get; set; }
        public virtual DbSet<Dogs> Dogs { get; set; }
        public virtual DbSet<Tricks> Tricks { get; set; }
        public virtual DbSet<TricksProgress> TricksProgress { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(SecretConfiguration.connetionString);
            }
        }

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

                entity.Property(e => e.Mood)
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('Happy')");

                entity.Property(e => e.PetName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.DogType)
                    .WithMany(p => p.Dogs)
                    .HasForeignKey(d => d.DogTypeId)
                    .HasConstraintName("FK__Dogs__DogTypeId__797309D9");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Dogs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Dogs__UserId__7A672E12");
            });

            modelBuilder.Entity<Tricks>(entity =>
            {
                entity.ToTable("Tricks", "RD");

                entity.Property(e => e.TrickBenefit)
                    .IsRequired()
                    .HasMaxLength(30);

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
                    .HasConstraintName("FK__TricksPro__PetId__04E4BC85");

                entity.HasOne(d => d.Trick)
                    .WithMany(p => p.TricksProgress)
                    .HasForeignKey(d => d.TrickId)
                    .HasConstraintName("FK__TricksPro__Trick__05D8E0BE");
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
