using HotelApp.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.BLL.Interfaces
{
    public interface IActiveOrderAdminService: IDisposable
    {
        public IEnumerable<ActiveOrderDTO> FindOrders();
        public ActiveOrderDTO FindOrder(int orderId);
        public ActiveOrderDTO InsertOrder(ActiveOrderDTO order);
        public bool UpdateOrder(ActiveOrderDTO order);
        public bool DeleteOrder(int deleteOrderId);
        public bool ConfirmPayment(int activeOrderId);
    }
}
