using System;

namespace HotelApp.Models
{
    public class HotelRoomSeachFilterModel
    {
        public int HotelId { get; set; }
        public TypeSizeEnumModel TypeSize { get; set; }
        public TypeComfortEnumModel TypeComfort { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
    }
}
