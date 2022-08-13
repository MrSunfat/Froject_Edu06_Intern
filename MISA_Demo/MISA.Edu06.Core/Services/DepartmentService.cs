using MISA.Edu06.Core.Entities;
using MISA.Edu06.Core.Exceptions;
using MISA.Edu06.Core.Interfaces.Infrastructure;
using MISA.Edu06.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Edu06.Core.Services
{
    public class DepartmentService : IDepartmentService
    {
        #region Properties
        private readonly IDepartmentRepository _departmentRepository;
        #endregion

        #region Constructor
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Validate tổ hợp môn trước khi sửa
        /// </summary>
        /// <param name="department">Thông tin mới của tổ hợp môn</param>
        /// <returns>Số lượng tổ hợp môn sửa</returns>
        /// <exception cref="EduValidateException">Thông báo lỗi</exception>
        public int ValidateService(Department department)
        {
            // 1. Tên phòng ban không được trống
            if (string.IsNullOrEmpty(department.DepartmentName) == true)
            {
                throw new EduValidateException(Resources.Content.E_DepartmentNameNotEmpty);
            }
            // 2. Tên phòng ban không được giống nhau
            if (_departmentRepository.CheckDepartmentNameDuplicate(department.DepartmentName) == true)
            {
                throw new EduValidateException(Resources.Content.E_DepartmentNameNotDuplicate);
            }
            return 1;
        }
        #endregion
    }
}
