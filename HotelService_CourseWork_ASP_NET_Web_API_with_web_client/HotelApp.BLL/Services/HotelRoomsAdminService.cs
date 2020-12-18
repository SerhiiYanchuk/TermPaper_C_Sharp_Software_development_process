using AutoMapper;
using HotelApp.BLL.DTO;
using HotelApp.BLL.Interfaces;
using HotelApp.DAL.Entities;
using HotelApp.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelApp.BLL.Services
{
    public class HotelRoomsAdminService: IHotelRoomsAdminService
    {
        private IUnitOfWork UnitOfWork { get; }
        private IMapper Mapper { get; }
        public HotelRoomsAdminService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }
        private IEnumerable<HotelRoom> FreeRoomsFilter(HotelRoomSeachFilterDTO filter)
        {
            TypeComfortEnum comfort = Mapper.Map<TypeComfortEnum>(filter.TypeComfort);
            TypeSizeEnum size = Mapper.Map<TypeSizeEnum>(filter.TypeSize);
            IQueryable<HotelRoom> rooms = UnitOfWork.HotelRooms.GetQuery().Include(p => p.TypeComfort).Include(p => p.TypeSize).Include(p => p.ActiveOrders);
            if (filter.HotelId != 0)
                rooms = rooms.Where(p => p.HotelId == filter.HotelId);
            if (size != 0)
                rooms = rooms.Where(p => p.TypeSize.Size == size);
            if (comfort != 0)
                rooms = rooms.Where(p => p.TypeComfort.Comfort == comfort);
            if (filter.CheckOutDate is null)
                rooms = rooms.Where(p => p.ActiveOrders.All(t => t.CheckInDate > filter.CheckInDate || t.CheckOutDate <= filter.CheckInDate));
            else
                rooms = rooms.Where(p => p.ActiveOrders.All(t => (filter.CheckInDate > t.CheckInDate && filter.CheckInDate >= t.CheckOutDate) || (filter.CheckOutDate <= t.CheckInDate && filter.CheckOutDate < t.CheckOutDate)));
            return rooms.AsNoTracking().ToList();
        }
        public IEnumerable<FreeHotelRoomDTO> SearchFreeRooms(HotelRoomSeachFilterDTO filter)
        {
            if (filter is null)
                throw new ArgumentNullException(nameof(filter));

            IEnumerable<HotelRoom> rooms = FreeRoomsFilter(filter);

            List<FreeHotelRoomDTO> result = new List<FreeHotelRoomDTO>();
            if (!rooms.Any())  // rooms.Count() не гуд, потому что будет перебирать всю колекцию
                return result;

            if (!(filter.CheckOutDate is null))
            {
                result = Mapper.Map<IEnumerable<HotelRoom>, List<FreeHotelRoomDTO>>(rooms);
                foreach (var room in result)
                {
                    room.CheckInDate = filter.CheckInDate;
                    room.MaxCheckOutDate = filter.CheckOutDate;
                }
                return result;
            }

            foreach (var room in rooms) // search for a period of time the room is free
            {
                DateTime? minDate = null;
                foreach (var date in room.ActiveOrders)
                {
                    if (date.CheckInDate > filter.CheckInDate && (minDate is null || minDate > date.CheckInDate))
                        minDate = date.CheckInDate;
                }
                var temp = Mapper.Map<HotelRoom, FreeHotelRoomDTO>(room);
                temp.CheckInDate = filter.CheckInDate;
                temp.MaxCheckOutDate = minDate;
                result.Add(temp);
            }
            return result;
        }
        public PageDTO<HotelRoomDTO> ShowRoomsPage(int pageIndex = 1, int pageSize = 5, int hotelId = 0)
        {
            IQueryable<HotelRoom> rooms = UnitOfWork.HotelRooms.GetQuery();
            if (hotelId != 0)
                rooms = rooms.Where(p => p.HotelId == hotelId);
            rooms = rooms.Include(p => p.TypeComfort).Include(p => p.TypeSize)
                .OrderBy(p => p.Number)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking();

            int size;
            if (hotelId == 0)
                size = UnitOfWork.HotelRooms.GetQuery().Count();
            else
                size = UnitOfWork.HotelRooms.GetQuery().Where(p => p.HotelId == hotelId).Count();

            IEnumerable<HotelRoomDTO> t = Mapper.Map<List<HotelRoom>, IEnumerable<HotelRoomDTO>>(rooms.ToList()); 

            return new PageDTO<HotelRoomDTO> { Count = size, Items = t };
            
        }
        public IEnumerable<HotelRoomDTO> FindRoomsByHotelId(int hotelId)
        {
            IEnumerable<HotelRoom> rooms = UnitOfWork.HotelRooms.Find(p => p.HotelId == hotelId, false);
            return Mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDTO>>(rooms);
        }
        public HotelRoomDTO FindRoom(int hotelRoomId)
        {
            HotelRoom room = UnitOfWork.HotelRooms.FindById(hotelRoomId, false);
            if (!(room is null))
                return Mapper.Map<HotelRoomDTO>(room);
            return null;
        }
        public HotelRoomDTO InsertRoom(HotelRoomDTO room)
        {
            if (room is null)
                throw new ArgumentNullException(nameof(room));
            HotelRoom newRoom = Mapper.Map<HotelRoom>(room);
            UnitOfWork.HotelRooms.Insert(newRoom);
            UnitOfWork.Save();
            return Mapper.Map<HotelRoomDTO>(newRoom);
        }
        public bool UpdateRoom(HotelRoomDTO room)
        {
            if (room is null)
                throw new ArgumentNullException(nameof(room));
            if (!UnitOfWork.HotelRooms.CheckAvailability(room.HotelRoomId))
                return false;
            HotelRoom editRoom = Mapper.Map<HotelRoom>(room);
            UnitOfWork.HotelRooms.Update(editRoom);
            UnitOfWork.Save();
            return true;
        }
        public bool DeleteRoom(int deleteRoomId)
        {
            if (!UnitOfWork.HotelRooms.CheckAvailability(deleteRoomId))
                return false;
            UnitOfWork.HotelRooms.Delete(deleteRoomId);
            UnitOfWork.Save();
            return true;
        }
        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
