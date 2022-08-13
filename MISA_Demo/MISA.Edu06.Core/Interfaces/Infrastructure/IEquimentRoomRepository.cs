using MISA.Edu06.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Edu06.Core.Interfaces.Infrastructure
{
    public interface IEquimentRoomRepository
    {
        #region Methods
        /// <summary>
        /// Lấy ra tất cả thông tin phòng
        /// </summary>
        /// <returns>Tất cả thông tin phòng</returns>
        /// CreatedBy: TNDanh (5/8/2022)
        public IEnumerable<EquimentRoom> GetAll();
        /// <summary>
        /// Thêm một thông tin phòng thiết bị mới
        /// </summary>
        /// <param name="room">Thông tin của phòng</param>
        /// <returns>Số lượng phòng thêm mới </returns>
        /// CreatedBy: TNDanh (5/8/2022)
        public int AddNewRoom(EquimentRoom room);
        /// <summary>
        /// Sửa thông tin phòng qua roomID
        /// </summary>
        /// <param name="room">Thông tin mới của phòng</param>
        /// <param name="roomID">Mã ID của phòng</param>
        /// <returns></returns>
        /// CreatedBy: TNDanh (5/8/2022)
        public int UpdateRoomByID(EquimentRoom room, int roomID);
        /// <summary>
        /// Xóa thông tin tổ hợp môn qua roomID
        /// </summary>
        /// <param name="roomID">Mã ID của phòng</param>
        /// <returns>Số lượng giáo viên bị xóa</returns>
        /// CreatedBy: TNDanh (5/8/2022)
        public int DeleteRoomByID(int roomID);
        #endregion
    }
}
