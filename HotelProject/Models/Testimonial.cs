using System;
using System.Collections.Generic;

namespace HotelProject.Models;

public partial class Testimonial
{
    public decimal Testimonialsid { get; set; }

    public string Contents { get; set; } = null!;

    public decimal? Rating { get; set; }

    public DateTime Testimonialdate { get; set; }

    public decimal? Usersid { get; set; }

    public string? Status { get; set; }

    public virtual User? Users { get; set; }
}
