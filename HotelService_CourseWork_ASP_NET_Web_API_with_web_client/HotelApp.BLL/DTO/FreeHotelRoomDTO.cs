using System;

namespace HotelApp.BLL.DTO
{
    public class FreeHotelRoomDTO: HotelRoomDTO
    {
        public DateTime CheckInDate { get; set; }
        public DateTime? MaxCheckOutDate { get; set; }
    }

}
