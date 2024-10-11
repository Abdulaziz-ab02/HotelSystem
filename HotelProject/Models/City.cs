using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelProject.Models;

public partial class City
{
    public decimal Cityid { get; set; }

    public string Cityname { get; set; } = null!;

    public string? Cityimage { get; set; }
   
    [NotMapped]
    public virtual IFormFile ImageFile { get; set; }
    public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
}
