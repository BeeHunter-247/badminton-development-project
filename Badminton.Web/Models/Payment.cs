﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Badminton.Web.Models;

public partial class Payment
{

    public int PaymentId { get; set; }

    public int UserId { get; set; }

    public decimal Tax { get; set; }

    public int? PromotionId { get; set; }

    public string PaymentContent { get; set; }

    public string PaymentCurrency { get; set; }

    public decimal? RequiredAmount { get; set; }

    public decimal TotalPrice { get; set; }

    public decimal? RefundAmount { get; set; }

    public string PaymentMethod { get; set; }

    public int PaymentStatus { get; set; }

    public DateTime PaymentDate { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Promotion Promotion { get; set; }

    public virtual User User { get; set; }
}