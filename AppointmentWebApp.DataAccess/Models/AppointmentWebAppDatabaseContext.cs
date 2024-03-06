using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AppointmentWebApp.DataAccess.Models
{
    public partial class AppointmentWebAppDatabaseContext : DbContext
    {
        public AppointmentWebAppDatabaseContext()
        {
        }

        public AppointmentWebAppDatabaseContext(DbContextOptions<AppointmentWebAppDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Mlocation> Mlocations { get; set; } = null!;
        public virtual DbSet<Mrole> Mroles { get; set; } = null!;
        public virtual DbSet<Muser> Musers { get; set; } = null!;
        public virtual DbSet<RuserRole> RuserRoles { get; set; } = null!;
        public virtual DbSet<Tappointment> Tappointments { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-C2BO152;User ID=sap;Password=indocyber;Database=AppointmentWebAppDatabase;Trusted_Connection=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mlocation>(entity =>
            {
                entity.HasKey(e => e.LocationId);

                entity.ToTable("mlocation");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.LocationName)
                    .HasMaxLength(50)
                    .HasColumnName("location_name");
            });

            modelBuilder.Entity<Mrole>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("mrole");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<Muser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("muser");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("user_id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("fullname");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.Password)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<RuserRole>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ruser_role");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Role)
                    .WithMany()
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_ruser_role_mrole");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ruser_role_muser");
            });

            modelBuilder.Entity<Tappointment>(entity =>
            {
                entity.HasKey(e => e.AppointmentId);

                entity.ToTable("tappointment");

                entity.Property(e => e.AppointmentId)
                    .ValueGeneratedNever()
                    .HasColumnName("appointment_id");

                entity.Property(e => e.AppointmentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("appointment_date");

                entity.Property(e => e.AppointmentPurpose).HasColumnName("appointment_purpose");

                entity.Property(e => e.AppointmentTitle)
                    .HasMaxLength(300)
                    .HasColumnName("appointment_title");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.LocationDescription).HasColumnName("location_description");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Tappointments)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_tappointment_mlocation");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tappointments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_tappointment_muser");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
