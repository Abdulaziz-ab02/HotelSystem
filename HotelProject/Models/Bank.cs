using System;
using System.Collections.Generic;

namespace HotelProject.Models;

public partial class Bank
{
    public decimal Bankid { get; set; }

    public string Cardnumber { get; set; } = null!;

    public string Cvv { get; set; } = null!;

    public DateTime? Expirydate { get; set; }

    public decimal? Balance { get; set; }
}
