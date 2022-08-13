using MISA.Edu06.Core.Entities;
using MISA.Edu06.Core.Exceptions;
using MISA.Edu06.Core.Interfaces.Infrastructure;
using MISA.Edu06.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Edu06.Core.Services
{
    public class TeacherService : ITeacherService
    {
        #region Properties
        private ITeacherRepository _teacherRepository;
        #endregion

        #region Constructor
        public TeacherService(ITeacherRepository teacherRepository)
        {
            this._teacherRepository = teacherRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Validate trước khi thêm mới giáo viên
        /// </summary>
        /// <param name="teacher">Thông tin giáo viên</param>
        /// <returns>Số lượng giáo viên validate</returns>
        /// <exception cref="EduValidateException">Thông báo lỗi</exception>
        /// CreatedBy: TNDanh (14/8/2022)
        public int InsertService(Teacher teacher)
        {
            // validate dữ liệu:
            // 1. Mã giáo viên
            // 1a. Không được để trống
            List<string> error = new List<string>();
            if (string.IsNullOrEmpty(teacher.TeacherCode))
            {
                error.Add(Resources.Content.E_TeacherCodeNotEmpty);
            }
            // 1b. Kiểm tra mã giáo viên có bị trùng không ?
            if (_teacherRepository.CheckTeacherCodeDuplicate(teacher.TeacherCode) == true)  
            {
                error.Add(Resources.Content.E_TeacherCodeNotDuplicate);
            }
            // 2. Tên giáo viên không được trống
            if (string.IsNullOrEmpty(teacher.FullName) == true)
            {
                error.Add(Resources.Content.E_TeacherNameNotEmpty);
            }
            // 3. Email
            //if ()

            if (error.Count > 0)
            {
                string errContent = String.Join(" - ", error);
                throw new EduValidateException(errContent);
            }
            return 1;
        }

        /// <summary>
        /// Validate trước khi sửa giáo viên
        /// </summary>
        /// <param name="teacher">Thông tin mới của giáo viên</param>
        /// <returns>Số lượng giáo viên validate</returns>
        /// <exception cref="EduValidateException">Thông báo lỗi</exception>
        /// CreatedBy: TNDanh (14/8/2022)
        public int UpdateService(Teacher teacher, Guid teacherID)
        {
            if (teacher == null)
            {   
                throw new EduValidateException(nameof(teacher));
            }
            return 0;
            throw new NotImplementedException();
        }
        #endregion
    }
}
