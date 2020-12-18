using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace HotelAppWPF.Models
{
    public class HotelModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private int hotelId;
        private string name;
        private string address;

        public HotelModel()
        {
            errors = new Dictionary<string, string>();
        }
        public int HotelId
        {
            get { return hotelId; }
            set
            {
                hotelId = value;
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                Regex check = new Regex(@"^[a-zA-Z0-9. ]+$");
                if (!check.IsMatch(name) || string.IsNullOrEmpty(name))
                {
                    errors["Name"] = "Incorrect hotel name";
                }
                else
                {
                    errors["Name"] = null;
                }
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                Regex check = new Regex(@"^[a-zA-Z0-9. ]+$");
                if (!check.IsMatch(address) || string.IsNullOrEmpty(address))
                {
                    errors["Address"] = "Incorrect hotel name";
                }
                else
                {
                    errors["Address"] = null;
                }
                OnPropertyChanged(nameof(Address));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        // Валидация 
        Dictionary<string, string> errors;
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