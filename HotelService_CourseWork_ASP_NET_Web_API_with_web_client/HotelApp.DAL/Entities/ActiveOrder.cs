using System;

namespace HotelApp.DAL.Entities
{
    public class ActiveOrder
    {
        public int ActiveOrderId { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int HotelRoomId { get; set; }
        public HotelRoom HotelRoom { get; set; }
        public PaymentStateEnum PaymentState { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime DateRegistration { get; set; }      
    }

    public enum PaymentStateEnum: byte
    {
        P = 1,
        B
    }
}
