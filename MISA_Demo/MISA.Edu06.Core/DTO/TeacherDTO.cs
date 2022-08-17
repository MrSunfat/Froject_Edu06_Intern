using MISA.Edu06.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Edu06.Core.DTO
{
    public class TeacherDTO
    {
        #region Properties 
        /// <summary>
        /// ID của giáo viên 
        /// </summary>
        public Guid TeacherID { get; set; }

        /// <summary>
        /// Mã giáo viên 
        /// </summary>
        public string TeacherCode { get; set; }

        /// <summary>
        /// Tên giáo viên
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Tên tổ hợp môn
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Trình độ nghiệp vụ
        /// </summary>
        public Boolean? IsProfessionalQualifications { get; set; }

        /// <summary>
        /// Tình trạng công việc
        /// </summary>
        public Boolean? IsWorking { get; set; }

        /// <summary>
        /// Ngày nghỉ việc
        /// </summary>
        public DateTime? DayOff { get; set; }

        /// <summary>
        /// Danh sách môn học
        /// </summary>
        public string? SubjectName { get; set; }

        /// <summary>
        /// Danh sách phòng thiết bị
        /// </summary>
        public string Room { get; set; }
        #endregion
    }
}
