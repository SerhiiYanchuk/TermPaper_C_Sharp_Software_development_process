using HotelAppWPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using HotelAppWPF.Commands;

namespace HotelAppWPF.ViewModel
{

    public class ApplicationViewModel : INotifyPropertyChanged
    {
        
        private ObservableCollection<HotelModel> hotels = new ObservableCollection<HotelModel>();
        private ObservableCollection<ClientModel> clients = new ObservableCollection<ClientModel>();
        private ObservableCollection<ActiveOrderModel> orders = new ObservableCollection<ActiveOrderModel>();
        private ObservableCollection<HotelRoomModel> rooms = new ObservableCollection<HotelRoomModel>();


        private RelayCommand addHotelCommand;
        private HotelModel newHotel = new HotelModel { Name = "", Address = "" };

        private RelayCommand addClientCommand;
        private ClientModel newClient = new ClientModel { FirstName = "", LastName = "" , PhoneNumber = ""};

        private RelayCommand deleteHotelCommand;
        private HotelModel selectedHotel;

        private RelayCommand searchOrdersCommand;
        private ClientModel selectedClient;

        private RelayCommand deleteOrderCommand;
        private RelayCommand confirmPaymentCommand;
        private ActiveOrderModel selectedOrder;

        private RelayCommand searchFreeRoomsCommand;
        private RoomFilterModel roomFilter = new RoomFilterModel { TypeComfort = TypeComfortEnumModel.NoMatter, TypeSize = TypeSizeEnumModel.NoMatter};

