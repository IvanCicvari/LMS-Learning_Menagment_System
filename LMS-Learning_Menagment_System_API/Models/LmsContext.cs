using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LMS_Learning_Menagment_System_API.Models;

public partial class LmsContext : DbContext
{
    public LmsContext()
    {
    }

    public LmsContext(DbContextOptions<LmsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:LMSConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Idcity).HasName("PK__City__36D350834518391A");

            entity.ToTable("City");

            entity.HasIndex(e => new { e.Name, e.CountryId }, "UQ__City__927892FDDF9918C3").IsUnique();

            entity.HasIndex(e => e.CountryId, "idx_City_CountryID");

            entity.Property(e => e.Idcity).HasColumnName("IDCity");
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__City__CountryID__3C69FB99");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Idcountry).HasName("PK__Country__D9D5A6944CDA7076");

            entity.ToTable("Country");

            entity.HasIndex(e => e.Name, "UQ__Country__737584F67F78DC00").IsUnique();

            entity.Property(e => e.Idcountry).HasColumnName("IDCountry");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Idcourse).HasName("PK__Courses__5CAFB690567F4CB0");

            entity.HasIndex(e => e.CreatedBy, "idx_Courses_CreatedBy");

            entity.Property(e => e.Idcourse).HasColumnName("IDCourse");
            entity.Property(e => e.CourseName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Courses)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Courses__Created__4D94879B");
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.Idexam).HasName("PK__Exams__56EA53855C9C8F3F");

            entity.HasIndex(e => e.CourseId, "idx_Exams_CourseID");

            entity.Property(e => e.Idexam).HasColumnName("IDExam");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.ExamName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(d => d.Course).WithMany(p => p.Exams)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Exams__CourseID__5629CD9C");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Exams)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Exams__CreatedBy__571DF1D5");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.Idgrade).HasName("PK__Grades__CEDFC9FDDE041407");

            entity.HasIndex(e => new { e.StudentId, e.CourseId }, "idx_Grades_StudentID_CourseID");

            entity.Property(e => e.Idgrade).HasColumnName("IDGrade");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.ExamId).HasColumnName("ExamID");
            entity.Property(e => e.Grade1).HasColumnName("Grade");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.Course).WithMany(p => p.Grades)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Grades__CourseID__5CD6CB2B");

            entity.HasOne(d => d.Exam).WithMany(p => p.Grades)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Grades__ExamID__5DCAEF64");

            entity.HasOne(d => d.Student).WithMany(p => p.Grades)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Grades__StudentI__5BE2A6F2");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Idmaterial).HasName("PK__Material__C343DC5D59B67B7E");

            entity.HasIndex(e => e.CourseId, "idx_Materials_CourseID");

            entity.Property(e => e.Idmaterial).HasColumnName("IDMaterial");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.MaterialName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MaterialUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("MaterialURL");

            entity.HasOne(d => d.Course).WithMany(p => p.Materials)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Materials__Cours__5165187F");

            entity.HasOne(d => d.UploadedByNavigation).WithMany(p => p.Materials)
                .HasForeignKey(d => d.UploadedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Materials__Uploa__52593CB8");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Idnotification).HasName("PK__Notifica__5456E7BC67A9A3B5");

            entity.HasIndex(e => e.UserId, "idx_Notifications_UserID");

            entity.Property(e => e.Idnotification).HasColumnName("IDNotification");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.NotificationText)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Notificat__UserI__6754599E");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Idschedule).HasName("PK__Schedule__ECEE6B1F070B080E");

            entity.HasIndex(e => e.CourseId, "idx_Schedules_CourseID");

            entity.Property(e => e.Idschedule).HasColumnName("IDSchedule");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.ExamId).HasColumnName("ExamID");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(d => d.Course).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Schedules__Cours__619B8048");

            entity.HasOne(d => d.Exam).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Schedules__ExamI__628FA481");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Iduser).HasName("PK__Users__EAE6D9DFF6EC56E9");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534265B19E1").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__Users__C9F28456E2793E91").IsUnique();

            entity.HasIndex(e => new { e.CountryId, e.CityId }, "idx_Users_CountryID_CityID");

            entity.Property(e => e.Iduser).HasColumnName("IDUser");
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.City).WithMany(p => p.Users)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Users__CityID__46E78A0C");

            entity.HasOne(d => d.Country).WithMany(p => p.Users)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Users__CountryID__45F365D3");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Users__RoleID__47DBAE45");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Idrole).HasName("PK__User_Rol__A1BA16C4E03DB693");

            entity.ToTable("User_Roles");

            entity.HasIndex(e => e.RoleName, "UQ__User_Rol__8A2B616019151CBC").IsUnique();

            entity.Property(e => e.Idrole).HasColumnName("IDRole");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
