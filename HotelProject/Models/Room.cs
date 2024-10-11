using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelProject.Models;

public partial class Room
{
    [DisplayFormat(DataFormatString = "{0:F0}")]
    public decimal Roomid { get; set; }

    public string Roomnumber { get; set; } = null!;

    public string Roomtype { get; set; } = null!;

    public string? Roomdescription { get; set; }

    public decimal? Capacity { get; set; }

    public decimal? Floor { get; set; }

    public string Availability { get; set; } = null!;

    public decimal? Pricepernight { get; set; }

    public decimal? Hotelid { get; set; }

    public virtual Hotel? Hotel { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
