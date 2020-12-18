using HotelApp.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.BLL.Interfaces
{
    public interface IHotelRoomsAdminService: IDisposable
    {
        public IEnumerable<FreeHotelRoomDTO> SearchFreeRooms(HotelRoomSeachFilterDTO filter);
        public PageDTO<HotelRoomDTO> ShowRoomsPage(int pageIndex = 1, int pageSize = 5, int hotelId = 0);
        public HotelRoomDTO FindRoom(int hotelRoomId);
        public HotelRoomDTO InsertRoom(HotelRoomDTO room);
        public bool UpdateRoom(HotelRoomDTO room);
        public bool DeleteRoom(int deleteRoomId);
        public IEnumerable<HotelRoomDTO> FindRoomsByHotelId(int hotelId);
    }
}
