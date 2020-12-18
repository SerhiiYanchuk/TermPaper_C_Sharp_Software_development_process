using HotelApp.DAL.Entities;
using System;
using System.Collections.Generic;

namespace HotelApp.DAL.Interfaces
{
    public interface IHotelRoomRepository: IRepository<HotelRoom>
    {
        public void LoadHotel(HotelRoom room);
        public void LoadActiveOrders(HotelRoom room);
        public void LoadClients(HotelRoom room);

        //public IEnumerable<HotelRoom> FindFreeRooms(TypeSizeEnum size, TypeComfortEnum comfort, DateTime checkInDate, DateTime? checkOutDate = null);
        //public IEnumerable<HotelRoom> GetRoomsPage(int pageIndex, int pageSize = 5, int hotelId = 0);
    }
}
