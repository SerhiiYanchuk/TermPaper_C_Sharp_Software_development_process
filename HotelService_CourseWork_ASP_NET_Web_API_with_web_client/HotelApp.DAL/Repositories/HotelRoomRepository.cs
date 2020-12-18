using HotelApp.DAL.Interfaces;
using HotelApp.DAL.Entities;
using HotelApp.DAL.EF;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace HotelApp.DAL.Repositories
{
    public class HotelRoomRepository: Repository<HotelRoom>, IHotelRoomRepository
    {
        public HotelRoomRepository(HotelDbContext context): base(context)
        {
        }
        public override IEnumerable<HotelRoom> FindAll(bool isTracked = true)
        {
            IQueryable<HotelRoom> set = context.HotelRooms.Include(p => p.TypeSize).Include(p => p.TypeComfort);
            if (!isTracked)
                set.AsNoTracking();
            return set.ToList();
        }
        public override HotelRoom FindById(int id, bool isTracked = true)
        {
            IQueryable<HotelRoom> set = context.HotelRooms.Where(p => p.HotelRoomId == id).Include(p => p.TypeComfort).Include(p => p.TypeSize);
            if (!isTracked)
                set.AsNoTracking();
            return set.SingleOrDefault();        
        }
        public override IEnumerable<HotelRoom> Find(Expression<Func<HotelRoom, bool>> predicate, bool isTracked = true)
        {
            IQueryable<HotelRoom> set = context.HotelRooms.Include(p => p.TypeComfort).Include(p => p.TypeSize).Where(predicate);
            if (!isTracked)
                return set.AsNoTracking();
            return set.ToList();
        }
        public override void Insert(HotelRoom item)
        {
            item.TypeComfort = context.Set<TypeComfort>().Where(p => p.Comfort == item.TypeComfort.Comfort).SingleOrDefault();
            item.TypeSize = context.Set<TypeSize>().Where(p => p.Size == item.TypeSize.Size).SingleOrDefault();
            context.Add(item);
        }
        public override void Update(HotelRoom item)
        {
            item.TypeComfort = context.Set<TypeComfort>().Where(p => p.Comfort == item.TypeComfort.Comfort).SingleOrDefault();
            item.TypeSize = context.Set<TypeSize>().Where(p => p.Size == item.TypeSize.Size).SingleOrDefault();
            context.Update(item);
        }
        public void LoadHotel(HotelRoom room)
        {
            context.Entry(room).Reference(p => p.Hotel).Load();
        }
        public void LoadActiveOrders(HotelRoom room)
        {
            context.Entry(room).Collection(p => p.ActiveOrders).Load();
        }
        public void LoadClients(HotelRoom room)
        {
            context.Entry(room).Collection(p => p.Clients).Load();
        }
        public override bool CheckAvailability(int id)
        {
            return context.HotelRooms.Any(p => p.HotelRoomId == id);
        }

        //public IEnumerable<HotelRoom> FindFreeRooms(TypeSizeEnum size, TypeComfortEnum comfort, DateTime checkInDate, DateTime? checkOutDate = null)
        //{
        //    IQueryable<HotelRoom> rooms = context.HotelRooms.Include(p => p.TypeComfort).Include(p => p.TypeSize).Include(p => p.ActiveOrders);
        //    if (size != 0)
        //        rooms = rooms.Where(p => p.TypeSize.Size == size);
        //    if (comfort != 0)
        //        rooms = rooms.Where(p => p.TypeComfort.Comfort == comfort);
        //    if (checkOutDate is null)
        //        rooms = rooms.Where(p => p.ActiveOrders.All(t => t.CheckInDate > checkInDate || t.CheckOutDate <= checkInDate));
        //    else
        //        rooms = rooms.Where(p => p.ActiveOrders.All(t => (checkInDate > t.CheckInDate && checkInDate >= t.CheckOutDate) || (checkOutDate <= t.CheckInDate && checkOutDate < t.CheckOutDate)));
        //    return rooms.AsNoTracking().ToList();
        //}
        //public IEnumerable<HotelRoom> GetRoomsPage(int pageIndex, int pageSize = 5, int hotelId = 0)
        //{
        //    IQueryable<HotelRoom> rooms = context.HotelRooms;
        //    if (hotelId != 0)
        //        rooms = rooms.Where(p => p.HotelId == hotelId);
        //    return rooms.Include(p => p.TypeComfort).Include(p => p.TypeSize)
        //        .OrderBy(p => p.Number)
        //        .Skip((pageIndex - 1) * pageSize)
        //        .Take(pageSize)
        //        .AsNoTracking()
        //        .ToList();
        //}

    }
}
