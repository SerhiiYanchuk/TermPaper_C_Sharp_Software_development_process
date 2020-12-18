using HotelApp.DAL.Entities;
using System;
using System.Collections.Generic;

namespace HotelApp.DAL.Interfaces
{
    public interface IHotelRepository: IRepository<Hotel>
    {
        public void LoadHotelRooms(Hotel hotel);
    }
}
