using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelProject.Models;

public partial class User
{
    public decimal Usersid { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string? Phone { get; set; }

    public string Email { get; set; } = null!;

    public DateTime? Dateofbirth { get; set; }

    public decimal? Userloginsid { get; set; }

    public string? Userimage { get; set; }
    [NotMapped]
    public virtual IFormFile ImageFile { get; set; }
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<Testimonial> Testimonials { get; set; } = new List<Testimonial>();

    public virtual Userlogin? Userlogins { get; set; }
}
