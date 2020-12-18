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
    public class ClientsController : ControllerBase
    {
        private readonly IClientAdminService clientAdminService;
        private readonly IMapper mapper;
        public ClientsController(IClientAdminService clientAdminService, IMapper mapper)
        {
            this.clientAdminService = clientAdminService;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetClients([FromQuery] ClientFilterModel filter)
        {
            IEnumerable<ClientDTO> clients = clientAdminService.FindClients(mapper.Map<ClientFilterDTO>(filter));
            return Ok(mapper.Map<IEnumerable<ClientDTO>, IEnumerable<ClientModel>>(clients));
        }
        [HttpGet("{id}")]
        public IActionResult GetClient(int id)
        {
            ClientDTO client = clientAdminService.FindClient(id);
            if (client is null)
                return NotFound();
            return Ok(mapper.Map<ClientModel>(client));
        }     
        [HttpPost]
        public IActionResult PostClient(ClientModel client)
        {
            if (client is null)
                return BadRequest(new ArgumentNullException(nameof(client)));
            if(clientAdminService.IsClientExist(client.PhoneNumber))
            {
                ModelState.AddModelError("PhoneNumber", "Phone number is busy");
                return BadRequest(ModelState);
            }
                 
            var newClient = clientAdminService.InsertClient(mapper.Map<ClientDTO>(client));
            return CreatedAtAction(nameof(GetClient), new { id = newClient.ClientId }, newClient);
        }
        [HttpPut]
        public IActionResult PutClient(ClientModel client)
        {
            if (client is null)
                return BadRequest(new ArgumentNullException(nameof(client)));
            if (clientAdminService.IsClientExist(client.PhoneNumber))
            {
                ModelState.AddModelError("PhoneNumber", "Phone number is busy");
                return BadRequest(ModelState);
            }
            if (clientAdminService.UpdateClient(mapper.Map<ClientDTO>(client)))
                return Ok();
            else
                return NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id)
        {
            if (clientAdminService.DeleteClient(id))
                return NoContent();
            else
                return NotFound();
        }

        [HttpGet("{id}/orders")]
        public IActionResult GetClientOrders(int id)
        {
            ClientDTO client = clientAdminService.FindClient(id);
            if (client is null)
                return NotFound();
            IEnumerable<ActiveOrderModel> orders = mapper.Map<IEnumerable<ActiveOrderDTO>, IEnumerable<ActiveOrderModel>>(client.ActiveOrders);
            foreach (var order in orders)
                order.HotelRoom.Hotel.HotelRooms = null;
            return Ok(orders);
            //return Ok(2);
        }
        [HttpGet("phone")]
        public IActionResult GetClientByPhoneNumber([FromQuery] string phoneNumber)
        {
            ClientDTO client = clientAdminService.FindClient(phoneNumber);
            if (client is null)
                return NotFound();
            return Ok(mapper.Map<ClientModel>(client));
        }
        //[HttpPost("orders")]
        //public IActionResult PostOrder(ClientOrderViewModel request)
        //{
        //    if (request is null || request.Client is null || request.Order is null)
        //        return BadRequest(new ArgumentNullException(nameof(request)));
        //    if (request.Order.CheckOutDate != null && request.Order.CheckInDate >= request.Order.CheckOutDate)
        //        ModelState.AddModelError("", "CheckInDate can't be more or equal than CheckOutDate");
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    ClientDTO client = mapper.Map<ClientDTO>(request.Client);
        //    ActiveOrderDTO order = mapper.Map<ActiveOrderDTO>(request.Order);
        //    ActiveOrderDTO newOrder = clientAdminService.AddClientActiveOrder(client, order);
        //    //return CreatedAtAction(nameof(GetClient), new { id = newOrder.ActiveOrderId }, newOrder);

        //    return Created(new Uri($"/api/orders/{newOrder.ActiveOrderId}", UriKind.Relative), newOrder);
        //}
        //[HttpGet("orders")]
        //public IActionResult GetClientOrders([FromQuery] string phoneNumber, [FromQuery] PaymentStateEnumModel paymentState = default)
        //{
        //    IEnumerable<ActiveOrderDTO> orders = clientAdminService.FindClientActiveOrders(phoneNumber, mapper.Map<PaymentStateEnumDTO>(paymentState));
        //    return new ObjectResult(mapper.Map<IEnumerable<ActiveOrderDTO>, IEnumerable<ActiveOrderModel>>(orders));
        //}
    }
}
