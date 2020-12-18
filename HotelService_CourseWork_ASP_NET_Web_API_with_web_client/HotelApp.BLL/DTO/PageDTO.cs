using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.BLL.DTO
{
    public class PageDTO<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Count { get; set; }
    }
}
