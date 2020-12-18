using System;

namespace HotelApp.Models
{
    public class ActiveOrderModel
    {
        public int ActiveOrderId { get; set; }
        public int ClientId { get; set; }
        public int HotelRoomId { get; set; }
        public HotelRoomModel HotelRoom { get; set; }
        public PaymentStateEnumModel PaymentState { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
    }

    public enum PaymentStateEnumModel : byte
    {
        Undefined = 0,
        Paid ,
        Booked
    }
}
