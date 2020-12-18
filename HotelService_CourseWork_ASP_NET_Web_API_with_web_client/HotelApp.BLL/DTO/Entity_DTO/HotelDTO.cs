using System.Collections.Generic;

namespace HotelApp.BLL.DTO
{
    public class HotelDTO
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<HotelRoomDTO> HotelRooms { get; set; } = new List<HotelRoomDTO>();
    }
}