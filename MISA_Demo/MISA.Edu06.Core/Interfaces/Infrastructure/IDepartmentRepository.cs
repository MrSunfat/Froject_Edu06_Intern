using MISA.Edu06.Core.Entities;

namespace MISA.Edu06.Core.Interfaces.Infrastructure
{
    public interface IDepartmentRepository
    {
        #region Methods 
        /// <summary>
        /// Lấy ra tất cả thông tin tổ hợp môn
        /// </summary>
        /// <returns>Tất cả thông tin tổ hợp môn</returns>
        /// CreatedBy: TNDanh (5/8/2022)
        public IEnumerable<Department> GetAll();
        /// <summary>
        /// Thêm một thông tin tổ hợp môn mới
        /// </summary>
        /// <param name="department">Thông tin của tổ hợp môn</param>
        /// <returns>Số lượng tổ hợp môn thêm</returns>
        /// CreatedBy: TNDanh (5/8/2022)
        public int AddNewDepartment(Department department);
        /// <summary>
        /// Sửa thông tin các môn trong tổ hợp môn qua departmentID
        /// </summary>
        /// <param name="department">Thông tin mới của tổ hợp môn</param>
        /// <param name="departmentID">Mã ID của tổ hợp môn</param>
        /// <returns>Số lượng tổ hợp môn sửa</returns>
        /// CreatedBy: TNDanh (5/8/2022)
        public int UpdateDepartmentByID(Department department, int departmentID);
        /// <summary>
        /// Xóa thông tin tổ hợp môn qua departmentID
        /// </summary>
        /// <param name="departmentID">Mã ID của tổ hợp môn</param>
        /// <returns>Số lượng tổ hợp môn xóa</returns>
        /// CreatedBy: TNDanh (5/8/2022)
        public int DeleteDepartmentByID(int departmentID);
        /// <summary>
        /// Kiểm tra tên phòng ban có bị trùng không ?
        /// </summary>
        /// <param name="departmentName">Tên tổ hợp</param>
        /// <returns>True - Trùng, False - Không trùng</returns>
        /// CreatedBy: TNDanh (9/8/2022)
        public bool CheckDepartmentNameDuplicate(string departmentName);
        #endregion
    }
}
