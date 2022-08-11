using MISA.Edu06.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Edu06.Core.Interfaces.Services
{
    public interface IDepartmentService
    {
        #region Dùng để kiểm tra dữ liệu trước khi thực hiện thay đổi data
        public int ValidateService(Department department);
        #endregion
    }
}
