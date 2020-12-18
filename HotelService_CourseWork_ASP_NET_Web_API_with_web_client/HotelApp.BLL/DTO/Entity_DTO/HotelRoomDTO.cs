
namespace HotelApp.BLL.DTO
{
    public class HotelRoomDTO
    {
        public int HotelRoomId { get; set; }
        public HotelDTO Hotel { get; set; }
        public int HotelId { get; set; }
        public string Number { get; set; }
        public decimal PricePerDay { get; set; }
        public TypeSizeEnumDTO TypeSize { get; set; }
        public TypeComfortEnumDTO TypeComfort { get; set; }
    }
}
