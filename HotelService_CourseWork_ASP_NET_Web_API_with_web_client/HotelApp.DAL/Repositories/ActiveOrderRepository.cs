using HotelApp.DAL.Interfaces;
using HotelApp.DAL.Entities;
using HotelApp.DAL.EF;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace HotelApp.DAL.Repositories
{
    public class ActiveOrderRepository : Repository<ActiveOrder>, IActiveOrderRepository
    {
        public ActiveOrderRepository(HotelDbContext context) : base(context)
        {
        }
        public override bool CheckAvailability(int id)
        {
            return context.ActiveOrders.Any(p => p.ActiveOrderId == id);
        }
    }
}
