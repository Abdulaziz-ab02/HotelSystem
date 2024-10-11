using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelProject.Models;

public partial class Userlogin
{
    public decimal Userloginsid { get; set; }
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = null!;
    [Required(ErrorMessage = "Password is required")]
    public string PasswordHash { get; set; } = null!;

    public decimal? Roleid { get; set; }

    public virtual Role? Role { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
