using System.ComponentModel.DataAnnotations.Schema;

namespace HotelProject.Models
{
    public class Home
    {
        public int HomeID { get; set; }
        public string SliderImage1 { get; set; }
        public string SliderContent1 { get; set; }
        public string SliderImage2 { get; set; }
        public string SliderContent2 { get; set; }
        public string SliderImage3 { get; set; }
        public string SliderContent3 { get; set; }
        [NotMapped]
        public IFormFile ImageFile1 { get; set; }
        [NotMapped]
        public IFormFile ImageFile2 { get; set; }
        [NotMapped]
        public IFormFile ImageFile3 { get; set; }

    }

}
