using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Edu06.Core.Entities
{
    public class Teacher
    {
        #region "Trường giáo viên"
        // ID của giáo viên
        public Guid TeacherID { get; set; }
        // Mã giáo viên
        public string TeacherCode { get; set; }
        // Tên giáo viên
        public string FullName { get; set; }
        // Email
        public string? Email { get; set; }
        // Số điện thoại
        public string? PhoneNumber { get; set; }
        // ID của tổ hợp môn
        public int? DepartmentID { get; set; }
        // tên tổ hợp môn
        public string? DepartmentName { get; set; }
        // trình độ nghiệp vụ
        public Boolean? IsProfessionalQualifications { get; set; }
        // tình trạng công việc
        public Boolean? IsWorking { get; set; }
        // ngày nghỉ việc
        public DateTime? DayOff { get; set; }
        // danh sách các môn học
        public List<Subject> ListSubject { get; set; } = new List<Subject>();
        // danh sách các phòng thiết bị
        public List<EquimentRoom> ListRoom { get; set; } = new List<EquimentRoom>();
        #endregion

    }
}
