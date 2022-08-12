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
        /// <returns></returns>
        /// CreatedBy: TNDanh (5/8/2022)
        public IEnumerable<EquimentRoom> GetAll();
        /// <summary>
        /// Thêm một thông tin phòng mới
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        /// CreatedBy: TNDanh (5/8/2022)
        public int AddNewRoom(EquimentRoom room);
        /// <summary>
        /// Sửa thông tin phòng qua id
        /// </summary>
        /// <param name="teacher"></param>
        /// <param name="teacherID"></param>
        /// <returns></returns>
        /// CreatedBy: TNDanh (5/8/2022)
        public int UpdateRoomByID(EquimentRoom room, int roomID);
        /// <summary>
        /// Xóa thông tin tổ hợp môn qua id
        /// </summary>
        /// <param name="teacherID"></param>
        /// <returns></returns>
        /// CreatedBy: TNDanh (5/8/2022)
        public int DeleteRoomByID(int roomID);
        /// <summary>
        /// Kiểm tra tên phòng ban có bị trùng không ?
        /// </summary>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        /// CreatedBy: TNDanh (9/8/2022)
        public bool CheckRoomNameDuplicate(string departmentName);
        #endregion
    }
}
