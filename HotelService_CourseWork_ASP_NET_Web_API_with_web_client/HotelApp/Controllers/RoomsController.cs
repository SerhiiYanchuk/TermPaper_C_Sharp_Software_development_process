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
    public class RoomsController : ControllerBase
    {
        private readonly IHotelRoomsAdminService roomsAdminService;
        private readonly IMapper mapper;
        public RoomsController(IHotelRoomsAdminService roomsAdminService, IMapper mapper)
        {
            this.roomsAdminService = roomsAdminService;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetHotelRooms([FromQuery] int hotelId)
        {
            IEnumerable<HotelRoomDTO> hotels = roomsAdminService.FindRoomsByHotelId(hotelId);
            return Ok(mapper.Map<IEnumerable<HotelRoomDTO>, IEnumerable<HotelRoomModel>>(hotels));
        }
        [HttpGet("{id}")]
        public IActionResult GetHotelRoom(int id)
        {
            HotelRoomDTO room = roomsAdminService.FindRoom(id);
            if (room is null)
                return NotFound();
            return Ok(mapper.Map<HotelRoomModel>(room));
        }
        [HttpPost]
        public IActionResult PostHotelRoom(HotelRoomModel room)
        {
            if (room is null)
                return BadRequest(new ArgumentNullException(nameof(room)));
            var newRoom = roomsAdminService.InsertRoom(mapper.Map<HotelRoomDTO>(room));
            return CreatedAtAction(nameof(GetHotelRoom), new { id = newRoom.HotelId }, newRoom);
        }
        [HttpPut]
        public IActionResult PutHotelRoom(HotelRoomModel room)
        {
            if (room is null)
                return BadRequest(new ArgumentNullException(nameof(room)));
            if (roomsAdminService.UpdateRoom(mapper.Map<HotelRoomDTO>(room)))
                return Ok();
            else
                return NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteHotelRoom(int id)
        {
            if (roomsAdminService.DeleteRoom(id))
                return NoContent();
            else
                return NotFound();
        }

        [HttpGet("page/{pageIndex}")]
        public IActionResult GetRoomsPage(int pageIndex = 1, [FromQuery] int hotelId = 0)
        {
            PageDTO<HotelRoomDTO> page = roomsAdminService.ShowRoomsPage(pageIndex, 5, hotelId);

            PageViewModel pageViewModel = new PageViewModel(page.Count, pageIndex, 3);
            
            RoomsPageViewModel roomsPageViewModel = new RoomsPageViewModel
            {
                HotelRooms = mapper.Map<IEnumerable<HotelRoomDTO>, IEnumerable<HotelRoomModel>>(page.Items),
                PageViewModel = pageViewModel
            };
            return new ObjectResult(roomsPageViewModel);
        }

        [HttpGet("free")]
        public IActionResult GetFreeRooms([FromQuery] HotelRoomSeachFilterModel filter)
        {
            if (filter is null)
                return BadRequest(new ArgumentNullException(nameof(filter)));
            if (filter.CheckOutDate != null && filter.CheckInDate >= filter.CheckOutDate)
                ModelState.AddModelError("CheckOutDate", "Check-in date can't be more or equal than check-out date");
            if (filter.CheckInDate.Date < DateTime.Today)
                ModelState.AddModelError("CheckInDate", "Check-in date can't be less than current date");
            if (ModelState.IsValid)
            {
                IEnumerable<FreeHotelRoomDTO> rooms = roomsAdminService.SearchFreeRooms(mapper.Map<HotelRoomSeachFilterDTO>(filter));
                return new ObjectResult(mapper.Map<IEnumerable<FreeHotelRoomDTO>, IEnumerable<FreeHotelRoomModel>>(rooms));
            }
            return BadRequest(ModelState);
        }
        
    }
}
