using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Xunit;

namespace TheHotelManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpPost]
        public async Task<ActionResult<Room?>> CreateRoom(Room room)
        {
            return await _roomService.CreateAsync(room);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetAll()
            => Ok(await _roomService.GetAllAsync());

        [HttpGet]
        [Route("GetById")]
        public async Task<ActionResult<IEnumerable<Room>>> GetById(string id)
            => Ok(await _roomService.GetByIdAsync(id));

        [HttpGet]
        [Route("GetByCategory")]
        public async Task<ActionResult<IEnumerable<Room>>> GetByCategoryId(string id)
            => Ok(await _roomService.GetByCategoryAsync(id));

        [HttpPut]
        public async Task<ActionResult<ReplaceOneResult?>> UpdateRoom(Room room)
        {
            return await _roomService.UpdateAsync(room);
        }

        [HttpDelete]
        public async Task<ActionResult<DeleteResult?>> DeleteRoom(string id)
        {
            return await _roomService.DeleteAsync(id);
        }
    }
}
