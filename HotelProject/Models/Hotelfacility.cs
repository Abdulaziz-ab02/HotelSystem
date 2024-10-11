using System;
using System.Collections.Generic;

namespace HotelProject.Models;

public partial class Hotelfacility
{
    public decimal HotelFacilitiesid { get; set; }

    public decimal? Hotelid { get; set; }

    public decimal? Facilityid { get; set; }

    public virtual Facility? Facility { get; set; }

    public virtual Hotel? Hotel { get; set; }
}
