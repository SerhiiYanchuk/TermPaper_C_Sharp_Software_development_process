using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;


namespace HotelAppWPF.Models
{
    public class HotelRoomModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private int hotelRoomId;
        private HotelModel hotel;
        private int hotelId;
        private string number;
        private decimal pricePerDay;
        private TypeSizeEnumModel typeSize;
        private TypeComfortEnumModel typeComfort;
        public DateTime CheckInDate { get; set; }
        public DateTime? MaxCheckOutDate { get; set; }
        public int HotelRoomId { 
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
        public HotelModel Hotel
        {
            get
            {
                return hotel;
            }
            set
            {
                hotel = value;
                OnPropertyChanged(nameof(Hotel));
            }
        }
        public int HotelId
        {
            get
            {
                return hotelId;
            }
            set
            {
                hotelId = value;
                OnPropertyChanged(nameof(HotelId));
            }
        }
        public string Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
                OnPropertyChanged(nameof(Number));
            }
        }
        public decimal PricePerDay
        {
            get
            {
                return pricePerDay;
            }
            set
            {
                pricePerDay = value;
                OnPropertyChanged(nameof(PricePerDay));
            }
        }
        public TypeSizeEnumModel TypeSize
        {
            get
            {
                return typeSize;
            }
            set
            {
                typeSize = value;
                OnPropertyChanged(nameof(TypeSize));
            }
        }
        public TypeComfortEnumModel TypeComfort
        {
            get
            {
                return typeComfort;
            }
            set
            {
                typeComfort = value;
                OnPropertyChanged(nameof(TypeComfort));
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
}
