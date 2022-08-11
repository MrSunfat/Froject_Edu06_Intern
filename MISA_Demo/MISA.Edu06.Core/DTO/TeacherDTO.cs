using MISA.Edu06.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Edu06.Core.DTO
{
    public class TeacherDTO : Teacher
    {
        #region "Trường giáo viên cụ thể"
        // ID của môn học
        public int SubjectID { get; set; } = -1;
        // Tên môn học
        public string SubjectName { get; set; }
        // Các môn học
        public List<Subject> ListSubjects { get; set; } = new List<Subject>();
        // ID của phòng thiết bị
        public int EquimentRoomID { get; set; } = -1;
        // tên phòng thiết bị
        public string EquimentRoomName { get; set; }
        // ID của tổ hợp môn
        public int DepartmentID { get; set; } = -1;
        // tên của tổ hợp môn
        public string DepartmentName { get; set; }
        #endregion
    }
}
