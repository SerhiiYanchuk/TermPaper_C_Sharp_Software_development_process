

namespace HotelApp.DAL.Entities
{
    public class TypeSize
    {
        public int TypeSizeId { get; set; }
        public TypeSizeEnum Size { get; set; }
    }

    public enum TypeSizeEnum: byte
    {
        SGL = 1, 
        DBL,
        DBL_TWN,
        TRPL,
        DBL_EXB, 
        TRPL_EXB
    }
}
