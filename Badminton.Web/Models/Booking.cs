﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Badminton.Web.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int UserId { get; set; }

    public int SubCourtId { get; set; }

    public int TimeSlotId { get; set; }

    public int ScheduleId { get; set; }

    public DateTime BookingDate { get; set; }

    public int BookingType { get; set; }

    public int Status { get; set; }

    public string CancellationReason { get; set; }

    public int PaymentId { get; set; }

    public virtual ICollection<CheckIn> CheckIns { get; set; } = new List<CheckIn>();

    public virtual Payment Payment { get; set; }

    public virtual Schedule Schedule { get; set; }

    public virtual SubCourt SubCourt { get; set; }

    public virtual TimeSlot TimeSlot { get; set; }

    public virtual User User { get; set; }
}