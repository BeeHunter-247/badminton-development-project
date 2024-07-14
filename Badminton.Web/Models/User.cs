﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Badminton.Web.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; }

    public string FullName { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public int RoleType { get; set; }

    public string Otp { get; set; }

    public DateTime? OtpExpiration { get; set; }

    public int? Verify { get; set; }

    public int? UserStatus { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<CheckIn> CheckIns { get; set; } = new List<CheckIn>();

    public virtual ICollection<Court> Courts { get; set; } = new List<Court>();

    public virtual ICollection<Evaluate> Evaluates { get; set; } = new List<Evaluate>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}