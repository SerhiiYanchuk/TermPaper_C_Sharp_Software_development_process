

namespace HotelApp.DAL.Entities
{
    public class TypeComfort
    {
        public int TypeComfortId { get; set; }
        public TypeComfortEnum Comfort { get; set; }
    }

    public enum TypeComfortEnum : byte
    {
        Standart = 1,
        Suite,
        De_Luxe,
        Duplex,
        Family_Room,
        Honeymoon_Room
    }
}
