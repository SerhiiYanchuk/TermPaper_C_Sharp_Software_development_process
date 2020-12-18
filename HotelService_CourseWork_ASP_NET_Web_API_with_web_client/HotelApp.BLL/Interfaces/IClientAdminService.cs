using HotelApp.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.BLL.Interfaces
{
    public interface IClientAdminService: IDisposable
    {
        public IEnumerable<ClientDTO> FindClients(ClientFilterDTO filter);
        public ClientDTO FindClient(int clientId);
        public ClientDTO FindClient(string phoneNumber);
        public ClientDTO InsertClient(ClientDTO client);
        public bool UpdateClient(ClientDTO client);
        public bool DeleteClient(int deleteClientId);
        public IEnumerable<ActiveOrderDTO> FindClientActiveOrders(string phoneNumber, PaymentStateEnumDTO paymentState = default);
        public bool IsClientExist(string phoneNumber);
        //public ActiveOrderDTO AddClientActiveOrder(ClientDTO _client, ActiveOrderDTO _order);
    }
}
