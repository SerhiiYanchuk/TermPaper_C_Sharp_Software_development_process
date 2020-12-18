using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace HotelAppWPF.Models
{
    public class ActiveOrderModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private int activeOrderId;
        private int clientId;
        private int hotelRoomId;
        private HotelRoomModel hotelRoom;
        private PaymentStateEnumModel paymentState;
        private DateTime checkInDate;
        private DateTime? checkOutDate;

        public int ActiveOrderId
        {
            get
            {
                return activeOrderId;
            }
            set
            {
                activeOrderId = value;
                OnPropertyChanged(nameof(ActiveOrderId));
            }
        }
        public int ClientId
        {
            get
            {
                return clientId;
            }
            set
            {
                clientId = value;
                OnPropertyChanged(nameof(ClientId));
            }
        }
        public int HotelRoomId
        {
            get
            {
                return hotelRoomId;
            }
            set
            {
                hotelRoomId = value;
                OnPropertyChanged(nameof(HotelRoomId));
            }
        }
        public HotelRoomModel HotelRoom
        {
            get
            {
                return hotelRoom;
            }
            set
            {
                hotelRoom = value;
                OnPropertyChanged(nameof(HotelRoom));
            }
        }
        public PaymentStateEnumModel PaymentState
        {
            get
            {
                return paymentState;
            }
            set
            {
                paymentState = value;
                OnPropertyChanged(nameof(PaymentState));
            }
        }
        public DateTime CheckInDate
        {
            get
            {
                return checkInDate;
            }
            set
            {
                checkInDate = value;
                OnPropertyChanged(nameof(CheckInDate));
            }
        }
        public DateTime? CheckOutDate
        {
            get
            {
                return checkOutDate;
            }
            set
            {
                checkOutDate = value;
                OnPropertyChanged(nameof(CheckOutDate));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        // Валидация 
        Dictionary<string, string> errors = new Dictionary<string, string>();
        public string this[string columnName] => errors.ContainsKey(columnName) ? errors[columnName] : null;

        // Если все тексты ошибок null - данные валидные
        public bool IsValid => !errors.Values.Any(x => x != null);
        public string Error
        {
            get
            {
                return null;
            }
        }
    }

    public enum PaymentStateEnumModel : byte
    {
        Undefined = 0,
        Paid ,
        Booked
    }
}
