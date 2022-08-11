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
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public int ValidateService(Department department)
        {
            // 1. Tên phòng ban không được trống
            if (string.IsNullOrEmpty(department.DepartmentName) == true)
            {
                throw new EduValidateException(Resources.Content.departmentNameNotEmpty);
            }
            // 2. Tên phòng ban không được giống nhau
            if (_departmentRepository.CheckDepartmentNameDuplicate(department.DepartmentName) == true)
            {
                throw new EduValidateException(Resources.Content.departmentNameNotDuplicate);
            }
            return 1;
        }
    }
}