        public ObservableCollection<HotelModel> Hotels
        {
            get { return hotels; }
            set
            {
                hotels = value;
                OnPropertyChanged(nameof(Hotels));
            }
        }
        public ObservableCollection<ClientModel> Clients
        {
            get { return clients; }
            set
            {
                clients = value;
                OnPropertyChanged(nameof(Clients));
            }
        }
        public ObservableCollection<ActiveOrderModel> Orders
        {
            get { return orders; }
            set
            {
                orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }
        public ObservableCollection<HotelRoomModel> Rooms
        {
            get { return rooms; }
            set
            {
                rooms = value;
                OnPropertyChanged(nameof(Rooms));
            }
        }

        public RoomFilterModel RoomFilter
        {
            get { return roomFilter; }
            set
            {
                roomFilter = value;
                OnPropertyChanged(nameof(RoomFilter));
            }
        }
        public RelayCommand SearchFreeRoomsCommand
        {
            get
            {
                return searchFreeRoomsCommand ??
                  (searchFreeRoomsCommand = new RelayCommand(obj =>
                  {
                      RoomFilterModel filter = obj as RoomFilterModel;
                      if (filter is null || selectedHotel is null)
                          return;
                      rooms.Clear();
             
                      filter.HotelId = selectedHotel.HotelId;
                      using (var client = new HttpClient())
                      {
                          int size = (int)filter.TypeSize;
                          int comfort = (int)filter.TypeComfort;
                          string path = "https://localhost:44364/api/rooms/free?" + "HotelId=" + filter.HotelId.ToString() + "&TypeComfort=" + comfort + "&TypeSize=" + size + "&CheckInDate="
                            + filter.CheckInDate.Value.Date.ToString("MM/dd/yyyy") + "&CheckOutDate=" + filter.CheckOutDate.Value.Date.ToString("MM/dd/yyyy");
                          //string path = "https://localhost:44364/api/rooms/free?" + "HotelId=" + filter.HotelId.ToString() + "&TypeComfort=" + comfort + "&TypeSize=" + size ;
                          var response = client.GetAsync(path).Result;
                          var result = response.Content.ReadAsStringAsync().Result;
                          if (response.StatusCode == System.Net.HttpStatusCode.OK)
                          {
                              IEnumerable<HotelRoomModel> something = JsonConvert.DeserializeObject<IEnumerable<HotelRoomModel>>(result);
                              foreach (var temp in something)
                              {
                                  rooms.Add(temp);
                              }
                              
                          }
                      }
                  },
                  obj => (obj as RoomFilterModel)?.IsValid ?? false));
            }
        }

        public HotelModel NewHotel
        {
            get { return newHotel; }
            set
            {
                newHotel = value;
                OnPropertyChanged(nameof(NewHotel));
            }
        }
        public RelayCommand AddHotelCommand
        {
            get
            {
                return addHotelCommand ??
                  (addHotelCommand = new RelayCommand(obj =>
                  {
                      HotelModel hotel = obj as HotelModel;
                      if (hotel is null)
                          return;
                      using (var client = new HttpClient())
                      {
                          var response = client.PostAsJsonAsync("https://localhost:44364/api/hotels", hotel).Result;
                          if (response.StatusCode == System.Net.HttpStatusCode.Created)
                          {
                              var result = response.Content.ReadAsStringAsync().Result;
                              HotelModel something = JsonConvert.DeserializeObject<HotelModel>(result);
                              if (something != null)
                                  Hotels.Add(something);
                              NewHotel = new HotelModel { Name = "", Address = "" };
                          }

                      }
                  }, 
                  obj => (obj as HotelModel)?.IsValid ?? false));
            }
        }

        public ClientModel NewClient
        {
            get { return newClient; }
            set
            {
                newClient = value;
                OnPropertyChanged(nameof(NewClient));
            }
        }
        public RelayCommand AddClientCommand
        {
            get
            {
                return addClientCommand ??
                  (addClientCommand = new RelayCommand(obj =>
                  {
                      ClientModel client = obj as ClientModel;
                      if (client is null)
                          return;
                      using (var httpClient = new HttpClient())
                      {
                          var response = httpClient.PostAsJsonAsync("https://localhost:44364/api/clients", client).Result;
                          if (response.StatusCode == System.Net.HttpStatusCode.Created)
                          {
                              var result = response.Content.ReadAsStringAsync().Result;
                              ClientModel something = JsonConvert.DeserializeObject<ClientModel>(result);
                              if (something != null)
                                  Clients.Add(something);
                              NewClient = new ClientModel { FirstName = "", LastName = "", PhoneNumber = "" };
                          }
                          if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                          {
                              //var result = response.Content.ReadAsStringAsync().Result;
                              //var something = JsonConvert.DeserializeObject(result);
                              NewClient.PhoneNumber = "Number is busy";
                          }
                      }
                  },
                  obj => (obj as ClientModel)?.IsValid ?? false));
            }
        }

        public HotelModel SelectedHotel
        {
            get { return selectedHotel; }
            set
            {
                selectedHotel = value;
                OnPropertyChanged(nameof(SelectedHotel));
            }
        }
        public RelayCommand DeleteHotelCommand
        {
            get
            {
                return deleteHotelCommand ??
                  (deleteHotelCommand = new RelayCommand(obj =>
                  {
                      HotelModel hotel = obj as HotelModel;
                      if (hotel is null)
                          return;
                      int hotelId = (int) hotel.HotelId;

                      using (var client = new HttpClient())
                      {
                          string path = "https://localhost:44364/api/hotels/" + hotelId.ToString();
                          var response = client.DeleteAsync(path).Result;
                          if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                              hotels.Remove(hotel);
                      }
                  },
                  obj => (obj as HotelModel)?.IsValid ?? false));
            }
        }

        public ClientModel SelectedClient
        {
            get { return selectedClient; }
            set
            {
                selectedClient = value;
                OnPropertyChanged(nameof(SelectedClient));
            }
        }
        public RelayCommand SearchOrdersCommand
        {
            get
            {
                return searchOrdersCommand ??
                  (searchOrdersCommand = new RelayCommand(obj =>
                  {
                      ClientModel client = obj as ClientModel;
                      if (client is null)
                          return;
                      int clientId = (int)client.ClientId;
                      orders.Clear();
                      using (var httpClient = new HttpClient())
                      {
                          string path = "https://localhost:44364/api/clients/" + clientId.ToString() + "/orders";
                          var response = httpClient.GetAsync(path).Result;
                          var result = response.Content.ReadAsStringAsync().Result;
                          if (response.StatusCode == System.Net.HttpStatusCode.OK)
                          {
                              IEnumerable<ActiveOrderModel> something = JsonConvert.DeserializeObject<IEnumerable<ActiveOrderModel>>(result);
                              foreach (var temp in something)
                              {
                                  orders.Add(temp);
                              }

                          }
                      }
                  },
                  obj => (obj as ClientModel)?.IsValid ?? false));
            }
        }

        public ActiveOrderModel SelectedOrder
        {
            get { return selectedOrder; }
            set
            {
                selectedOrder = value;
                OnPropertyChanged(nameof(SelectedOrder));
            }
        }
        public RelayCommand DeleteOrderCommand
        {
            get
            {
                return deleteOrderCommand ??
                  (deleteOrderCommand = new RelayCommand(obj =>
                  {
                      ActiveOrderModel order = obj as ActiveOrderModel;
                      if (order is null)
                          return;
                      int orderId = (int)order.ActiveOrderId;

                      using (var client = new HttpClient())
                      {
                          string path = "https://localhost:44364/api/orders/" + orderId.ToString();
                          var response = client.DeleteAsync(path).Result;
                          if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                              orders.Remove(order);
                      }
                  },
                  obj => (obj as ActiveOrderModel)?.IsValid ?? false));
            }
        }
        public RelayCommand ConfirmPaymentCommand
        {
            get
            {
                return confirmPaymentCommand ??
                  (confirmPaymentCommand = new RelayCommand(obj =>
                  {
                      ActiveOrderModel order = obj as ActiveOrderModel;
                      if (order is null || order.PaymentState != PaymentStateEnumModel.Booked)
                          return;
                      int orderId = (int)order.ActiveOrderId;

                      using (var client = new HttpClient())
                      {
                          string path = "https://localhost:44364/api/orders/" + orderId.ToString() + "/paid";
                          var response = client.PutAsync(path, null).Result;
                          if (response.StatusCode == System.Net.HttpStatusCode.OK)
                              order.PaymentState = PaymentStateEnumModel.Paid;
                      }
                  },
                  obj => (obj as ActiveOrderModel)?.IsValid ?? false));
            }
        }

        public ApplicationViewModel()
        {          
            GetHotels();
            GetClients();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private void GetHotels()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync("https://localhost:44364/api/hotels").Result;
                var result = response.Content.ReadAsStringAsync().Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    IEnumerable<HotelModel> something = JsonConvert.DeserializeObject<IEnumerable<HotelModel>>(result);
                    foreach (var temp in something)
                    {
                        hotels.Add(temp);
                    }

                }            
            }
        }
        private void GetClients()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync("https://localhost:44364/api/clients").Result;
                var result = response.Content.ReadAsStringAsync().Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    IEnumerable<ClientModel> something = JsonConvert.DeserializeObject<IEnumerable<ClientModel>>(result);
                    foreach (var temp in something)
                    {
                        clients.Add(temp);
                    }

                }
            }
        }
    }
}