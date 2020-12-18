using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.Models
{
    public class RoomsPageViewModel
    {
        public IEnumerable<HotelRoomModel> HotelRooms { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
