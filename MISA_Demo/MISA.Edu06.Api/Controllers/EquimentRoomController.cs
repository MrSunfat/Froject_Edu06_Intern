using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Edu06.Core.Entities;
using MISA.Edu06.Core.Interfaces.Infrastructure;

namespace MISA.Edu06.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquimentRoomController : ControllerBase
    {
        private readonly IEquimentRoomRepository _roomRepository;

        public EquimentRoomController(IEquimentRoomRepository roomRepository)
        {
            this._roomRepository = roomRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var res = _roomRepository.GetAll();
            return Ok(res);
        }

        [HttpPost]
        public IActionResult AddNewRoom(EquimentRoom room)
        {
            var res = _roomRepository.AddNewRoom(room);
            return Ok(res);
        }

        [HttpPut("{roomID}")]
        public IActionResult EditRoomByID(EquimentRoom room, int roomID)
        {
            var res = _roomRepository.UpdateRoomByID(room, roomID);
            return Ok(res);
        }

        [HttpDelete("{roomID}")]
        public IActionResult DeleteRoomByID(int roomID)
        {
            var res = _roomRepository.DeleteRoomByID(roomID);
            return Ok(res);
        }
    }
}
