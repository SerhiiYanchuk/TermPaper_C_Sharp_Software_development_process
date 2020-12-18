
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelApp.Models
{
    public class HotelModel
    {
        public int HotelId { get; set; }

        [Required(ErrorMessage = "Hotel name is necessary")]
        [RegularExpression(@"^[a-zA-Z0-9. ]+$", ErrorMessage = "Incorrect hotel name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is necessary")]
        [RegularExpression(@"^[a-zA-Z0-9. ]+$", ErrorMessage = "Incorrect address")]
        public string Address { get; set; }
        public List<HotelRoomModel> HotelRooms { get; set; } = new List<HotelRoomModel>();
    }
}
