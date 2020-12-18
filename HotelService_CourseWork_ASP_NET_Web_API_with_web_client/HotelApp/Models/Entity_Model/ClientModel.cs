using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelApp.Models
{
    public class ClientModel
    {
        public int ClientId { get; set; }

        [Required(ErrorMessage = "First name is necessary")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Incorrect first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is necessary")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Incorrect last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone number is necessary")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Incorrect phone number")]
        public string PhoneNumber { get; set; }
        public List<ActiveOrderModel> ActiveOrders { get; set; } = new List<ActiveOrderModel>();
        //public List<HotelRoomDTO> HotelRooms { get; set; } = new List<HotelRoomDTO>();
    }
}
