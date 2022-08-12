using MISA.Edu06.Core.DTO;
using MISA.Edu06.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Edu06.Core.Interfaces.Infrastructure
{
    public interface ITeacherRepository
    {
        #region Methods
        /// <summary>
        /// Lấy ra tất cả thông tin giáo viên
        /// </summary>
        /// <returns>Thông tin tất cả giáo viên</returns>
        /// CreatedBy: TNDanh (5/8/2022)
        public IEnumerable<Teacher> GetAll();
        /// <summary>
        /// Lấy thông tin giáo viên qua id
        /// </summary>
        /// <param name="teacherID">Mã ID của giáo viên</param>
        /// <returns>Thông tin của giáo viên qua mã ID</returns>
        /// CreatedBy: TNDanh (5/8/2022)
        public Teacher GetTeacherByID(Guid teacherID);
        /// <summary>
        /// Tạo một teacherCode mới
        /// </summary>
        /// <returns>Tạo ra mã giáo viên mới</returns>
        /// CreatedBy: TNDanh (8/8/2021)
        public string NewTeacherCode();
        /// <summary>
        /// Thêm một thông tin giáo viên mới
        /// </summary>
        /// <param name="teacher">Thông tin giáo viên</param>
        /// <returns>Số lượng giáo viên được thêm</returns>
        /// CreatedBy: TNDanh (5/8/2022)
        public int AddNewTeacher(Teacher teacher);
        /// <summary>
        /// Sửa thông tin giáo viên qua id
        /// </summary>
        /// <param name="teacher">Thông tin giáo viên</param>
        /// <param name="teacherID">Mã ID của giáo viên</param>
        /// <returns>Số lượng giáo viên được sửa</returns>
        /// CreatedBy: TNDanh (5/8/2022)
        public int UpdateTeacherByID(Teacher teacher, Guid teacherID);
        /// <summary>
        /// Xóa thông tin giáo viên qua id
        /// </summary>
        /// <param name="teacherID">Mã ID của giáo viên</param>
        /// <returns>Số lượng giáo viên được xóa</returns>
        /// CreatedBy: TNDanh (5/8/2022)
        public int DeleteTeacherByID(Guid teacherID);
        /// <summary>
        /// Xóa nhiều giáo viên qua danh sách các teacherID
        /// </summary>
        /// <param name="listTeacherID">Danh sách chứa mã ID của giáo viên</param>
        /// <returns>Số lượng giáo viên được xóa</returns>
        /// CreatedBy: TNDanh (3/8/2022)
        public int DeleteListTeacher(Guid[] listTeacherID);

        /// <summary>
        /// Tìm kiếm bằng mã giáo viên or tên và phân trang bằng pageSize, pageNumber
        /// </summary>
        /// <param name="search">Nội dung cần tìm</param>
        /// <param name="pageSize">Số lượng giáo viên cần trả về</param>
        /// <param name="pageNumber">Chỉ số trang</param>
        /// <returns>Trả về các giáo viên phù hợp với điều kiện trên</returns>
        /// CreatedBy: TNDanh (5/8/2022)
        public object FilterTeacher(string? search, int pageSize, int pageNumber);
        /// <summary>
        /// Kiểm tra mã giáo viên bị trùng
        /// </summary>
        /// <param name="teacherCode"></param>
        /// <returns></returns>
        public bool CheckTeacherCodeDuplicate(string teacherCode);
        #endregion
    }
}
