using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelProject.Models;

public partial class Hotel
{
    public decimal Hotelid { get; set; }

    public string Hotelname { get; set; } = null!;

    public string Hoteladress { get; set; } = null!;

    public string? Hotelphone { get; set; }

    public string? Hotelemail { get; set; }

    public string? Hoteldescription { get; set; }

    public string? Hotelimage { get; set; }

    public decimal? Cityid { get; set; }

    public string? Hotelmapiframe { get; set; }
    [NotMapped]
    public virtual IFormFile ImageFile { get; set; }

    public virtual City? City { get; set; }

    public virtual ICollection<Hotelfacility> Hotelfacilities { get; set; } = new List<Hotelfacility>();

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
