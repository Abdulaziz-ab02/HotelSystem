using System;
using System.Collections.Generic;

namespace HotelProject.Models;

public partial class Facility
{
    public decimal Facilitiesid { get; set; }

    public string Facilityname { get; set; } = null!;

    public virtual ICollection<Hotelfacility> Hotelfacilities { get; set; } = new List<Hotelfacility>();
}
