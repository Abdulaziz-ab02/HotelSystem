using System.ComponentModel.DataAnnotations.Schema;

namespace HotelProject.Models
{
    public class AboutUs
    {
        public decimal AboutUsID { get; set; }
        public string Image { get; set; }
        public string LeftHeader { get; set; }
        public string RightHeader { get; set; }
        public string Paragraph { get; set; }
        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }


    }

}
