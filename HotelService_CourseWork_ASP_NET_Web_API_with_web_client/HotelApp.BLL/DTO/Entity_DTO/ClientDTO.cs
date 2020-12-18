using System.Collections.Generic;

namespace HotelApp.BLL.DTO
{
    public class ClientDTO
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public List<ActiveOrderDTO> ActiveOrders { get; set; } = new List<ActiveOrderDTO>();
        //public List<HotelRoomDTO> HotelRooms { get; set; } = new List<HotelRoomDTO>();
    }
}
