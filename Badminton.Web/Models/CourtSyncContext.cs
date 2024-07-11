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
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__73951ACD62D1D01F");

            entity.ToTable("Booking");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CancellationReason)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.SubCourtId).HasColumnName("SubCourtID");
            entity.Property(e => e.TimeSlotId).HasColumnName("TimeSlotID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.SubCourt).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.SubCourtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__SubCour__5FB337D6");

            entity.HasOne(d => d.TimeSlot).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.TimeSlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__TimeSlo__60A75C0F");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__UserID__5EBF139D");
        });

        modelBuilder.Entity<CheckIn>(entity =>
        {
            entity.HasKey(e => e.CheckInId).HasName("PK__CheckIn__E64976A4E6E82F88");

            entity.ToTable("CheckIn");

            entity.Property(e => e.CheckInId).HasColumnName("CheckInID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.CheckInTime).HasColumnType("datetime");
            entity.Property(e => e.SubCourtId).HasColumnName("SubCourtID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Booking).WithMany(p => p.CheckIns)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CheckIn__Booking__6477ECF3");

            entity.HasOne(d => d.SubCourt).WithMany(p => p.CheckIns)
                .HasForeignKey(d => d.SubCourtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CheckIn__SubCour__6383C8BA");

            entity.HasOne(d => d.User).WithMany(p => p.CheckIns)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CheckIn__UserID__656C112C");
        });

        modelBuilder.Entity<Court>(entity =>
        {
            entity.HasKey(e => e.CourtId).HasName("PK__Court__C3A67CFAB97AA093");

            entity.ToTable("Court");

            entity.Property(e => e.CourtId).HasColumnName("CourtID");
            entity.Property(e => e.Announcement)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.CourtManagerId).HasColumnName("CourtManagerID");
            entity.Property(e => e.CourtName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Image).IsRequired();
            entity.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.OpeningHours)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.CourtManager).WithMany(p => p.Courts)
                .HasForeignKey(d => d.CourtManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Court__CourtMana__4E88ABD4");
        });

        modelBuilder.Entity<Evaluate>(entity =>
        {
            entity.HasKey(e => e.EvaluateId).HasName("PK__Evaluate__2092E4DA48C2034D");

            entity.ToTable("Evaluate");

            entity.Property(e => e.EvaluateId).HasColumnName("EvaluateID");
            entity.Property(e => e.Comment).IsUnicode(false);
            entity.Property(e => e.CourtId).HasColumnName("CourtID");
            entity.Property(e => e.EvaluateDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Court).WithMany(p => p.Evaluates)
                .HasForeignKey(d => d.CourtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Evaluate__CourtI__693CA210");

            entity.HasOne(d => d.User).WithMany(p => p.Evaluates)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Evaluate__UserID__68487DD7");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A5876906B2A");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.PaymentCurrency).HasMaxLength(10);
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PromotionId).HasColumnName("PromotionID");
            entity.Property(e => e.RefundAmount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RequiredAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Tax).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Promotion).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PromotionId)
                .HasConstraintName("FK__Payment__Promoti__5BE2A6F2");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payment__UserID__5AEE82B9");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromotionId).HasName("PK__Promotio__52C42F2FBE6D51B5");

            entity.ToTable("Promotion");

            entity.Property(e => e.PromotionId).HasColumnName("PromotionID");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Percentage).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PromotionCode)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SubCourt>(entity =>
        {
            entity.HasKey(e => e.SubCourtId).HasName("PK__SubCourt__D8ADDD0CF620A14B");

            entity.ToTable("SubCourt");

            entity.Property(e => e.SubCourtId).HasColumnName("SubCourtID");
            entity.Property(e => e.CourtId).HasColumnName("CourtID");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PricePerHour).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TimeSlotId).HasColumnName("TimeSlotID");

            entity.HasOne(d => d.Court).WithMany(p => p.SubCourts)
                .HasForeignKey(d => d.CourtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SubCourt__CourtI__534D60F1");

            entity.HasOne(d => d.TimeSlot).WithMany(p => p.SubCourts)
                .HasForeignKey(d => d.TimeSlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SubCourt__TimeSl__5441852A");
        });

        modelBuilder.Entity<TimeSlot>(entity =>
        {
            entity.HasKey(e => e.TimeSlotId).HasName("PK__TimeSlot__41CC1F52FFFD5ADC");

            entity.ToTable("TimeSlot");

            entity.Property(e => e.TimeSlotId).HasColumnName("TimeSlotID");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCAC599BE8A9");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__A9D1053445771040").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__User__C9F28456F7D5D9FF").IsUnique();

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
            entity.Property(e => e.Verify).HasDefaultValue(0);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}