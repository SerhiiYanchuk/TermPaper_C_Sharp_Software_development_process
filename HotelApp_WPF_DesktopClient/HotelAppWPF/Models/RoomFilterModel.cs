using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HotelAppWPF.Models
{
    public class RoomFilterModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private int hotelId;
        private TypeSizeEnumModel typeSize;
        private TypeComfortEnumModel typeComfort;
        private DateTime? checkInDate;
        private DateTime? checkOutDate;
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
        public DateTime? CheckInDate
        {
            get
            {
                return checkInDate;
            }
            set
            {
                checkInDate = value;

                if (checkInDate is null || checkInDate.Value.Date < DateTime.Today)
                {
                    errors["CheckInDate"] = "Incorrect check-in date";
                }
                else
                {
                    errors["CheckInDate"] = null;
                }
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

                if (checkInDate is null || checkOutDate <= checkInDate)
                {
                    errors["CheckOutDate"] = "Incorrect check-out date";
                }
                else
                {
                    errors["CheckOutDate"] = null;
                }
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
}
