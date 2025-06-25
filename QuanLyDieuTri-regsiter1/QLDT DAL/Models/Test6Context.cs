using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QLDT_DAL.Models;

public partial class Test6Context : DbContext
{
    public Test6Context()
    {
    }

    public Test6Context(DbContextOptions<Test6Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Examination> Examinations { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<InfoDoctor> InfoDoctors { get; set; }

    public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TreatmentProcess> TreatmentProcesses { get; set; }

    public virtual DbSet<TreatmentService> TreatmentServices { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(" Data Source=DESKTOP-450FEQU\\COPYGIAOLANG;Initial Catalog=test6;Persist Security Info=True;User ID=sa;Password=12345;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.IdBlog).HasName("PK__Blog__F1F67AB80E06F3E5");

            entity.ToTable("Blog");

            entity.Property(e => e.IdBlog).HasColumnName("ID_Blog");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__35ABFDE0FC6C3D4C");

            entity.ToTable("Booking");

            entity.Property(e => e.BookingId).HasColumnName("Booking_ID");
            entity.Property(e => e.CreateAt).HasColumnName("create_at");
            entity.Property(e => e.MemberId).HasColumnName("Member_ID");
            entity.Property(e => e.StatusBooking)
                .HasMaxLength(50)
                .HasColumnName("status_booking");
            entity.Property(e => e.TreatmentServiceId).HasColumnName("TreatmentService_ID");

            entity.HasOne(d => d.Member).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__Member___48CFD27E");

            entity.HasOne(d => d.TreatmentService).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.TreatmentServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__Treatme__47DBAE45");
        });

        modelBuilder.Entity<Examination>(entity =>
        {
            entity.HasKey(e => e.ExaminationId).HasName("PK__Examinat__D05D757C9AB73440");

            entity.ToTable("Examination");

            entity.Property(e => e.ExaminationId).HasColumnName("Examination_ID");
            entity.Property(e => e.BookingId).HasColumnName("Booking_ID");
            entity.Property(e => e.DateMeet).HasColumnName("date_meet");
            entity.Property(e => e.DoctorUserId).HasColumnName("Doctor_User_ID");

            entity.HasOne(d => d.Booking).WithMany(p => p.Examinations)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Examinati__Booki__4BAC3F29");

            entity.HasOne(d => d.DoctorUser).WithMany(p => p.Examinations)
                .HasForeignKey(d => d.DoctorUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Examinati__Docto__4CA06362");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__CD3992F8D1AC427F");

            entity.ToTable("Feedback");

            entity.Property(e => e.FeedbackId).HasColumnName("Feedback_ID");
            entity.Property(e => e.ContentFeedback).HasColumnName("content_feedback");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("Create_at");
            entity.Property(e => e.MemberId).HasColumnName("Member_ID");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.TargetType)
                .HasMaxLength(50)
                .HasColumnName("Target_type");

            entity.HasOne(d => d.Member).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feedback__Member__59063A47");
        });

        modelBuilder.Entity<InfoDoctor>(entity =>
        {
            entity.HasKey(e => e.InfoId).HasName("PK__Info_doc__072F05078847EF0C");

            entity.ToTable("Info_doctor");

            entity.HasIndex(e => e.UserId, "UQ__Info_doc__206D91914AC0BB07").IsUnique();

            entity.Property(e => e.InfoId).HasColumnName("Info_ID");
            entity.Property(e => e.Certificate)
                .HasMaxLength(255)
                .HasColumnName("certificate");
            entity.Property(e => e.Degree)
                .HasMaxLength(100)
                .HasColumnName("degree");
            entity.Property(e => e.ExperianYear).HasColumnName("experian_year");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.Speciality)
                .HasMaxLength(255)
                .HasColumnName("speciality");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.User).WithOne(p => p.InfoDoctor)
                .HasForeignKey<InfoDoctor>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Info_doct__User___3F466844");
        });

        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__Medical___603A0C60BE51E3EA");

            entity.ToTable("Medical_Record");

            entity.Property(e => e.RecordId).HasColumnName("Record_ID");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.MemberId).HasColumnName("Member_ID");

            entity.HasOne(d => d.Member).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Medical_R__Membe__4F7CD00D");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Member__42A68F27FC214AC1");

            entity.ToTable("Member");

            entity.HasIndex(e => e.UserId, "UQ__Member__206D9191DF8993C5").IsUnique();

            entity.Property(e => e.MemberId).HasColumnName("Member_ID");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.User).WithOne(p => p.Member)
                .HasForeignKey<Member>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Member__User_ID__4316F928");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__8C1160B535789810");

            entity.ToTable("Notification");

            entity.Property(e => e.NotificationId).HasColumnName("Notification_ID");
            entity.Property(e => e.ContentNoti).HasColumnName("content_noti");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.IsRead).HasColumnName("Is_read");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__User___5DCAEF64");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__Result__5E08F543F27C79EF");

            entity.ToTable("Result");

            entity.Property(e => e.ResultId).HasColumnName("Result_ID");
            entity.Property(e => e.ExaminationId).HasColumnName("Examination_ID");
            entity.Property(e => e.ResultTest).HasColumnName("Result_test");

            entity.HasOne(d => d.Examination).WithMany(p => p.Results)
                .HasForeignKey(d => d.ExaminationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Result__Examinat__52593CB8");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__D80AB49B939E4792");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("Role_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TreatmentProcess>(entity =>
        {
            entity.HasKey(e => e.TreatmentProcessId).HasName("PK__Treatmen__AB6EF780E15F307F");

            entity.ToTable("TreatmentProcess");

            entity.Property(e => e.TreatmentProcessId).HasColumnName("TreatmentProcess_ID");
            entity.Property(e => e.DateTreatment).HasColumnName("date_treatment");
            entity.Property(e => e.Descriptions).HasColumnName("descriptions");
            entity.Property(e => e.ExaminationId).HasColumnName("Examination_ID");
            entity.Property(e => e.PlanTreatment).HasColumnName("plan_treatment");
            entity.Property(e => e.RecordId).HasColumnName("Record_ID");

            entity.HasOne(d => d.Examination).WithMany(p => p.TreatmentProcesses)
                .HasForeignKey(d => d.ExaminationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Treatment__Exami__5629CD9C");

            entity.HasOne(d => d.Record).WithMany(p => p.TreatmentProcesses)
                .HasForeignKey(d => d.RecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Treatment__Recor__5535A963");
        });

        modelBuilder.Entity<TreatmentService>(entity =>
        {
            entity.HasKey(e => e.TreatmentServiceId).HasName("PK__Treatmen__7AD5BD10DC2DA33B");

            entity.ToTable("TreatmentService");

            entity.Property(e => e.TreatmentServiceId).HasColumnName("TreatmentService_ID");
            entity.Property(e => e.Descriptions).HasColumnName("descriptions");
            entity.Property(e => e.Durations)
                .HasMaxLength(50)
                .HasColumnName("durations");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__206D9190E59348B5");

            entity.ToTable("User");

            entity.HasIndex(e => e.UserName, "UQ__User__7C9273C4B02D8215").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__User__A9D10534663DBA80").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("User_ID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.EmailConfirmationToken).HasMaxLength(255);
            entity.Property(e => e.NewEmail).HasMaxLength(255);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("Role_ID");
            entity.Property(e => e.TokenExpiration).HasColumnType("datetime");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .HasColumnName("user_name");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__Role_ID__3A81B327");

            entity.HasMany(d => d.IdBlogs).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "StaffBlog",
                    r => r.HasOne<Blog>().WithMany()
                        .HasForeignKey("IdBlog")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Staff_Blo__ID_Bl__619B8048"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Staff_Blo__User___60A75C0F"),
                    j =>
                    {
                        j.HasKey("UserId", "IdBlog").HasName("PK__Staff_Bl__BF72F63B6B153E53");
                        j.ToTable("Staff_Blog");
                        j.IndexerProperty<int>("UserId").HasColumnName("User_ID");
                        j.IndexerProperty<int>("IdBlog").HasColumnName("ID_Blog");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
