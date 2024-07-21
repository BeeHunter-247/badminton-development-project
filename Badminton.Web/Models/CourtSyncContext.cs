﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Badminton.Web.Models;

public partial class CourtSyncContext : DbContext
{
    public CourtSyncContext(DbContextOptions<CourtSyncContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<CheckIn> CheckIns { get; set; }

    public virtual DbSet<Court> Courts { get; set; }

    public virtual DbSet<Evaluate> Evaluates { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<SubCourt> SubCourts { get; set; }

    public virtual DbSet<TimeSlot> TimeSlots { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {

            entity.HasKey(e => e.BookingId).HasName("PK__Booking__73951ACDA7B7ED5A");

            entity.HasKey(e => e.BookingId).HasName("PK__Booking__73951ACDC3843BD6");


            entity.ToTable("Booking");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 3)");
            entity.Property(e => e.CancellationReason).HasMaxLength(255);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.PromotionCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SubCourtId).HasColumnName("SubCourtID");
            entity.Property(e => e.TimeSlotId).HasColumnName("TimeSlotID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Owner).WithMany(p => p.BookingOwners)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__OwnerId__4E88ABD4");

            entity.HasOne(d => d.PromotionCodeNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PromotionCode)
                .HasConstraintName("FK__Booking__Promoti__5165187F");

            entity.HasOne(d => d.SubCourt).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.SubCourtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__SubCour__4F7CD00D");

            entity.HasOne(d => d.TimeSlot).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.TimeSlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__TimeSlo__5070F446");

            entity.HasOne(d => d.User).WithMany(p => p.BookingUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__UserID__4D94879B");
        });

        modelBuilder.Entity<CheckIn>(entity =>
        {

            entity.HasKey(e => e.CheckInId).HasName("PK__CheckIn__E64976A4FB5B1AF9");

            entity.HasKey(e => e.CheckInId).HasName("PK__CheckIn__E64976A4ECD974FA");


            entity.ToTable("CheckIn");

            entity.Property(e => e.CheckInId).HasColumnName("CheckInID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.CheckInTime).HasColumnType("datetime");
            entity.Property(e => e.SubCourtId).HasColumnName("SubCourtID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Booking).WithMany(p => p.CheckIns)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CheckIn__Booking__5535A963");

            entity.HasOne(d => d.SubCourt).WithMany(p => p.CheckIns)
                .HasForeignKey(d => d.SubCourtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CheckIn__SubCour__5441852A");

            entity.HasOne(d => d.User).WithMany(p => p.CheckIns)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CheckIn__UserID__5629CD9C");
        });

        modelBuilder.Entity<Court>(entity =>
        {

            entity.HasKey(e => e.CourtId).HasName("PK__Court__C3A67CFACD255A49");

            entity.HasKey(e => e.CourtId).HasName("PK__Court__C3A67CFAD521B342");


            entity.ToTable("Court");

            entity.Property(e => e.CourtId).HasColumnName("CourtID");
            entity.Property(e => e.Announcement).HasMaxLength(2000);
            entity.Property(e => e.CourtName)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.OpeningHours)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PromotionId).HasColumnName("PromotionID");

            entity.HasOne(d => d.Owner).WithMany(p => p.Courts)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Court__OwnerID__3D5E1FD2");
        });

        modelBuilder.Entity<Evaluate>(entity =>
        {

            entity.HasKey(e => e.EvaluateId).HasName("PK__Evaluate__2092E4DA0AA0AEC8");

            entity.HasKey(e => e.EvaluateId).HasName("PK__Evaluate__2092E4DA6E3518A9");


            entity.ToTable("Evaluate");

            entity.Property(e => e.EvaluateId).HasColumnName("EvaluateID");
            entity.Property(e => e.CourtId).HasColumnName("CourtID");
            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EvaluateDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Court).WithMany(p => p.Evaluates)
                .HasForeignKey(d => d.CourtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Evaluate__CourtI__59FA5E80");

            entity.HasOne(d => d.User).WithMany(p => p.Evaluates)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Evaluate__UserID__59063A47");
        });

        modelBuilder.Entity<Payment>(entity =>
        {

            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A5847C340F5");

            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A584414186F");


            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("PaymentID");
            entity.Property(e => e.ExpireDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentCurrency).HasMaxLength(10);
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PaymentStatus).IsRequired();
            entity.Property(e => e.RequiredAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payment__UserID__4AB81AF0");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {

            entity.HasKey(e => e.PromotionCode).HasName("PK__Promotio__A617E4B77605A2CD");

            entity.HasKey(e => e.PromotionCode).HasName("PK__Promotio__A617E4B7329641D6");


            entity.ToTable("Promotion");

            entity.Property(e => e.PromotionCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CourtId).HasColumnName("CourtID");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Percentage).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PromotionId)
                .ValueGeneratedOnAdd()
                .HasColumnName("PromotionID");

            entity.HasOne(d => d.Court).WithMany(p => p.Promotions)
                .HasForeignKey(d => d.CourtId)
                .HasConstraintName("FK__Promotion__Court__46E78A0C");
        });

        modelBuilder.Entity<SubCourt>(entity =>
        {

            entity.HasKey(e => e.SubCourtId).HasName("PK__SubCourt__D8ADDD0CF1D53A69");

            entity.HasKey(e => e.SubCourtId).HasName("PK__SubCourt__D8ADDD0C60740F13");

            entity.ToTable("SubCourt");

            entity.Property(e => e.SubCourtId).HasColumnName("SubCourtID");
            entity.Property(e => e.CourtId).HasColumnName("CourtID");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.PricePerHour).HasColumnType("decimal(10, 3)");
            entity.Property(e => e.TimeSlotId).HasColumnName("TimeSlotID");

            entity.HasOne(d => d.Court).WithMany(p => p.SubCourts)
                .HasForeignKey(d => d.CourtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SubCourt__CourtI__4222D4EF");

            entity.HasOne(d => d.TimeSlot).WithMany(p => p.SubCourts)
                .HasForeignKey(d => d.TimeSlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SubCourt__TimeSl__4316F928");
        });

        modelBuilder.Entity<TimeSlot>(entity =>
        {

            entity.HasKey(e => e.TimeSlotId).HasName("PK__TimeSlot__41CC1F52A7384E5F");

            entity.HasKey(e => e.TimeSlotId).HasName("PK__TimeSlot__41CC1F527877DADA");


            entity.ToTable("TimeSlot");

            entity.Property(e => e.TimeSlotId).HasColumnName("TimeSlotID");
        });

        modelBuilder.Entity<User>(entity =>
        {

            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCAC57F2A325");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__A9D105349C920AC2").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__User__C9F284566EB41505").IsUnique();

            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCACEE456CEE");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__A9D105349BC582FB").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__User__C9F28456B7B46B4B").IsUnique();


            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.AccountBalance)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Otp).HasMaxLength(60);
            entity.Property(e => e.OtpExpiration).HasColumnType("datetime");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserStatus).HasDefaultValue(0);
            entity.Property(e => e.Verify).HasDefaultValue(0);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}