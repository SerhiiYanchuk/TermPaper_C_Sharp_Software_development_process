using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.BLL.DTO
{
    public class OrderFilterDTO
    {
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public PaymentStateEnumDTO PaymentState { get; set; }
    }
}
