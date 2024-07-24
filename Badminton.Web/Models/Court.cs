﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
namespace Badminton.Web.Models;

public partial class Court
{
    public int CourtId { get; set; }

    public string CourtName { get; set; }

    public int OwnerId { get; set; }

    public string Location { get; set; }

    public string Phone { get; set; }

    public string OpeningHours { get; set; }

    public string Image { get; set; }

    public string Announcement { get; set; }

    public int? PromotionId { get; set; }

    public int Status { get; set; }

    public virtual ICollection<Evaluate> Evaluates { get; set; } = new List<Evaluate>();

    public virtual User Owner { get; set; }

    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    public virtual ICollection<SubCourt> SubCourts { get; set; } = new List<SubCourt>();
}