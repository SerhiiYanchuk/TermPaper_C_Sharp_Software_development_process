using HotelApp.DAL.EF;
using HotelApp.DAL.Entities;
using HotelApp.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace HotelApp.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private HotelDbContext Context { get; }
        private IHotelRepository hotelRepository;
        private IHotelRoomRepository hotelRoomRepository;
        private IClientRepository clientRepository;
        private IActiveOrderRepository activeRepository;
        public UnitOfWork(HotelDbContext context)
        {
            Context = context;
        }
        public IHotelRepository Hotels
        {
            get
            {
                if (hotelRepository == null)
                    hotelRepository = new HotelRepository(Context);
                return hotelRepository;
            }
        }
        public IHotelRoomRepository HotelRooms
        {
            get
            {
                if (hotelRoomRepository == null)
                    hotelRoomRepository = new HotelRoomRepository(Context);
                return hotelRoomRepository;
            }
        }
        public IClientRepository Clients
        {
            get
            {
                if (clientRepository == null)
                    clientRepository = new ClientRepository(Context);
                return clientRepository;
            }
        }
        public IActiveOrderRepository ActiveOrders
        {
            get
            {
                if (activeRepository == null)
                    activeRepository = new ActiveOrderRepository(Context);
                return activeRepository;
            }
        }
        public void Save()
        {
            Context.SaveChanges();
        }
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
