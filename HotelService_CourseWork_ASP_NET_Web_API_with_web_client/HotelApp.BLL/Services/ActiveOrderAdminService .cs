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
    public class ActiveOrderAdminService: IActiveOrderAdminService
    {
        private IUnitOfWork UnitOfWork { get; }
        private IMapper Mapper { get; }
        public ActiveOrderAdminService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }
        public IEnumerable<ActiveOrderDTO> FindOrders()
        {
            IEnumerable<ActiveOrder> orders = UnitOfWork.ActiveOrders.FindAll(false);
            return Mapper.Map<IEnumerable<ActiveOrder>, IEnumerable<ActiveOrderDTO>>(orders);
        }
        public ActiveOrderDTO FindOrder(int orderId)
        {
            ActiveOrder order = UnitOfWork.ActiveOrders.FindById(orderId);
            if (!(order is null))
            {
                //UnitOfWork.ActiveOrders.LoadActiveOrders(order);
                return Mapper.Map<ActiveOrderDTO>(order);
            }               
            return null;
        }
        public ActiveOrderDTO InsertOrder(ActiveOrderDTO order)
        {
            if (order is null)
                throw new ArgumentNullException(nameof(order));
            ActiveOrder newOrder = Mapper.Map<ActiveOrder>(order);
            UnitOfWork.ActiveOrders.Insert(newOrder);
            UnitOfWork.Save();
            return Mapper.Map<ActiveOrderDTO>(newOrder);
        }
        public bool UpdateOrder(ActiveOrderDTO order)
        {
            if (order is null)
                throw new ArgumentNullException(nameof(order));
            if (!UnitOfWork.Clients.CheckAvailability(order.ActiveOrderId))
                return false;
            ActiveOrder editOrder = Mapper.Map<ActiveOrder>(order);
            UnitOfWork.ActiveOrders.Update(editOrder);
            UnitOfWork.Save();
            return true;
        }
        public bool DeleteOrder(int deleteOrderId)
        {
            if (!UnitOfWork.ActiveOrders.CheckAvailability(deleteOrderId))
                return false;
            UnitOfWork.ActiveOrders.Delete(deleteOrderId);
            UnitOfWork.Save();
            return true;
        }
        public bool ConfirmPayment(int activeOrderId)
        {
            ActiveOrder order = UnitOfWork.ActiveOrders.FindById(activeOrderId);
            if (!(order is null))
            {
                order.PaymentState = PaymentStateEnum.P;
                UnitOfWork.ActiveOrders.Update(order);
                UnitOfWork.Save();
                return true;
            }
            return false;
        }
        public void Dispose()
        {
            UnitOfWork.Dispose();
        }

    }
}