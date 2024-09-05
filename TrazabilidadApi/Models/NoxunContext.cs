using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TrazabilidadApi.Models;

namespace TrazabilidadApi.Models;

public partial class NoxunContext : DbContext
{
    public NoxunContext()
    {
    }

    public NoxunContext(DbContextOptions<NoxunContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DataSet> DataSets { get; set; }

    public virtual DbSet<Field> Fields { get; set; }

    public virtual DbSet<Procedure> Procedures { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DataSet>(entity =>
        {
            entity.HasKey(e => e.DataSetId).HasName("PK__DataSets__222C05FF6D73628F");

            entity.Property(e => e.DataSetId).HasColumnName("DataSetID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DataSetName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FieldId).HasColumnName("FieldID");
            entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ProcedureId).HasColumnName("ProcedureID");

            entity.HasOne(d => d.Field).WithMany(p => p.DataSets)
                .HasForeignKey(d => d.FieldId)
                .HasConstraintName("FK__DataSets__FieldI__47DBAE45");

            entity.HasOne(d => d.Procedure).WithMany(p => p.DataSets)
                .HasForeignKey(d => d.ProcedureId)
                .HasConstraintName("FK__DataSets__Proced__46E78A0C");
        });

        modelBuilder.Entity<Field>(entity =>
        {
            entity.HasKey(e => e.FieldId).HasName("PK__Fields__C8B6FF275EDE3E11");

            entity.Property(e => e.FieldId).HasColumnName("FieldID");
            entity.Property(e => e.DataType).HasMaxLength(50);
            entity.Property(e => e.FieldName).HasMaxLength(100);
        });

        modelBuilder.Entity<Procedure>(entity =>
        {
            entity.HasKey(e => e.ProcedureId).HasName("PK__Procedur__54C2E50D76FE1B80");

            entity.Property(e => e.ProcedureId).HasColumnName("ProcedureID");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.LastModifiedUserId).HasColumnName("LastModifiedUserID");
            entity.Property(e => e.ProcedureName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.ProcedureCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK__Procedure__Creat__403A8C7D");

            entity.HasOne(d => d.LastModifiedUser).WithMany(p => p.ProcedureLastModifiedUsers)
                .HasForeignKey(d => d.LastModifiedUserId)
                .HasConstraintName("FK__Procedure__LastM__412EB0B6");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A2CEF41AF");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.RoleName)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC54A97311");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserRole__3214EC2759F91FB8");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRoles__RoleI__3C69FB99");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRoles__UserI__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
