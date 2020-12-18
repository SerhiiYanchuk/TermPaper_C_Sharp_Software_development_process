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
    public class ClientAdminService: IClientAdminService
    {
        private IUnitOfWork UnitOfWork { get; }
        private IMapper Mapper { get; }
        public ClientAdminService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }
        public IEnumerable<ClientDTO> FindClients(ClientFilterDTO filter)
        {
            IEnumerable<Client> clients;
            if (filter is null)
            {
                clients = UnitOfWork.Clients.FindAll(false).OrderBy(p => p.LastName);
                return Mapper.Map<IEnumerable<Client>, IEnumerable<ClientDTO>>(clients);
            }
            if (!string.IsNullOrEmpty(filter.Keyword))
                clients = UnitOfWork.Clients.Find(p => p.FirstName.Contains(filter.Keyword) || p.LastName.Contains(filter.Keyword), false);
            else
                clients = UnitOfWork.Clients.FindAll(false);
            switch (filter.SortState)
            {
                case SortClientEnumDTO.LastNameAsc:
                    clients = clients.OrderBy(p => p.LastName);
                    break;
                case SortClientEnumDTO.LastNameDesc:
                    clients = clients.OrderByDescending(p => p.LastName);
                    break;
                case SortClientEnumDTO.FirstNameAsc:
                    clients = clients.OrderBy(p => p.FirstName);
                    break;
                case SortClientEnumDTO.FirstNameDesc:
                    clients = clients.OrderByDescending(p => p.FirstName);
                    break;
            }
            return Mapper.Map<IEnumerable<Client>, IEnumerable<ClientDTO>>(clients);
        }
        public ClientDTO FindClient(int clientId)
        {
            Client client = UnitOfWork.Clients.FindById(clientId);
            if (!(client is null))
            {
                UnitOfWork.Clients.LoadActiveOrdersWithRooms(client);
                return Mapper.Map<ClientDTO>(client);
            }               
            return null;
        }
        public ClientDTO FindClient(string phoneNumber)
        {
            Client client = UnitOfWork.Clients.FindByPhoneNumber(phoneNumber);
            if (!(client is null))
            {
                UnitOfWork.Clients.LoadActiveOrders(client);
                return Mapper.Map<ClientDTO>(client);
            }
            return null;
        }
        public ClientDTO InsertClient(ClientDTO client)
        {
            if (client is null)
                throw new ArgumentNullException(nameof(client));
            Client newClient = Mapper.Map<Client>(client);
            UnitOfWork.Clients.Insert(newClient);
            UnitOfWork.Save();
            return Mapper.Map<ClientDTO>(newClient);
        }
        public bool UpdateClient(ClientDTO client)
        {
            if (client is null)
                throw new ArgumentNullException(nameof(client));
            if (!UnitOfWork.Clients.CheckAvailability(client.ClientId))
                return false;
            Client editClient = Mapper.Map<Client>(client);
            UnitOfWork.Clients.Update(editClient);
            UnitOfWork.Save();
            return true;
        }
        public bool DeleteClient(int deleteClientId)
        {
            if (!UnitOfWork.Clients.CheckAvailability(deleteClientId))
                return false;
            UnitOfWork.Clients.Delete(deleteClientId);
            UnitOfWork.Save();
            return true;
        }
        public bool IsClientExist(string phoneNumber)
        {
            if (UnitOfWork.Clients.FindByPhoneNumber(phoneNumber) is null)
                return false;
            else
                return true;
        }
        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
        public IEnumerable<ActiveOrderDTO> FindClientActiveOrders(string phoneNumber, PaymentStateEnumDTO paymentState = default)
        {
            Client client = UnitOfWork.Clients.FindByPhoneNumber(phoneNumber);
            if (!(client is null))
            {
                PaymentStateEnum state = Mapper.Map<PaymentStateEnum>(paymentState);
                UnitOfWork.Clients.LoadActiveOrders(client);
                return Mapper.Map<List<ActiveOrder>, IEnumerable<ActiveOrderDTO>>(client.ActiveOrders);
            }
            return new List<ActiveOrderDTO>();
        }
        //public ActiveOrderDTO AddClientActiveOrder(ClientDTO _client, ActiveOrderDTO _order)
        //{
        //    if (_order is null)
        //        throw new ArgumentNullException(nameof(_client));
        //    if (_client is null)
        //        throw new ArgumentException(nameof(_order));

        //    ActiveOrder order = Mapper.Map<ActiveOrder>(_order);
        //    Client client = UnitOfWork.Clients.FindByPhoneNumber(_client.PhoneNumber);
        //    if (client is null)
        //    {
        //        client = Mapper.Map<Client>(_client);
        //        client.ActiveOrders.Add(order);
        //        UnitOfWork.Clients.Insert(client);
        //    }
        //    else
        //    {
        //        client.ActiveOrders.Add(order);
        //        UnitOfWork.Clients.Update(client);
        //    }
        //    UnitOfWork.Save();
        //    return Mapper.Map<ActiveOrderDTO>(order);
        //}

    }
}


