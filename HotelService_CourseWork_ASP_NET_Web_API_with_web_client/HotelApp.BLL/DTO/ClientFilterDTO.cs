using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.BLL.DTO
{
    public class ClientFilterDTO
    {
        public SortClientEnumDTO SortState { get; set; }
        public string Keyword { get; set; }
    }
}
