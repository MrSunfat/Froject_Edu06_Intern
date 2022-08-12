using MISA.Edu06.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Edu06.Core.Interfaces.Infrastructure
{
    public interface ISubjectRepository
    {
        #region Methods
        /// <summary>
        /// Lấy ra tất cả thông tin môn
        /// </summary>
        /// <returns>Trả về thông tin của tất cả môn học</returns>
        /// CreatedBy: TNDanh (5/8/2022)
        public IEnumerable<Subject> GetAll();
        /// <summary>
        /// Thêm một thông tin môn mới
        /// </summary>
        /// <param name="subject"></param>
        /// <returns>Số lượng môn học mới được thêm</returns>
        /// CreatedBy: TNDanh (5/8/2022)
        public int AddNewSubject(Subject subject);
        /// <summary>
        /// Sửa thông tin môn học qua id
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="subjectID"></param>
        /// <returns>Số lượng môn học sửa đổi</returns>
        /// CreatedBy: TNDanh (5/8/2022)
        public int UpdateSubjectByID(Subject subject, int subjectID);
        /// <summary>
        /// Xóa thông tin môn qua id
        /// </summary>
        /// <param name="subjectID"></param>
        /// <returns>Số lượng giáo viên bị xóa</returns>
        /// CreatedBy: TNDanh (5/8/2022)
        public int DeleteSubjectByID(int subjectID);
        #endregion
    }
}
