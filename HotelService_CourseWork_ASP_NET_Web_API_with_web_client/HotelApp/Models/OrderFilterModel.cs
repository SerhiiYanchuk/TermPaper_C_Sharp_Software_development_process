using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Models
{
    public class OrderFilterModel
    {
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public PaymentStateEnumModel PaymentState { get; set; }
    }
}
