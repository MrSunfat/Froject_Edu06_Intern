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
        #region Methods
        /// <summary>
        /// Dùng để kiểm tra dữ liệu trước khi thực hiện thay đổi data
        /// </summary>
        /// <param name="department">Tổ hợp môn</param>
        /// <returns>Validate thông tin tổ hợp môn</returns>
        public int ValidateService(Department department);
        #endregion
    }
}
