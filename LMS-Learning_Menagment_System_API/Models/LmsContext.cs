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

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Homework> Homeworks { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationType> NotificationTypes { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:LMSConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Idcity).HasName("PK__City__36D350834BBF966B");

            entity.ToTable("City");

            entity.HasIndex(e => new { e.Name, e.CountryId }, "UQ__City__927892FDD1E8CFF3").IsUnique();

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

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Idclass).HasName("PK__Class__778C6D9BC0D63C66");

            entity.ToTable("Class");

            entity.HasIndex(e => e.ClassName, "UQ__Class__F8BF561B1EE6CC4A").IsUnique();

            entity.Property(e => e.Idclass).HasColumnName("IDClass");
            entity.Property(e => e.ClassName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Idcountry).HasName("PK__Country__D9D5A694EB07DF13");

            entity.ToTable("Country");

            entity.HasIndex(e => e.Name, "UQ__Country__737584F60317FFCC").IsUnique();

            entity.Property(e => e.Idcountry).HasColumnName("IDCountry");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Idcourse).HasName("PK__Courses__5CAFB69041CEE052");

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
                .HasConstraintName("FK__Courses__Created__52593CB8");
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.Idexam).HasName("PK__Exams__56EA5385B42B1624");

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
                .HasConstraintName("FK__Exams__CourseID__5AEE82B9");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Exams)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Exams__CreatedBy__5BE2A6F2");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.Idgrade).HasName("PK__Grades__CEDFC9FD9A2ACB2F");

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
                .HasConstraintName("FK__Grades__CourseID__619B8048");

            entity.HasOne(d => d.Exam).WithMany(p => p.Grades)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Grades__ExamID__628FA481");

            entity.HasOne(d => d.Student).WithMany(p => p.Grades)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Grades__StudentI__60A75C0F");
        });

        modelBuilder.Entity<Homework>(entity =>
        {
            entity.HasKey(e => e.Idhomework).HasName("PK__Homework__576CC0C3D5A865DB");

            entity.ToTable("Homework");

            entity.HasIndex(e => e.CourseId, "idx_Homework_CourseID");

            entity.Property(e => e.Idhomework).HasColumnName("IDHomework");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.AssignedByNavigation).WithMany(p => p.Homeworks)
                .HasForeignKey(d => d.AssignedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Homework__Assign__6754599E");

            entity.HasOne(d => d.Course).WithMany(p => p.Homeworks)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Homework__Course__66603565");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Idmaterial).HasName("PK__Material__C343DC5DA268975E");

            entity.HasIndex(e => e.CourseId, "idx_Materials_CourseID");

            entity.Property(e => e.Idmaterial).HasColumnName("IDMaterial");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.FileType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.MaterialName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Course).WithMany(p => p.Materials)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Materials__Cours__5629CD9C");

            entity.HasOne(d => d.UploadedByNavigation).WithMany(p => p.Materials)
                .HasForeignKey(d => d.UploadedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Materials__Uploa__571DF1D5");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Idmessage).HasName("PK__Messages__195595ECD9F3E0DF");

            entity.HasIndex(e => new { e.SenderId, e.ReceiverId }, "idx_Messages_SenderID_ReceiverID");

            entity.Property(e => e.Idmessage).HasColumnName("IDMessage");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.MessageText)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ReceiverId).HasColumnName("ReceiverID");
            entity.Property(e => e.SenderId).HasColumnName("SenderID");
            entity.Property(e => e.SentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Receiver).WithMany(p => p.MessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Messages__Receiv__7A672E12");

            entity.HasOne(d => d.Sender).WithMany(p => p.MessageSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Messages__Sender__797309D9");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Idnotification).HasName("PK__Notifica__5456E7BC51009F53");

            entity.HasIndex(e => e.UserId, "idx_Notifications_UserID");

            entity.Property(e => e.Idnotification).HasColumnName("IDNotification");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.NotificationText)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NotificationTypeId).HasColumnName("NotificationTypeID");
            entity.Property(e => e.SentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.NotificationType).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.NotificationTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Notificat__Notif__74AE54BC");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Notificat__UserI__73BA3083");
        });

        modelBuilder.Entity<NotificationType>(entity =>
        {
            entity.HasKey(e => e.IdnotificationType).HasName("PK__Notifica__9DCD8190AD054B75");

            entity.HasIndex(e => e.TypeName, "UQ__Notifica__D4E7DFA89C7FCDEF").IsUnique();

            entity.Property(e => e.IdnotificationType).HasColumnName("IDNotificationType");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Idschedule).HasName("PK__Schedule__ECEE6B1FAB62E1C5");

            entity.HasIndex(e => e.CourseId, "idx_Schedules_CourseID");

            entity.Property(e => e.Idschedule).HasColumnName("IDSchedule");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.ExamId).HasColumnName("ExamID");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(d => d.Course).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Schedules__Cours__6B24EA82");

            entity.HasOne(d => d.Exam).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Schedules__ExamI__6C190EBB");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Iduser).HasName("PK__Users__EAE6D9DFC0FE136B");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534641FE6C3").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__Users__C9F284563C945831").IsUnique();

            entity.HasIndex(e => e.ClassId, "idx_Users_ClassID");

            entity.HasIndex(e => new { e.CountryId, e.CityId }, "idx_Users_CountryID_CityID");

            entity.Property(e => e.Iduser).HasColumnName("IDUser");
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
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
                .HasConstraintName("FK__Users__CityID__4AB81AF0");

            entity.HasOne(d => d.Class).WithMany(p => p.Users)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Users__ClassID__4CA06362");

            entity.HasOne(d => d.Country).WithMany(p => p.Users)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Users__CountryID__49C3F6B7");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Users__RoleID__4BAC3F29");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Idrole).HasName("PK__User_Rol__A1BA16C464E41A65");

            entity.ToTable("User_Roles");

            entity.HasIndex(e => e.RoleName, "UQ__User_Rol__8A2B616029207CA9").IsUnique();

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
