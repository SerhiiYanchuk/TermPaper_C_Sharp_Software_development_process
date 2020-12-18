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
    public class HotelAdminService: IHotelAdminService
    {
        private IUnitOfWork UnitOfWork { get; }
        private IMapper Mapper { get; }
        public HotelAdminService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }
        public IEnumerable<HotelDTO> FindHotels(string keyword = "")
        {
            IEnumerable<Hotel> hotels;
            if (string.IsNullOrWhiteSpace(keyword))
                hotels = UnitOfWork.Hotels.FindAll(false);
            else
                hotels = UnitOfWork.Hotels.Find(p => p.Name.Contains(keyword) || p.Address.Contains(keyword), false);
            return Mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelDTO>>(hotels);
        }
        public HotelDTO FindHotel(int hotelId)
        {
            Hotel hotel = UnitOfWork.Hotels.FindById(hotelId);
            if (!(hotel is null))
            {
                UnitOfWork.Hotels.LoadHotelRooms(hotel);
                return Mapper.Map<HotelDTO>(hotel);
            }               
            return null;
        }
        public HotelDTO InsertHotel(HotelDTO hotel)
        {
            if (hotel is null)
                throw new ArgumentNullException(nameof(hotel));
            Hotel newHotel = Mapper.Map<Hotel>(hotel);
            UnitOfWork.Hotels.Insert(newHotel);
            UnitOfWork.Save();
            return Mapper.Map<HotelDTO>(newHotel);
        }
        public bool UpdateHotel(HotelDTO hotel)
        {
            if (hotel is null)
                throw new ArgumentNullException(nameof(hotel));
            if (!UnitOfWork.Hotels.CheckAvailability(hotel.HotelId))
                return false;
            Hotel editHotel = Mapper.Map<Hotel>(hotel);
            UnitOfWork.Hotels.Update(editHotel);
            UnitOfWork.Save();
            return true;
        }
        public bool DeleteHotel(int deleteHotelId)
        {
            if (!UnitOfWork.Hotels.CheckAvailability(deleteHotelId))
                return false;
            UnitOfWork.Hotels.Delete(deleteHotelId);
            UnitOfWork.Save();
            return true;
        }
        public IEnumerable<ActiveOrderDTO> GetHotelOrders(int hotelId, OrderFilterDTO filter)
        {
            if (filter is null)
                throw new ArgumentNullException(nameof(filter));
            IEnumerable<ActiveOrder> orders = UnitOfWork.ActiveOrders.GetQuery().Include(p => p.HotelRoom).ThenInclude(p => p.TypeComfort).Include(p => p.HotelRoom).ThenInclude(p => p.TypeSize)
                 .Where(p => p.HotelRoom.HotelId == hotelId);
            if (filter.PaymentState != 0)
            {
                orders = orders.Where(p => p.PaymentState == Mapper.Map<PaymentStateEnum>(filter.PaymentState));
     
            }
            if (filter.Start != null && filter.End != null)
            {
                orders = orders.Where(p => p.CheckInDate >= filter.Start && p.CheckInDate <= filter.End);          
            }
            orders = orders.OrderBy(p => p.CheckInDate);
            return Mapper.Map<IEnumerable<ActiveOrder>, IEnumerable<ActiveOrderDTO>>(orders.ToList());
        }
        public InfoHotelDTO GetHotelInfo(int hotelId)
        {
            if (!UnitOfWork.Hotels.CheckAvailability(hotelId))
                return null;
            int paidOrders = UnitOfWork.ActiveOrders.GetQuery().Where(p => p.HotelRoom.HotelId == hotelId).Where(p => p.PaymentState == PaymentStateEnum.P).Count();
            int bookedOrders = UnitOfWork.ActiveOrders.GetQuery().Where(p => p.HotelRoom.HotelId == hotelId).Where(p => p.PaymentState == PaymentStateEnum.B).Count();
            int rooms = UnitOfWork.HotelRooms.GetQuery().Where(p => p.HotelId == hotelId).Count();
            return new InfoHotelDTO { quantityRooms = rooms, quantityPaidOrders = paidOrders, quantityBookedOrders = bookedOrders };
        }
        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
