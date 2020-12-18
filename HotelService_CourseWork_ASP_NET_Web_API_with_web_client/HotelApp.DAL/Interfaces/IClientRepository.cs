using HotelApp.DAL.Entities;

namespace HotelApp.DAL.Interfaces
{
    public interface IClientRepository: IRepository<Client>
    {
        public void LoadActiveOrders(Client client, PaymentStateEnum paymentState = default);
        public void LoadActiveOrdersWithRooms(Client client, PaymentStateEnum paymentState = default);
        public void LoadHotelRooms(Client client);
        public Client FindByPhoneNumber(string number);
    }
}
