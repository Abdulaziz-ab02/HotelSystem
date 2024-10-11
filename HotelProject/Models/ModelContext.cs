using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Facility> Facilities { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<Hotelfacility> Hotelfacilities { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Testimonial> Testimonials { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Userlogin> Userlogins { get; set; }
    public virtual DbSet<ContactUs> ContactUs { get; set; }
    public virtual DbSet<AboutUs> AboutUs { get; set; }
    public virtual DbSet<Home> Home { get; set; }
    public virtual DbSet<HeaderFooter> HeaderFooter { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SID=xe)));User Id=C##Hotel;Password=Test123;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("C##HOTEL")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Bank>(entity =>
        {
            entity.HasKey(e => e.Bankid).HasName("SYS_C008604");

            entity.ToTable("BANK");

            entity.Property(e => e.Bankid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("BANKID");
            entity.Property(e => e.Balance)
                .HasColumnType("NUMBER")
                .HasColumnName("BALANCE");
            entity.Property(e => e.Cardnumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CARDNUMBER");
            entity.Property(e => e.Cvv)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("CVV");
            entity.Property(e => e.Expirydate)
                .HasColumnType("DATE")
                .HasColumnName("EXPIRYDATE");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Cityid).HasName("SYS_C008575");

            entity.ToTable("CITY");

            entity.Property(e => e.Cityid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CITYID");
            entity.Property(e => e.Cityimage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CITYIMAGE");
            entity.Property(e => e.Cityname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CITYNAME");
        });

        modelBuilder.Entity<Facility>(entity =>
        {
            entity.HasKey(e => e.Facilitiesid).HasName("SYS_C008583");

            entity.ToTable("FACILITIES");

            entity.Property(e => e.Facilitiesid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("FACILITIESID");
            entity.Property(e => e.Facilityname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FACILITYNAME");
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.Hotelid).HasName("SYS_C008579");

            entity.ToTable("HOTEL");

            entity.Property(e => e.Hotelid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOTELID");
            entity.Property(e => e.Cityid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CITYID");
            entity.Property(e => e.Hoteladress)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("HOTELADRESS");
            entity.Property(e => e.Hoteldescription)
                .HasMaxLength(900)
                .IsUnicode(false)
                .HasColumnName("HOTELDESCRIPTION");
            entity.Property(e => e.Hotelemail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("HOTELEMAIL");
            entity.Property(e => e.Hotelimage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("HOTELIMAGE");
            entity.Property(e => e.Hotelmapiframe)
                .HasColumnType("CLOB")
                .HasColumnName("HOTELMAPIFRAME");
            entity.Property(e => e.Hotelname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("HOTELNAME");
            entity.Property(e => e.Hotelphone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("HOTELPHONE");

            entity.HasOne(d => d.City).WithMany(p => p.Hotels)
                .HasForeignKey(d => d.Cityid)
                .HasConstraintName("SYS_C008580");
        });

        modelBuilder.Entity<Hotelfacility>(entity =>
        {
            entity.HasKey(e => e.HotelFacilitiesid).HasName("SYS_C008585");

            entity.ToTable("HOTELFACILITIES");

            entity.Property(e => e.HotelFacilitiesid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOTEL_FACILITIESID");
            entity.Property(e => e.Facilityid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("FACILITYID");
            entity.Property(e => e.Hotelid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOTELID");

            entity.HasOne(d => d.Facility).WithMany(p => p.Hotelfacilities)
                .HasForeignKey(d => d.Facilityid)
                .HasConstraintName("SYS_C008587");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Hotelfacilities)
                .HasForeignKey(d => d.Hotelid)
                .HasConstraintName("SYS_C008586");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Reservationid).HasName("SYS_C008598");

            entity.ToTable("RESERVATIONS");

            entity.Property(e => e.Reservationid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("RESERVATIONID");
            entity.Property(e => e.Checkindate)
                .HasColumnType("DATE")
                .HasColumnName("CHECKINDATE");
            entity.Property(e => e.Checkoutdate)
                .HasColumnType("DATE")
                .HasColumnName("CHECKOUTDATE");
            entity.Property(e => e.Reservationdate)
                .HasDefaultValueSql("SYSDATE ")
                .HasColumnType("DATE")
                .HasColumnName("RESERVATIONDATE");
            entity.Property(e => e.Roomid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROOMID");
            entity.Property(e => e.Totalprice)
                .HasColumnType("NUMBER")
                .HasColumnName("TOTALPRICE");
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERID");

            entity.HasOne(d => d.Room).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.Roomid)
                .HasConstraintName("SYS_C008600");

            entity.HasOne(d => d.User).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("SYS_C008599");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("SYS_C008561");

            entity.ToTable("ROLE");

            entity.Property(e => e.Roleid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROLEID");
            entity.Property(e => e.Rolename)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ROLENAME");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Roomid).HasName("SYS_C008592");

            entity.ToTable("ROOMS");

            entity.Property(e => e.Roomid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROOMID");
            entity.Property(e => e.Availability)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("AVAILABILITY");
            entity.Property(e => e.Capacity)
                .HasColumnType("NUMBER")
                .HasColumnName("CAPACITY");
            entity.Property(e => e.Floor)
                .HasColumnType("NUMBER")
                .HasColumnName("FLOOR");
            entity.Property(e => e.Hotelid)
                .HasColumnType("NUMBER")
                .HasColumnName("HOTELID");
            entity.Property(e => e.Pricepernight)
                .HasColumnType("NUMBER")
                .HasColumnName("PRICEPERNIGHT");
            entity.Property(e => e.Roomdescription)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ROOMDESCRIPTION");
            entity.Property(e => e.Roomnumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ROOMNUMBER");
            entity.Property(e => e.Roomtype)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ROOMTYPE");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.Hotelid)
                .HasConstraintName("SYS_C008593");
        });

        modelBuilder.Entity<Testimonial>(entity =>
        {
            entity.HasKey(e => e.Testimonialsid).HasName("SYS_C008615");

            entity.ToTable("TESTIMONIALS");

            entity.Property(e => e.Testimonialsid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TESTIMONIALSID");
            entity.Property(e => e.Contents)
                .HasMaxLength(600)
                .IsUnicode(false)
                .HasColumnName("CONTENTS");
            entity.Property(e => e.Rating)
                .HasColumnType("NUMBER")
                .HasColumnName("RATING");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("'Pending' ")
                .HasColumnName("STATUS");
            entity.Property(e => e.Testimonialdate)
                .HasColumnType("DATE")
                .HasColumnName("TESTIMONIALDATE");
            entity.Property(e => e.Usersid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERSID");

            entity.HasOne(d => d.Users).WithMany(p => p.Testimonials)
                .HasForeignKey(d => d.Usersid)
                .HasConstraintName("SYS_C008616");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Usersid).HasName("SYS_C008571");

            entity.ToTable("USERS");

            entity.Property(e => e.Usersid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERSID");
            entity.Property(e => e.Dateofbirth)
                .HasColumnType("DATE")
                .HasColumnName("DATEOFBIRTH");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FIRSTNAME");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LASTNAME");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PHONE");
            entity.Property(e => e.Userimage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USERIMAGE");
            entity.Property(e => e.Userloginsid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERLOGINSID");

            entity.HasOne(d => d.Userlogins).WithMany(p => p.Users)
                .HasForeignKey(d => d.Userloginsid)
                .HasConstraintName("SYS_C008572");
        });

        modelBuilder.Entity<Userlogin>(entity =>
        {
            entity.HasKey(e => e.Userloginsid).HasName("SYS_C008565");

            entity.ToTable("USERLOGINS");

            entity.Property(e => e.Userloginsid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERLOGINSID");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("PASSWORD_HASH");
            entity.Property(e => e.Roleid)
                .HasColumnType("NUMBER")
                .HasColumnName("ROLEID");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USERNAME");

            entity.HasOne(d => d.Role).WithMany(p => p.Userlogins)
                .HasForeignKey(d => d.Roleid)
                .HasConstraintName("SYS_C008566");
        });
        modelBuilder.Entity<ContactUs>(entity =>
        {
            entity.HasKey(e => e.ContactUsID).HasName("SYS_C000001");

            entity.ToTable("CONTACTUS");

            entity.Property(e => e.ContactUsID)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CONTACTUSID");

            entity.Property(e => e.ContactImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CONTACTIMAGE");

            entity.Property(e => e.Paragraph)
                .HasColumnType("CLOB")
                .HasColumnName("PARAGRAPH");

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ADDRESS");

            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PHONENUMBER");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");

            entity.Property(e => e.Iframe)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("IFRAME");
        });
        modelBuilder.Entity<AboutUs>(entity =>
        {
            entity.HasKey(e => e.AboutUsID).HasName("SYS_C000002");

            entity.ToTable("ABOUTUS");

            entity.Property(e => e.AboutUsID)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ABOUTUSID");

            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IMAGE");

            entity.Property(e => e.LeftHeader)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("LEFTHEADER");

            entity.Property(e => e.RightHeader)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("RIGHTHEADER");

            entity.Property(e => e.Paragraph)
                .HasColumnType("CLOB")
                .HasColumnName("PARAGRAPH");
        });
        modelBuilder.Entity<Home>(entity =>
        {
            entity.HasKey(e => e.HomeID).HasName("SYS_C000003");

            entity.ToTable("HOME");

            entity.Property(e => e.HomeID)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOMEID");

            entity.Property(e => e.SliderImage1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("SLIDERIMAGE1");

            entity.Property(e => e.SliderContent1)
                .HasColumnType("CLOB")
                .HasColumnName("SLIDERCONTENT1");

            entity.Property(e => e.SliderImage2)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("SLIDERIMAGE2");

            entity.Property(e => e.SliderContent2)
                .HasColumnType("CLOB")
                .HasColumnName("SLIDERCONTENT2");

            entity.Property(e => e.SliderImage3)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("SLIDERIMAGE3");

            entity.Property(e => e.SliderContent3)
                .HasColumnType("CLOB")
                .HasColumnName("SLIDERCONTENT3");
        });
        modelBuilder.Entity<HeaderFooter>(entity =>
        {
            entity.HasKey(e => e.HeaderFooterID).HasName("SYS_C000004");

            entity.ToTable("HEADERFOOTER");

            entity.Property(e => e.HeaderFooterID)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HEADERFOOTERID");

            entity.Property(e => e.LogoPart1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("LOGOPART1");

            entity.Property(e => e.LogoPart2)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("LOGOPART2");

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ADDRESS");

            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PHONENUMBER");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");

            entity.Property(e => e.Facebook)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("FACEBOOK");

            entity.Property(e => e.Instagram)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("INSTAGRAM");

            entity.Property(e => e.Youtube)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("YOUTUBE");

            entity.Property(e => e.Twitter)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TWITTER");

            entity.Property(e => e.CopyrightStatement)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("'© 2024 All rights reserved. Designed by Abdulaziz Ababneh'")
                .HasColumnName("COPYRIGHTSTATEMENT");
        });




        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
