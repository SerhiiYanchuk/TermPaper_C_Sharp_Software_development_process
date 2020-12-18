using System.Collections.Generic;

namespace HotelApp.DAL.Entities
{
    public class Hotel
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<HotelRoom> HotelRooms { get; set; } = new List<HotelRoom>();
    }
}