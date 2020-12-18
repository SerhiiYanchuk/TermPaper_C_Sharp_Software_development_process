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
    public class HotelRepository: Repository<Hotel>, IHotelRepository
    {
        public HotelRepository(HotelDbContext context): base(context)
        {
        }
        public void LoadHotelRooms(Hotel hotel)
        {
            context.Entry(hotel).Collection(p => p.HotelRooms).Query().Include(p => p.TypeSize).Include(p => p.TypeComfort).Load();
        }
        public override bool CheckAvailability(int id)
        {
            return context.Hotels.Any(p => p.HotelId == id);
        }
    }
}
