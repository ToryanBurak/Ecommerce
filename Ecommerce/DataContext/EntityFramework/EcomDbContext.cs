using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataContext.EntityFramework
{
    public partial class EcomDbContext : DbContext
    {
        public EcomDbContext()
        {
        }

        public EcomDbContext(DbContextOptions<EcomDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=bursimemfurecommerce.c9wcy6ku69dt.us-east-2.rds.amazonaws.com;Database=Ecommerce;User Id=admin;Password=2605.Burak;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Guid)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrlId).HasColumnName("ImageUrlID");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SaltString)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Tc)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("TC");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VerifyConfirmCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
