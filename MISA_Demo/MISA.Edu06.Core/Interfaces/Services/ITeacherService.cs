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
        #region Methods
        /// <summary>
        /// Thêm giáo viên
        /// </summary>
        /// <param name="teacher">Thông tin giáo viên</param>
        /// <returns>Số lượng giáo viên được thêm</returns>
        /// CreatedBy: TNDanh (01/08/2021)
        int InsertService(Teacher teacher);
        /// <summary>
        /// Sửa giáo viên
        /// </summary>
        /// <param name="teacher">Thông tin giáo viên</param>
        /// <param name="teacherID">Mã ID của giáo viên</param>
        /// <returns>Số lượng giáo viên được sửa</returns>
        /// CreatedBy: TNDanh (01/08/2022)
        int UpdateService(Teacher teacher, Guid teacherID);
        #endregion
    }
}
