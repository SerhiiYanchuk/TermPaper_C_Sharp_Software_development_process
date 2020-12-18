
using System.ComponentModel.DataAnnotations;

namespace HotelApp.Models
{
    public class HotelRoomModel
    {
        public int HotelRoomId { get; set; }
        public HotelModel Hotel { get; set; }
        public int HotelId { get; set; }

        [Required(ErrorMessage = "Number is necessary")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Incorrect number")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Price is necessary")]
        public decimal PricePerDay { get; set; }
        [Required(ErrorMessage = "Type size is necessary")]
        public TypeSizeEnumModel TypeSize { get; set; }
        [Required(ErrorMessage = "Type comfort is necessary")]
        public TypeComfortEnumModel TypeComfort { get; set; }
    }
}
