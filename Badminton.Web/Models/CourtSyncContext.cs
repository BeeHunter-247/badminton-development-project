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

    public virtual DbSet<Verify> Verifies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__73951ACDD250CD02");

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

            entity.HasOne(d => d.PromotionCodeNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PromotionCode)
                .HasConstraintName("FK__Booking__Promoti__534D60F1");

            entity.HasOne(d => d.SubCourt).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.SubCourtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__SubCour__5165187F");

            entity.HasOne(d => d.TimeSlot).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.TimeSlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__TimeSlo__52593CB8");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__UserID__5070F446");
        });

        modelBuilder.Entity<CheckIn>(entity =>
        {
            entity.HasKey(e => e.CheckInId).HasName("PK__CheckIn__E64976A48A39B7C3");

            entity.ToTable("CheckIn");

            entity.Property(e => e.CheckInId).HasColumnName("CheckInID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.CheckInTime).HasColumnType("datetime");
            entity.Property(e => e.SubCourtId).HasColumnName("SubCourtID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Booking).WithMany(p => p.CheckIns)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CheckIn__Booking__571DF1D5");

            entity.HasOne(d => d.SubCourt).WithMany(p => p.CheckIns)
                .HasForeignKey(d => d.SubCourtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CheckIn__SubCour__5629CD9C");

            entity.HasOne(d => d.User).WithMany(p => p.CheckIns)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CheckIn__UserID__5812160E");
        });

        modelBuilder.Entity<Court>(entity =>
        {
            entity.HasKey(e => e.CourtId).HasName("PK__Court__C3A67CFAA7CF3DB7");

            entity.ToTable("Court");

            entity.Property(e => e.CourtId).HasColumnName("CourtID");
            entity.Property(e => e.Announcement).HasMaxLength(2000);
            entity.Property(e => e.CourtName)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Image).IsRequired();
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
                .HasConstraintName("FK__Court__OwnerID__403A8C7D");
        });

        modelBuilder.Entity<Evaluate>(entity =>
        {
            entity.HasKey(e => e.EvaluateId).HasName("PK__Evaluate__2092E4DA7F1F24F3");

            entity.ToTable("Evaluate");

            entity.Property(e => e.EvaluateId).HasColumnName("EvaluateID");
            entity.Property(e => e.CourtId).HasColumnName("CourtID");
            entity.Property(e => e.EvaluateDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Court).WithMany(p => p.Evaluates)
                .HasForeignKey(d => d.CourtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Evaluate__CourtI__5BE2A6F2");

            entity.HasOne(d => d.User).WithMany(p => p.Evaluates)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Evaluate__UserID__5AEE82B9");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A584617A0CF");

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
                .HasConstraintName("FK__Payment__UserID__4D94879B");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromotionCode).HasName("PK__Promotio__A617E4B72A0F3C9D");

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
                .HasConstraintName("FK__Promotion__Court__49C3F6B7");
        });

        modelBuilder.Entity<SubCourt>(entity =>
        {
            entity.HasKey(e => e.SubCourtId).HasName("PK__SubCourt__D8ADDD0C6DC6C670");

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
                .HasConstraintName("FK__SubCourt__CourtI__44FF419A");

            entity.HasOne(d => d.TimeSlot).WithMany(p => p.SubCourts)
                .HasForeignKey(d => d.TimeSlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SubCourt__TimeSl__45F365D3");
        });

        modelBuilder.Entity<TimeSlot>(entity =>
        {
            entity.HasKey(e => e.TimeSlotId).HasName("PK__TimeSlot__41CC1F52F746CB11");

            entity.ToTable("TimeSlot");

            entity.Property(e => e.TimeSlotId).HasColumnName("TimeSlotID");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCACC9F33A38");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__A9D10534B0255C12").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__User__C9F28456F545C159").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
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

        modelBuilder.Entity<Verify>(entity =>
        {
            entity.HasKey(e => e.VerifyId).HasName("PK__Verify__0A2710C9C2809FD3");

            entity.ToTable("Verify");

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ExpirationDateTime).HasColumnType("datetime");
            entity.Property(e => e.Otp)
                .IsRequired()
                .HasMaxLength(60);
            entity.Property(e => e.VerifiedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Verifies)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Verify_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}