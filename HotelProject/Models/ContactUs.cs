using System.ComponentModel.DataAnnotations.Schema;

namespace HotelProject.Models
{
    public class ContactUs
    {
        public decimal ContactUsID { get; set; }
        public string ContactImage { get; set; }
        public string Paragraph { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Iframe { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }

}
