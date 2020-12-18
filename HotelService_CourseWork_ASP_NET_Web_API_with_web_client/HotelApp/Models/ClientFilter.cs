using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Models
{
    public class ClientFilterModel
    {
        public SortClientEnumModel SortState { get; set; }
        public string Keyword { get; set; }
    }
}
