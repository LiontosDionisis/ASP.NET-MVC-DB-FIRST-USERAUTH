using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TeachersMVC.Data;

public partial class TeachersMvcdbContext : DbContext
{
    public TeachersMvcdbContext()
    {
    }

    public TeachersMvcdbContext(DbContextOptions<TeachersMvcdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=TeachersMVCDB;User=dionisis;Password=12345;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.ToTable("TEACHERS");

            entity.HasIndex(e => e.UserId, "UQ_USER").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");

            entity.HasOne(d => d.User).WithOne(p => p.Teacher)
                .HasForeignKey<Teacher>(d => d.UserId)
                .HasConstraintName("FK_TEACHERS_USERS");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("USERS");

            entity.HasIndex(e => e.Lastname, "IX_LASTNAME");

            entity.HasIndex(e => e.Email, "UQ_EMAIL").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("FIRSTNAME");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("LASTNAME");
            entity.Property(e => e.Password)
                .HasMaxLength(512)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("PHONE_NUMBER");
            entity.Property(e => e.UserRole)
                .HasMaxLength(50)
                .HasColumnName("USER_ROLE");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("USERNAME");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
