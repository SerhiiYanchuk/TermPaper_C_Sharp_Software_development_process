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
    public class HotelsController : ControllerBase
    {
        private readonly IHotelAdminService hotelAdminService;
        private readonly IMapper mapper;
        public HotelsController(IHotelAdminService hotelAdminService, IMapper mapper)
        {
            this.hotelAdminService = hotelAdminService;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetHotels([FromQuery] string keyword)
        {
            IEnumerable<HotelDTO> hotels = hotelAdminService.FindHotels(keyword);
            return Ok(mapper.Map<IEnumerable<HotelDTO>, IEnumerable<HotelModel>>(hotels));
            

        }
        [HttpGet("{id}")]
        public IActionResult GetHotel(int id)
        {
            HotelDTO hotel = hotelAdminService.FindHotel(id);
            if (hotel is null)
                return NotFound();
            return Ok(mapper.Map<HotelModel>(hotel));
        }
        [HttpPost]
        public IActionResult PostHotel(HotelModel hotel)
        {
            if (hotel is null)
                return BadRequest(new ArgumentNullException(nameof(hotel)));
            var newHotel = hotelAdminService.InsertHotel(mapper.Map<HotelDTO>(hotel));
            return CreatedAtAction(nameof(GetHotel), new { id = newHotel.HotelId }, newHotel);
        }
        [HttpPut]
        public IActionResult PutHotel(HotelModel hotel)
        {
            if (hotel is null)
                return BadRequest(new ArgumentNullException(nameof(hotel)));
            if (hotelAdminService.UpdateHotel(mapper.Map<HotelDTO>(hotel)))
                return Ok();
            else
                return NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteHotel(int id)
        {
            if (hotelAdminService.DeleteHotel(id))
                return NoContent();
            else
                return NotFound();
        }
        [HttpGet("{id}/info")]
        public IActionResult GetHotelInfo(int id)
        {
            InfoHotelDTO info = hotelAdminService.GetHotelInfo(id);
            if (info is null)
                return NotFound();
            return Ok(mapper.Map<InfoHotelModel>(info));
        }
        [HttpGet("{id}/orders")]
        public IActionResult GetHotelOrders(int id, [FromQuery] OrderFilterModel filter)
        {
            if (filter is null)
                return BadRequest(nameof(filter));
            IEnumerable<ActiveOrderDTO> orders = hotelAdminService.GetHotelOrders(id, mapper.Map<OrderFilterDTO>(filter));
            return Ok(mapper.Map<IEnumerable<ActiveOrderDTO>, IEnumerable<ActiveOrderModel>>(orders));
        }
    }
}
