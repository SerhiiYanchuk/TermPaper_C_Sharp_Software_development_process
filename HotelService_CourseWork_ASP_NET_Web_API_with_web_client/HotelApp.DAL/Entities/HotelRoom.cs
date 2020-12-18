using System.Collections.Generic;

namespace HotelApp.DAL.Entities
{
    public class HotelRoom
    {
        public int HotelRoomId { get; set; }
        public string Number { get; set; }
        public decimal PricePerDay { get; set; }
        public int TypeSizeId { get; set; }
        public TypeSize TypeSize { get; set; }
        public int TypeComfortId { get; set; }
        public TypeComfort TypeComfort { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public List<ActiveOrder> ActiveOrders { get; set; } = new List<ActiveOrder>();
        public List<Client> Clients { get; set; } = new List<Client>();
     }
}
