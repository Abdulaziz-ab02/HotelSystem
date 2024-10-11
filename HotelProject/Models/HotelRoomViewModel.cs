namespace HotelProject.Models
{
    public class HotelRoomViewModel
    {
        public string HotelName { get; set; } = string.Empty;
        public int AvailableRooms { get; set; }
        public int BookedRooms { get; set; }
    }
}
