using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelApp.BLL.DTO;
using HotelApp.BLL.Interfaces;
using HotelApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IActiveOrderAdminService orderAdminService;
        private readonly IMapper mapper;
        public OrdersController(IActiveOrderAdminService orderAdminService, IMapper mapper)
        {
            this.orderAdminService = orderAdminService;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetOrders()
        {
            IEnumerable<ActiveOrderDTO> orders = orderAdminService.FindOrders();
            return Ok(mapper.Map<IEnumerable<ActiveOrderDTO>, IEnumerable<ActiveOrderModel>>(orders));
        }
        [HttpGet("{id?}")]
        public IActionResult GetOrder(int id)
        {
            ActiveOrderDTO order = orderAdminService.FindOrder(id);
            if (order is null)
                return NotFound();
            return Ok(mapper.Map<ActiveOrderModel>(order));
        }
        [HttpPost]
        public IActionResult PostOrder(ActiveOrderModel order)
        {
            if (order is null)
                return BadRequest(new ArgumentNullException(nameof(order)));
            var newOrder = orderAdminService.InsertOrder(mapper.Map<ActiveOrderDTO>(order));
            return CreatedAtAction(nameof(GetOrder), new { id = newOrder.ActiveOrderId }, newOrder);
        }
        [HttpPut]
        public IActionResult PutOrder(ActiveOrderModel order)
        {
            if (order is null)
                return BadRequest(new ArgumentNullException(nameof(order)));
            if (orderAdminService.UpdateOrder(mapper.Map<ActiveOrderDTO>(order)))
                return Ok();
            else
                return NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            if (orderAdminService.DeleteOrder(id))
                return NoContent();
            else
                return NotFound();
        }
        [HttpPut("{id}/paid")]
        public IActionResult PutConfirmPayment(int id)
        {
            if(orderAdminService.ConfirmPayment(id))
                return Ok();
            else
                return NotFound();
        }
    }
}
