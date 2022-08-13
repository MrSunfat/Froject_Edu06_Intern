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
        #region Properties
        private readonly IEquimentRoomRepository _roomRepository;
        #endregion

        #region Constructor
        public EquimentRoomController(IEquimentRoomRepository roomRepository)
        {
            this._roomRepository = roomRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy tất cả thông tin của phòng thiết bị
        /// </summary>
        /// <returns>Thông tin phòng thiết bị</returns>
        /// CreatedBy: TNDanh (13/8/2022)
        [HttpGet]
        public IActionResult GetAll()
        {
            var res = _roomRepository.GetAll();
            return Ok(res);
        }

        /// <summary>
        /// Thêm thông tin của phòng thiết bị mới
        /// </summary>
        /// <param name="room">Thông tin của phòng thiết bị mới</param>
        /// <returns>Số lượng phòng thêm mới</returns>
        /// CreatedBy: TNDanh (13/8/2022)
        [HttpPost]
        public IActionResult AddNewRoom(EquimentRoom room)
        {
            var res = _roomRepository.AddNewRoom(room);
            return Ok(res);
        }

        /// <summary>
        /// Sửa thông tin của phòng thiết bị qua roomID
        /// </summary>
        /// <param name="room">Thông tin chỉnh sửa của phòng</param>
        /// <param name="roomID">Mã ID của phòng thiết bị</param>
        /// <returns>Số lượng phòng sửa</returns>
        /// CreatedBy: TNDanh (13/8/2022)
        [HttpPut("{roomID}")]
        public IActionResult EditRoomByID(EquimentRoom room, int roomID)
        {
            var res = _roomRepository.UpdateRoomByID(room, roomID);
            return Ok(res);
        }

        /// <summary>
        /// Xóa phòng thiết bị bằng roomID
        /// </summary>
        /// <param name="roomID">Mã ID của phòng thiết bị</param>
        /// <returns>Số lượng phòng xóa</returns>
        /// CreatedBy: TNDanh (13/8/2022)
        [HttpDelete("{roomID}")]
        public IActionResult DeleteRoomByID(int roomID)
        {
            var res = _roomRepository.DeleteRoomByID(roomID);
            return Ok(res);
        }
        #endregion
    }
}
