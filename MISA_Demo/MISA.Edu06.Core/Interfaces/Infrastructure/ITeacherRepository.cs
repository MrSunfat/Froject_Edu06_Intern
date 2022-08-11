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
        /// <summary>
        /// Lấy ra tất cả thông tin giáo viên
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: TNDanh (5/8/2022)
        public IEnumerable<Teacher> GetAll();
        /// <summary>
        /// Lấy thông tin giáo viên qua id
        /// </summary>
        /// <param name="teacherID"></param>
        /// <returns></returns>
        /// CreatedBy: TNDanh (5/8/2022)
        public Teacher GetTeacherByID(Guid teacherID);
        /// <summary>
        /// Tạo một teacherCode mới
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: TNDanh (8/8/2021)
        public string NewTeacherCode();
        /// <summary>
        /// Thêm một thông tin giáo viên mới
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        /// CreatedBy: TNDanh (5/8/2022)
        public int AddNewTeacher(Teacher teacher);
        /// <summary>
        /// Sửa thông tin giáo viên qua id
        /// </summary>
        /// <param name="teacher"></param>
        /// <param name="teacherID"></param>
        /// <returns></returns>
        /// CreatedBy: TNDanh (5/8/2022)
        public int UpdateTeacherByID(Teacher teacher, Guid teacherID);
        /// <summary>
        /// Xóa thông tin giáo viên qua id
        /// </summary>
        /// <param name="teacherID"></param>
        /// <returns></returns>
        /// CreatedBy: TNDanh (5/8/2022)
        public int DeleteTeacherByID(Guid teacherID);
        /// <summary>
        /// Xóa nhiều giáo viên qua danh sách các teacherID
        /// </summary>
        /// <param name="listTeacherID"></param>
        /// <returns></returns>
        /// CreatedBy: TNDanh (3/8/2022)
        public int DeleteListTeacher(Guid[] listTeacherID);
        /// <summary>
        /// Tìm kiếm bằng mã giáo viên or tên và phân trang bằng pageSize, pageNumber
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        /// CreatedBy: TNDanh (5/8/2022)
        public object FilterTeacher(string? search, int pageSize, int pageNumber);
        /// <summary>
        /// Kiểm tra mã giáo viên bị trùng
        /// </summary>
        /// <param name="teacherCode"></param>
        /// <returns></returns>
        public bool CheckTeacherCodeDuplicate(string teacherCode);
    }
}
