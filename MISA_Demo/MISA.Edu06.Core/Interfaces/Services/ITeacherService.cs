using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Edu06.Core.Entities;

namespace MISA.Edu06.Core.Interfaces.Services
{
    public interface ITeacherService
    {
        #region Dùng để kiểm tra dữ liệu trước khi đẩy lên tầng Api
        /// <summary>
        /// Thêm giáo viên
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        /// CreatedBy: TNDanh (01/08/2021)
        int InsertService(Teacher teacher);
        /// <summary>
        /// Sửa giáo viên
        /// </summary>
        /// <param name="teacher"></param>
        /// <param name="teacherID"></param>
        /// <returns></returns>
        /// CreatedBy: TNDanh (01/08/2022)
        int UpdateService(Teacher teacher, Guid teacherID);
        #endregion
    }
}
