using System;

namespace HotelApp.Models
{
    public class FreeHotelRoomModel : HotelRoomModel
    {
        public DateTime CheckInDate { get; set; }
        public DateTime? MaxCheckOutDate { get; set; }
    }

}
